using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class CrtRecommendations : IHasID
    {
        [XmlElement("recommendationID")]
        public int ID { get; set; }

        [XmlElement("shipTypeID")]
        public int ShipTypeID { get; set; }

        [XmlElement("certificateID")]
        public int CertificateID { get; set; }

        [XmlElement("recommendationLevel")]
        public int Level { get; set; }
    }
}