using EVEMon.Common.Collections;
using EVEMon.Common.Constants;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Serialization.Datafiles;
using EVEMon.XmlGenerator.Extensions;
using EVEMon.XmlGenerator.Interfaces;
using EVEMon.XmlGenerator.Providers;
using EVEMon.XmlGenerator.StaticData;
using EVEMon.XmlGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace EVEMon.XmlGenerator.Datafiles
{
    internal static class Items
    {
        private static List<InvMarketGroups> s_injectedMarketGroups;
        private static List<InvTypes> s_nullMarketItems;

        private static IDictionary<int, IGrouping<int, DgmTypeAttributes>> s_dgmTypeAttributesPerItem;
        private static IDictionary<int, IGrouping<int, InvTypes>> s_invTypesPerMarketGroup;
        private static ISet<int> s_factionMarketGroups;

        /// <summary>
        /// Generate the items datafile.
        /// </summary>
        internal static void GenerateDatafile()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Util.ResetCounters();

            Console.WriteLine();
            Console.Write(@"Generating items datafile... ");

            // Move non existing makret group to custom market group
            ConfigureNonExistingMarketGroupItems();

            // Create custom market groups that do not exist in EVE
            ConfigureNullMarketItems();

            // Prepare lookup tables (after configuring the non-existent/null market group items)
            s_invTypesPerMarketGroup = Database.InvTypesTable.GroupBy(x => x.MarketGroupID.
                GetValueOrDefault()).ToDictionary(x => x.Key);
            s_dgmTypeAttributesPerItem = Database.DgmTypeAttributesTable.GroupBy(x => x.ItemID).
                ToDictionary(x => x.Key);
            var factionGroups = DBConstants.FactionMarketGroupIDs;
            s_factionMarketGroups = new HashSet<int>(Database.InvMarketGroupsTable.Where(
                group => factionGroups.Contains(group.ID) || (group.ParentID.HasValue &&
                factionGroups.Contains(group.ParentID.Value))).Select(x => x.ID));

            // Create the market groups
            var groups = new Dictionary<int, SerializableMarketGroup>();
            CreateMarketGroups(groups);

            // Create the parent-children groups relations
            foreach (SerializableMarketGroup group in groups.Values)
            {
                var children = Database.InvMarketGroupsTable.Concat(s_injectedMarketGroups).
                    Where(x => x.ParentID.GetValueOrDefault() == group.ID).Select(x =>
                    groups[x.ID]).OrderBy(x => x.Name);

                group.SubGroups.AddRange(children);
            }

            // Pick the family
            SetItemFamilyByMarketGroup(groups[DBConstants.BlueprintsMarketGroupID],
                ItemFamily.Blueprint);
            SetItemFamilyByMarketGroup(groups[DBConstants.ShipsMarketGroupID],
                ItemFamily.Ship);
            SetItemFamilyByMarketGroup(groups[DBConstants.ImplantsMarketGroupID],
                ItemFamily.Implant);
            SetItemFamilyByMarketGroup(groups[DBConstants.DronesMarketGroupID],
                ItemFamily.Drone);
            SetItemFamilyByMarketGroup(groups[DBConstants.StarbaseStructuresMarketGroupID],
                ItemFamily.StarbaseStructure);

            // Sort groups
            var rootGroups = Database.InvMarketGroupsTable.Concat(s_injectedMarketGroups).Where(
                x => !x.ParentID.HasValue).Select(x => groups[x.ID]).OrderBy(x => x.Name);

            // Reset the custom market groups
            s_nullMarketItems.ForEach(srcItem => srcItem.MarketGroupID = null);

            // Serialize
            ItemsDatafile datafile = new ItemsDatafile();
            datafile.MarketGroups.AddRange(rootGroups);

            Util.DisplayEndTime(stopwatch);

            // DEBUG: Find which items have not been generated
            if (Debugger.IsAttached)
            {
                var itemids = groups.Values.SelectMany(x => x.Items).Select(y => y.ID).ToList();
                var diff = Database.InvTypesTable.Where(item => !itemids.Contains(item.ID)).ToList();

                if (diff.Any())
                    Console.WriteLine("{0} items were not generated.", diff.Count);
            }

            Util.SerializeXml(datafile, DatafileConstants.ItemsDatafile);
        }

        /// <summary>
        /// Creates the market groups.
        /// </summary>
        /// <param name="groups">The groups.</param>
        private static void CreateMarketGroups(IDictionary<int, SerializableMarketGroup> groups)
        {
            foreach (InvMarketGroups marketGroup in Database.InvMarketGroupsTable.Concat(
                s_injectedMarketGroups))
            {
                var group = new SerializableMarketGroup
                {
                    ID = marketGroup.ID,
                    Name = marketGroup.Name
                };
                groups[marketGroup.ID] = group;

                // Add the items in this group; excluding implants which are added below
                var items = new List<SerializableItem>();
                if (s_invTypesPerMarketGroup.ContainsKey(marketGroup.ID))
                {
                    bool validGroup = marketGroup.ParentID != DBConstants.RootNonMarketGroupID;
                    foreach (var srcItem in s_invTypesPerMarketGroup[marketGroup.ID])
                    {
                        if (!srcItem.Generated && (validGroup || Database.InvGroupsTable[
                            srcItem.GroupID].CategoryID != DBConstants.ImplantCategoryID ||
                            srcItem.GroupID == DBConstants.CyberLearningImplantsGroupID))
                            CreateItem(srcItem, items);
                    }
                }

                // Store the items
                group.Items.AddRange(items.OrderBy(x => x.Name));
            }
        }

        /// <summary>
        /// Configures the null market items.
        /// </summary>
        private static void ConfigureNullMarketItems()
        {
            s_injectedMarketGroups = new List<InvMarketGroups>
            {
                new InvMarketGroups
                {
                    Name = "Unique Designs",
                    Description = "Ships of a unique design",
                    ID = DBConstants.UniqueDesignsRootNonMarketGroupID,
                    ParentID = DBConstants.ShipsMarketGroupID,
                    IconID = DBConstants.UnknownShipIconID
                },
                new InvMarketGroups
                {
                    Name = "Various Non-Market",
                    Description = "Non-Market Items",
                    ID = DBConstants.RootNonMarketGroupID,
                    ParentID = null,
                    IconID = DBConstants.UnknownIconID
                }
            };

            // Add all items with null market group
            s_nullMarketItems = Database.InvTypesTable.Where(x => x.MarketGroupID == null).ToList();

            // Set some attributes to items because their MarketGroupID is NULL
            foreach (InvTypes srcItem in s_nullMarketItems)
            {
                // Set all items to market groups manually
                srcItem.MarketGroupID = DBConstants.RootNonMarketGroupID;

                // Set some ships market group and race
                switch (srcItem.ID)
                {
                    case DBConstants.CapsuleID:
                        srcItem.MarketGroupID = DBConstants.UniqueDesignsRootNonMarketGroupID;
                        srcItem.RaceID = (int)Race.All;
                        break;
                }
            }
        }

        /// <summary>
        /// Configures the non existing market group items.
        /// </summary>
        private static void ConfigureNonExistingMarketGroupItems()
        {
            var items = Database.InvTypesTable.Where(x => x.MarketGroupID != null && Database.
                InvMarketGroupsTable.All(y => y.ID != x.MarketGroupID)).ToList();

            foreach (var item in items)
            {
                item.MarketGroupID = DBConstants.RootNonMarketGroupID;
            }
        }

        /// <summary>
        /// Add properties to an item.
        /// </summary>
        /// <param name="srcItem"></param>
        /// <param name="groupItems"></param>
        /// <returns></returns>
        private static void CreateItem(InvTypes srcItem, ICollection<SerializableItem> groupItems)
        {
            Util.UpdatePercentDone(Database.ItemsTotalCount);

            srcItem.Generated = true;

            InvGroups itemGroup = Database.InvGroupsTable[srcItem.GroupID];

            // Creates the item with base information
            var categories = Database.InvCategoriesTable;
            int cID = itemGroup.CategoryID;
            SerializableItem item = new SerializableItem
            {
                ID = srcItem.ID,
                Name = srcItem.Name,
                Description = srcItem.Description ?? string.Empty,
                Icon = srcItem.IconID.HasValue ? Database.EveIconsTable[srcItem.IconID.
                    Value].Icon : string.Empty,
                PortionSize = srcItem.PortionSize,
                MetaGroup = ItemMetaGroup.None,
                Group = itemGroup.Name,
                Category = categories.HasValue(cID) ? Database.InvCategoriesTable[cID].Name :
                    "",
                Race = (Race)Enum.ToObject(typeof(Race), srcItem.RaceID ?? 0)
            };

            // Set race to Faction if item race is Jovian or belongs to a Faction market group
            if (item.Race == Race.Jove || (srcItem.MarketGroupID.HasValue &&
                    s_factionMarketGroups.Contains(srcItem.MarketGroupID.Value)))
                item.Race = Race.Faction;

            // Add traits
            AddTraits(srcItem, item);

            // Add the properties and prereqs
            AddItemPropsAndPrereq(srcItem, item);

            // Metagroup
            AddMetaGroup(srcItem, item);

            // Look for slots
            var typeEffects = Database.DgmTypeEffectsTable;
            if (typeEffects.Contains(srcItem.ID, 0, DBConstants.LowSlotEffectID))
                item.Slot = ItemSlot.Low;
            else if (typeEffects.Contains(srcItem.ID, 0, DBConstants.MedSlotEffectID))
                item.Slot = ItemSlot.Medium;
            else if (typeEffects.Contains(srcItem.ID, 0, DBConstants.HiSlotEffectID))
                item.Slot = ItemSlot.High;
            else
                item.Slot = ItemSlot.NoSlot;

            // Add reaction info for reactions
            if (Database.InvGroupsTable[srcItem.GroupID].CategoryID == DBConstants.
                    ReactionCategoryID)
                AddReactionInfo(srcItem, item);

            // Add fuel info for control towers
            if (srcItem.GroupID == DBConstants.ControlTowerGroupID)
                AddControlTowerFuelInfo(srcItem, item);

            // Add this item
            groupItems.Add(item);

            // If the current item is not in a market group then we are done
            if (srcItem.MarketGroupID != DBConstants.RootNonMarketGroupID)
                // Look for variations which are not in any market group
                foreach (var variation in Database.InvMetaTypesTable)
                    if (variation.ParentItemID == srcItem.ID)
                    {
                        var variationItem = Database.InvTypesTable[variation.ItemID];
                        if (variationItem.Published && variationItem.MarketGroupID ==
                            DBConstants.RootNonMarketGroupID)
                        {
                            variationItem.RaceID = (int)Race.Faction;
                            CreateItem(variationItem, groupItems);
                        }
                    }
        }

        /// <summary>
        /// Appends formatted bonuses to the output text.
        /// </summary>
        /// <param name="bonuses">The list of bonuses to apply.</param>
        /// <param name="buffer">The location to place the text.</param>
        /// <returns>The number of bonuses added in this way.</returns>
        private static int AddBonuses(IEnumerable<InvTraits> bonuses, StringBuilder buffer)
        {
            int count = 0;
            foreach (InvTraits bonus in bonuses)
            {
                // 5
                if (bonus.bonus.HasValue)
                    buffer.Append(bonus.bonus);
                // %
                if (bonus.UnitID.HasValue)
                    buffer.Append(Database.EveUnitsTable[bonus.UnitID.Value].DisplayName).Append(' ');
                // bonus to Small Energy Turret damage
                buffer.AppendLine(bonus.BonusText);
                count++;
            }
            if (count > 0)
                buffer.AppendLine();
            return count;
        }

        /// <summary>
        /// Adds the traits.
        /// </summary>
        /// <param name="srcItem">The source item.</param>
        /// <param name="item">The item.</param>
        private static void AddTraits(InvTypes srcItem, SerializableItem item)
        {
            if (Database.InvGroupsTable[srcItem.GroupID].CategoryID != DBConstants.ShipCategoryID)
                return;

            var skillBonusesText = new StringBuilder(512);
            var roleBonusesText = new StringBuilder(512);
            var miscBonusesText = new StringBuilder(512);
            int numSkillBonuses = 0, numRoleBonuses, numMiscBonuses;

            // Group by the bonusing skill
            foreach (var bonuses in Database.InvTraitsTable.Where(x => x.typeID ==
                srcItem.ID && x.skillID > 0).GroupBy(x => x.skillID))
            {
                int skillID = bonuses.Key ?? 0;
                skillBonusesText.Append(Database.InvTypesTable[skillID].Name);
                skillBonusesText.AppendLine(" bonuses (per skill level):");

                numSkillBonuses += AddBonuses(bonuses, skillBonusesText);
            }
            skillBonusesText.AppendLine();

            // Find the role bonuses
            var RoleBonuses = Database.InvTraitsTable.Where(x => x.typeID == srcItem.ID &&
                x.skillID == -1);
            roleBonusesText.AppendLine("Role bonus:");
            numRoleBonuses = AddBonuses(RoleBonuses, roleBonusesText);

            // Find the misc bonuses
            var MiscBonuses = Database.InvTraitsTable.Where(x => x.typeID == srcItem.ID &&
                x.skillID == -2);
            miscBonusesText.AppendLine("Misc bonus:");
            numMiscBonuses = AddBonuses(MiscBonuses, miscBonusesText);

            // For any T3 destroyer, we need to deal with CCP being horrific cheats. The 'ship
            // traits' are actually derived through some epic hacking from some hidden items.
            // Hard coding some things in the short term, but need to make this MOAR BETTER.
#if false
            List<long> T3DIDs = new List<long> { 34562, 35683, 34317, 34828 };
			if (T3DIDs.Contains(item.ID))
			{
				Dictionary<string, int> T3DModeInfo = new Dictionary<string, int>();
				T3DModeInfo.Add("Sharpshooter", 0);
				T3DModeInfo.Add("Defense", 0);
				T3DModeInfo.Add("Propulsion", 0);

				// Determine which T3D we have, and get the relevant sub-item IDs
				switch (item.ID)
				{
					case 34562:
						// Svipul
						T3DModeInfo["Sharpshooter"] = 34570;
						T3DModeInfo["Propulsion"] = 34566;
						T3DModeInfo["Defense"] = 34564;
						break;
					case 35683:
						// Hecate
						T3DModeInfo["Sharpshooter"] = 35688;
						T3DModeInfo["Propulsion"] = 35687;
						T3DModeInfo["Defense"] = 35686;
						break;
					case 34317:
						// Confessor
						T3DModeInfo["Sharpshooter"] = 34321;
						T3DModeInfo["Propulsion"] = 34323;
						T3DModeInfo["Defense"] = 34319;
						break;
					case 34828:
						// Jackdaw
						T3DModeInfo["Sharpshooter"] = 35678;
						T3DModeInfo["Propulsion"] = 35677;
						T3DModeInfo["Defense"] = 35676;
						break;
					default:
						break;
				}

                foreach (var T3DMode in T3DModeInfo)
                {
                    int id = T3DMode.Value;
                    if (id > 0)
                    {
                        var DBRecord = Database.InvTypesTable[id];
                        miscBonusesText.Append(T3DMode.Key).AppendLine(" Mode:");
                        miscBonusesText.AppendLine(DBRecord.Description).AppendLine();
                    }
				}
			}
#endif
            // Skip if no bonuses
            if (numSkillBonuses > 0 || numRoleBonuses > 0 || numMiscBonuses > 0)
            {
                var sb = new StringBuilder(1024);
                sb.AppendLine().AppendLine().AppendLine("--- Traits ---");
                if (numSkillBonuses > 0)
                    sb.Append(skillBonusesText.ToString());
                if (numRoleBonuses > 0)
                    sb.Append(roleBonusesText.ToString());
                if (numMiscBonuses > 0)
                    sb.Append(miscBonusesText.ToString());

                // Add to item description
                item.Description += sb.ToString();
            }
        }

        /// <summary>
        /// Adds the control tower fuel info.
        /// </summary>
        /// <param name="srcItem">The source item.</param>
        /// <param name="item">The item.</param>
        private static void AddControlTowerFuelInfo(IHasID srcItem, SerializableItem item)
        {
            var controlTowerResourcesTable = Database.InvControlTowerResourcesTable.Join(
                Database.InvControlTowerResourcePurposesTable, ctr => ctr.PurposeID,
                ctrp => ctrp.ID, (ctr, ctrp) => new { ctr, ctrp }).Where(x =>
                x.ctr.ID == srcItem.ID).Select(resource => new SerializableControlTowerFuel
                {
                    ID = resource.ctr.ResourceID,
                    Purpose = resource.ctrp.PurposeName,
                    Quantity = resource.ctr.Quantity,
                    MinSecurityLevel = resource.ctr.MinSecurityLevel.HasValue ? resource.ctr.
                        MinSecurityLevel.Value.ToString(CultureInfo.InvariantCulture) : string.Empty,
                    FactionID = resource.ctr.FactionID.HasValue ? resource.ctr.FactionID.Value.
                        ToString(CultureInfo.InvariantCulture) : string.Empty,
                    FactionName = resource.ctr.FactionID.HasValue ? Database.ChrFactionsTable[
                        resource.ctr.FactionID.Value].FactionName : string.Empty
                });

            item.ControlTowerFuelInfo.AddRange(controlTowerResourcesTable);
        }

        /// <summary>
        /// Adds the reaction info.
        /// </summary>
        /// <param name="srcItem">The source item.</param>
        /// <param name="item">The item.</param>
        private static void AddReactionInfo(IHasID srcItem, SerializableItem item)
        {
            foreach (var srcReaction in Database.InvTypeReactionsTable)
                if (srcReaction.ID == srcItem.ID)
                {
                    int reactionTypeID = srcReaction.TypeID;
                    long multiplier = (Database.DgmTypeAttributesTable.FirstOrDefault(x =>
                        x.ItemID == reactionTypeID && x.AttributeID == DBConstants.
                        MoonMiningAmountPropertyID)?.GetInt64Value) ?? 1L, qty = srcReaction.
                        Quantity;
                    item.ReactionInfo.Add(new SerializableReactionInfo
                    {
                        ID = srcReaction.TypeID,
                        IsInput = srcReaction.Input,
                        Quantity = multiplier > 0L ? qty * multiplier : qty
                    });
                }
        }

        /// <summary>
        /// Adds the meta group.
        /// </summary>
        /// <param name="srcItem">The source item.</param>
        /// <param name="item">The serializable item.</param>
        private static void AddMetaGroup(IHasID srcItem, SerializableItem item)
        {
            foreach (InvMetaTypes relation in Database.InvMetaTypesTable.Where(x => x.ItemID == srcItem.ID))
                switch (relation.MetaGroupID)
                {
                    case DBConstants.TechIMetaGroupID:
                        item.MetaGroup = ItemMetaGroup.T1;
                        break;
                    case DBConstants.TechIIMetaGroupID:
                        item.MetaGroup = ItemMetaGroup.T2;
                        break;
                    case DBConstants.StorylineMetaGroupID:
                        item.MetaGroup = ItemMetaGroup.Storyline;
                        break;
                    case DBConstants.FactionMetaGroupID:
                        item.MetaGroup = ItemMetaGroup.Faction;
                        break;
                    case DBConstants.OfficerMetaGroupID:
                        item.MetaGroup = ItemMetaGroup.Officer;
                        break;
                    case DBConstants.DeadspaceMetaGroupID:
                        item.MetaGroup = ItemMetaGroup.Deadspace;
                        break;
                    case DBConstants.TechIIIMetaGroupID:
                        item.MetaGroup = ItemMetaGroup.T3;
                        break;
                    default:
                        item.MetaGroup = ItemMetaGroup.None;
                        break;
                }

            if (item.MetaGroup == ItemMetaGroup.None)
                item.MetaGroup = ItemMetaGroup.T1;
        }

        /// <summary>
        /// Adds a single item property to the list if it is found in the list of recognized
        /// property IDs.
        /// </summary>
        /// <param name="ids">The property IDs to search.</param>
        /// <param name="attrib">The attribute to add.</param>
        /// <param name="props">The property list to add the attribute, if found.</param>
        /// <returns>true if the attribute was found and added, or false otherwise.</returns>
        private static bool AddItemProp(IReadOnlyCollection<int> ids, DgmTypeAttributes attrib,
            IList<SerializablePropertyValue> props)
        {
            int id = attrib.AttributeID, index = ids.IndexOf(id);
            if (index >= 0)
            {
                // Index was found in the list of known IDs
                long propInt64Value = attrib.GetInt64Value;
                var invGroups = Database.InvGroupsTable;
                props.Add(new SerializablePropertyValue
                {
                    ID = id,
                    Value = invGroups.HasValue(propInt64Value) ? invGroups[propInt64Value].Name :
                        string.Empty
                });
            }
            return index > -1;
        }

        /// <summary>
        /// Adds the item properties and prerequisites.
        /// </summary>
        /// <param name="srcItem">The source item.</param>
        /// <param name="item">The serializable item.</param>
        /// <returns></returns>
        private static void AddItemPropsAndPrereq(InvTypes srcItem, SerializableItem item)
        {
            long[] prereqSkills = new long[DBConstants.RequiredSkillPropertyIDs.Count];
            long[] prereqLevels = new long[DBConstants.RequiredSkillPropertyIDs.Count];
            var props = new List<SerializablePropertyValue>();
            double warpSpeedMultiplier = 1.0;
            if (s_dgmTypeAttributesPerItem.ContainsKey(srcItem.ID))
                foreach (DgmTypeAttributes srcProp in s_dgmTypeAttributesPerItem[srcItem.ID])
                {
                    long propValue = srcProp.GetInt64Value;
                    int id = srcProp.AttributeID;
                    // Is it a prereq skill?
                    int prereqIndex = DBConstants.RequiredSkillPropertyIDs.IndexOf(id);
                    if (prereqIndex > -1)
                    {
                        prereqSkills[prereqIndex] = propValue;
                        continue;
                    }
                    // Is it a prereq level?
                    prereqIndex = DBConstants.RequiredSkillLevelPropertyIDs.IndexOf(id);
                    if (prereqIndex > -1)
                    {
                        prereqLevels[prereqIndex] = propValue;
                        continue;
                    }
                    // Recognized group? Includes Launcher, CanFitShip, ModuleShip,
                    // SpecialisationAsteroid, Reaction, and PosCargobayAccept
                    if (AddItemProp(DBConstants.LauncherGroupPropertyIDs, srcProp, props) ||
                        AddItemProp(DBConstants.ChargeGroupPropertyIDs, srcProp, props) ||
                        AddItemProp(DBConstants.CanFitShipGroupPropertyIDs, srcProp, props) ||
                        AddItemProp(DBConstants.ModuleShipGroupPropertyIDs, srcProp, props) ||
                        AddItemProp(DBConstants.SpecialisationAsteroidGroupPropertyIDs, srcProp, props) ||
                        AddItemProp(DBConstants.ReactionGroupPropertyIDs, srcProp, props) ||
                        AddItemProp(DBConstants.PosCargobayAcceptGroupPropertyIDs, srcProp, props))
                        continue;
                    // Get the warp speed multiplier
                    if (id == DBConstants.WarpSpeedMultiplierPropertyID && srcProp.ValueFloat != null)
                        warpSpeedMultiplier = srcProp.ValueFloat.Value;
                    // Calculate and add the ships warp speed
                    if (id == DBConstants.ShipWarpSpeedPropertyID)
                    {
                        props.Add(new SerializablePropertyValue
                        {
                            ID = id,
                            Value = warpSpeedMultiplier.ToString(CultureConstants.InvariantCulture)
                        });
                        // Also add packaged volume as a prop as only ships have 'ship warp speed' attribute
                        props.Add(new SerializablePropertyValue
                        {
                            ID = Properties.PackagedVolumePropertyID,
                            Value = GetPackagedVolume(srcItem.GroupID).ToString(CultureConstants.InvariantCulture)
                        });
                    }
                    // Other properties
                    props.Add(new SerializablePropertyValue { ID = id, Value = srcProp.FormatPropertyValue() });
                    AddMetaData(item, propValue, srcProp);
                }

            CompleteItemPropertiesAddition(srcItem, props);
            // Add properties info to item
            item.Properties.AddRange(props);

            // Prerequisites completion
            var prereqs = new List<SerializablePrerequisiteSkill>();
            for (int i = 0; i < prereqSkills.Length; i++)
            {
                if (prereqSkills[i] != 0)
                    prereqs.Add(new SerializablePrerequisiteSkill { ID = prereqSkills[i], Level = prereqLevels[i] });
            }

            // Add prerequisite skills info to item
            item.PrerequisiteSkills.AddRange(prereqs);
        }

        /// <summary>
        /// Adds the meta data.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="propInt64Value">The prop int value.</param>
        /// <param name="srcProp">The SRC prop.</param>
        private static void AddMetaData(SerializableItem item, long propInt64Value, DgmTypeAttributes srcProp)
        {
            // Is metalevel property ?
            switch (srcProp.AttributeID)
            {
                case DBConstants.MetaLevelPropertyID:
                    item.MetaLevel = propInt64Value;
                    break;
                // Is techlevel property ?
                case DBConstants.TechLevelPropertyID:
                    switch (propInt64Value)
                    {
                        case DBConstants.TechLevelII:
                            item.MetaGroup = ItemMetaGroup.T2;
                            break;
                        case DBConstants.TechLevelIII:
                            item.MetaGroup = ItemMetaGroup.T3;
                            break;
                        default:
                            item.MetaGroup = ItemMetaGroup.T1;
                            break;
                    }
                    break;
                // Is metagroup property ?
                case DBConstants.MetaGroupPropertyID:
                    switch (propInt64Value)
                    {
                        case DBConstants.StorylineMetaGroupID:
                            item.MetaGroup = ItemMetaGroup.Storyline;
                            break;
                        case DBConstants.FactionMetaGroupID:
                            item.MetaGroup = ItemMetaGroup.Faction;
                            break;
                        case DBConstants.OfficerMetaGroupID:
                            item.MetaGroup = ItemMetaGroup.Officer;
                            break;
                        case DBConstants.DeadspaceMetaGroupID:
                            item.MetaGroup = ItemMetaGroup.Deadspace;
                            break;
                        default:
                            item.MetaGroup = ItemMetaGroup.None;
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Completes the item properties addition.
        /// </summary>
        /// <param name="srcItem">The SRC item.</param>
        /// <param name="props">The props.</param>
        private static void CompleteItemPropertiesAddition(InvTypes srcItem, ICollection<SerializablePropertyValue> props)
        {
            // Ensures there is a mass and add it to prop
            if (Math.Abs(srcItem.Mass) > double.Epsilon)
                props.Add(new SerializablePropertyValue
                {
                    ID = DBConstants.MassPropertyID,
                    Value = srcItem.Mass.ToString(CultureConstants.InvariantCulture)
                });

            // Ensures there is a cargo capacity and add it to prop
            if (Math.Abs(srcItem.Capacity) > double.Epsilon)
                props.Add(new SerializablePropertyValue
                {
                    ID = DBConstants.CargoCapacityPropertyID,
                    Value = srcItem.Capacity.ToString(CultureConstants.InvariantCulture)
                });

            // Ensures there is a volume and add it to prop
            if (Math.Abs(srcItem.Volume) > double.Epsilon)
                props.Add(new SerializablePropertyValue
                {
                    ID = DBConstants.VolumePropertyID,
                    Value = srcItem.Volume.ToString(CultureConstants.InvariantCulture)
                });

            // Add unit to refine prop where applicable
            if (Database.InvGroupsTable[srcItem.GroupID].CategoryID == DBConstants.AsteroidCategoryID)
                props.Add(new SerializablePropertyValue
                {
                    ID = Properties.UnitsToRefinePropertyID,
                    Value = srcItem.PortionSize.ToString(CultureInfo.InvariantCulture)
                });

            // Add base price as a prop
            props.Add(new SerializablePropertyValue
            {
                ID = Properties.BasePricePropertyID,
                Value = srcItem.BasePrice.FormatDecimal()
            });
        }

        /// <summary>
        /// Gets the packaged volume of a ship.
        /// </summary>
        /// <param name="groupID">The group ID.</param>
        /// <returns></returns>
        private static int GetPackagedVolume(int groupID)
        {
            switch (groupID)
            {
                case DBConstants.FrigateGroupID:
                case DBConstants.RookieShipGroupID:
                case DBConstants.AssaultFrigateGroupID:
                case DBConstants.CovertOpsGroupID:
                case DBConstants.InterceptorGroupID:
                case DBConstants.StealthBomberGroupID:
                case DBConstants.ElectronicAttackShipGroupID:
                case DBConstants.ExpeditionFrigateGroupID:
                    return 2500;
                case DBConstants.MiningBargeGroupID:
                case DBConstants.ExhumerGroupID:
                    return 3750;
                case DBConstants.DestroyerGroupID:
                case DBConstants.InterdictorGroupID:
                case DBConstants.StrategicCruiserGroupID:
                case DBConstants.TacticalDestroyerGroupID:
                    return 5000;
                case DBConstants.CruiserGroupID:
                case DBConstants.HeavyAssaultCruiserGroupID:
                case DBConstants.LogisticsGroupID:
                case DBConstants.ForceReconShipGroupID:
                case DBConstants.HeavyInterdictorCruiserGroupID:
                case DBConstants.CombatReconShipGroupID:
                    return 10000;
                case DBConstants.CombatBattlecruiserGroupID:
                case DBConstants.CommandShipGroupID:
                case DBConstants.AttackBattlecruiserGroupID:
                    return 15000;
                case DBConstants.IndustrialGroupID:
                case DBConstants.DeepSpaceTransportGroupID:
                case DBConstants.BlockadeRunnerGroupID:
                    return 20000;
                case DBConstants.BattleshipGroupID:
                case DBConstants.EliteBattleshipGroupID:
                case DBConstants.BlackOpsGroupID:
                case DBConstants.MarauderGroupID:
                    return 50000;
                case DBConstants.IndustrialCommandShipGroupID:
                    return 500000;
                case DBConstants.DreadnoughtGroupID:
                case DBConstants.FreighterGroupID:
                case DBConstants.CarrierGroupID:
                case DBConstants.SupercarrierGroupID:
                case DBConstants.CapitalIndustrialShipGroupID:
                case DBConstants.JumpFreighterGroupID:
                    return 1000000;
                case DBConstants.TitanGroupID:
                    return 10000000;
                default:
                    return 500;
            }
        }

        /// <summary>
        /// Sets the item family according to its market group
        /// </summary>
        /// <param name="group"></param>
        /// <param name="itemFamily"></param>
        private static void SetItemFamilyByMarketGroup(SerializableMarketGroup group, ItemFamily itemFamily)
        {
            foreach (SerializableItem item in group.Items)
                item.Family = itemFamily;

            foreach (SerializableMarketGroup childGroup in group.SubGroups)
                SetItemFamilyByMarketGroup(childGroup, itemFamily);
        }
    }
}
