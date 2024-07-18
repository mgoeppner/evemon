namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class eveGraphics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int graphicID { get; set; }

        [Required]
        [StringLength(500)]
        public string graphicFile { get; set; }

        [Required]
        public string description { get; set; }

        public bool obsolete { get; set; }

        [StringLength(100)]
        public string graphicType { get; set; }

        public bool? collidable { get; set; }

        public int? directoryID { get; set; }

        [Required]
        [StringLength(64)]
        public string graphicName { get; set; }

        [StringLength(255)]
        public string gfxRaceID { get; set; }

        [StringLength(255)]
        public string colorScheme { get; set; }

        [StringLength(64)]
        public string sofHullName { get; set; }
    }
}
