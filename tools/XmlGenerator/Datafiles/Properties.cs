using EVEMon.Common.Collections;
using EVEMon.Common.Constants;
using EVEMon.Common.Serialization.Datafiles;
using EVEMon.XmlGenerator.Interfaces;
using EVEMon.XmlGenerator.Providers;
using EVEMon.XmlGenerator.StaticData;
using EVEMon.XmlGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace EVEMon.XmlGenerator.Datafiles
{
    internal static class Properties
    {
        private static List<EveUnits> s_injectedUnits;
        private static List<DgmAttributeTypes> s_injectedProperties;

        /// <summary>
        /// Gets the base price property ID.
        /// </summary>
        internal static int BasePricePropertyID { get; private set; }

        /// <summary>
        /// Gets the packaged volume property ID.
        /// </summary>
        internal static int PackagedVolumePropertyID { get; private set; }

        /// <summary>
        /// Gets the units to refine property ID.
        /// </summary>
        internal static int UnitsToRefinePropertyID { get; private set; }

        /// <summary>
        /// Generate the properties datafile.
        /// </summary>
        internal static void GenerateDatafile()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Util.ResetCounters();

            Console.WriteLine();
            Console.Write(@"Generating properties datafile... ");

            ConfigureNullCategoryProperties();

            IEnumerable<SerializablePropertyCategory> categories = ExportAttributeCategories();

            // Sort groups
            string[] orderedGroupNames =
            {
                DBConstants.GeneralCategoryName, "Fitting", "Drones", "Structure", "Hangers & Bays", "Armor", "Shield",
                "Capacitor", "Targeting", DBConstants.PropulsionCategoryName, "Turrets", "Missile", "Remote Assistance",
                "Fighter Attributes", "EW - Energy Neutralizing", "EW - Remote Electronic Counter Measures", "EW - Resistance",
                "EW - Sensor Dampening", "EW - Target Jamming", "EW - Target Painting", "EW - Tracking Disruption",
                "EW - Warp Scrambling", "EW - Webbing", "Loot", "Miscellaneous", "NULL", "AI", "Graphics"
            };

            // Serialize
            PropertiesDatafile datafile = new PropertiesDatafile();
            datafile.Categories.AddRange(categories.OrderBy(x => orderedGroupNames.IndexOf(x.Name)));

            Util.DisplayEndTime(stopwatch);

            Util.SerializeXml(datafile, DatafileConstants.PropertiesDatafile);
        }

        /// <summary>
        /// Configures the null category properties.
        /// </summary>
        private static void ConfigureNullCategoryProperties()
        {
            // Create EVEMon custom units
            int newUnitID = Database.EveUnitsTable.Last().ID;
            s_injectedUnits = new List<EveUnits>();

            EveUnits warpSpeedUnit = new EveUnits
            {
                ID = ++newUnitID,
                Name = "Warp Speed",
                DisplayName = "AU/s",
                Description = "Astronomical Unit per second."
            };
            s_injectedUnits.Add(warpSpeedUnit);

            EveUnits perHourUnit = new EveUnits
            {
                ID = ++newUnitID,
                Name = DBConstants.ConsumptionRatePropertyName,
                DisplayName = "per hour",
                Description = "Used to describe the consumption rate."
            };
            s_injectedUnits.Add(perHourUnit);

            // TODO: New unit added in 20.02, remove when SDE conversion is updated
            EveUnits modifierRealPercentUnit = new EveUnits
            {
                ID = 205,
                Name = "modifier realPercent",
                DisplayName = "%",
                Description = "Used for multipliers displayed as % 10 is +10% -10 is -10% 3.6 is +3.6%"
            };
            s_injectedUnits.Add(modifierRealPercentUnit);

            // Create EVEMon custom properties
            int newPropID = Database.DgmAttributeTypesTable.Last().ID;
            PackagedVolumePropertyID = ++newPropID;
            UnitsToRefinePropertyID = ++newPropID;
            BasePricePropertyID = ++newPropID;

            s_injectedProperties = new List<DgmAttributeTypes>
            {
                new DgmAttributeTypes
                {
                    ID = PackagedVolumePropertyID,
                    Name = "packagedVolume",
                    Description = "The packaged volume of a ship.",
                    IconID = 67,
                    DefaultValue = "0",
                    Published = true,
                    DisplayName = DBConstants.PackagedVolumePropertyName,
                    UnitID = 9,
                    HigherIsBetter = true,
                    CategoryID = 4
                },
                new DgmAttributeTypes
                {
                    ID = UnitsToRefinePropertyID,
                    Name = "unitToRefine",
                    Description = "The units required to perform the refining process.",
                    IconID = 0,
                    DefaultValue = "0",
                    Published = true,
                    DisplayName = "Units to Refine",
                    UnitID = null,
                    HigherIsBetter = false,
                    CategoryID = 7
                },
                new DgmAttributeTypes
                {
                    ID = BasePricePropertyID,
                    Name = "basePrice",
                    Description = "The price from NPC vendors (does not mean there is any).",
                    IconID = 67,
                    DefaultValue = "0",
                    Published = true,
                    DisplayName = "Base Price",
                    UnitID = 133,
                    HigherIsBetter = true,
                    CategoryID = 7
                },
                new DgmAttributeTypes
                {
                    ID = ++newPropID,
                    Name = "consumptionRate",
                    Description =
                        "The rate of the given resource type needed to be consumed for each activation cycle of this structure.",
                    IconID = 0,
                    DefaultValue = "0",
                    Published = true,
                    DisplayName = DBConstants.ConsumptionRatePropertyName,
                    UnitID = perHourUnit.ID,
                    HigherIsBetter = false,
                    CategoryID = 7
                }
            };

            // Set attributes with CategoryID 'NULL" to NULL category
            foreach (DgmAttributeTypes attribute in Database.DgmAttributeTypesTable.Where(x => x.CategoryID == null))
            {
                attribute.CategoryID = attribute.Published
                    ? DBConstants.MiscellaneousAttributeCategoryID
                    : DBConstants.NullAtributeCategoryID;
            }

            // Assign injected properties units
            Database.DgmAttributeTypesTable[DBConstants.ShipWarpSpeedPropertyID].UnitID = warpSpeedUnit.ID;

            // Change some display names and default values
            Database.DgmAttributeCategoriesTable[DBConstants.SpeedAtributeCategoryID].Name = DBConstants.PropulsionCategoryName;

            Database.DgmAttributeTypesTable[DBConstants.StructureHitpointsPropertyID].DisplayName = "Structure HP";
            Database.DgmAttributeTypesTable[DBConstants.ShieldHitpointsPropertyID].DisplayName = "Shield HP";
            Database.DgmAttributeTypesTable[DBConstants.ArmorHitpointsPropertyID].DisplayName = "Armor HP";
            Database.DgmAttributeTypesTable[DBConstants.CargoCapacityPropertyID].DisplayName = "Cargo Capacity";
            Database.DgmAttributeTypesTable[DBConstants.CPUOutputPropertyID].DisplayName = "CPU";
            Database.DgmAttributeTypesTable[DBConstants.PGOutputPropertyID].DisplayName = "Powergrid";

            // Shield
            Database.DgmAttributeTypesTable[DBConstants.ShieldEMResistancePropertyID].DisplayName = "EM Resistance";
            Database.DgmAttributeTypesTable[DBConstants.ShieldExplosiveResistancePropertyID].DisplayName = "Explosive Resistance";
            Database.DgmAttributeTypesTable[DBConstants.ShieldKineticResistancePropertyID].DisplayName = "Kinetic Resistance";
            Database.DgmAttributeTypesTable[DBConstants.ShieldThermalResistancePropertyID].DisplayName = "Thermal Resistance";

            // Armor
            Database.DgmAttributeTypesTable[DBConstants.ArmorEMResistancePropertyID].DisplayName = "EM Resistance";
            Database.DgmAttributeTypesTable[DBConstants.ArmorExplosiveResistancePropertyID].DisplayName = "Explosive Resistance";
            Database.DgmAttributeTypesTable[DBConstants.ArmorKineticResistancePropertyID].DisplayName = "Kinetic Resistance";
            Database.DgmAttributeTypesTable[DBConstants.ArmorThermalResistancePropertyID].DisplayName = "Thermal Resistance";

            // Hull
            Database.DgmAttributeTypesTable[DBConstants.HullEMResistancePropertyID].DisplayName = "EM Resistance";
            Database.DgmAttributeTypesTable[DBConstants.HullExplosiveResistancePropertyID].DisplayName = "Explosive Resistance";
            Database.DgmAttributeTypesTable[DBConstants.HullKineticResistancePropertyID].DisplayName = "Kinetic Resistance";
            Database.DgmAttributeTypesTable[DBConstants.HullThermalResistancePropertyID].DisplayName = "Thermal Resistance";

            // Items attribute
            Database.DgmAttributeTypesTable[DBConstants.CPUNeedPropertyID].DisplayName = "CPU Usage";
            Database.DgmAttributeTypesTable[DBConstants.ShieldTransferRangePropertyID].DisplayName = "Shield Transfer Range";
            Database.DgmAttributeTypesTable[DBConstants.CPUOutputBonusPropertyID].DisplayName = "CPU Output Bonus";
            Database.DgmAttributeTypesTable[DBConstants.CPUPenaltyPercentPropertyID].DisplayName = "CPU Penalty";

            // Changing the categoryID for some attributes 
            Database.DgmAttributeTypesTable[DBConstants.UpgradeCapacityPropertyID].CategoryID =
                DBConstants.FittingAtributeCategoryID;
            Database.DgmAttributeTypesTable[DBConstants.RigSizePropertyID].CategoryID = DBConstants.FittingAtributeCategoryID;
            Database.DgmAttributeTypesTable[DBConstants.MaxSubSystemsPropertyID].CategoryID =
                DBConstants.FittingAtributeCategoryID;
            Database.DgmAttributeTypesTable[DBConstants.FitsToShipTypePropertyID].CategoryID =
                DBConstants.FittingAtributeCategoryID;
            Database.DgmAttributeTypesTable[DBConstants.ShipMaintenanceBayCapacityPropertyID].CategoryID =
                DBConstants.StructureAtributeCategoryID;
            Database.DgmAttributeTypesTable[DBConstants.TurretHardPointModifierPropertyID].CategoryID =
                DBConstants.StructureAtributeCategoryID;
            Database.DgmAttributeTypesTable[DBConstants.LauncherHardPointModifierPropertyID].CategoryID =
                DBConstants.StructureAtributeCategoryID;
            Database.DgmAttributeTypesTable[DBConstants.HiSlotModifierPropertyID].CategoryID =
                DBConstants.StructureAtributeCategoryID;
            Database.DgmAttributeTypesTable[DBConstants.MedSlotModifierPropertyID].CategoryID =
                DBConstants.StructureAtributeCategoryID;
            Database.DgmAttributeTypesTable[DBConstants.LowSlotModifierPropertyID].CategoryID =
                DBConstants.StructureAtributeCategoryID;
            Database.DgmAttributeTypesTable[DBConstants.ShipWarpSpeedPropertyID].CategoryID =
                DBConstants.SpeedAtributeCategoryID;

            // Changing HigherIsBetter to false (CCP has this wrong?)
            Database.DgmAttributeTypesTable[DBConstants.PGNeedPropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.CPUNeedPropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.VolumePropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.AgilityPropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.MassPropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.CapacitorNeedPropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.CapacitorRechargeRatePropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.CapacitorRechargeRateMultiplierPropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.ShieldRechargeRatePropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.SignatureRadiusPropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.CloakingTargetingDelayPropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.CPUPenaltyPercentPropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.UpgradeCostPropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.DroneBandwidthUsedPropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.AITargetSwitchTimerPropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.AnchoringDelayPropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.UnanchoringDelayPropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.OnliningDelayPropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.IceHarvestCycleBonusPropertyID].HigherIsBetter = false;
            Database.DgmAttributeTypesTable[DBConstants.ModuleReactivationDelayPropertyID].HigherIsBetter = false;

            // Changing the categoryID for those attributes that their names do not start with a capital letter 
            foreach (DgmAttributeTypes attribute in Database.DgmAttributeTypesTable.Where(x => x.CategoryID != null)
                .Select(attribute =>
                    new
                    {
                        attribute,
                        name = string.IsNullOrEmpty(attribute.DisplayName) ? attribute.Name : attribute.DisplayName
                    })
                .Where(
                    att => !string.IsNullOrEmpty(att.name) &&
                        Regex.IsMatch(att.name.Substring(0, 1), "[a-z]", RegexOptions.Compiled | RegexOptions.CultureInvariant) &&
                        att.attribute.CategoryID != DBConstants.MiscellaneousAttributeCategoryID &&
                        att.attribute.CategoryID != DBConstants.NullAtributeCategoryID).Select(att => att.attribute))
            {
                attribute.CategoryID = DBConstants.NullAtributeCategoryID;
            }
        }

        /// <summary>
        /// Exports the attribute categories.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<SerializablePropertyCategory> ExportAttributeCategories()
        {
            List<SerializablePropertyCategory> categories = new List<SerializablePropertyCategory>();

            // Export attribute categories
            List<SerializableProperty> gProperties = new List<SerializableProperty>();
            foreach (DgmAttributeCategories srcCategory in Database.DgmAttributeCategoriesTable)
            {
                List<SerializableProperty> properties = new List<SerializableProperty>();
                SerializablePropertyCategory category = new SerializablePropertyCategory
                {
                    ID = srcCategory.ID,
                    Description = srcCategory.Description,
                    Name = srcCategory.Name
                };
                categories.Add(category);

                // Export attributes
                foreach (DgmAttributeTypes srcProp in Database.DgmAttributeTypesTable.Concat(s_injectedProperties).Where(
                    x => x.CategoryID == category.ID))
                {
                    Util.UpdatePercentDone(Database.PropertiesTotalCount);

                    SerializableProperty prop = new SerializableProperty();
                    properties.Add(prop);

                    prop.ID = srcProp.ID;
                    prop.DefaultValue = srcProp.DefaultValue;
                    prop.Description = srcProp.Description;
                    prop.HigherIsBetter = srcProp.HigherIsBetter;
                    prop.Name = !string.IsNullOrEmpty(srcProp.DisplayName)
                        ? srcProp.DisplayName
                        : !string.IsNullOrEmpty(srcProp.Name)
                            ? srcProp.Name
                            : string.Empty;

                    // Unit
                    prop.UnitID = srcProp.UnitID.GetValueOrDefault();
                    prop.Unit = srcProp.UnitID.HasValue
                        ? Database.EveUnitsTable.Concat(s_injectedUnits).First(
                            x => x.ID == srcProp.UnitID.Value).DisplayName
                        : string.Empty;

                    // Icon
                    prop.Icon = srcProp.IconID.HasValue ? Database.EveIconsTable[srcProp.IconID.Value].Icon : string.Empty;

                    // Reordering some properties
                    ReorderProperties(gProperties, prop, srcProp, properties);
                }

                category.Properties.AddRange(properties);
            }

            // New category ID
            int newCategoryID = Database.DgmAttributeCategoriesTable.Last().ID;

            // We insert custom categories
            SerializablePropertyCategory general = new SerializablePropertyCategory
            {
                ID = ++newCategoryID,
                Name = DBConstants.GeneralCategoryName,
                Description = "General information"
            };
            general.Properties.AddRange(gProperties);
            categories.Insert(0, general);

            return categories;
        }

        /// <summary>
        /// Reorders the properties.
        /// </summary>
        /// <param name="gProperties">The properties in general category.</param>
        /// <param name="prop">The prop.</param>
        /// <param name="srcProp">The source prop.</param>
        /// <param name="properties">The properties.</param>
        private static void ReorderProperties(IList<SerializableProperty> gProperties,
            SerializableProperty prop, IHasID srcProp, IList<SerializableProperty> properties)
        {
            int index = properties.IndexOf(prop);

            if (srcProp.ID == PackagedVolumePropertyID)
            {
                properties.Insert(4, prop);
                properties.RemoveAt(index + 1);
                return;
            }

            if (srcProp.ID == BasePricePropertyID)
            {
                gProperties.Insert(0, prop);
                properties.RemoveAt(index);
                return;
            }

            switch (srcProp.ID)
            {
                case DBConstants.StructureHitpointsPropertyID:
                    properties.Insert(0, prop);
                    properties.RemoveAt(index + 1);
                    break;
                case DBConstants.CargoCapacityPropertyID:
                    properties.Insert(1, prop);
                    properties.RemoveAt(index + 1);
                    break;
                case DBConstants.CPUOutputPropertyID:
                    properties.Insert(0, prop);
                    properties.RemoveAt(index + 1);
                    break;
                case DBConstants.VolumePropertyID:
                    properties.Insert(3, prop);
                    properties.RemoveAt(index + 1);
                    break;
                case DBConstants.TechLevelPropertyID:
                    gProperties.Insert(0, prop);
                    properties.RemoveAt(index);
                    break;
                case DBConstants.ShieldRechargeRatePropertyID:
                    properties.Insert(6, prop);
                    properties.RemoveAt(index + 1);
                    break;
                case DBConstants.CapacitorCapacityPropertyID:
                    properties.Insert(0, prop);
                    properties.RemoveAt(index + 1);
                    break;
                case DBConstants.ScanResolutionPropertyID:
                    properties.Insert(4, prop);
                    properties.RemoveAt(index + 1);
                    break;
                case DBConstants.MetaLevelPropertyID:
                    gProperties.Insert(1, prop);
                    properties.RemoveAt(index);
                    break;
                case DBConstants.HullEMResistancePropertyID:
                    properties.Insert(5, prop);
                    properties.RemoveAt(index + 1);
                    break;
                case DBConstants.HullExplosiveResistancePropertyID:
                    properties.Insert(6, prop);
                    properties.RemoveAt(index + 1);
                    break;
                case DBConstants.HullKineticResistancePropertyID:
                    properties.Insert(7, prop);
                    properties.RemoveAt(index + 1);
                    break;
                case DBConstants.HullThermalResistancePropertyID:
                    properties.Insert(8, prop);
                    properties.RemoveAt(index + 1);
                    break;
                case DBConstants.UpgradeCapacityPropertyID:
                    properties.Insert(2, prop);
                    properties.RemoveAt(index + 1);
                    break;
                case DBConstants.RigSlotsPropertyID:
                    properties.Insert(10, prop);
                    properties.RemoveAt(index + 1);
                    break;
                case DBConstants.RigSizePropertyID:
                    properties.Insert(11, prop);
                    properties.RemoveAt(index + 1);
                    break;
            }
        }
    }
}
