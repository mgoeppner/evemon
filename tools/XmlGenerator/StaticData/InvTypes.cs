using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class InvTypes : IHasID
    {
        [XmlElement("typeID")]
        public int ID { get; set; }

        [XmlElement("groupID")]
        public int GroupID { get; set; }

        [XmlElement("iconID")]
        public int? IconID { get; set; }

        [XmlElement("typeName")]
        public string Name { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("mass")]
        public double Mass { get; set; }

        [XmlElement("volume")]
        public double Volume { get; set; }

        [XmlElement("capacity")]
        public double Capacity { get; set; }

        [XmlElement("portionSize")]
        public int PortionSize { get; set; }

        [XmlElement("raceID")]
        public int? RaceID { get; set; }

        [XmlElement("marketGroupID")]
        public int? MarketGroupID { get; set; }

        [XmlElement("basePrice")]
        public decimal BasePrice { get; set; }

        [XmlElement("published")]
        public bool Published { get; set; }

        [XmlIgnore]
        public bool Generated { get; set; }
    }
}