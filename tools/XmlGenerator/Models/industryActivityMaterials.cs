using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVEMon.XmlGenerator.Models
{
    public partial class industryActivityMaterials
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? typeID { get; set; }

        public int? activityID { get; set; }

        public int? materialTypeID { get; set; }

        public int? quantity { get; set; }
    }
}


