namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class mapLandmarks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short landmarkID { get; set; }

        [StringLength(100)]
        public string landmarkName { get; set; }

        [StringLength(7000)]
        public string description { get; set; }

        public int? locationID { get; set; }

        public double? x { get; set; }

        public double? y { get; set; }

        public double? z { get; set; }

        public double? radius { get; set; }

        public int? iconID { get; set; }

        public byte? importance { get; set; }
    }
}
