using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVEMon.XmlGenerator.Models
{
    public partial class industryActivityRaces
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? typeID { get; set; }

        public int? activityID { get; set; }

        public int? productTypeID { get; set; }

        public int? raceID { get; set; }
    }
}


