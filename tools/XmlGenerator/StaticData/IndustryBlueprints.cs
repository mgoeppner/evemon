using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class IndustryBlueprints : IHasID
    {
        [XmlElement("typeID")]
        public int ID { get; set; }

        [XmlElement("maxProductionLimit")]
        public int MaxProductionLimit { get; set; }
    }
}
