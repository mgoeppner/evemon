namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class trnTranslationColumns
    {
        public short? tcGroupID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short tcID { get; set; }

        [Required]
        [StringLength(256)]
        public string tableName { get; set; }

        [Required]
        [StringLength(128)]
        public string columnName { get; set; }

        [StringLength(128)]
        public string masterID { get; set; }
    }
}
