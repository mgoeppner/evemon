using System;
using System.Threading.Tasks;
using EVEMon.Common.Data;
using EVEMon.Common.Extensions;
using EVEMon.Common.Helpers;
using EVEMon.Common.Serialization.Datafiles;

namespace EVEMon.Common.Collections.Global
{
    /// <summary>
    /// Implements a collection of datafiles
    /// </summary>
    public sealed class GlobalDatafileCollection : ReadonlyCollection<Datafile>
    {
        public static event EventHandler LoadingProgress;

        /// <summary>
        /// Default constructor. This class is only instantiated by EveMonClient.
        /// </summary>
        internal GlobalDatafileCollection()
        {
            try
            {
                Items.Clear();
                Items.Add(new Datafile(DatafileConstants.PropertiesDatafile));
                Items.Add(new Datafile(DatafileConstants.ItemsDatafile));
                Items.Add(new Datafile(DatafileConstants.SkillsDatafile));
                Items.Add(new Datafile(DatafileConstants.CertificatesDatafile));
                Items.Add(new Datafile(DatafileConstants.MasteriesDatafile));
                Items.Add(new Datafile(DatafileConstants.BlueprintsDatafile));
                Items.Add(new Datafile(DatafileConstants.GeographyDatafile));
                Items.Add(new Datafile(DatafileConstants.ReprocessingDatafile));
            }
            catch (Exception ex)
            {
                // Don't worry if we can't create MD5 maybe they have FIPS enforced
                EveMonClient.Trace(
                    "Couldn't compute datafiles checksums. FIPS was enforced, the datafiles were missing, or we couldn't copy to %APPDATA%.");
                ExceptionHandler.LogRethrowException(ex);
                throw;
            }
        }

        /// <summary>
        /// Loads the static data.
        /// </summary>
        public static async Task LoadAsync()
        {
            // Quit if the client has been shut down
            if (EveMonClient.Closed)
                return;

            // This is the time optimal loading order
            // (min order to follow : 
            // skills before anything else,
            // properties before items,
            // items before blueprints, reprocessing and certificates,
            // certs before masteries)

            EveMonClient.Trace("Datafiles.Load - begin", printMethod: false);

            // Must always run first
            // It will have finished loading until static skills finish
            Task properties = Task.Run(() => StaticProperties.Load());

            // Must always run before items
            Task skills = Task.Run(() => StaticSkills.Load());

            await Task.WhenAll(skills, properties);

            // Must always run synchronously as blueprints, reprocessing and certificates depend on it
            await Task.Run(() => StaticItems.Load());

            // Must always run synchronously as masteries depend on it
            await Task.Run(() => StaticCertificates.Load());

            // Must always run synchronously as ID to name depends on it
            await Task.Run(() => StaticGeography.Load());

            // Non critical loadings as all dependencies have been loaded
            Task blueprints = Task.Run(() => StaticBlueprints.Load());
            Task reprocessing = Task.Run(() => StaticReprocessing.Load());
            await Task.Run(() => StaticMasteries.Load());

            EveMonClient.Trace("Datafiles.Load - done", printMethod: false);
        }

        /// <summary>
        /// Called when a datafile has been loaded.
        /// </summary>
        public static void OnDatafileLoaded()
        {
            // Notify the subscribers
            LoadingProgress?.ThreadSafeInvoke(null, EventArgs.Empty);
        }
    }
}
