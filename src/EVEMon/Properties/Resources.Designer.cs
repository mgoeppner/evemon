﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EVEMon.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EVEMon.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The reminder interval must be a strictly positive integer..
        /// </summary>
        internal static string ErrorBadReminder {
            get {
                return ResourceManager.GetString("ErrorBadReminder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error retrieving error data. Wow, things are really messed up!.
        /// </summary>
        internal static string ErrorBuildingError {
            get {
                return ResourceManager.GetString("ErrorBuildingError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not complete the operation as the clipboard is being used by another process. Wait a few moments and try again..
        /// </summary>
        internal static string ErrorClipboardFailure {
            get {
                return ResourceManager.GetString("ErrorClipboardFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Contents of the clipboard is not a valid list of skills or contains invalid skill levels..
        /// </summary>
        internal static string ErrorClipboardImport {
            get {
                return ResourceManager.GetString("ErrorClipboardImport", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File failed to download correctly, do you wish to try again?.
        /// </summary>
        internal static string ErrorDownloadFailure {
            get {
                return ResourceManager.GetString("ErrorDownloadFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Warning: The data this ESI key provides is not sufficient for basic EVEMon features..
        /// </summary>
        internal static string ErrorFewScopes {
            get {
                return ResourceManager.GetString("ErrorFewScopes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The installer file could not be found. EVEMon will continue without updating..
        /// </summary>
        internal static string ErrorInstallerNotFound {
            get {
                return ResourceManager.GetString("ErrorInstallerNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A calendar at that path could not be found..
        /// </summary>
        internal static string ErrorNoCalendar {
            get {
                return ResourceManager.GetString("ErrorNoCalendar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to create datafile report.
        /// </summary>
        internal static string ErrorNoDataFile {
            get {
                return ResourceManager.GetString("ErrorNoDataFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Warning: This ESI key does not authorize any scopes and thus will not provide any data..
        /// </summary>
        internal static string ErrorNoScopes {
            get {
                return ResourceManager.GetString("ErrorNoScopes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ESI token could not be obtained..
        /// </summary>
        internal static string ErrorNoToken {
            get {
                return ResourceManager.GetString("ErrorNoToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to load trace file for inclusion in report.
        /// </summary>
        internal static string ErrorNoTraceFile {
            get {
                return ResourceManager.GetString("ErrorNoTraceFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please set the ESI Client ID and Client Secret in Settings &gt; Network before adding ESI keys..
        /// </summary>
        internal static string ErrorSetClientID {
            get {
                return ResourceManager.GetString("ErrorSetClientID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to start SSO server. Check your firewall settings (using port {0:D}) and ensure that only one instance of EVEMon is active while adding ESI keys..
        /// </summary>
        internal static string ErrorSSOStartup {
            get {
                return ResourceManager.GetString("ErrorSSOStartup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to establish a trusted connection to the CCP API servers.
        ///
        ///This may be due to an active man-in-the-middle attack or outdated local SSL certificates.
        ///
        ///Ensure that the system is up to date with all security patches and try again..
        /// </summary>
        internal static string ErrorTrustFailure {
            get {
                return ResourceManager.GetString("ErrorTrustFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error occurred and EVEMon was unable to handle the error message gracefully.
        ///
        ///The exception encountered was &apos;{0}&apos;..
        /// </summary>
        internal static string ErrorUnhandled {
            get {
                return ResourceManager.GetString("ErrorUnhandled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///The original exception encountered was &apos;{0}&apos;..
        /// </summary>
        internal static string ErrorUnhandledBase {
            get {
                return ResourceManager.GetString("ErrorUnhandledBase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///Please report this on the EVEMon forums..
        /// </summary>
        internal static string ErrorUnhandledEnd {
            get {
                return ResourceManager.GetString("ErrorUnhandledEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use the tree on the left to select a blueprint to view..
        /// </summary>
        internal static string MessageBlueprintSelect {
            get {
                return ResourceManager.GetString("MessageBlueprintSelect", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pasted skills and all dependencies have already been trained or planned..
        /// </summary>
        internal static string MessageClipboardPlanned {
            get {
                return ResourceManager.GetString("MessageClipboardPlanned", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pasted skills and all dependencies have already been trained..
        /// </summary>
        internal static string MessageClipboardTrained {
            get {
                return ResourceManager.GetString("MessageClipboardTrained", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The error details have been copied to the clipboard..
        /// </summary>
        internal static string MessageCopiedDetails {
            get {
                return ResourceManager.GetString("MessageCopiedDetails", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The selected entries have been copied to the clipboard..
        /// </summary>
        internal static string MessageCopiedPlan {
            get {
                return ResourceManager.GetString("MessageCopiedPlan", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to To begin using EVEMon, select the File &gt;Add Character... menu option, enter your ESI API information and choose the character to monitor..
        /// </summary>
        internal static string MessageGettingStarted {
            get {
                return ResourceManager.GetString("MessageGettingStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} has the skill for {1:D} Jump Clones (plus 1 for the implants in your active body).
        /// </summary>
        internal static string MessageJumpCloneSkills {
            get {
                return ResourceManager.GetString("MessageJumpCloneSkills", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This would result in a priority conflict, either pre-requisites with a lower priority or dependant skills with a higher priority.
        ///
        ///Select Yes if you wish to do this and adjust the other skills
        ///or No if you do not wish to change the priority..
        /// </summary>
        internal static string MessagePriorityConflict {
            get {
                return ResourceManager.GetString("MessagePriorityConflict", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Are you sure you want to clear the cache?.
        /// </summary>
        internal static string PromptClearCache {
            get {
                return ResourceManager.GetString("PromptClearCache", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Are you sure you want to delete this plan?.
        /// </summary>
        internal static string PromptDeletePlan {
            get {
                return ResourceManager.GetString("PromptDeletePlan", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Are you sure you want to ignore this update? You will not be prompted again until a newer version is released..
        /// </summary>
        internal static string PromptIgnoreUpdate {
            get {
                return ResourceManager.GetString("PromptIgnoreUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Are you sure you want to reset settings?
        ///Everything will be lost, including the plans..
        /// </summary>
        internal static string PromptResetSettings {
            get {
                return ResourceManager.GetString("PromptResetSettings", resourceCulture);
            }
        }
    }
}
