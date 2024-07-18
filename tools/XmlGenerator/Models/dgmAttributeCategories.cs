namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class dgmAttributeCategories
    {
        [Key]
        public byte categoryID { get; set; }

        [StringLength(50)]
        public string categoryName { get; set; }

        [StringLength(200)]
        public string categoryDescription { get; set; }
    }
}
