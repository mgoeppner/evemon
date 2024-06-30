namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class invGroups
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int groupID { get; set; }

        public int? categoryID { get; set; }

        [StringLength(100)]
        public string groupName { get; set; }

        public int? iconID { get; set; }

        public bool? useBasePrice { get; set; }

        public bool? anchored { get; set; }

        public bool? anchorable { get; set; }

        public bool? fittableNonSingleton { get; set; }

        public bool? published { get; set; }
    }
}
