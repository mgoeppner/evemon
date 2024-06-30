namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class staServices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int serviceID { get; set; }

        [StringLength(100)]
        public string serviceName { get; set; }

        [StringLength(1000)]
        public string description { get; set; }
    }
}
