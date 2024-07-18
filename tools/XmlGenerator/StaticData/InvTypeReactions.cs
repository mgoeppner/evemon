using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class InvTypeReactions : IHasID
    {
        [XmlElement("reactionTypeID")]
        public int ID { get; set; }

        [XmlElement("input")]
        public bool Input { get; set; }

        [XmlElement("typeID")]
        public int TypeID { get; set; }

        [XmlElement("quantity")]
        public int Quantity { get; set; }
    }
}