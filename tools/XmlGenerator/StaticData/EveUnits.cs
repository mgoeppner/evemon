using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class EveUnits : IHasID
    {
        [XmlElement("unitID")]
        public int ID { get; set; }

        [XmlElement("unitName")]
        public string Name { get; set; }

        [XmlElement("displayName")]
        public string DisplayName { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }
    }
}