namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class chrAttributes
    {
        [Key]
        public byte attributeID { get; set; }

        [StringLength(100)]
        public string attributeName { get; set; }

        [StringLength(1000)]
        public string description { get; set; }

        public int? iconID { get; set; }

        [StringLength(500)]
        public string shortDescription { get; set; }

        [StringLength(500)]
        public string notes { get; set; }
    }
}
