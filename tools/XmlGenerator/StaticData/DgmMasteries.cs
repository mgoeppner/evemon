using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class DgmMasteries : IHasID
    {
        [XmlElement("masteryID")]
        public int ID { get; set; }

        [XmlElement("certificateID")]
        public int CertificateID { get; set; }

        [XmlElement("grade")]
        public short Grade { get; set; }
    }
}
