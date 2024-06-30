namespace EVEMon.XmlGenerator.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class invItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long itemID { get; set; }

        public int typeID { get; set; }

        public int ownerID { get; set; }

        public long locationID { get; set; }

        public short flagID { get; set; }

        public int quantity { get; set; }
    }
}
