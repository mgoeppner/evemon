namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class warCombatZoneSystems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int solarSystemID { get; set; }

        public int? combatZoneID { get; set; }
    }
}
