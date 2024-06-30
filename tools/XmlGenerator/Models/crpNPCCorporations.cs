namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class crpNPCCorporations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int corporationID { get; set; }

        [StringLength(1)]
        public string size { get; set; }

        [StringLength(1)]
        public string extent { get; set; }

        public int? solarSystemID { get; set; }

        public int? investorID1 { get; set; }

        public byte? investorShares1 { get; set; }

        public int? investorID2 { get; set; }

        public byte? investorShares2 { get; set; }

        public int? investorID3 { get; set; }

        public byte? investorShares3 { get; set; }

        public int? investorID4 { get; set; }

        public byte? investorShares4 { get; set; }

        public int? friendID { get; set; }

        public int? enemyID { get; set; }

        public long? publicShares { get; set; }

        public int? initialPrice { get; set; }

        public double? minSecurity { get; set; }

        public bool? scattered { get; set; }

        public byte? fringe { get; set; }

        public byte? corridor { get; set; }

        public byte? hub { get; set; }

        public byte? border { get; set; }

        public int? factionID { get; set; }

        public double? sizeFactor { get; set; }

        public short? stationCount { get; set; }

        public short? stationSystemCount { get; set; }

        [StringLength(4000)]
        public string description { get; set; }

        public int? iconID { get; set; }
    }
}
