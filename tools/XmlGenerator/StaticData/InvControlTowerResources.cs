using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class InvControlTowerResources : IHasID
    {
        [XmlElement("controlTowerTypeID")]
        public int ID { get; set; }

        [XmlElement("resourceTypeID")]
        public int ResourceID { get; set; }

        [XmlElement("purposeID")]
        public int PurposeID { get; set; }

        [XmlElement("quantity")]
        public long Quantity { get; set; }

        [XmlElement("minSecurityLevel")]
        public double? MinSecurityLevel { get; set; }

        [XmlElement("factionID")]
        public int? FactionID { get; set; }
    }
}