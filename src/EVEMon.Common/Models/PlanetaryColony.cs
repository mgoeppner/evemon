using EVEMon.Common.Constants;
using EVEMon.Common.Data;
using EVEMon.Common.Enumerations.CCPAPI;
using EVEMon.Common.Net;
using EVEMon.Common.Serialization.Esi;
using EVEMon.Common.Serialization.Eve;
using System;
using System.Collections.Generic;

namespace EVEMon.Common.Models
{
    public sealed class PlanetaryColony : IComparable, IComparable<PlanetaryColony>
    {
        private readonly List<PlanetaryPin> m_planetaryPins = new List<PlanetaryPin>();
        private readonly List<PlanetaryRoute> m_planetaryRoutes = new List<PlanetaryRoute>();
        private readonly List<PlanetaryLink> m_planetaryLinks = new List<PlanetaryLink>();

        private bool m_queryPinsPending;
        private ResponseParams m_layoutResponse;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanetaryColony"/> class.
        /// </summary>
        /// <param name="ccpCharacter">The CCP character.</param>
        /// <param name="src">The source.</param>
        internal PlanetaryColony(CCPCharacter ccpCharacter, EsiPlanetaryColonyListItem src)
        {
            Character = ccpCharacter;
            SolarSystem = StaticGeography.GetSolarSystemByID(src.SolarSystemID);
            PlanetID = src.PlanetID;
            PlanetTypeID = src.PlanetType;
            PlanetTypeName = StaticItems.GetItemName(PlanetTypeID);
            PlanetName = SolarSystem.FindPlanetByID(PlanetID)?.Name ?? EveMonConstants.
                UnknownText;
            LastUpdate = src.LastUpdate;
            UpgradeLevel = src.UpgradeLevel;
            NumberOfPins = src.NumberOfPins;
            m_layoutResponse = null;

            GetColonyLayout();
        }

        #endregion


        #region Public Properties

        /// <summary>
        /// Gets the character.
        /// </summary>
        public CCPCharacter Character { get; }

        /// <summary>
        /// Gets the planet identifier.
        /// </summary>
        /// <value>
        /// The planet identifier.
        /// </value>
        public int PlanetID { get; }

        /// <summary>
        /// Gets the name of the planet.
        /// </summary>
        /// <value>
        /// The name of the planet.
        /// </value>
        public string PlanetName { get; private set; }

        /// <summary>
        /// Gets the planet type identifier.
        /// </summary>
        /// <value>
        /// The planet identifier.
        /// </value>
        public int PlanetTypeID { get; }

        /// <summary>
        /// Gets the name of the planet type.
        /// </summary>
        /// <value>
        /// The name of the planet type.
        /// </value>
        public string PlanetTypeName { get; }

        /// <summary>
        /// Gets the solar system where this job is located.
        /// </summary>
        /// <value>
        /// The solar system.
        /// </value>
        public SolarSystem SolarSystem { get; }

        /// <summary>
        /// Gets the installation full celestrial path.
        /// </summary>
        /// <value>
        /// The full location.
        /// </value>
        public string FullLocation => $"{SolarSystem.FullLocation} > {PlanetName}";

        /// <summary>
        /// Gets the last update.
        /// </summary>
        /// <value>
        /// The last update.
        /// </value>
        public DateTime LastUpdate { get; }

        /// <summary>
        /// Gets the upgrade level.
        /// </summary>
        /// <value>
        /// The upgrade level.
        /// </value>
        public int UpgradeLevel { get; }

        /// <summary>
        /// Gets the number of pins.
        /// </summary>
        /// <value>
        /// The number of pins.
        /// </value>
        public int NumberOfPins { get; }

        /// <summary>
        /// Gets the pins.
        /// </summary>
        /// <value>
        /// The pins.
        /// </value>
        public IEnumerable<PlanetaryPin> Pins => m_planetaryPins;

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <value>
        /// The routes.
        /// </value>
        public IEnumerable<PlanetaryRoute> Routes => m_planetaryRoutes;

        /// <summary>
        /// Gets the links.
        /// </summary>
        /// <value>
        /// The links.
        /// </value>
        public IEnumerable<PlanetaryLink> Links => m_planetaryLinks;

        #endregion


        #region Helper Methods
        
        /// <summary>
        /// Gets the colony pins.
        /// </summary>
        private void GetColonyLayout()
        {
            if (!m_queryPinsPending && !EsiErrors.IsErrorCountExceeded)
            {
                // Find the API key associated with planetary pins
                ESIKey apiKey = Character.Identity.FindAPIKeyWithAccess(ESIAPICharacterMethods.
                    PlanetaryLayout);
                string accessToken = apiKey?.GetAccessTokenForQuery();
                // Skip if no key or the token is not usable yet (e.g. refresh after resume).
                // GetColonyLayout is private and only invoked from the constructor, so the
                // retry path here is the parent PlanetaryColonies monitor recreating colony
                // objects on its next refresh once the token is fresh. If the parent reuses
                // an existing colony instance instead of recreating it, the layout will
                // remain stale until the next full reimport — acceptable since the parent
                // monitor itself is gated on a usable token by CCPQueryMonitorBase.
                if (apiKey == null || string.IsNullOrEmpty(accessToken))
                    return;
                m_queryPinsPending = true;
                EveMonClient.APIProviders.CurrentProvider.QueryEsi<EsiAPIPlanetaryColony>(
                    ESIAPICharacterMethods.PlanetaryLayout, OnPlanetaryPinsUpdated,
                    new ESIParams(m_layoutResponse, accessToken) {
                        ParamOne = Character.CharacterID,
                        ParamTwo = PlanetID
                    });
            }
        }
        
        /// <summary>
        /// Called when planetary pins updated.
        /// </summary>
        /// <param name="result">The result.</param>
        private void OnPlanetaryPinsUpdated(EsiResult<EsiAPIPlanetaryColony> result, object
            ignore)
        {
            m_queryPinsPending = false;
            m_layoutResponse = result.Response;
            // Notify if an error occured
            if (Character.ShouldNotifyError(result, ESIAPICharacterMethods.PlanetaryLayout))
                EveMonClient.Notifications.NotifyCharacterPlanetaryLayoutError(Character,
                    result);
            if (!result.HasError)
            {
                EveMonClient.Notifications.InvalidateCharacterAPIError(Character);
                if (result.HasData)
                {
                    Import(result.Result);
                    EveMonClient.OnCharacterPlanetaryLayoutUpdated(Character);
                }
            }
        }
        
        #endregion


        #region Importation

        /// <summary>
        /// Imports the planeatry layout.
        /// </summary>
        /// <param name="src">The source.</param>
        private void Import(EsiAPIPlanetaryColony src)
        {
            foreach (var item in src.Pins)
                m_planetaryPins.Add(new PlanetaryPin(this, item));
            foreach (var item in src.Links)
                m_planetaryLinks.Add(new PlanetaryLink(this, item));
            foreach (var item in src.Routes)
                m_planetaryRoutes.Add(new PlanetaryRoute(this, item));
        }
        
        #endregion


        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than <paramref name="obj" />. Zero This instance is equal to <paramref name="obj" />. Greater than zero This instance is greater than <paramref name="obj" />.
        /// </returns>
        public int CompareTo(object obj) => CompareTo((PlanetaryColony)obj);

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This object is equal to <paramref name="other" />. Greater than zero This object is greater than <paramref name="other" />.
        /// </returns>
        public int CompareTo(PlanetaryColony other) => this == other ? 1 : -1;
    }
}
