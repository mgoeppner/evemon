namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ramAssemblyLineStations
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int stationID { get; set; }

        [Column(Order = 1)]
        public byte assemblyLineTypeID { get; set; }

        public byte? quantity { get; set; }

        public int? stationTypeID { get; set; }

        public int? ownerID { get; set; }

        public int? solarSystemID { get; set; }

        public int? regionID { get; set; }
    }
}
