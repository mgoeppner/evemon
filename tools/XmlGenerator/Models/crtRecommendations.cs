namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class crtRecommendations
    {
        [Key]
        public int recommendationID { get; set; }

        public int? shipTypeID { get; set; }

        public int? certificateID { get; set; }

        public int? recommendationLevel { get; set; }
    }
}
