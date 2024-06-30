namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class mapLocationScenes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int locationID { get; set; }

        public int? graphicID { get; set; }
    }
}
