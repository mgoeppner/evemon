namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class crtRelationships
    {
        [Key]
        public int relationshipID { get; set; }

        public int? parentID { get; set; }

        public int? parentTypeID { get; set; }

        public int? parentLevel { get; set; }

        public int? childID { get; set; }

        public int? grade { get; set; }
    }
}
