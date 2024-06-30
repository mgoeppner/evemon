namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class invMetaGroups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short metaGroupID { get; set; }

        [StringLength(100)]
        public string metaGroupName { get; set; }

        [StringLength(1000)]
        public string description { get; set; }

        public int? iconID { get; set; }
    }
}
