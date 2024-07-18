using Microsoft.EntityFrameworkCore;

namespace EVEMon.XmlGenerator.Models
{
    public partial class EveStaticData : DbContext
    {
        public EveStaticData()
            : base(new DbContextOptionsBuilder().UseSqlite("Data Source=..\\..\\..\\sqlite-latest.sqlite").Options)
        {
        }

        public virtual DbSet<agtAgents> agtAgents { get; set; }
        public virtual DbSet<agtAgentTypes> agtAgentTypes { get; set; }
        public virtual DbSet<agtResearchAgents> agtResearchAgents { get; set; }
        public virtual DbSet<chrAncestries> chrAncestries { get; set; }
        public virtual DbSet<chrAttributes> chrAttributes { get; set; }
        public virtual DbSet<chrBloodlines> chrBloodlines { get; set; }
        public virtual DbSet<chrFactions> chrFactions { get; set; }
        public virtual DbSet<chrRaces> chrRaces { get; set; }
        public virtual DbSet<crpActivities> crpActivities { get; set; }
        public virtual DbSet<crpNPCCorporationDivisions> crpNPCCorporationDivisions { get; set; }
        public virtual DbSet<crpNPCCorporationResearchFields> crpNPCCorporationResearchFields { get; set; }
        public virtual DbSet<crpNPCCorporations> crpNPCCorporations { get; set; }
        public virtual DbSet<crpNPCCorporationTrades> crpNPCCorporationTrades { get; set; }
        public virtual DbSet<crpNPCDivisions> crpNPCDivisions { get; set; }
        public virtual DbSet<dgmAttributeCategories> dgmAttributeCategories { get; set; }
        public virtual DbSet<dgmAttributeTypes> dgmAttributeTypes { get; set; }
        public virtual DbSet<dgmEffects> dgmEffects { get; set; }
        public virtual DbSet<dgmExpressions> dgmExpressions { get; set; }
        public virtual DbSet<dgmTypeAttributes> dgmTypeAttributes { get; set; }
        public virtual DbSet<dgmTypeEffects> dgmTypeEffects { get; set; }
        public virtual DbSet<eveGraphics> eveGraphics { get; set; }
        public virtual DbSet<eveIcons> eveIcons { get; set; }
        public virtual DbSet<eveUnits> eveUnits { get; set; }
        public virtual DbSet<industryActivity> industryActivity { get; set; }
        public virtual DbSet<industryActivityMaterials> industryActivityMaterials { get; set; }
        public virtual DbSet<industryActivityProbabilities> industryActivityProbabilities { get; set; }
        public virtual DbSet<industryActivityProducts> industryActivityProducts { get; set; }
        public virtual DbSet<industryActivitySkills> industryActivitySkills { get; set; }
        public virtual DbSet<industryBlueprints> industryBlueprints { get; set; }
        public virtual DbSet<invCategories> invCategories { get; set; }
        public virtual DbSet<invContrabandTypes> invContrabandTypes { get; set; }
        public virtual DbSet<invControlTowerResourcePurposes> invControlTowerResourcePurposes { get; set; }
        public virtual DbSet<invControlTowerResources> invControlTowerResources { get; set; }
        public virtual DbSet<invFlags> invFlags { get; set; }
        public virtual DbSet<invGroups> invGroups { get; set; }
        public virtual DbSet<invItems> invItems { get; set; }
        public virtual DbSet<invMarketGroups> invMarketGroups { get; set; }
        public virtual DbSet<invMetaGroups> invMetaGroups { get; set; }
        public virtual DbSet<invMetaTypes> invMetaTypes { get; set; }
        public virtual DbSet<invNames> invNames { get; set; }
        public virtual DbSet<invPositions> invPositions { get; set; }
        public virtual DbSet<invTraits> invTraits { get; set; }
        public virtual DbSet<invTypeMaterials> invTypeMaterials { get; set; }
        public virtual DbSet<invTypeReactions> invTypeReactions { get; set; }
        public virtual DbSet<invTypes> invTypes { get; set; }
        public virtual DbSet<invUniqueNames> invUniqueNames { get; set; }
        public virtual DbSet<mapCelestialStatistics> mapCelestialStatistics { get; set; }
        public virtual DbSet<mapConstellationJumps> mapConstellationJumps { get; set; }
        public virtual DbSet<mapConstellations> mapConstellations { get; set; }
        public virtual DbSet<mapDenormalize> mapDenormalize { get; set; }
        public virtual DbSet<mapJumps> mapJumps { get; set; }
        public virtual DbSet<mapLandmarks> mapLandmarks { get; set; }
        public virtual DbSet<mapLocationScenes> mapLocationScenes { get; set; }
        public virtual DbSet<mapLocationWormholeClasses> mapLocationWormholeClasses { get; set; }
        public virtual DbSet<mapRegionJumps> mapRegionJumps { get; set; }
        public virtual DbSet<mapRegions> mapRegions { get; set; }
        public virtual DbSet<mapSolarSystemJumps> mapSolarSystemJumps { get; set; }
        public virtual DbSet<mapSolarSystems> mapSolarSystems { get; set; }
        public virtual DbSet<mapUniverse> mapUniverse { get; set; }
        public virtual DbSet<planetSchematics> planetSchematics { get; set; }
        public virtual DbSet<planetSchematicsPinMap> planetSchematicsPinMap { get; set; }
        public virtual DbSet<planetSchematicsTypeMap> planetSchematicsTypeMap { get; set; }
        public virtual DbSet<ramActivities> ramActivities { get; set; }
        public virtual DbSet<ramAssemblyLineStations> ramAssemblyLineStations { get; set; }
        public virtual DbSet<ramAssemblyLineTypeDetailPerCategory> ramAssemblyLineTypeDetailPerCategory { get; set; }
        public virtual DbSet<ramAssemblyLineTypeDetailPerGroup> ramAssemblyLineTypeDetailPerGroup { get; set; }
        public virtual DbSet<ramAssemblyLineTypes> ramAssemblyLineTypes { get; set; }
        public virtual DbSet<ramInstallationTypeContents> ramInstallationTypeContents { get; set; }
        public virtual DbSet<sknLicenses> sknLicenses { get; set; }
        public virtual DbSet<sknMaterials> sknMaterials { get; set; }
        public virtual DbSet<sknSkins> sknSkins { get; set; }
        public virtual DbSet<staOperations> staOperations { get; set; }
        public virtual DbSet<staOperationServices> staOperationServices { get; set; }
        public virtual DbSet<staServices> staServices { get; set; }
        public virtual DbSet<staStations> staStations { get; set; }
        public virtual DbSet<staStationTypes> staStationTypes { get; set; }
        public virtual DbSet<translationTables> translationTables { get; set; }
        public virtual DbSet<trnTranslationColumns> trnTranslationColumns { get; set; }
        public virtual DbSet<trnTranslationLanguages> trnTranslationLanguages { get; set; }
        public virtual DbSet<trnTranslations> trnTranslations { get; set; }
        public virtual DbSet<warCombatZones> warCombatZones { get; set; }
        public virtual DbSet<warCombatZoneSystems> warCombatZoneSystems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<agtAgentTypes>()
                .Property(e => e.agentType)
                .IsUnicode(false);

            modelBuilder.Entity<chrAttributes>()
                .Property(e => e.attributeName)
                .IsUnicode(false);

            modelBuilder.Entity<chrAttributes>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<chrFactions>()
                .Property(e => e.factionName)
                .IsUnicode(false);

            modelBuilder.Entity<chrFactions>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<chrRaces>()
                .Property(e => e.raceName)
                .IsUnicode(false);

            modelBuilder.Entity<chrRaces>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<chrRaces>()
                .Property(e => e.shortDescription)
                .IsUnicode(false);

            modelBuilder.Entity<crpNPCCorporations>()
                .Property(e => e.size)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<crpNPCCorporations>()
                .Property(e => e.extent)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<dgmAttributeTypes>()
                .Property(e => e.attributeName)
                .IsUnicode(false);

            modelBuilder.Entity<dgmAttributeTypes>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<dgmAttributeTypes>()
                .Property(e => e.displayName)
                .IsUnicode(false);

            modelBuilder.Entity<dgmEffects>()
                .Property(e => e.effectName)
                .IsUnicode(false);

            modelBuilder.Entity<dgmEffects>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<dgmEffects>()
                .Property(e => e.guid)
                .IsUnicode(false);

            modelBuilder.Entity<dgmEffects>()
                .Property(e => e.displayName)
                .IsUnicode(false);

            modelBuilder.Entity<dgmEffects>()
                .Property(e => e.sfxName)
                .IsUnicode(false);

            modelBuilder.Entity<dgmEffects>()
                .Property(e => e.modifierInfo)
                .IsUnicode(false);

            modelBuilder.Entity<dgmExpressions>()
                .Property(e => e.expressionValue)
                .IsUnicode(false);

            modelBuilder.Entity<dgmExpressions>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<dgmExpressions>()
                .Property(e => e.expressionName)
                .IsUnicode(false);

            modelBuilder.Entity<eveGraphics>()
                .Property(e => e.graphicFile)
                .IsUnicode(false);

            modelBuilder.Entity<eveGraphics>()
                .Property(e => e.graphicType)
                .IsUnicode(false);

            modelBuilder.Entity<eveGraphics>()
                .Property(e => e.gfxRaceID)
                .IsUnicode(false);

            modelBuilder.Entity<eveGraphics>()
                .Property(e => e.colorScheme)
                .IsUnicode(false);

            modelBuilder.Entity<eveGraphics>()
                .Property(e => e.sofHullName)
                .IsUnicode(false);

            modelBuilder.Entity<eveIcons>()
                .Property(e => e.iconFile)
                .IsUnicode(false);

            modelBuilder.Entity<eveUnits>()
                .Property(e => e.unitName)
                .IsUnicode(false);

            modelBuilder.Entity<eveUnits>()
                .Property(e => e.displayName)
                .IsUnicode(false);

            modelBuilder.Entity<eveUnits>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<invControlTowerResourcePurposes>()
                .Property(e => e.purposeText)
                .IsUnicode(false);

            modelBuilder.Entity<invFlags>()
                .Property(e => e.flagName)
                .IsUnicode(false);

            modelBuilder.Entity<invFlags>()
                .Property(e => e.flagText)
                .IsUnicode(false);

            modelBuilder.Entity<invTypes>()
                .Property(e => e.basePrice);

            modelBuilder.Entity<mapCelestialStatistics>()
                .Property(e => e.spectralClass)
                .IsUnicode(false);

            modelBuilder.Entity<mapLandmarks>()
                .Property(e => e.landmarkName)
                .IsUnicode(false);

            modelBuilder.Entity<mapLandmarks>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<mapSolarSystems>()
                .Property(e => e.securityClass)
                .IsUnicode(false);

            modelBuilder.Entity<mapUniverse>()
                .Property(e => e.universeName)
                .IsUnicode(false);

            modelBuilder.Entity<ramActivities>()
                .Property(e => e.iconNo)
                .IsUnicode(false);

            modelBuilder.Entity<trnTranslationLanguages>()
                .Property(e => e.languageID)
                .IsUnicode(false);

            modelBuilder.Entity<trnTranslations>()
                .Property(e => e.languageID)
                .IsUnicode(false);
        }
    }
}
