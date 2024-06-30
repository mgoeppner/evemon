namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class trnTranslationLanguages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int numericLanguageID { get; set; }

        [StringLength(50)]
        public string languageID { get; set; }

        [StringLength(200)]
        public string languageName { get; set; }
    }
}
