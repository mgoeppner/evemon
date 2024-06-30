namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class sknMaterials
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int skinMaterialID { get; set; }

        public int materialSetID { get; set; }

        public int displayNameID { get; set; }

        [Required]
        [StringLength(255)]
        public string material { get; set; }

        [StringLength(6)]
        public string colorHull { get; set; }

        [StringLength(6)]
        public string colorWindow { get; set; }

        [StringLength(6)]
        public string colorPrimary { get; set; }

        [StringLength(6)]
        public string colorSecondary { get; set; }
    }
}
