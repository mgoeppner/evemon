[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [ValidatePattern('^[0-9]+(?:\.[0-9]+)*$')]
    [string]$Build,

    [Parameter(Mandatory = $true)]
    [ValidatePattern('^[A-Za-z0-9_.-]+/[A-Za-z0-9_.-]+$')]
    [string]$Repository,

    [ValidatePattern('^[A-Za-z0-9][A-Za-z0-9._-]*$')]
    [string]$TagName,

    [string]$BaselinePatch,

    [switch]$Remote
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$expectedNames = @(
    'eve-blueprints-en-US.xml.gzip'
    'eve-certificates-en-US.xml.gzip'
    'eve-geography-en-US.xml.gzip'
    'eve-items-en-US.xml.gzip'
    'eve-masteries-en-US.xml.gzip'
    'eve-properties-en-US.xml.gzip'
    'eve-reprocessing-en-US.xml.gzip'
    'eve-skills-en-US.xml.gzip'
)

$workspace = if ($env:GITHUB_WORKSPACE) {
    $env:GITHUB_WORKSPACE
}
else {
    (Get-Location).Path
}

$resourcesDirectory = Join-Path $workspace 'src/EVEMon.Common/Resources'
$patchPath = Join-Path $workspace 'updates/patch.xml'
$tag = if ($TagName) { $TagName } else { "sde-$Build" }
$expectedBaseUrl = "https://raw.githubusercontent.com/$Repository/$tag/src/EVEMon.Common/Resources"

if (-not (Test-Path -LiteralPath $patchPath -PathType Leaf)) {
    throw "patch.xml not found: $patchPath"
}

[xml]$patch = Get-Content -LiteralPath $patchPath -Raw

if ($BaselinePatch) {
    if (-not (Test-Path -LiteralPath $BaselinePatch -PathType Leaf)) {
        throw "Baseline patch.xml not found: $BaselinePatch"
    }

    [xml]$baseline = Get-Content -LiteralPath $BaselinePatch -Raw
    foreach ($sectionName in @('newest', 'releases')) {
        $baselineSection = $baseline.SelectSingleNode("/evemon/$sectionName")
        $currentSection = $patch.SelectSingleNode("/evemon/$sectionName")
        if ($null -eq $baselineSection -or $null -eq $currentSection) {
            throw "Required application-release section <$sectionName> is missing."
        }

        if ($baselineSection.OuterXml -cne $currentSection.OuterXml) {
            throw "Application-release section <$sectionName> changed during the SDE update."
        }
    }
}

$entries = @($patch.evemon.datafiles.datafile)
if ($entries.Count -ne $expectedNames.Count) {
    throw "Expected exactly $($expectedNames.Count) datafile entries, found $($entries.Count)."
}

$entryNames = @($entries | ForEach-Object { [string]$_.name })
$unexpectedNames = @($entryNames | Where-Object { $_ -cnotin $expectedNames })
if ($unexpectedNames.Count -gt 0) {
    throw "Unexpected datafile entries: $($unexpectedNames -join ', ')"
}

$downloadDirectory = $null
try {
    if ($Remote) {
        $downloadDirectory = Join-Path ([IO.Path]::GetTempPath()) "evemon-sde-$Build-$PID"
        [IO.Directory]::CreateDirectory($downloadDirectory) | Out-Null
    }

    foreach ($name in $expectedNames) {
        $matchingEntries = @($entries | Where-Object { [string]$_.name -ceq $name })
        if ($matchingEntries.Count -ne 1) {
            throw "Expected exactly one patch.xml entry for $name, found $($matchingEntries.Count)."
        }

        $entry = $matchingEntries[0]
        $entryUrl = [string]$entry.url
        if ($entryUrl -cne $expectedBaseUrl) {
            throw "Unexpected base URL for $name. Expected '$expectedBaseUrl', found '$entryUrl'."
        }

        $type = $name.Replace('eve-', '').Replace('-en-US.xml.gzip', '')
        $expectedMessage = "SDE $Build $type data file by the EVEMon Development Team`nNOT COMPATIBLE with EVEMon prior to version 2.2.0"
        $messageElements = @($entry.SelectNodes('./message'))
        if ($messageElements.Count -ne 1) {
            throw "Expected exactly one <message> element for $name, found $($messageElements.Count)."
        }
        $messageElement = $messageElements[0]
        if ($messageElement.ChildNodes.Count -ne 1 -or
            $messageElement.FirstChild.NodeType -ne [System.Xml.XmlNodeType]::CDATA) {
            throw "Datafile message for $name must contain exactly one CDATA section."
        }
        if ([string]$messageElement.FirstChild.Value -cne $expectedMessage) {
            throw "Unexpected datafile message for $name."
        }

        $localPath = Join-Path $resourcesDirectory $name
        if (-not (Test-Path -LiteralPath $localPath -PathType Leaf)) {
            throw "Local datafile not found: $localPath"
        }

        $manifestHash = [string]$entry.md5
        if ($manifestHash -cnotmatch '^[0-9a-f]{32}$') {
            throw "Invalid MD5 for $name. Expected exactly 32 lowercase hexadecimal characters, found '$manifestHash'."
        }
        $localHash = (Get-FileHash -LiteralPath $localPath -Algorithm MD5).Hash.ToLowerInvariant()
        if ($localHash -cne $manifestHash) {
            throw "Local MD5 mismatch for $name. patch.xml=$manifestHash local=$localHash"
        }

        if (-not $Remote) {
            continue
        }

        $remoteUrl = "$entryUrl/$name"
        $remotePath = Join-Path $downloadDirectory $name
        $downloaded = $false
        for ($attempt = 1; $attempt -le 6; $attempt++) {
            try {
                Invoke-WebRequest -Uri $remoteUrl -OutFile $remotePath
                $downloaded = $true
                break
            }
            catch {
                if ($attempt -eq 6) {
                    throw "Failed to download $remoteUrl after $attempt attempts: $($_.Exception.Message)"
                }

                Start-Sleep -Seconds 2
            }
        }

        if (-not $downloaded) {
            throw "Failed to download $remoteUrl."
        }

        $remoteHash = (Get-FileHash -LiteralPath $remotePath -Algorithm MD5).Hash.ToLowerInvariant()
        if ($remoteHash -cne $manifestHash) {
            throw "Remote MD5 mismatch for $name. patch.xml=$manifestHash remote=$remoteHash"
        }

        Write-Host "Validated $name ($manifestHash) from $remoteUrl"
    }
}
finally {
    if ($downloadDirectory -and [IO.Directory]::Exists($downloadDirectory)) {
        [IO.Directory]::Delete($downloadDirectory, $true)
    }
}

$scope = if ($Remote) { 'local and remote' } else { 'local' }
Write-Host "Validated all $($expectedNames.Count) SDE datafiles ($scope) for $tag."
