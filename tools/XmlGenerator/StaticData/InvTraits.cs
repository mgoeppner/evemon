using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class InvTraits : IHasID
    {
        [XmlElement("traitID")]
        public int ID { get; set; }

        [XmlElement("skillID")]
        public int? skillID { get; set; }

        [XmlElement("typeID")]
        public int? typeID { get; set; }

        public double? bonus { get; set; }

        [XmlElement("bonusText")]
        public string BonusText { get; set; }

        [XmlElement("unitID")]
        public int? UnitID { get; set; }
    }
}
