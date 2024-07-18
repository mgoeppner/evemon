using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVEMon.XmlGenerator.Models
{
    [Table("industryActivity")]
    public partial class industryActivity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int typeID { get; set; }

        public int activityID { get; set; }

        public int? time { get; set; }
    }
}
