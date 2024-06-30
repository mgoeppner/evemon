namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class ramActivities
    {
        [Key]
        public byte activityID { get; set; }

        [StringLength(100)]
        public string activityName { get; set; }

        [StringLength(5)]
        public string iconNo { get; set; }

        [StringLength(1000)]
        public string description { get; set; }

        public bool? published { get; set; }
    }
}
