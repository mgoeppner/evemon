namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class dgmExpressions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int expressionID { get; set; }

        public int? operandID { get; set; }

        public int? arg1 { get; set; }

        public int? arg2 { get; set; }

        [StringLength(100)]
        public string expressionValue { get; set; }

        [StringLength(1000)]
        public string description { get; set; }

        [StringLength(500)]
        public string expressionName { get; set; }

        public int? expressionTypeID { get; set; }

        public short? expressionGroupID { get; set; }

        public short? expressionAttributeID { get; set; }
    }
}
