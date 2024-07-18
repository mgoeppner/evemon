using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVEMon.XmlGenerator.Models
{
    public partial class industryActivityProbabilities
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? typeID { get; set; }

        public int? activityID { get; set; }

        public int? productTypeID { get; set; }

        public decimal? probability { get; set; }
    }
}


