namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class dgmMasteries
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int masteryID { get; set; }

        public int? certificateID { get; set; }

        public int? grade { get; set; }
    }
}
