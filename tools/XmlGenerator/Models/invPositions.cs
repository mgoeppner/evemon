namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class invPositions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long itemID { get; set; }

        public double x { get; set; }

        public double y { get; set; }

        public double z { get; set; }

        public float? yaw { get; set; }

        public float? pitch { get; set; }

        public float? roll { get; set; }
    }
}
