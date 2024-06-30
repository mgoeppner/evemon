namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class invTraits
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int traitID { get; set; }

        public int? typeID { get; set; }

        public int? skillID { get; set; }

        public float? bonus { get; set; }

        [Required]
        [StringLength(500)]
        public string BonusText { get; set; }

        public int? unitID { get; set; }
    }
}
