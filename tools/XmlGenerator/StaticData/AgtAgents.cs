using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class AgtAgents : IHasID
    {
        [XmlElement("agentID")]
        public int ID { get; set; }

        [XmlElement("divisionID")]
        public int DivisionID { get; set; }

        [XmlElement("locationID")]
        public int LocationID { get; set; }

        [XmlElement("level")]
        public int Level { get; set; }

        [XmlElement("quality")]
        public int Quality { get; set; }

        [XmlElement("agentTypeID")]
        public int AgentTypeID { get; set; }

        [XmlElement("isLocator")]
        public bool IsLocator { get; set; }
    }
}