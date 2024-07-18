using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class InvNames : IHasID
    {
        [XmlElement("itemID")]
        public int ID { get; set; }

        [XmlElement("itemName")]
        public string Name { get; set; }
    }
}