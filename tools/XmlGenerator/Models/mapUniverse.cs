namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("mapUniverse")]
    public partial class mapUniverse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int universeID { get; set; }

        [StringLength(100)]
        public string universeName { get; set; }

        public double? x { get; set; }

        public double? y { get; set; }

        public double? z { get; set; }

        public double? xMin { get; set; }

        public double? xMax { get; set; }

        public double? yMin { get; set; }

        public double? yMax { get; set; }

        public double? zMin { get; set; }

        public double? zMax { get; set; }

        public double? radius { get; set; }
    }
}
