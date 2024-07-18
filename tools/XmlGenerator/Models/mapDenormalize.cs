namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("mapDenormalize")]
    public partial class mapDenormalize
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int itemID { get; set; }

        public int? typeID { get; set; }

        public int? groupID { get; set; }

        public int? solarSystemID { get; set; }

        public int? constellationID { get; set; }

        public int? regionID { get; set; }

        public int? orbitID { get; set; }

        public double? x { get; set; }

        public double? y { get; set; }

        public double? z { get; set; }

        public double? radius { get; set; }

        [StringLength(100)]
        public string itemName { get; set; }

        public double? security { get; set; }

        public byte? celestialIndex { get; set; }

        public byte? orbitIndex { get; set; }
    }
}
