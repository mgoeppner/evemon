using EVEMon.Common.Collections;
using EVEMon.Common.Constants;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Serialization.Datafiles;
using EVEMon.XmlGenerator.Interfaces;
using EVEMon.XmlGenerator.Providers;
using EVEMon.XmlGenerator.StaticData;
using EVEMon.XmlGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EVEMon.XmlGenerator.Datafiles
{
    internal static class Blueprints
    {
        private static List<InvMarketGroups> s_injectedMarketGroups;
        private static List<InvTypes> s_nullMarketBlueprints;

        /// <summary>
        /// Generate the skills datafile.
        /// </summary>
        internal static void GenerateDatafile()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Util.ResetCounters();

            Console.WriteLine();
            Console.Write(@"Generating blueprints datafile... ");

            // Configure blueprints with Null market group
            ConfigureNullMarketBlueprint();

            Dictionary<int, SerializableBlueprintMarketGroup> groups = new Dictionary<int, SerializableBlueprintMarketGroup>();

            // Export blueprint groups           
            CreateMarketGroups(groups);

            // Create the parent-children groups relations
            foreach (SerializableBlueprintMarketGroup group in groups.Values)
            {
                IEnumerable<SerializableBlueprintMarketGroup> children = Database.InvMarketGroupsTable.Concat(
                    s_injectedMarketGroups).Where(x => x.ParentID == group.ID).Select(
                        x => groups[x.ID]).OrderBy(x => x.Name);

                group.SubGroups.AddRange(children);
            }

            // Sort groups
            IEnumerable<SerializableBlueprintMarketGroup> blueprintGroups = Database.InvMarketGroupsTable.Concat(
                s_injectedMarketGroups).Where(x => x.ParentID == DBConstants.BlueprintsMarketGroupID).Select(
                    x => groups[x.ID]).OrderBy(x => x.Name);

            // Reset the custom market groups
            s_nullMarketBlueprints.ForEach(srcItem => srcItem.MarketGroupID = null);
            Database.InvTypesTable
                .Where(item => Database.InvGroupsTable[item.GroupID].CategoryID == DBConstants.AncientRelicsCategoryID)
                .ToList()
                .ForEach(x => x.MarketGroupID = DBConstants.AncientRelicsMarketGroupID);

            // Serialize
            BlueprintsDatafile datafile = new BlueprintsDatafile();
            datafile.MarketGroups.AddRange(blueprintGroups);

            Util.DisplayEndTime(stopwatch);

            // DEBUG: Find which blueprints have not been generated
            if (Debugger.IsAttached)
            {
                var blueprintIds = groups.Values.SelectMany(x => x.Blueprints).Select(y => y.ID).ToList();
                // Some typeIDs are present in blueprints.yaml but not in typeIDs.yaml (glorious CCP)
                // https://forums-archive.eveonline.com/message/6914995/#post6914995
                var diff = Database.IndustryBlueprintsTable.Where(blueprint => Database.InvTypesTable.HasValue(blueprint.ID)
                && !blueprintIds.Contains(blueprint.ID)).ToList();

                if (diff.Any())
                    Console.WriteLine("{0} blueprints were not generated.", diff.Count);
            }

            Util.SerializeXml(datafile, DatafileConstants.BlueprintsDatafile);
        }

        /// <summary>
        /// Creates the market groups.
        /// </summary>
        /// <param name="groups">The groups.</param>
        private static void CreateMarketGroups(IDictionary<int, SerializableBlueprintMarketGroup> groups)
        {
            foreach (InvMarketGroups marketGroup in Database.InvMarketGroupsTable.Concat(s_injectedMarketGroups))
            {
                SerializableBlueprintMarketGroup group = new SerializableBlueprintMarketGroup
                {
                    ID = marketGroup.ID,
                    Name = marketGroup.Name,
                };

                groups[marketGroup.ID] = group;

                // Add the items in this group
                List<SerializableBlueprint> blueprints = new List<SerializableBlueprint>();
                foreach (InvTypes item in Database.InvTypesTable.Where(
                    item => item.MarketGroupID.GetValueOrDefault() == marketGroup.ID &&
                            (Database.InvGroupsTable[item.GroupID].CategoryID == DBConstants.BlueprintCategoryID ||
                             Database.InvGroupsTable[item.GroupID].CategoryID == DBConstants.AncientRelicsCategoryID)))
                {
                    CreateBlueprint(item, blueprints);
                }

                // Store the items
                group.Blueprints.AddRange(blueprints.OrderBy(x => x.Name));
            }
        }

        /// <summary>
        /// Configures the null market blueprint.
        /// </summary>
        private static void ConfigureNullMarketBlueprint()
        {
            // Create custom market groups that don't exist in EVE
            s_injectedMarketGroups = new List<InvMarketGroups>
            {
                new InvMarketGroups
                {
                    Name = "Various Non-Market",
                    Description = "Various blueprints not in EVE market",
                    ID = DBConstants.BlueprintRootNonMarketGroupID,
                    ParentID = DBConstants.BlueprintsMarketGroupID,
                    IconID = DBConstants.UnknownBlueprintBackdropIconID
                },
                new InvMarketGroups
                {
                    Name = "Tech I",
                    Description = "Tech I blueprints not in EVE market",
                    ID = DBConstants.BlueprintTechINonMarketGroupID,
                    ParentID = DBConstants.BlueprintRootNonMarketGroupID,
                    IconID = DBConstants.UnknownBlueprintBackdropIconID
                },
                new InvMarketGroups
                {
                    Name = "Tech II",
                    Description = "Tech II blueprints not in EVE market",
                    ID = DBConstants.BlueprintTechIINonMarketGroupID,
                    ParentID = DBConstants.BlueprintRootNonMarketGroupID,
                    IconID = DBConstants.UnknownBlueprintBackdropIconID
                },
                new InvMarketGroups
                {
                    Name = "Storyline",
                    Description = "Storyline blueprints not in EVE market",
                    ID = DBConstants.BlueprintStorylineNonMarketGroupID,
                    ParentID = DBConstants.BlueprintRootNonMarketGroupID,
                    IconID = DBConstants.UnknownBlueprintBackdropIconID
                },
                new InvMarketGroups
                {
                    Name = "Faction",
                    Description = "Faction blueprints not in EVE market",
                    ID = DBConstants.BlueprintFactionNonMarketGroupID,
                    ParentID = DBConstants.BlueprintRootNonMarketGroupID,
                    IconID = DBConstants.UnknownBlueprintBackdropIconID
                },
                new InvMarketGroups
                {
                    Name = "Officer",
                    Description = "Officer blueprints not in EVE market",
                    ID = DBConstants.BlueprintOfficerNonMarketGroupID,
                    ParentID = DBConstants.BlueprintRootNonMarketGroupID,
                    IconID = DBConstants.UnknownBlueprintBackdropIconID
                },
                new InvMarketGroups
                {
                    Name = "Tech III",
                    Description = "Tech III blueprints not in EVE market",
                    ID = DBConstants.BlueprintTechIIINonMarketGroupID,
                    ParentID = DBConstants.BlueprintRootNonMarketGroupID,
                    IconID = DBConstants.UnknownBlueprintBackdropIconID
                },
                new InvMarketGroups
                {
                    Name = "Research Equipment",
                    Description = "Items that can be invented to a Tech III blueprint not in EVE market",
                    ID = DBConstants.ResearchEquipmentNonMarketGroupID,
                    ParentID = DBConstants.BlueprintRootNonMarketGroupID,
                    IconID = DBConstants.UnknownBlueprintBackdropIconID
                }
            };

            s_nullMarketBlueprints = Database.InvTypesTable
                .Where(item => item.MarketGroupID == null &&
                               Database.InvGroupsTable[item.GroupID].CategoryID == DBConstants.BlueprintCategoryID).ToList();

            // Set ancient relics to research equipment custom market group
            Database.InvTypesTable
                .Where(item => Database.InvGroupsTable[item.GroupID].CategoryID == DBConstants.AncientRelicsCategoryID)
                .ToList()
                .ForEach(x => x.MarketGroupID = DBConstants.ResearchEquipmentNonMarketGroupID);

            // Set the market group of the blueprints with NULL MarketGroupID to custom market groups
            foreach (InvTypes item in s_nullMarketBlueprints)
            {
                // Set some blueprints to market groups manually
                SetMarketGroupManually(item);

                // Set some blueprints to custom market groups according to their metagroup
                SetMarketGroupFromMetaGroup(item);

                if (item.MarketGroupID == null)
                    item.MarketGroupID = DBConstants.BlueprintTechINonMarketGroupID;
            }
        }

        /// <summary>
        /// Sets the market group.
        /// </summary>
        /// <param name="item">The item.</param>
        private static void SetMarketGroupManually(InvTypes item)
        {
            switch (item.ID)
            {
                case DBConstants.WildMinerIBlueprintID:
                case DBConstants.AlphaDataAnalyzerIBlueprintID:
                case DBConstants.DaemonDataAnalyzerIBlueprintID:
                case DBConstants.CodexDataAnalyzerIBlueprintID:
                case DBConstants.CropGasCloudHarvesterBlueprintID:
                case DBConstants.Dual1000mmScoutIAcceleratorCannonBlueprintID:
                case DBConstants.HabitatMinerIBlueprintID:
                case DBConstants.LibramDataAnalyzerIBlueprintID:
                case DBConstants.LimosCitadelCruiseLauncherIBlueprintID:
                case DBConstants.MagpieMobileTractorUnitBlueprintID:
                case DBConstants.PackratMobileTractorUnitBlueprintID:
                case DBConstants.PlowGascloudHarvesterBlueprintID:
                case DBConstants.ShockLimosCitadelTorpedoBayIBlueprintID:
                case DBConstants.WetuMobileDepotBlueprintID:
                case DBConstants.YurtMobileDepotBlueprintID:
                    item.MarketGroupID = DBConstants.BlueprintStorylineNonMarketGroupID;
                    break;

                case DBConstants.AsteroBlueprintID:
                case DBConstants.BarghestBlueprintID:
                case DBConstants.CambionBlueprintID:
                case DBConstants.ChremoasBlueprintID:
                case DBConstants.EtanaBlueprintID:
                case DBConstants.GarmurBlueprintID:
                case DBConstants.MaliceBlueprintID:
                case DBConstants.MorachaBlueprintID:
                case DBConstants.NestorBlueprintID:
                case DBConstants.OrthrusBlueprintID:
                case DBConstants.PolicePursuitCometBlueprintID:
                case DBConstants.ScorpionIshukoneWatchBlueprintID:
                case DBConstants.ShadowBlueprintID:
                case DBConstants.StratiosBlueprintID:
                case DBConstants.StratiosEmergencyResponderBlueprintID:
                case DBConstants.UtuBlueprintID:
                case DBConstants.VangelBlueprintID:
                case DBConstants.WhiptailBlueprintID:
                case DBConstants.AdrestiaBlueprintID:
                case DBConstants.EchelonBlueprintID:
                case DBConstants.ImperialNavySlicerBlueprintID:
                case DBConstants.CaldariNavyHookbillBlueprintID:
                case DBConstants.FederationNavyCometBlueprintID:
                case DBConstants.RepublicFleetFiretailBlueprintID:
                case DBConstants.NightmareBlueprintID:
                case DBConstants.MacharielBlueprintID:
                case DBConstants.DramielBlueprintID:
                case DBConstants.CruorBlueprintID:
                case DBConstants.SuccubusBlueprintID:
                case DBConstants.DaredevilBlueprintID:
                case DBConstants.CynabalBlueprintID:
                case DBConstants.AshimmuBlueprintID:
                case DBConstants.PhantasmBlueprintID:
                case DBConstants.GorusShuttleBlueprintID:
                case DBConstants.GuristasShuttleBlueprintID:
                case DBConstants.GallenteMiningLaserBlueprintID:
                case DBConstants.InterbusShuttleBlueprintID:
                case DBConstants.FrekiBlueprintID:
                case DBConstants.MimirBlueprintID:
                    item.MarketGroupID = DBConstants.BlueprintFactionNonMarketGroupID;
                    break;

                case DBConstants.BladeBlueprintID:
                case DBConstants.BlazeLBlueprintID:
                case DBConstants.BlazeMBlueprintID:
                case DBConstants.BlazeSBlueprintID:
                case DBConstants.BoltLBlueprintID:
                case DBConstants.BoltMBlueprintID:
                case DBConstants.BoltSBlueprintID:
                case DBConstants.CapitalRemoteCapacitorTransmitterIIBlueprintID:
                case DBConstants.CapitalRemoteShieldBoosterIIBlueprintID:
                case DBConstants.ChameleonBlueprintID:
                case DBConstants.DaggerBlueprintID:
                case DBConstants.DesolationLBlueprintID:
                case DBConstants.DesolationMBlueprintID:
                case DBConstants.DesolationSBlueprintID:
                case DBConstants.DroneDamageRigIIBlueprintID:
                case DBConstants.ErinyeBlueprintID:
                case DBConstants.GathererBlueprintID:
                case DBConstants.HighGradeAscendancyAlphaBlueprintID:
                case DBConstants.HighGradeAscendancyBetaBlueprintID:
                case DBConstants.HighGradeAscendancyGammaBlueprintID:
                case DBConstants.HighGradeAscendancyDeltaBlueprintID:
                case DBConstants.HighGradeAscendancyEpsilonBlueprintID:
                case DBConstants.HighGradeAscendancyOmegaBlueprintID:
                case DBConstants.KisharBlueprintID:
                case DBConstants.LuxLBlueprintID:
                case DBConstants.LuxMBlueprintID:
                case DBConstants.LuxSBlueprintID:
                case DBConstants.MackinawOREDevelopmentEditionBlueprintID:
                case DBConstants.MediumEWDroneRangeAugmentorIIBlueprintID:
                case DBConstants.MidGradeAscenancyAlphaBlueprintID:
                case DBConstants.MidGradeAscenancyBetaBlueprintID:
                case DBConstants.MidGradeAscenancyGammaBlueprintID:
                case DBConstants.MidGradeAscenancyDeltaBlueprintID:
                case DBConstants.MidGradeAscenancyEpsilonBlueprintID:
                case DBConstants.MidGradeAscenancyOmegaBlueprintID:
                case DBConstants.MinerIIChinaBlueprintID:
                case DBConstants.MiningLaserOptimizationIIBlueprintID:
                case DBConstants.MiningLaserRangeIIBlueprintID:
                case DBConstants.ReconProbeLauncherIIBlueprintID:
                case DBConstants.ScanProbeLauncherIIBlueprintID:
                case DBConstants.ShieldTransporterRigIIBlueprintID:
                case DBConstants.ShockLBlueprintID:
                case DBConstants.ShockMBlueprintID:
                case DBConstants.ShockSBlueprintID:
                case DBConstants.SmallEWDroneRangeAugmentorIIBlueprintID:
                case DBConstants.StormLBlueprintID:
                case DBConstants.StormMBlueprintID:
                case DBConstants.StormSBlueprintID:
                case DBConstants.TalismanAlphaBlueprintID:
                    item.MarketGroupID = DBConstants.BlueprintTechIINonMarketGroupID;
                    break;

                case DBConstants.LegionBlueprintID:
                case DBConstants.LegionDefensiveAdaptiveAugmenterBlueprintID:
                case DBConstants.LegionElectronicsEnergyParasiticComplexBlueprintID:
                case DBConstants.LegionEngineeringPowerCoreMultiplierBlueprintID:
                case DBConstants.LegionOffensiveDroneSynthesisProjectorBlueprintID:
                case DBConstants.LegionPropulsionChassisOptimizationBlueprintID:
                case DBConstants.LokiBlueprintID:
                case DBConstants.LokiDefensiveAdaptiveShieldingBlueprintID:
                case DBConstants.LokiElectronicsImmobilityDriversBlueprintID:
                case DBConstants.LokiEngineeringPowerCoreMultiplierBlueprintID:
                case DBConstants.LokiOffensiveTurretConcurrenceRegistryBlueprintID:
                case DBConstants.LokiPropulsionChassisOptimizationBlueprintID:
                case DBConstants.ProteusBlueprintID:
                case DBConstants.ProteusDefensiveAdaptiveAugmenterBlueprintID:
                case DBConstants.ProteusElectronicsFrictionExtensionProcessorBlueprintID:
                case DBConstants.ProteusEngineeringPowerCoreMultiplierBlueprintID:
                case DBConstants.ProteusOffensiveDissonicEncodingPlatformBlueprintID:
                case DBConstants.ProteusPropulsionWakeLimiterBlueprintID:
                case DBConstants.TenguBlueprintID:
                case DBConstants.TenguDefensiveAdaptiveShieldingBlueprintID:
                case DBConstants.TenguElectronicsObfuscationManifoldBlueprintID:
                case DBConstants.TenguEngineeringPowerCoreMultiplierBlueprintID:
                case DBConstants.TenguOffensiveAcceleratedEjectionBayBlueprintID:
                case DBConstants.TenguPropulsionIntercalatedNanofibersBlueprintID:
                    item.MarketGroupID = DBConstants.BlueprintTechIIINonMarketGroupID;
                    break;
            }
        }

        /// <summary>
        /// Sets the market group from meta group.
        /// </summary>
        /// <param name="item">The item.</param>
        private static void SetMarketGroupFromMetaGroup(InvTypes item)
        {
            // Guard in case an item of blueprint type is not contained in the blueprints table (glorious CCP)
            if (!Database.IndustryBlueprintsTable.HasValue(item.ID))
                return;

            var blueprint = Database.IndustryBlueprintsTable[item.ID];

            var productTypeID = Database.IndustryActivityProductsTable.Where(
                x => x.BlueprintTypeID == item.ID &&
                x.ActivityID == (int)BlueprintActivity.Manufacturing).Select(
                    x => x.ProductTypeID).SingleOrDefault();

            int relation = Database.InvMetaTypesTable.Where(
                x => x.ItemID == productTypeID).Select(
                    x => x.MetaGroupID).FirstOrDefault();

            switch (relation)
            {
                case DBConstants.TechIIMetaGroupID:
                    item.MarketGroupID = DBConstants.BlueprintTechIINonMarketGroupID;
                    break;
                case DBConstants.StorylineMetaGroupID:
                    item.MarketGroupID = DBConstants.BlueprintStorylineNonMarketGroupID;
                    break;
                case DBConstants.FactionMetaGroupID:
                    item.MarketGroupID = DBConstants.BlueprintFactionNonMarketGroupID;
                    break;
                case DBConstants.OfficerMetaGroupID:
                    item.MarketGroupID = DBConstants.BlueprintOfficerNonMarketGroupID;
                    break;
                case DBConstants.TechIIIMetaGroupID:
                    item.MarketGroupID = DBConstants.BlueprintTechIIINonMarketGroupID;
                    break;
            }
        }

        /// <summary>
        /// Add properties to a blueprint.
        /// </summary>
        /// <param name="srcBlueprint"></param>
        /// <param name="blueprintsGroup"></param>
        /// <returns></returns>
        private static void CreateBlueprint(InvTypes srcBlueprint, ICollection<SerializableBlueprint> blueprintsGroup)
        {
            Util.UpdatePercentDone(Database.BlueprintsTotalCount);

            // Guard in case an item of blueprint type is not contained in the blueprints table (glorious CCP)
            if (!Database.IndustryBlueprintsTable.HasValue(srcBlueprint.ID))
                return;

            var blueprintType = Database.IndustryBlueprintsTable[srcBlueprint.ID];

            var productType = Database.IndustryActivityProductsTable.Where(
                x => x.BlueprintTypeID == srcBlueprint.ID &&
                x.ActivityID == (int)BlueprintActivity.Manufacturing)
                .SingleOrDefault();

            var tempActivity = new IndustryActivity() { BlueprintTypeID = srcBlueprint.ID, ActivityID = (int)BlueprintActivity.Manufacturing };
            var productionTime = Database.IndustryActivityTable.Get(tempActivity)?.Time;

            tempActivity.ActivityID = (int)BlueprintActivity.ResearchingMaterialEfficiency;
            var researchProductivityTime = Database.IndustryActivityTable.Get(tempActivity)?.Time;

            tempActivity.ActivityID = (int)BlueprintActivity.ResearchingMaterialEfficiency;
            var researchMaterialTime = Database.IndustryActivityTable.Get(tempActivity)?.Time;

            tempActivity.ActivityID = (int)BlueprintActivity.Copying;
            var researchCopyTime = Database.IndustryActivityTable.Get(tempActivity)?.Time;

            tempActivity.ActivityID = (int)BlueprintActivity.Invention;
            var inventionTime = Database.IndustryActivityTable.Get(tempActivity)?.Time;

            tempActivity.ActivityID = (int)BlueprintActivity.ReverseEngineering;
            var reverseEngineeringTime = Database.IndustryActivityTable.Get(tempActivity)?.Time;

            tempActivity.ActivityID = (int)BlueprintActivity.Reactions;
            var reactionTime = Database.IndustryActivityTable.Get(tempActivity)?.Time;



            // Creates the blueprint with base informations
            SerializableBlueprint blueprint = new SerializableBlueprint
            {
                ID = srcBlueprint.ID,
                Name = srcBlueprint.Name,
                Icon = srcBlueprint.IconID.HasValue
                    ? Database.EveIconsTable[srcBlueprint.IconID.Value].Icon
                    : string.Empty,
                ProduceItemID = (productType?.ProductTypeID).GetValueOrDefault(),
                ProductionTime = productionTime.GetValueOrDefault(),
                ResearchProductivityTime = researchProductivityTime.GetValueOrDefault(),
                ResearchMaterialTime = researchMaterialTime.GetValueOrDefault(),
                ResearchCopyTime = researchCopyTime.GetValueOrDefault(),
                InventionTime = inventionTime.GetValueOrDefault(),
                ReverseEngineeringTime = reverseEngineeringTime.GetValueOrDefault(),
                ReactionTime = reactionTime.GetValueOrDefault(),
                MaxProductionLimit = blueprintType.MaxProductionLimit,
            };

            // Metagroup
            SetBlueprintMetaGroup(srcBlueprint, blueprint);

            // Export item requirements
            GetRequirements(srcBlueprint, blueprint);

            // Look for the tech 2 or tech 3 variations that this blueprint invents
            GetInventingItems(srcBlueprint, blueprint);

            // Look for reaction output
            GetReactionItems(srcBlueprint, blueprint);

            // Add this item
            blueprintsGroup.Add(blueprint);
        }

        /// <summary>
        /// Gets the reaction items.
        /// </summary>
        private static void GetReactionItems(InvTypes srcBlueprint, SerializableBlueprint blueprint)
        {
            if (Database.IndustryActivityTable.Contains(new IndustryActivity()
            {
                ActivityID = (int)BlueprintActivity.Reactions,
                BlueprintTypeID = srcBlueprint.ID
            }))
            {
                var outcome = Database.IndustryActivityProductsTable.SingleOrDefault(x =>
                 (x.ActivityID == (int)BlueprintActivity.Reactions || x.ActivityID == (int)BlueprintActivity.SimpleReactions)
                 && x.BlueprintTypeID == srcBlueprint.ID);

                if (outcome?.Quantity != null)
                    blueprint.ReactionOutcome = new SerializableMaterialQuantity()
                    {
                        ID = outcome.ProductTypeID,
                        Quantity = outcome.Quantity.Value
                    };
            }

            if (Database.IndustryActivityTable.Contains(new IndustryActivity()
            {
                ActivityID = (int)BlueprintActivity.SimpleReactions,
                BlueprintTypeID = srcBlueprint.ID
            }))
            {
                var outcome = Database.IndustryActivityProductsTable.SingleOrDefault(x =>
                 (x.ActivityID == (int)BlueprintActivity.Reactions || x.ActivityID == (int)BlueprintActivity.SimpleReactions)
                 && x.BlueprintTypeID == srcBlueprint.ID);

                if (outcome?.Quantity != null)
                    blueprint.ReactionOutcome = new SerializableMaterialQuantity()
                    {
                        ID = outcome.ProductTypeID,
                        Quantity = outcome.Quantity.Value
                    };
            }
        }

        /// <summary>
        /// Gets the inventing items.
        /// </summary>
        /// <param name="srcBlueprint">The source blueprint.</param>
        /// <param name="blueprint">The blueprint.</param>
        private static void GetInventingItems(InvTypes srcBlueprint, SerializableBlueprint blueprint)
        {
            foreach (var requirement in Database.IndustryActivityProbabilitiesTable.Where(x =>
                 x.BlueprintTypeID == srcBlueprint.ID &&
                 Database.IndustryBlueprintsTable.HasValue(x.ProductTypeID) &&
                 (x.ActivityID == (int)BlueprintActivity.Invention ||
                 x.ActivityID == (int)BlueprintActivity.ReverseEngineering)))
            {
                blueprint.InventionTypeIDs.Add(requirement.ProductTypeID, requirement.Probability.GetValueOrDefault());
            }
        }

        /// <summary>
        /// Sets the blueprint meta group.
        /// </summary>
        /// <param name="srcBlueprint">The SRC blueprint.</param>
        /// <param name="blueprint">The blueprint.</param>
        private static void SetBlueprintMetaGroup(InvTypes srcBlueprint, SerializableBlueprint blueprint)
        {
            var productTypeID = Database.IndustryActivityProductsTable.Where(
                x => x.BlueprintTypeID == srcBlueprint.ID &&
                x.ActivityID == (int)BlueprintActivity.Manufacturing).Select(
                    x => x.ProductTypeID).SingleOrDefault();

            foreach (InvMetaTypes relation in Database.InvMetaTypesTable.Where(
                x => x.ItemID == productTypeID))
            {
                switch (relation.MetaGroupID)
                {
                    default:
                        blueprint.MetaGroup = ItemMetaGroup.T1;
                        break;
                    case DBConstants.TechIIMetaGroupID:
                        blueprint.MetaGroup = ItemMetaGroup.T2;
                        break;
                    case DBConstants.StorylineMetaGroupID:
                        blueprint.MetaGroup = ItemMetaGroup.Storyline;
                        break;
                    case DBConstants.FactionMetaGroupID:
                        blueprint.MetaGroup = ItemMetaGroup.Faction;
                        break;
                    case DBConstants.OfficerMetaGroupID:
                        blueprint.MetaGroup = ItemMetaGroup.Officer;
                        break;
                    case DBConstants.DeadspaceMetaGroupID:
                        blueprint.MetaGroup = ItemMetaGroup.Deadspace;
                        break;
                    case DBConstants.TechIIIMetaGroupID:
                        blueprint.MetaGroup = ItemMetaGroup.T3;
                        break;
                }
            }

            // Metagroup for the custom market groups
            switch (srcBlueprint.MarketGroupID)
            {
                case DBConstants.BlueprintStorylineNonMarketGroupID:
                    blueprint.MetaGroup = ItemMetaGroup.Storyline;
                    break;
                case DBConstants.BlueprintFactionNonMarketGroupID:
                    blueprint.MetaGroup = ItemMetaGroup.Faction;
                    break;
                case DBConstants.BlueprintOfficerNonMarketGroupID:
                    blueprint.MetaGroup = ItemMetaGroup.Officer;
                    break;
                case DBConstants.BlueprintTechIIINonMarketGroupID:
                    blueprint.MetaGroup = ItemMetaGroup.T3;
                    break;
                case DBConstants.BlueprintTechIINonMarketGroupID:
                    blueprint.MetaGroup = ItemMetaGroup.T2;
                    break;
            }

            if (blueprint.MetaGroup == ItemMetaGroup.None)
                blueprint.MetaGroup = ItemMetaGroup.T1;
        }

        /// <summary>
        /// Get's the item requirements. 
        /// </summary>
        /// <param name="srcBlueprint"></param>
        /// <param name="blueprint"></param>
        private static void GetRequirements(IHasID srcBlueprint, SerializableBlueprint blueprint)
        {
            List<SerializablePrereqSkill> prerequisiteSkills = new List<SerializablePrereqSkill>();
            List<SerializableRequiredMaterial> requiredMaterials = new List<SerializableRequiredMaterial>();

            // Find required skills
            foreach (var requirement in Database.IndustryActivitySkillsTable
                .Where(x => x.BlueprintTypeID == srcBlueprint.ID))
            {
                if (requirement.Level.HasValue)
                {
                    prerequisiteSkills.Add(new SerializablePrereqSkill
                    {
                        ID = requirement.SkillID,
                        Level = requirement.Level.Value,
                        Activity = requirement.ActivityID
                    });
                }
            }

            // Find required materials
            foreach (var requirement in Database.IndustryActivityMaterialsTable
                .Where(x => x.BlueprintTypeID == srcBlueprint.ID))
            {
                if (requirement.Quantity.HasValue)
                {
                    requiredMaterials.Add(new SerializableRequiredMaterial
                    {
                        ID = requirement.MaterialTypeID,
                        Quantity = requirement.Quantity.Value,
                        Activity = requirement.ActivityID,
                    });
                }
            }

            // Add prerequisite skills to item
            blueprint.PrereqSkill.AddRange(prerequisiteSkills.OrderBy(x => x.Activity));

            // Add required materials to item
            blueprint.ReqMaterial.AddRange(requiredMaterials.OrderBy(x => x.Activity));
        }
    }
}
