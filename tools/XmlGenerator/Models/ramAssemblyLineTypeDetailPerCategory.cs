namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ramAssemblyLineTypeDetailPerCategory")]
    public partial class ramAssemblyLineTypeDetailPerCategory
    {
        [Key]
        [Column(Order = 0)]
        public byte assemblyLineTypeID { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int categoryID { get; set; }

        public double? timeMultiplier { get; set; }

        public double? materialMultiplier { get; set; }

        public double? costMultiplier { get; set; }
    }
}
