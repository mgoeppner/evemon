namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class crtClasses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int classID { get; set; }

        [StringLength(3000)]
        public string description { get; set; }

        [StringLength(256)]
        public string className { get; set; }
    }
}
