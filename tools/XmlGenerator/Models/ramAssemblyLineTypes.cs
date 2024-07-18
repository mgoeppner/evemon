namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class ramAssemblyLineTypes
    {
        [Key]
        public byte assemblyLineTypeID { get; set; }

        [StringLength(100)]
        public string assemblyLineTypeName { get; set; }

        [StringLength(1000)]
        public string description { get; set; }

        public double? baseTimeMultiplier { get; set; }

        public double? baseMaterialMultiplier { get; set; }

        public double? baseCostMultiplier { get; set; }

        public double? volume { get; set; }

        public byte? activityID { get; set; }

        public double? minCostPerHour { get; set; }
    }
}
