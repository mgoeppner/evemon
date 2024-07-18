using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class AgtResearchAgents : IHasID
    {
        [XmlElement("agentID")]
        public int ID { get; set; }

        [XmlElement("typeID")]
        public int ResearchSkillID { get; set; }
    }
}