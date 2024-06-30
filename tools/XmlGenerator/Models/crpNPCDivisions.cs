namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class crpNPCDivisions
    {
        [Key]
        public byte divisionID { get; set; }

        [StringLength(100)]
        public string divisionName { get; set; }

        [StringLength(1000)]
        public string description { get; set; }

        [StringLength(100)]
        public string leaderType { get; set; }
    }
}
