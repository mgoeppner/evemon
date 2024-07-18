using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class CrpNPCDivisions : IHasID
    {
        [XmlElement("divisionID")]
        public int ID { get; set; }

        [XmlElement("divisionName")]
        public string DivisionName { get; set; }
    }
}