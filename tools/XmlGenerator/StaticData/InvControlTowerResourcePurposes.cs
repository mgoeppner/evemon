using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class InvControlTowerResourcePurposes : IHasID
    {
        [XmlElement("purpose")]
        public int ID { get; set; }

        [XmlElement("purposeText")]
        public string PurposeName { get; set; }
    }
}