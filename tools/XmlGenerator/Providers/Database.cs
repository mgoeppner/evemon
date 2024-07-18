using EVEMon.Common.Constants;
using EVEMon.XmlGenerator.Collections;
using EVEMon.XmlGenerator.Extensions;
using EVEMon.XmlGenerator.Models;
using EVEMon.XmlGenerator.StaticData;
using EVEMon.XmlGenerator.Utils;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace EVEMon.XmlGenerator.Providers
{
    public static class Database
    {
        private static readonly EveStaticData s_context = new EveStaticData();

        private static DateTime s_startTime;
        private static string s_text = string.Empty;
        private static int s_totalTablesCount;

        #region Properties

        /// <summary>
        /// Gets the properties total count.
        /// </summary>
        /// <value>
        /// The properties total count.
        /// </value>
        internal static int PropertiesTotalCount { get; private set; }

        /// <summary>
        /// Gets the items total count.
        /// </summary>
        /// <value>
        /// The items total count.
        /// </value>
        internal static int ItemsTotalCount { get; private set; }

        /// <summary>
        /// Gets the skills total count.
        /// </summary>
        /// <value>
        /// The skills total count.
        /// </value>
        internal static int SkillsTotalCount { get; private set; }

        /// <summary>
        /// Gets the certificates total count.
        /// </summary>
        /// <value>
        /// The certificates total count.
        /// </value>
        internal static int CertificatesTotalCount { get; private set; }

        /// <summary>
        /// Gets the blueprints total count.
        /// </summary>
        /// <value>
        /// The blueprints total count.
        /// </value>
        internal static int BlueprintsTotalCount { get; private set; }

        /// <summary>
        /// Gets the geography total count.
        /// </summary>
        /// <value>
        /// The geography total count.
        /// </value>
        internal static int GeographyTotalCount { get; private set; }

        /// <summary>
        /// Gets the reprocessing total count.
        /// </summary>
        /// <value>
        /// The reprocessing total count.
        /// </value>
        internal static int ReprocessingTotalCount { get; private set; }

        /// <summary>
        /// Gets or sets the agt agents table.
        /// </summary>
        /// <value>The agt agents table.</value>
        internal static BagCollection<AgtAgents> AgtAgentsTable { get; private set; }

        /// <summary>
        /// Gets or sets the agt agent types table.
        /// </summary>
        /// <value>The agt agent types table.</value>
        internal static BagCollection<AgtAgentTypes> AgtAgentTypesTable { get; private set; }

        /// <summary>
        /// Gets or sets the agt research agents table.
        /// </summary>
        /// <value>The agt research agents table.</value>
        internal static BagCollection<AgtResearchAgents> AgtResearchAgentsTable { get; private set; }

        /// <summary>
        /// Gets or sets the chr factions table.
        /// </summary>
        /// <value>The chr factions table.</value>
        internal static BagCollection<ChrFactions> ChrFactionsTable { get; private set; }

        /// <summary>
        /// Gets or sets the crp NPC divisions table.
        /// </summary>
        /// <value>The crp NPC divisions table.</value>
        internal static BagCollection<CrpNPCDivisions> CrpNPCDivisionsTable { get; private set; }

        /// <summary>
        /// Gets or sets the dgm attribute categories table.
        /// </summary>
        /// <value>The dgm attribute categories table.</value>
        internal static BagCollection<DgmAttributeCategories> DgmAttributeCategoriesTable { get; private set; }

        /// <summary>
        /// Gets or sets the dgm attribute types table.
        /// </summary>
        /// <value>The dgm attribute types table.</value>
        internal static BagCollection<DgmAttributeTypes> DgmAttributeTypesTable { get; private set; }

        /// <summary>
        /// Gets or sets the dgm type attributes table.
        /// </summary>
        /// <value>The type attributes table.</value>
        internal static RelationSetCollection<DgmTypeAttributes> DgmTypeAttributesTable { get; private set; }

        /// <summary>
        /// Gets or sets the dgm type effects table.
        /// </summary>
        /// <value>The dgm type effects table.</value>
        internal static RelationSetCollection<DgmTypeEffects> DgmTypeEffectsTable { get; private set; }

        /// <summary>
        /// Gets or sets the eve icons table.
        /// </summary>
        /// <value>The eve icons table.</value>
        internal static BagCollection<EveIcons> EveIconsTable { get; private set; }

        /// <summary>
        /// Gets or sets the eve units table.
        /// </summary>
        /// <value>The eve units table.</value>
        internal static BagCollection<EveUnits> EveUnitsTable { get; private set; }

        /// <summary>
        /// Gets or sets the industry activity table.
        /// </summary>
        /// <value>The industry activity.</value>
        internal static RelationSetCollection<IndustryActivity> IndustryActivityTable { get; private set; }

        /// <summary>
        /// Gets or sets the industry activity materials table.
        /// </summary>
        /// <value>The industry activity material.</value>
        internal static RelationSetCollection<IndustryActivityMaterials> IndustryActivityMaterialsTable { get; private set; }

        /// <summary>
        /// Gets or sets the industry activity probabilities table.
        /// </summary>
        /// <value>The industry activity probability.</value>
        internal static RelationSetCollection<IndustryActivityProbabilities> IndustryActivityProbabilitiesTable { get; private set; }

        /// <summary>
        /// Gets or sets the industry activity products table.
        /// </summary>
        /// <value>The industry activity product.</value>
        internal static RelationSetCollection<IndustryActivityProducts> IndustryActivityProductsTable { get; private set; }

        /// <summary>
        /// Gets or sets the industry activity skills table.
        /// </summary>
        /// <value>The industry activity skill.</value>
        internal static RelationSetCollection<IndustryActivitySkills> IndustryActivitySkillsTable { get; private set; }

        /// <summary>
        /// Gets or sets the industry blueprints table.
        /// </summary>
        /// <value>The industry blueprint.</value>
        internal static BagCollection<IndustryBlueprints> IndustryBlueprintsTable { get; private set; }

        /// <summary>
        /// Gets or sets the inv items table.
        /// </summary>
        /// <value>The inv items table.</value>
        internal static BagCollection<InvItems> InvItemsTable { get; private set; }

        /// <summary>
        /// Gets or sets the inv names table.
        /// </summary>
        /// <value>The inv names table.</value>
        internal static BagCollection<InvNames> InvNamesTable { get; private set; }

        /// <summary>
        /// Gets or sets the dgm attribute types table.
        /// </summary>
        /// <value>The dgm attribute types table.</value>
        internal static BagCollection<InvTraits> InvTraitsTable { get; private set; }

        /// <summary>
        /// Gets or sets the inv categories table.
        /// </summary>
        /// <value>The inv categories table.</value>
        internal static BagCollection<InvCategories> InvCategoriesTable { get; private set; }

        /// <summary>
        /// Gets or sets the inv control tower resource purposes table.
        /// </summary>
        /// <value>The inv control tower resource purposes table.</value>
        internal static BagCollection<InvControlTowerResourcePurposes> InvControlTowerResourcePurposesTable { get; private set; }

        /// <summary>
        /// Gets or sets the inv control tower resources table.
        /// </summary>
        /// <value>The inv control tower resource table.</value>
        internal static List<InvControlTowerResources> InvControlTowerResourcesTable { get; private set; }

        /// <summary>
        /// Gets or sets the inv flag table.
        /// </summary>
        /// <value>The inv flags table.</value>
        internal static BagCollection<InvFlags> InvFlagsTable { get; private set; }

        /// <summary>
        /// Gets or sets the inv group table.
        /// </summary>
        /// <value>The inv groups table.</value>
        internal static BagCollection<InvGroups> InvGroupsTable { get; private set; }

        /// <summary>
        /// Gets or sets the inv market group table.
        /// </summary>
        /// <value>The inv market groups table.</value>
        internal static BagCollection<InvMarketGroups> InvMarketGroupsTable { get; private set; }

        /// <summary>
        /// Gets or sets the inv meta type table.
        /// </summary>
        /// <value>The inv meta types table.</value>
        internal static RelationSetCollection<InvMetaTypes> InvMetaTypesTable { get; private set; }

        /// <summary>
        /// Gets or sets the inv type table.
        /// </summary>
        /// <value>The inv types table.</value>
        internal static BagCollection<InvTypes> InvTypesTable { get; private set; }

        /// <summary>
        /// Gets or sets the inv type materials table.
        /// </summary>
        /// <value>The inv type materials table.</value>
        internal static List<InvTypeMaterials> InvTypeMaterialsTable { get; private set; }

        /// <summary>
        /// Gets the inv type reactions table.
        /// </summary>
        internal static List<InvTypeReactions> InvTypeReactionsTable { get; private set; }

        /// <summary>
        /// Gets or sets the map regions table.
        /// </summary>
        /// <value>The map regions table.</value>
        internal static BagCollection<MapRegions> MapRegionsTable { get; private set; }

        /// <summary>
        /// Gets or sets the map constellations table.
        /// </summary>
        /// <value>The map constellations table.</value>
        internal static BagCollection<MapConstellations> MapConstellationsTable { get; private set; }

        /// <summary>
        /// Gets or sets the map solar system table.
        /// </summary>
        /// <value>The map solar systems table.</value>
        internal static BagCollection<MapSolarSystems> MapSolarSystemsTable { get; private set; }

        /// <summary>
        /// Gets or sets the sta stations table.
        /// </summary>
        /// <value>The sta station table.</value>
        internal static LongBagCollection<StaStations> StaStationsTable { get; private set; }

        /// <summary>
        /// Gets or sets the map solar system jump table.
        /// </summary>
        /// <value>The map solar system jumps table.</value>
        internal static List<MapSolarSystemsJump> MapSolarSystemJumpsTable { get; private set; }

        #endregion


        #region Database Connection Methods

        /// <summary>
        /// Creates the connection to the SQL Database.
        /// </summary>
        /// <returns></returns>
        private static SqliteConnection CreateConnection()
        {
            s_text = "Connecting to SQL Server... ";
            Console.Write(s_text);

            // Initialize the SQL Connection
            var connection = GetConnection("EveStaticData");

            try
            {
                connection.Open();

                Console.SetCursorPosition(Console.CursorLeft - s_text.Length, Console.CursorTop);
                Console.WriteLine(@"Connection to SQL Server: Successful");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(Console.CursorLeft - s_text.Length, Console.CursorTop);
                Console.WriteLine(@"Connection to SQL Server: Failed");
                Console.WriteLine(@"Reason: {0}", ex.Message);
                Console.Write(@"Press any key to exit.");
                Console.ReadLine();
                Environment.Exit(-1);
            }

            return connection;
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <param name="connectionName">Name of the connection.</param>
        /// <returns></returns>
        private static SqliteConnection GetConnection(string connectionName)
        {
            ConnectionStringSettings connectionStringSetting = ConfigurationManager.ConnectionStrings[connectionName];
            if (connectionStringSetting != null)
                return new SqliteConnection(connectionStringSetting.ConnectionString);

            Console.SetCursorPosition(Console.CursorLeft - s_text.Length, Console.CursorTop);
            Console.WriteLine(@"Can not find conection string with name: {0}", connectionName);
            Console.Write(@"Press any key to exit.");
            Console.ReadLine();
            Environment.Exit(-1);
            return null;
        }

        #endregion


        #region Table Creation Methods

        /// <summary>
        /// Creates the tables from database.
        /// </summary>
        internal static void CreateTables()
        {
            s_totalTablesCount = Util.GetCountOfTypesInNamespace("EVEMon.XmlGenerator.StaticData");

            var connection = CreateConnection();

            // Data dumps are available from CCP
            Console.Write(@"Loading data from '{0}' database... ", connection.Database);

            s_startTime = DateTime.Now;

            Util.UpdatePercentDone(0);

            AgtAgentsTable = Agents();
            Util.UpdateProgress(s_totalTablesCount);
            AgtAgentTypesTable = AgentTypes();
            Util.UpdateProgress(s_totalTablesCount);
            AgtResearchAgentsTable = ResearchAgents();
            Util.UpdateProgress(s_totalTablesCount);
            ChrFactionsTable = Factions();
            Util.UpdateProgress(s_totalTablesCount);
            CrpNPCDivisionsTable = NPCDivisions();
            Util.UpdateProgress(s_totalTablesCount);
            DgmAttributeCategoriesTable = AttributeCategories();
            Util.UpdateProgress(s_totalTablesCount);
            DgmAttributeTypesTable = AttributeTypes();
            Util.UpdateProgress(s_totalTablesCount);
            InvTraitsTable = Traits();
            Util.UpdateProgress(s_totalTablesCount);
            DgmTypeAttributesTable = TypeAttributes();
            Util.UpdateProgress(s_totalTablesCount);
            DgmTypeEffectsTable = TypeEffects();
            Util.UpdateProgress(s_totalTablesCount);

            // Find out what this used to be and find a way around it... Is it even useful?
            //DgmTypeTraitsTable = TypeTraits();
            //Util.UpdateProgress(s_totalTablesCount);

            EveIconsTable = Icons();
            Util.UpdateProgress(s_totalTablesCount);
            EveUnitsTable = Units();
            Util.UpdateProgress(s_totalTablesCount);

            // New industry tables
            IndustryActivityTable = IndustryActivity();
            Util.UpdateProgress(s_totalTablesCount);
            IndustryActivityMaterialsTable = IndustryActivityMaterials();
            Util.UpdateProgress(s_totalTablesCount);
            IndustryActivityProbabilitiesTable = IndustryActivityProbabilities();
            Util.UpdateProgress(s_totalTablesCount);
            IndustryActivityProductsTable = IndustryActivityProducts();
            Util.UpdateProgress(s_totalTablesCount);
            IndustryActivitySkillsTable = IndustryActivitySkills();
            Util.UpdateProgress(s_totalTablesCount);
            IndustryBlueprintsTable = IndustryBlueprints();
            Util.UpdateProgress(s_totalTablesCount);

            InvCategoriesTable = Categories();
            Util.UpdateProgress(s_totalTablesCount);
            InvControlTowerResourcePurposesTable = ControlTowerResourcePurposes();
            Util.UpdateProgress(s_totalTablesCount);
            InvControlTowerResourcesTable = ControlTowerResources();
            Util.UpdateProgress(s_totalTablesCount);
            InvFlagsTable = Flags();
            Util.UpdateProgress(s_totalTablesCount);
            InvGroupsTable = Groups();
            Util.UpdateProgress(s_totalTablesCount);
            InvItemsTable = Items();
            Util.UpdateProgress(s_totalTablesCount);
            InvMarketGroupsTable = MarketGroups();
            Util.UpdateProgress(s_totalTablesCount);
            InvMetaTypesTable = MetaTypes();
            Util.UpdateProgress(s_totalTablesCount);
            InvNamesTable = Names();
            Util.UpdateProgress(s_totalTablesCount);
            InvTypeMaterialsTable = TypeMaterials();
            Util.UpdateProgress(s_totalTablesCount);
            InvTypeReactionsTable = TypeReactions();
            Util.UpdateProgress(s_totalTablesCount);
            InvTypesTable = Types();
            Util.UpdateProgress(s_totalTablesCount);
            MapConstellationsTable = Constellations();
            Util.UpdateProgress(s_totalTablesCount);
            MapRegionsTable = Regions();
            Util.UpdateProgress(s_totalTablesCount);
            MapSolarSystemJumpsTable = SolarSystemsJumps();
            Util.UpdateProgress(s_totalTablesCount);
            MapSolarSystemsTable = SolarSystems();
            Util.UpdateProgress(s_totalTablesCount);

            StaStationsTable = Stations();
            Util.UpdateProgress(s_totalTablesCount);

            Console.WriteLine(@" in {0}", DateTime.Now.Subtract(s_startTime).ToString("g", CultureConstants.DefaultCulture));
        }

        /// <summary>
        /// Agent Agents.
        /// </summary>
        /// <returns><c>BagCollection</c> of Agent Agents.</returns>
        private static BagCollection<AgtAgents> Agents()
        {
            IndexedCollection<AgtAgents> collection = new IndexedCollection<AgtAgents>();

            foreach (agtAgents agent in s_context.agtAgents)
            {
                AgtAgents item = new AgtAgents
                {
                    ID = agent.agentID,
                };

                if (agent.divisionID.HasValue)
                    item.DivisionID = agent.divisionID.Value;

                if (agent.locationID.HasValue)
                    item.LocationID = agent.locationID.Value;

                if (agent.level.HasValue)
                    item.Level = agent.level.Value;

                if (agent.quality.HasValue)
                    item.Quality = agent.quality.Value;

                if (agent.agentTypeID.HasValue)
                    item.AgentTypeID = agent.agentTypeID.Value;

                if (agent.isLocator.HasValue)
                    item.IsLocator = agent.isLocator.Value;

                collection.Items.Add(item);
            }

            return collection.ToBag();
        }

        /// <summary>
        /// Agent Agent Types.
        /// </summary>
        /// <returns><c>BagCollection</c> of Agent Agent Types.</returns>
        private static BagCollection<AgtAgentTypes> AgentTypes()
        {
            IndexedCollection<AgtAgentTypes> collection = new IndexedCollection<AgtAgentTypes>();

            foreach (AgtAgentTypes item in s_context.agtAgentTypes.Select(
                agentType => new AgtAgentTypes
                {
                    ID = agentType.agentTypeID,
                    AgentType = agentType.agentType
                }))
            {
                collection.Items.Add(item);
            }

            return collection.ToBag();
        }

        /// <summary>
        /// Agent Research Agents.
        /// </summary>
        /// <returns><c>BagCollection</c> of Agent Research Agents.</returns>
        private static BagCollection<AgtResearchAgents> ResearchAgents()
        {
            IndexedCollection<AgtResearchAgents> collection = new IndexedCollection<AgtResearchAgents>();

            foreach (AgtResearchAgents item in s_context.agtResearchAgents.Select(
                researchAgent => new AgtResearchAgents
                {
                    ID = researchAgent.agentID,
                    ResearchSkillID = researchAgent.typeID
                }))
            {
                collection.Items.Add(item);
            }

            return collection.ToBag();
        }

        /// <summary>
        /// Character Factions.
        /// </summary>
        /// <returns><c>BagCollection</c> of Character Factions.</returns>
        private static BagCollection<ChrFactions> Factions()
        {
            IndexedCollection<ChrFactions> collection = new IndexedCollection<ChrFactions>();

            foreach (chrFactions faction in s_context.chrFactions)
            {
                ChrFactions item = new ChrFactions
                {
                    ID = faction.factionID,
                    FactionName = faction.factionName,
                    Description = faction.description,
                    MilitiaCorporationID = faction.militiaCorporationID,
                };

                item.Description = item.Description.Clean();

                if (faction.raceIDs.HasValue)
                    item.RaceID = faction.raceIDs.Value;

                if (faction.solarSystemID.HasValue)
                    item.SolarSystemID = faction.solarSystemID.Value;

                if (faction.corporationID.HasValue)
                    item.CorporationID = faction.corporationID.Value;

                if (faction.sizeFactor.HasValue)
                    item.SizeFactor = faction.sizeFactor.Value;

                // TODO - Fix these...
                if (faction.stationCount.HasValue)
                    item.StationCount = (short)faction.stationCount.Value;

                if (faction.stationSystemCount.HasValue)
                    item.StationSystemCount = (short)faction.stationSystemCount.Value;

                if (faction.iconID.HasValue)
                    item.IconID = faction.iconID.Value;

                collection.Items.Add(item);
            }

            return collection.ToBag();
        }

        /// <summary>
        /// Corporation NPC Divisions.
        /// </summary>
        /// <returns><c>BagCollection</c> of Corporation NPC Divisions.</returns>
        private static BagCollection<CrpNPCDivisions> NPCDivisions()
        {
            var collection = new IndexedCollection<CrpNPCDivisions>();
            var items = collection.Items;

            foreach (CrpNPCDivisions item in s_context.crpNPCDivisions.Select(
                npcDivision => new CrpNPCDivisions
                {
                    ID = npcDivision.divisionID,
                    DivisionName = npcDivision.divisionName
                }))
            {
                items.Add(item);
            }

            // Looks like some new divisions were forgotten in the SDE
            for (int i = 18; i <= 30; i++)
                items.Add(new CrpNPCDivisions
                {
                    ID = i,
                    DivisionName = "Unknown"
                });

            return collection.ToBag();
        }

        /// <summary>
        /// Dogma Attribute categories.
        /// </summary>
        /// <returns><c>BagCollection</c> of Dogma Attribute Categories.</returns>
        private static BagCollection<DgmAttributeCategories> AttributeCategories()
        {
            IndexedCollection<DgmAttributeCategories> collection = new IndexedCollection<DgmAttributeCategories>();

            foreach (DgmAttributeCategories item in s_context.dgmAttributeCategories.Select(
                category => new DgmAttributeCategories
                {
                    ID = category.categoryID,
                    Description = category.categoryDescription,
                    Name = category.categoryName
                }))
            {
                item.Description = item.Description.Clean();
                item.Name = item.Name.Clean();

                collection.Items.Add(item);
            }

            return collection.ToBag();
        }

        /// <summary>
        /// Dogma Attribute Types.
        /// </summary>
        /// <returns><c>BagCollection</c> of Dogma Attribute Types.</returns>
        private static BagCollection<DgmAttributeTypes> AttributeTypes()
        {
            IndexedCollection<DgmAttributeTypes> collection = new IndexedCollection<DgmAttributeTypes>();

            foreach (dgmAttributeTypes attribute in s_context.dgmAttributeTypes)
            {
                DgmAttributeTypes item = new DgmAttributeTypes
                {
                    ID = attribute.attributeID,
                    CategoryID = attribute.categoryID,
                    Description = attribute.description,
                    DisplayName = attribute.displayName,
                    IconID = attribute.iconID,
                    Name = attribute.attributeName,
                    UnitID = attribute.unitID,
                };

                item.Description = item.Description.Clean();
                item.DisplayName = item.DisplayName.Clean();
                item.Name = item.Name.Clean();

                if (attribute.defaultValue.HasValue)
                    item.DefaultValue = attribute.defaultValue.Value.ToString(CultureInfo.InvariantCulture);

                if (attribute.published.HasValue)
                    item.Published = attribute.published.Value;

                if (attribute.highIsGood.HasValue)
                    item.HigherIsBetter = attribute.highIsGood.Value;

                collection.Items.Add(item);
            }

            // Set properties total count
            PropertiesTotalCount = collection.Items.Count;

            return collection.ToBag();
        }

        /// <summary>
        /// Dogma Traits.
        /// </summary>
        /// <returns></returns>
        private static BagCollection<InvTraits> Traits()
        {
            IndexedCollection<InvTraits> collection = new IndexedCollection<InvTraits>();

            foreach (InvTraits item in s_context.invTraits.Select(
                trait => new InvTraits
                {
                    ID = trait.traitID,
                    skillID = trait.skillID,
                    typeID = trait.typeID,
                    bonus = trait.bonus,
                    BonusText = trait.BonusText,
                    UnitID = trait.unitID
                }))
            {
                item.BonusText = item.BonusText.Clean();

                collection.Items.Add(item);
            }

            return collection.ToBag();
        }

        /// <summary>
        /// Dogma Type Attributes.
        /// </summary>
        /// <returns><c>RelationSetCollection</c> of attributes for types.</returns>
        private static RelationSetCollection<DgmTypeAttributes> TypeAttributes()
        {
            IEnumerable<DgmTypeAttributes> list = s_context.dgmTypeAttributes.Select(
                typeAttribute => new DgmTypeAttributes
                {
                    AttributeID = typeAttribute.attributeID,
                    ItemID = typeAttribute.typeID,
                    ValueFloat = typeAttribute.valueFloat,
                    ValueInt64 = typeAttribute.valueInt
                }).ToList();

            return new RelationSetCollection<DgmTypeAttributes>(list);
        }

        /// <summary>
        /// Dogma Type Effects.
        /// </summary>
        /// <returns><c>RelationSetCollection</c> of Types and Effects.</returns>
        private static RelationSetCollection<DgmTypeEffects> TypeEffects()
        {
            List<DgmTypeEffects> list = s_context.dgmTypeEffects.Select(
                typeEffect => new DgmTypeEffects
                {
                    EffectID = typeEffect.effectID,
                    ItemID = typeEffect.typeID
                }).ToList();

            return new RelationSetCollection<DgmTypeEffects>(list);
        }

        /// <summary>
        /// EVE Icons.
        /// </summary>
        /// <returns><c>BagCollection</c> of EVE icons.</returns>
        private static BagCollection<EveIcons> Icons()
        {
            IndexedCollection<EveIcons> collection = new IndexedCollection<EveIcons>();

            foreach (EveIcons item in s_context.eveIcons.Select(
                icon => new EveIcons
                {
                    ID = icon.iconID,
                    Icon = icon.iconFile
                }))
            {
                collection.Items.Add(item);
            }

            return collection.ToBag();
        }

        /// <summary>
        /// EVE Units.
        /// </summary>
        /// <returns><c>BagCollection</c> of EVE Units.</returns>
        private static BagCollection<EveUnits> Units()
        {
            IndexedCollection<EveUnits> collection = new IndexedCollection<EveUnits>();

            foreach (EveUnits item in s_context.eveUnits.Select(
                unit => new EveUnits
                {
                    Description = unit.description,
                    DisplayName = unit.displayName,
                    ID = unit.unitID,
                    Name = unit.unitName
                }))
            {
                item.Description = item.Description.Clean();
                item.DisplayName = item.DisplayName.Clean();

                collection.Items.Add(item);
            }

            return collection.ToBag();
        }

        /// <summary>
        /// Industry Activity.
        /// </summary>
        /// <returns><c>RelationSetCollection</c> of industry activities</returns>
        private static RelationSetCollection<IndustryActivity> IndustryActivity()
        {
            IEnumerable<IndustryActivity> list = s_context.industryActivity.Select(x =>
            new IndustryActivity()
            {
                ActivityID = x.activityID,
                BlueprintTypeID = x.typeID,
                Time = x.time
            });

            return new RelationSetCollection<IndustryActivity>(list);
        }

        /// <summary>
        /// Industry Activity Materials.
        /// </summary>
        /// <returns><c>RelationSetCollection</c> of industry activity materials</returns>
        private static RelationSetCollection<IndustryActivityMaterials> IndustryActivityMaterials()
        {
            IEnumerable<IndustryActivityMaterials> list = s_context.industryActivityMaterials
                .Where(x => x.activityID.HasValue && x.typeID.HasValue && x.materialTypeID.HasValue)
                .Select(x => new IndustryActivityMaterials()
                {
                    ActivityID = x.activityID.Value,
                    BlueprintTypeID = x.typeID.Value,
                    MaterialTypeID = x.materialTypeID.Value,
                    Quantity = x.quantity
                });
            return new RelationSetCollection<IndustryActivityMaterials>(list);
        }

        /// <summary>
        /// Industry Activity Probabilities.
        /// </summary>
        /// <returns><c>RelationSetCollection</c> of industry activity probabilities</returns>
        private static RelationSetCollection<IndustryActivityProbabilities> IndustryActivityProbabilities()
        {
            IEnumerable<IndustryActivityProbabilities> list = s_context.industryActivityProbabilities
                .Where(x => x.activityID.HasValue && x.typeID.HasValue && x.productTypeID.HasValue)
                .Select(x => new IndustryActivityProbabilities()
                {
                    ActivityID = x.activityID.Value,
                    BlueprintTypeID = x.typeID.Value,
                    ProductTypeID = x.productTypeID.Value,
                    Probability = x.probability
                });

            return new RelationSetCollection<IndustryActivityProbabilities>(list);
        }

        /// <summary>
        /// Industry Activity Products.
        /// </summary>
        /// <returns><c>RelationSetCollection</c> of industry activity products</returns>
        private static RelationSetCollection<IndustryActivityProducts> IndustryActivityProducts()
        {
            IEnumerable<IndustryActivityProducts> list = s_context.industryActivityProducts
                .Where(x => x.activityID.HasValue && x.typeID.HasValue && x.productTypeID.HasValue)
                .Select(x => new IndustryActivityProducts()
                {
                    ActivityID = x.activityID.Value,
                    BlueprintTypeID = x.typeID.Value,
                    ProductTypeID = x.productTypeID.Value,
                    Quantity = x.quantity
                });

            return new RelationSetCollection<IndustryActivityProducts>(list);
        }

        /// <summary>
        /// Industry Activity Skills.
        /// </summary>
        /// <returns><c>RelationSetCollection</c> of industry activity skills</returns>
        private static RelationSetCollection<IndustryActivitySkills> IndustryActivitySkills()
        {
            IEnumerable<IndustryActivitySkills> list = s_context.industryActivitySkills
                .Where(x => x.activityID.HasValue && x.typeID.HasValue && x.skillID.HasValue)
                .Select(x => new IndustryActivitySkills()
                {
                    ActivityID = x.activityID.Value,
                    BlueprintTypeID = x.typeID.Value,
                    SkillID = x.skillID.Value,
                    Level = x.level
                });

            return new RelationSetCollection<IndustryActivitySkills>(list);
        }

        /// <summary>
        /// Industry Blueprints.
        /// </summary>
        /// <returns><c>BagCollection</c> of industry blueprints.</returns>
        private static BagCollection<IndustryBlueprints> IndustryBlueprints()
        {
            IndexedCollection<IndustryBlueprints> collection = new IndexedCollection<IndustryBlueprints>();

            foreach (industryBlueprints blueprint in s_context.industryBlueprints)
            {
                IndustryBlueprints item = new IndustryBlueprints
                {
                    ID = blueprint.typeID,
                    MaxProductionLimit = blueprint.maxProductionLimit
                };

                collection.Items.Add(item);
            }

            BlueprintsTotalCount = collection.Items.Count;

            return collection.ToBag();
        }

        /// <summary>
        /// Inventory Categories.
        /// </summary>
        /// <returns><c>BagCollection</c> of Inventory Categories.</returns>
        private static BagCollection<InvCategories> Categories()
        {
            IndexedCollection<InvCategories> collection = new IndexedCollection<InvCategories>();

            foreach (invCategories category in s_context.invCategories)
            {
                InvCategories item = new InvCategories
                {
                    ID = category.categoryID,
                    Name = category.categoryName,
                    IconID = category.iconID
                };

                if (category.published.HasValue)
                    item.Published = category.published.Value;

                collection.Items.Add(item);
            }

            return collection.ToBag();
        }

        /// <summary>
        /// Inventory Control Tower Resource Purposes.
        /// </summary>
        /// <returns><c>BagCollection</c> of Inventory Control Tower Resource Purposes.</returns>
        private static BagCollection<InvControlTowerResourcePurposes> ControlTowerResourcePurposes()
        {
            IndexedCollection<InvControlTowerResourcePurposes> collection =
                new IndexedCollection<InvControlTowerResourcePurposes>();

            foreach (InvControlTowerResourcePurposes item in s_context.invControlTowerResourcePurposes.Select(
                resource => new InvControlTowerResourcePurposes
                {
                    ID = resource.purpose,
                    PurposeName = resource.purposeText,
                }))
            {
                collection.Items.Add(item);
            }

            return collection.ToBag();
        }

        /// <summary>
        /// Inventory Control Tower Resources.
        /// </summary>
        /// <returns><c>List</c> of Inventory Control Tower Resources .</returns>
        private static List<InvControlTowerResources> ControlTowerResources()
        {
            List<InvControlTowerResources> list = new List<InvControlTowerResources>();

            foreach (invControlTowerResources resource in s_context.invControlTowerResources)
            {
                InvControlTowerResources item = new InvControlTowerResources
                {
                    ID = resource.controlTowerTypeID,
                    ResourceID = resource.resourceTypeID,
                    MinSecurityLevel = resource.minSecurityLevel,
                    FactionID = resource.factionID,
                };

                if (resource.purpose.HasValue)
                    item.PurposeID = resource.purpose.Value;

                if (resource.quantity.HasValue)
                    item.Quantity = resource.quantity.Value;

                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// Inventory Flags.
        /// </summary>
        /// <returns><c>BagCollection</c> of Inventory Flags.</returns>
        private static BagCollection<InvFlags> Flags()
        {
            IndexedCollection<InvFlags> collection = new IndexedCollection<InvFlags>();

            foreach (InvFlags item in s_context.invFlags.Select(
                flag => new InvFlags
                {
                    ID = flag.flagID,
                    Name = flag.flagName,
                    Text = flag.flagText,
                }))
            {
                item.Text = item.Text.Clean();
                collection.Items.Add(item);
            }

            return collection.ToBag();
        }

        /// <summary>
        /// Inventory Groups.
        /// </summary>
        /// <returns><c>BagCollection</c> of Inventory Groups.</returns>
        private static BagCollection<InvGroups> Groups()
        {
            IndexedCollection<InvGroups> collection = new IndexedCollection<InvGroups>();

            foreach (invGroups group in s_context.invGroups)
            {
                InvGroups item = new InvGroups
                {
                    ID = group.groupID,
                    Name = group.groupName,
                    UseBasePrice = group.useBasePrice,
                    Anchored = group.anchored,
                    Anchorable = group.anchorable,
                    FittableNonSingleton = group.fittableNonSingleton
                };

                if (group.published.HasValue)
                    item.Published = group.published.Value;

                if (group.categoryID.HasValue)
                    item.CategoryID = group.categoryID.Value;

                collection.Items.Add(item);
            }

            // CCPLease missing groups
            collection.Items.Add(new InvGroups()
            {
                ID = 4052,
                Name = "Jump Filament Blueprint",
                Anchorable = false,
                Anchored = false,
                FittableNonSingleton = false,
                Published = false
            });
            collection.Items.Add(new InvGroups()
            {
                ID = 4053,
                Name = "Irregular Capsule",
                Anchorable = false,
                Anchored = false,
                FittableNonSingleton = false,
                Published = false
            });

            return collection.ToBag();
        }

        /// <summary>
        /// Inventory Items.
        /// </summary>
        /// <returns><c>BagCollection</c> of Inventory Items.</returns>
        private static BagCollection<InvItems> Items()
        {
            IndexedCollection<InvItems> collection = new IndexedCollection<InvItems>();

            foreach (InvItems item in s_context.invItems.Select(
                item => new InvItems
                {
                    ID = (int)item.itemID,
                    FlagID = (int)item.flagID,
                    LocationID = (int)item.locationID,
                    OwnerID = item.ownerID,
                    Quantity = item.quantity,
                    TypeID = item.typeID
                }))
            {
                collection.Items.Add(item);
            }

            return collection.ToBag();
        }

        /// <summary>
        /// Inventory Market Groups.
        /// </summary>
        /// <returns><c>BagCollection</c> of Market Groups available on the market.</returns>
        private static BagCollection<InvMarketGroups> MarketGroups()
        {
            IndexedCollection<InvMarketGroups> collection = new IndexedCollection<InvMarketGroups>();

            foreach (InvMarketGroups item in s_context.invMarketGroups.Select(
                marketGroup => new InvMarketGroups
                {
                    ID = marketGroup.marketGroupID,
                    Description = marketGroup.description,
                    IconID = marketGroup.iconID,
                    Name = marketGroup.marketGroupName,
                    ParentID = marketGroup.parentGroupID
                }))
            {
                item.Description = item.Description.Clean();

                collection.Items.Add(item);
            }

            // CCPLease missing market groups
            collection.Items.Add(new InvMarketGroups()
            {
                ID = 2763,
                Name = "Mobile Cynosural Beacons",
                ParentID = 404,
                Description = "",
                IconID = 0
            });

            return collection.ToBag();
        }

        /// <summary>
        /// Inventory Meta Types.
        /// </summary>
        /// <returns><c>RelationSetCollection</c> parent-child relationships between types.</returns>
        private static RelationSetCollection<InvMetaTypes> MetaTypes()
        {
            List<InvMetaTypes> list = new List<InvMetaTypes>();

            foreach (invMetaTypes metaType in s_context.invMetaTypes)
            {
                InvMetaTypes item = new InvMetaTypes
                {
                    ItemID = metaType.typeID
                };

                if (metaType.metaGroupID.HasValue)
                    item.MetaGroupID = Convert.ToInt32(metaType.metaGroupID, CultureInfo.InvariantCulture);

                if (metaType.parentTypeID.HasValue)
                    item.ParentItemID = Convert.ToInt32(metaType.parentTypeID, CultureInfo.InvariantCulture);
                list.Add(item);
            }
            return new RelationSetCollection<InvMetaTypes>(list);
        }

        /// <summary>
        /// Inventory Names.
        /// </summary>
        /// <returns><c>BagCollection</c> of Inventory Names.</returns>
        private static BagCollection<InvNames> Names()
        {
            IndexedCollection<InvNames> collection = new IndexedCollection<InvNames>();

            foreach (InvNames item in s_context.invNames.Select(
                name => new InvNames
                {
                    ID = (int)name.itemID,
                    Name = name.itemName
                }))
            {
                collection.Items.Add(item);
            }

            return collection.ToBag();
        }

        /// <summary>
        /// Inventory Materials.
        /// </summary>
        /// <returns>List of Materials.</returns>
        private static List<InvTypeMaterials> TypeMaterials()
            => s_context.invTypeMaterials.Select(
                material => new InvTypeMaterials
                {
                    ID = material.typeID,
                    MaterialTypeID = material.materialTypeID,
                    Quantity = material.quantity
                }).ToList();

        /// <summary>
        /// Inventory Type Reactions.
        /// </summary>
        /// <returns>List of reaction info.</returns>
        private static List<InvTypeReactions> TypeReactions()
        {
            List<InvTypeReactions> list = new List<InvTypeReactions>();

            foreach (invTypeReactions reaction in s_context.invTypeReactions)
            {
                InvTypeReactions item = new InvTypeReactions
                {
                    ID = reaction.reactionTypeID,
                    Input = reaction.input,
                    TypeID = reaction.typeID,
                };

                if (reaction.quantity.HasValue)
                    item.Quantity = reaction.quantity.Value;

                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// Inventory Types.
        /// </summary>
        /// <returns><c>BagCollection</c> of items from the Inventory.</returns>
        private static BagCollection<InvTypes> Types()
        {
            IndexedCollection<InvTypes> collection = new IndexedCollection<InvTypes>();

            foreach (invTypes type in s_context.invTypes)
            {
                InvTypes item = new InvTypes
                {
                    ID = type.typeID,
                    Description = type.description,
                    MarketGroupID = type.marketGroupID,
                    Name = type.typeName,
                    RaceID = type.raceID
                };
                item.Description = item.Description.Clean();

                if (type.basePrice.HasValue)
                    item.BasePrice = type.basePrice.Value;

                if (type.capacity.HasValue)
                    item.Capacity = type.capacity.Value;

                if (type.groupID.HasValue)
                    item.GroupID = type.groupID.Value;

                if (type.mass.HasValue)
                    item.Mass = type.mass.Value;

                if (type.published.HasValue)
                    item.Published = type.published.Value;

                if (type.volume.HasValue)
                    item.Volume = type.volume.Value;

                if (type.portionSize.HasValue)
                    item.PortionSize = type.portionSize.Value;

                collection.Items.Add(item);
            }

            // Set items total count
            ItemsTotalCount = ReprocessingTotalCount = collection.Items.Count;

            // Set skills total count
            SkillsTotalCount = collection.Items.Count(item => item.GroupID != DBConstants.
                FakeSkillsGroupID && InvGroupsTable[item.GroupID].CategoryID ==
                DBConstants.SkillCategoryID);

            return collection.ToBag();
        }

        /// <summary>
        /// Map Constellations.
        /// </summary>
        /// <returns><c>BagCollection</c> of Map Constellations.</returns>
        /// <remarks>Constallations in the EVE Universe.</remarks>
        private static BagCollection<MapConstellations> Constellations()
        {
            IndexedCollection<MapConstellations> collection = new IndexedCollection<MapConstellations>();

            foreach (mapConstellations constellation in s_context.mapConstellations)
            {
                MapConstellations item = new MapConstellations
                {
                    ID = constellation.constellationID,
                    Name = constellation.constellationName,
                };

                if (constellation.regionID.HasValue)
                    item.RegionID = constellation.regionID.Value;

                collection.Items.Add(item);
            }

            return collection.ToBag();
        }

        /// <summary>
        /// Map Regions.
        /// </summary>
        /// <returns><c>BagCollection</c> of Map Regions.</returns>
        /// <remarks>Regions in the EVE Universe.</remarks>
        private static BagCollection<MapRegions> Regions()
        {
            IndexedCollection<MapRegions> collection = new IndexedCollection<MapRegions>();

            foreach (MapRegions item in s_context.mapRegions.Select(
                region => new MapRegions
                {
                    ID = region.regionID,
                    Name = region.regionName,
                    FactionID = region.factionID
                }))
            {
                collection.Items.Add(item);
            }

            GeographyTotalCount = collection.Items.Count;

            return collection.ToBag();
        }

        /// <summary>
        /// Map Solar Systems Jump.
        /// </summary>
        /// <returns><c>List</c> of Map Solar Systems Jump.</returns>
        /// <remarks>Jumps between two solar systems in the EVE Universe.</remarks>
        private static List<MapSolarSystemsJump> SolarSystemsJumps()
            => s_context.mapSolarSystemJumps.Select(
                jump => new MapSolarSystemsJump
                {
                    A = jump.fromSolarSystemID,
                    B = jump.toSolarSystemID
                }).ToList();

        /// <summary>
        /// Map Solar Systems.
        /// </summary>
        /// <returns><c>BagCollection</c> of Map Solar Systems.</returns>
        private static BagCollection<MapSolarSystems> SolarSystems()
        {
            IndexedCollection<MapSolarSystems> collection = new IndexedCollection<MapSolarSystems>();

            foreach (mapSolarSystems solarsystem in s_context.mapSolarSystems)
            {
                MapSolarSystems item = new MapSolarSystems
                {
                    ID = solarsystem.solarSystemID,
                    Name = solarsystem.solarSystemName
                };

                if (solarsystem.constellationID.HasValue)
                    item.ConstellationID = solarsystem.constellationID.Value;

                if (solarsystem.security.HasValue)
                    item.SecurityLevel = (float)solarsystem.security.Value;

                if (solarsystem.x.HasValue)
                    item.X = solarsystem.x.Value;

                if (solarsystem.y.HasValue)
                    item.Y = solarsystem.y.Value;

                if (solarsystem.z.HasValue)
                    item.Z = solarsystem.z.Value;

                collection.Items.Add(item);
            }

            return collection.ToBag();
        }

        /// <summary>
        /// Station Stations.
        /// </summary>
        /// <returns><c>BagCollection</c> of Station Stations.</returns>
        /// <remarks>Stations in the EVE Universe.</remarks>
        private static LongBagCollection<StaStations> Stations()
        {
            LongIndexedCollection<StaStations> collection = new LongIndexedCollection<StaStations>();

            foreach (staStations station in s_context.staStations)
            {
                StaStations item = new StaStations
                {
                    ID = station.stationID,
                    Name = station.stationName,
                };

                if (station.reprocessingEfficiency.HasValue)
                    item.ReprocessingEfficiency = (float)station.reprocessingEfficiency.Value;

                if (station.reprocessingStationsTake.HasValue)
                    item.ReprocessingStationsTake = (float)station.reprocessingStationsTake.Value;

                if (station.security.HasValue)
                    item.SecurityLevel = station.security.Value;

                if (station.solarSystemID.HasValue)
                    item.SolarSystemID = station.solarSystemID.Value;

                if (station.corporationID.HasValue)
                    item.CorporationID = station.corporationID.Value;

                collection.Items.Add(item);
            }

            return collection.ToBag();
        }

        #endregion
    }
}
