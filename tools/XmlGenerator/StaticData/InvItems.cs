using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class InvItems : IHasID
    {
        [XmlElement("itemID")]
        public int ID { get; set; }

        [XmlElement("typeID")]
        public int TypeID { get; set; }

        [XmlElement("ownerID")]
        public int OwnerID { get; set; }

        [XmlElement("locationID")]
        public int LocationID { get; set; }

        [XmlElement("flagID")]
        public int FlagID { get; set; }

        [XmlElement("quantity")]
        public int Quantity { get; set; }
    }
}
