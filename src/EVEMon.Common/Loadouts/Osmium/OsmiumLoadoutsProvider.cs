using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVEMon.Common.Collections;
using EVEMon.Common.Constants;
using EVEMon.Common.Data;
using EVEMon.Common.Extensions;
using EVEMon.Common.Helpers;
using EVEMon.Common.Interfaces;
using EVEMon.Common.Net;
using EVEMon.Common.Serialization.Osmium.Loadout;


namespace EVEMon.Common.Loadouts.Osmium
{
    public sealed class OsmiumLoadoutsProvider : LoadoutsProvider
    {
        #region Fields

        private static bool s_queryFeedPending;
        private static bool s_queryPending;

        #endregion


        #region Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public override string Name => "Osmium";

        /// <summary>
        /// Gets a value indicating whether this <see cref="LoadoutsProvider" /> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        protected override bool Enabled => false;

        #endregion


        #region Inherited Methods

        /// <summary>
        /// Gets the loadouts feed.
        /// </summary>
        /// <param name="ship">The ship.</param>
        public override async Task GetLoadoutsFeedAsync(Item ship)
        {
            // Quit if query is pending
            if (s_queryFeedPending)
                return;

            Uri url = new Uri(NetworkConstants.OsmiumBaseUrl + string.Format(
                CultureConstants.InvariantCulture, NetworkConstants.OsmiumLoadoutFeed, ship.Name));

            s_queryFeedPending = true;

            var result = await Util.DownloadJsonAsync<List<SerializableOsmiumLoadoutFeed>>(url,
                new RequestParams()
                {
                    AcceptEncoded = true
                });
            OnLoadoutsFeedDownloaded(result.Result, result.Exception?.Message);
        }

        /// <summary>
        /// Gets the loadout by type ID.
        /// </summary>
        /// <param name="id">The id.</param>
        public override async Task GetLoadoutByIDAsync(long id)
        {
            // Quit if query is pending
            if (s_queryPending)
                return;

            Uri url = new Uri(NetworkConstants.OsmiumBaseUrl + string.Format(
                CultureConstants.InvariantCulture, NetworkConstants.OsmiumLoadoutDetails, id));

            s_queryPending = true;

            OnLoadoutDownloaded(await HttpWebClientService.DownloadStringAsync(url));
        }

        /// <summary>
        /// Deserializes the loadouts feed.
        /// </summary>
        /// <param name="ship">The ship.</param>
        /// <param name="feed">The feed.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">feed</exception>
        public override ILoadoutInfo DeserializeLoadoutsFeed(Item ship, object feed)
        {
            feed.ThrowIfNull(nameof(feed));
            var loadoutFeed = feed as List<SerializableOsmiumLoadoutFeed>;
            return (loadoutFeed == null) ? new LoadoutInfo() : DeserializeOsmiumJsonFeedFormat(
                ship, loadoutFeed);
        }

        /// <summary>
        /// Deserializes the loadout.
        /// </summary>
        /// <param name="loadout">The loadout.</param>
        /// <param name="feed">The feed.</param>
        /// <exception cref="System.ArgumentNullException">
        /// loadout
        /// or
        /// feed
        /// </exception>
        public override void DeserializeLoadout(Loadout loadout, object feed)
        {
            loadout.ThrowIfNull(nameof(loadout));

            feed.ThrowIfNull(nameof(feed));

            loadout.Items = LoadoutHelper.DeserializeEftFormat(feed as string).Loadouts.First().Items;
        }

        /// <summary>
        /// Occurs when we downloaded a loadouts feed from the provider.
        /// </summary>
        /// <param name="loadoutFeed">The loadout feed.</param>
        /// <param name="errorMessage">The error message.</param>
        private static void OnLoadoutsFeedDownloaded(object loadoutFeed, string errorMessage)
        {
            s_queryFeedPending = false;

            EveMonClient.OnLoadoutsFeedDownloaded(loadoutFeed, errorMessage);
        }

        /// <summary>
        /// Occurs when we downloaded a loadout from the provider.
        /// </summary>
        /// <param name="result">The result.</param>
        private static void OnLoadoutDownloaded(DownloadResult<string> result)
        {
            s_queryPending = false;

            EveMonClient.OnLoadoutDownloaded(result.Result, result.Error?.Message);
        }

        /// <summary>
        /// Deserializes the Osmium Json feed format.
        /// </summary>
        /// <param name="ship">The ship.</param>
        /// <param name="feed">The feed.</param>
        /// <returns></returns>
        private static ILoadoutInfo DeserializeOsmiumJsonFeedFormat(Item ship, IEnumerable<SerializableOsmiumLoadoutFeed> feed)
        {
            ILoadoutInfo loadoutInfo = new LoadoutInfo
            {
                Ship = ship
            };

            if (feed == null)
                return loadoutInfo;

            loadoutInfo.Loadouts
                .AddRange(feed
                    .Select(serialLoadout =>
                        new Loadout
                        {
                            ID = serialLoadout.ID,
                            Name = serialLoadout.Name,
                            Description = serialLoadout.RawDescription,
                            Author = serialLoadout.Author.Name,
                            Rating = serialLoadout.Rating,
                            SubmissionDate = serialLoadout.CreationDate.UnixTimeStampToDateTime(),
                            TopicUrl = new Uri(serialLoadout.Uri),
                            Items = Enumerable.Empty<Item>()
                        }));

            return loadoutInfo;
        }

        #endregion

    }
}

