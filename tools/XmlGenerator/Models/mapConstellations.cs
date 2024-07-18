namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class mapConstellations
    {
        public int? regionID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int constellationID { get; set; }

        [StringLength(100)]
        public string constellationName { get; set; }

        public double? x { get; set; }

        public double? y { get; set; }

        public double? z { get; set; }

        public double? xMin { get; set; }

        public double? xMax { get; set; }

        public double? yMin { get; set; }

        public double? yMax { get; set; }

        public double? zMin { get; set; }

        public double? zMax { get; set; }

        public int? factionID { get; set; }

        public double? radius { get; set; }
    }
}
