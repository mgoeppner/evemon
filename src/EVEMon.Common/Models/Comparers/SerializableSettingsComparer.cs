using System.Collections.Generic;
using EVEMon.Common.Serialization.Settings;

namespace EVEMon.Common.Models.Comparers
{
    /// <summary>
    /// Performs a comparison between two <see cref="EVEMon.Common.Serialization.Settings.SerializableSettings"/> types.
    /// </summary>
    public sealed class SerializableSettingsComparer : IEqualityComparer<SerializableSettings>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type <see cref="EVEMon.Common.Serialization.Settings.SerializableSettings" /> to compare.</param>
        /// <param name="y">The second object of type <see cref="EVEMon.Common.Serialization.Settings.SerializableSettings" /> to compare.</param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(SerializableSettings x, SerializableSettings y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null))
                return false;
            if (ReferenceEquals(y, null))
                return false;
            if (x.GetType() != y.GetType())
                return false;
            return x.Revision == y.Revision &&
                   x.Compatibility == y.Compatibility &&
                   Equals(x.Updates, y.Updates) &&
                   Equals(x.Notifications, y.Notifications) &&
                   Equals(x.Scheduler, y.Scheduler) &&
                   Equals(x.Calendar, y.Calendar) &&
                   Equals(x.Exportation, y.Exportation) &&
                   Equals(x.MarketPricer, y.MarketPricer) &&
                   Equals(x.LoadoutsProvider, y.LoadoutsProvider) &&
                   Equals(x.CloudStorageServiceProvider, y.CloudStorageServiceProvider) &&
                   Equals(x.PortableEveInstallations, y.PortableEveInstallations) &&
                   Equals(x.UI, y.UI) &&
                   Equals(x.Proxy, y.Proxy);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public int GetHashCode(SerializableSettings obj)
        {
            unchecked
            {
                var hashCode = obj.Revision;
                hashCode = (hashCode * 397) ^ (int)obj.Compatibility;
                hashCode = (hashCode * 397) ^ (obj.Updates?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (obj.Notifications?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (obj.Scheduler?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (obj.Calendar?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (obj.Exportation?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (obj.MarketPricer?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (obj.LoadoutsProvider?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (obj.CloudStorageServiceProvider?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (obj.PortableEveInstallations?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (obj.UI?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (obj.Proxy?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}
