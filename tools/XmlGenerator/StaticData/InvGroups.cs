using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class InvGroups : IHasID
    {
        [XmlElement("groupID")]
        public int ID { get; set; }

        [XmlElement("categoryID")]
        public int CategoryID { get; set; }

        [XmlElement("groupName")]
        public string Name { get; set; }

        [XmlElement("decription")]
        public string Description { get; set; }

        [XmlElement("published")]
        public bool? Published { get; set; }

        public bool? UseBasePrice { get; set; }
        public bool? Anchored { get; set; }
        public bool? Anchorable { get; set; }
        public bool? FittableNonSingleton { get; set; }
    }
}