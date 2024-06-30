namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class invMarketGroups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int marketGroupID { get; set; }

        public int? parentGroupID { get; set; }

        [StringLength(100)]
        public string marketGroupName { get; set; }

        [StringLength(3000)]
        public string description { get; set; }

        public int? iconID { get; set; }

        public bool? hasTypes { get; set; }
    }
}
