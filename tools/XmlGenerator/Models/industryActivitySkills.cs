using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVEMon.XmlGenerator.Models
{
    public partial class industryActivitySkills
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? typeID { get; set; }

        public int? activityID { get; set; }

        public int? skillID { get; set; }

        public int? level { get; set; }
    }
}


