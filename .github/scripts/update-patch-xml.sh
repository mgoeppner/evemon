#!/bin/bash
# Generates an updated patch.xml with new MD5 checksums and release asset URLs.
# Usage: update-patch-xml.sh <build_number> <repo> <patch_xml_path> <resources_dir>

set -e

BUILD="$1"
REPO="$2"
PATCH_XML="$3"
RESOURCES_DIR="$4"

DATE=$(date -u +"%d %B %Y")
RELEASE_URL="https://github.com/${REPO}/releases/download/sde-${BUILD}"

{
  echo '<?xml version="1.0" encoding="utf-8"?>'
  echo '<evemon xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">'

  # Preserve the existing newest and releases sections unchanged
  sed -n '/<newest>/,/<\/newest>/p' "$PATCH_XML"
  sed -n '/<releases>/,/<\/releases>/p' "$PATCH_XML"

  # Generate datafiles section with new MD5s and release asset URLs
  echo '  <datafiles>'
  for f in "${RESOURCES_DIR}"/eve-*-en-US.xml.gzip; do
    NAME=$(basename "$f")
    MD5=$(md5sum "$f" | cut -d' ' -f1)
    TYPE=$(echo "$NAME" | sed 's/eve-//;s/-en-US.xml.gzip//')
    cat <<DATAFILE
    <datafile>
      <name>${NAME}</name>
      <date>${DATE}</date>
      <md5>${MD5}</md5>
      <url>${RELEASE_URL}</url>
      <message><![CDATA[SDE ${BUILD} ${TYPE} data file by the EVEMon Development Team
NOT COMPATIBLE with EVEMon prior to version 2.2.0]]></message>
    </datafile>
DATAFILE
  done
  echo '  </datafiles>'
  echo '</evemon>'
} > "${PATCH_XML}.new"

mv "${PATCH_XML}.new" "$PATCH_XML"
echo "Updated patch.xml with MD5s and release URLs pointing to sde-${BUILD}"
