namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class sknSkins
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int skinID { get; set; }

        [Required]
        [StringLength(100)]
        public string internalName { get; set; }

        public int? skinMaterialID { get; set; }

        public int? typeID { get; set; }

        public bool allowCCPDevs { get; set; }

        public bool visibleSerenity { get; set; }

        public bool visibleTranquility { get; set; }
    }
}
