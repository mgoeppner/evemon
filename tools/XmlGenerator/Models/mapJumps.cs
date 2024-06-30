namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class mapJumps
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int stargateID { get; set; }

        public int? celestialID { get; set; }
    }
}
