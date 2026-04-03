using EVEMon.Common.Collections;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Enumerations.CCPAPI;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Serialization.Esi;
using EVEMon.Common.Serialization.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EVEMon.Common.Models.Collections
{
    /// <summary>
    /// A collection of industry jobs.
    /// </summary>
    public sealed class IndustryJobCollection : ReadonlyCollection<IndustryJob>
    {
        private readonly CCPCharacter m_ccpCharacter;

        /// <summary>
        /// Internal constructor.
        /// </summary>
        /// <param name="character">The character.</param>
        internal IndustryJobCollection(CCPCharacter character)
        {
            m_ccpCharacter = character;
            EveMonClient.TimerTick += EveMonClient_TimerTick;
        }

        /// <summary>
        /// Called when the object gets disposed.
        /// </summary>
        internal void Dispose()
        {
            EveMonClient.TimerTick -= EveMonClient_TimerTick;
        }

        /// <summary>
        /// Handles the TimerTick event of the EveMonClient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EveMonClient_TimerTick(object sender, EventArgs e)
        {
            IQueryMonitor charIndustryJobsMonitor = m_ccpCharacter.QueryMonitors.Any(x =>
                (ESIAPICharacterMethods)x.Method == ESIAPICharacterMethods.IndustryJobs) ?
                m_ccpCharacter.QueryMonitors[ESIAPICharacterMethods.IndustryJobs] : null;
            IQueryMonitor corpIndustryJobsMonitor = m_ccpCharacter.QueryMonitors.Any(x =>
                (ESIAPICorporationMethods)x.Method == ESIAPICorporationMethods.
                CorporationIndustryJobs) ? m_ccpCharacter.QueryMonitors[
                ESIAPICorporationMethods.CorporationIndustryJobs] : null;

            if ((charIndustryJobsMonitor != null && charIndustryJobsMonitor.Enabled) ||
                (corpIndustryJobsMonitor != null && corpIndustryJobsMonitor.Enabled))
                UpdateOnTimerTick();
        }

        /// <summary>
        /// Imports an enumeration of serialization objects.
        /// </summary>
        /// <param name="src"></param>
        internal void Import(IEnumerable<SerializableJob> src)
        {
            Items.Clear();
            foreach (SerializableJob srcJob in src)
                Items.Add(new IndustryJob(srcJob) { InstallerID = m_ccpCharacter.CharacterID });
        }

        /// <summary>
        /// Imports an enumeration of API objects.
        /// </summary>
        /// <param name="src">The enumeration of serializable jobs from the API.</param>
        /// <param name="issuedFor">Whether these jobs were issued for the corporation or
        /// character.</param>
        internal void Import(IEnumerable<EsiJobListItem> src, IssuedFor issuedFor)
        {
            // Mark all jobs for deletion, jobs found in the API will be unmarked
            foreach (IndustryJob job in Items)
                job.MarkedForDeletion = true;
            var newJobs = new LinkedList<IndustryJob>();
            var now = DateTime.UtcNow;
            // Import the jobs from the API
            foreach (EsiJobListItem job in src)
            {
                DateTime limit = job.EndDate.AddDays(IndustryJob.MaxEndedDays);
                // For jobs which are not yet ended, or are active and not ready (active is
                // defined as having an empty completion date)
                if (limit >= now || (job.CompletedDate == DateTime.MinValue && job.Status !=
                    CCPJobCompletedStatus.Ready))
                {
                    // Where the job isn't already in the list
                    if (!Items.Any(x => x.TryImport(job, issuedFor, m_ccpCharacter)))
                    {
                        // Only add jobs with valid items
                        var ij = new IndustryJob(job, issuedFor);
                        if (ij.InstalledItem != null && ij.OutputItem != null)
                            newJobs.AddLast(ij);
                    }
                }
            }
            // Add the items that are no longer marked for deletion
            newJobs.AddRange(Items.Where(x => !x.MarkedForDeletion));
            // Replace the old list with the new one
            Items.Clear();
            Items.AddRange(newJobs);
        }

        /// <summary>
        /// Exports only the character issued jobs to a serialization object for the settings file.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Used to export only the corporation jobs issued by the character.</remarks>
        internal IEnumerable<SerializableJob> ExportOnlyIssuedByCharacter() => Items.Where(
            job => job.InstallerID == m_ccpCharacter.CharacterID).Select(job => job.Export());

        /// <summary>
        /// Exports the jobs to a serialization object for the settings file.
        /// </summary>
        /// <returns>List of serializable jobs.</returns>
        /// <remarks>Used to export all jobs of the collection.</remarks>
        internal IEnumerable<SerializableJob> Export() => Items.Select(job => job.Export());

        /// <summary>
        /// Notify the user on a job completion.
        /// </summary>
        private void UpdateOnTimerTick()
        {
            bool isCorporateMonitor = true;
            if (Items.Count > 0)
            {
                // Snapshot the items list to avoid IndexOutOfRangeException if the
                // notification chain causes Items to be modified during iteration
                var snapshot = Items.ToList();

                // Add the not notified "Ready" jobs to the completed list
                var jobsCompleted = new LinkedList<IndustryJob>();
                var characterJobs = new LinkedList<IndustryJob>();
                foreach (IndustryJob job in snapshot)
                {
                    if (job.IsActive && job.TTC.Length == 0 && !job.NotificationSend)
                    {
                        job.NotificationSend = true;
                        jobsCompleted.AddLast(job);
                        // Track if "jobs on behalf of character" needs to also be displayed
                        if (job.InstallerID == m_ccpCharacter.CharacterID)
                            characterJobs.AddLast(job);
                    }
                    // If this job was not issued for corp, ensure notification is for that
                    // character only
                    if (job.IssuedFor != IssuedFor.Corporation)
                        isCorporateMonitor = false;
                }
                // Only notify if jobs have been completed
                if (jobsCompleted.Count > 0)
                {
                    // Sends a notification
                    if (isCorporateMonitor)
                    {
                        if (characterJobs.Count > 0)
                            // Fire event for corporation job completion on behalf of character
                            EveMonClient.OnCharacterIndustryJobsCompleted(m_ccpCharacter,
                                characterJobs);
                        // Fire event for corporation job completion
                        EveMonClient.OnCorporationIndustryJobsCompleted(m_ccpCharacter,
                            jobsCompleted);
                    }
                    else
                        // Fire event for character job completion
                        EveMonClient.OnCharacterIndustryJobsCompleted(m_ccpCharacter,
                            jobsCompleted);
                }
            }
        }
    }
}
