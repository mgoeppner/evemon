namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class sknLicenses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int licenseTypeID { get; set; }

        public int skinID { get; set; }

        public int duration { get; set; }
    }
}
