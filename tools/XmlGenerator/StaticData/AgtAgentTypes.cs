using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class AgtAgentTypes : IHasID
    {
        [XmlElement("agentTypeID")]
        public int ID { get; set; }

        [XmlElement("agentType")]
        public string AgentType { get; set; }
    }
}