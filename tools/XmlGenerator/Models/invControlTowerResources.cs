namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class invControlTowerResources
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int controlTowerTypeID { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int resourceTypeID { get; set; }

        public int? purpose { get; set; }

        public int? quantity { get; set; }

        public double? minSecurityLevel { get; set; }

        public int? factionID { get; set; }
    }
}
