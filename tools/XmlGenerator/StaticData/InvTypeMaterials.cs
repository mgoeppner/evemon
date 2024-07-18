using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class InvTypeMaterials : IHasID
    {
        [XmlElement("typeID")]
        public int ID { get; set; }

        [XmlElement("materialTypeID")]
        public int MaterialTypeID { get; set; }

        [XmlElement("quantity")]
        public int Quantity { get; set; }
    }
}