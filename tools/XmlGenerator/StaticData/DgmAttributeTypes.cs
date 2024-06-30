using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class DgmAttributeTypes : IHasID
    {
        [XmlElement("attributeID")]
        public int ID { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("attributeName")]
        public string Name { get; set; }

        [XmlElement("displayName")]
        public string DisplayName { get; set; }

        [XmlElement("defaultValue")]
        public string DefaultValue { get; set; }

        [XmlElement("iconID")]
        public int? IconID { get; set; }

        [XmlElement("published")]
        public bool Published { get; set; }

        [XmlElement("unitID")]
        public int? UnitID { get; set; }

        [XmlElement("categoryID")]
        public int? CategoryID { get; set; }

        [XmlElement("highIsGood")]
        public bool HigherIsBetter { get; set; }
    }
}