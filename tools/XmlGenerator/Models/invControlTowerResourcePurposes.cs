namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class invControlTowerResourcePurposes
    {
        [Key]
        public byte purpose { get; set; }

        [StringLength(100)]
        public string purposeText { get; set; }
    }
}
