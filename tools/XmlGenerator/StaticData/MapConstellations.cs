using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class MapConstellations : IHasID
    {
        [XmlElement("constellationID")]
        public int ID { get; set; }

        [XmlElement("constellationName")]
        public string Name { get; set; }

        [XmlElement("regionID")]
        public int RegionID { get; set; }
    }
}