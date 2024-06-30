namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class dgmAttributeTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int attributeID { get; set; }

        [StringLength(100)]
        public string attributeName { get; set; }

        [StringLength(1000)]
        public string description { get; set; }

        public int? iconID { get; set; }

        public double? defaultValue { get; set; }

        public bool? published { get; set; }

        [StringLength(100)]
        public string displayName { get; set; }

        public int? unitID { get; set; }

        public bool? stackable { get; set; }

        public bool? highIsGood { get; set; }

        public int? categoryID { get; set; }
    }
}
