namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class planetSchematics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short schematicID { get; set; }

        [StringLength(255)]
        public string schematicName { get; set; }

        public int? cycleTime { get; set; }
    }
}
