namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class dgmEffects
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short effectID { get; set; }

        [StringLength(400)]
        public string effectName { get; set; }

        public short? effectCategory { get; set; }

        public int? preExpression { get; set; }

        public int? postExpression { get; set; }

        [StringLength(1000)]
        public string description { get; set; }

        [StringLength(60)]
        public string guid { get; set; }

        public int? iconID { get; set; }

        public bool? isOffensive { get; set; }

        public bool? isAssistance { get; set; }

        public short? durationAttributeID { get; set; }

        public short? trackingSpeedAttributeID { get; set; }

        public short? dischargeAttributeID { get; set; }

        public short? rangeAttributeID { get; set; }

        public short? falloffAttributeID { get; set; }

        public bool? disallowAutoRepeat { get; set; }

        public bool? published { get; set; }

        [StringLength(100)]
        public string displayName { get; set; }

        public bool? isWarpSafe { get; set; }

        public bool? rangeChance { get; set; }

        public bool? electronicChance { get; set; }

        public bool? propulsionChance { get; set; }

        public byte? distribution { get; set; }

        [StringLength(20)]
        public string sfxName { get; set; }

        public short? npcUsageChanceAttributeID { get; set; }

        public short? npcActivationChanceAttributeID { get; set; }

        public short? fittingUsageChanceAttributeID { get; set; }

        public string modifierInfo { get; set; }
    }
}
