namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class crtCertificates
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int certificateID { get; set; }

        public int? groupID { get; set; }

        public int? classID { get; set; }

        public int? grade { get; set; }

        [StringLength(256)]
        public string name { get; set; }

        [StringLength(3000)]
        public string description { get; set; }
    }
}
