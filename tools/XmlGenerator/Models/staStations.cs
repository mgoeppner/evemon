namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class staStations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long stationID { get; set; }

        public double? security { get; set; }

        public double? dockingCostPerVolume { get; set; }

        public double? maxShipVolumeDockable { get; set; }

        public int? officeRentalCost { get; set; }

        public int? operationID { get; set; }

        public int? stationTypeID { get; set; }

        public int? corporationID { get; set; }

        public int? solarSystemID { get; set; }

        public int? constellationID { get; set; }

        public int? regionID { get; set; }

        [StringLength(100)]
        public string stationName { get; set; }

        public double? x { get; set; }

        public double? y { get; set; }

        public double? z { get; set; }

        public double? reprocessingEfficiency { get; set; }

        public double? reprocessingStationsTake { get; set; }

        public int? reprocessingHangarFlag { get; set; }
    }
}
