using System;
using System.Xml.Serialization;

namespace EVEMon.Common.Serialization.Eve
{
    /// <summary>
    /// Represents the character's attributes
    /// </summary>
    public sealed class SerializableCharacterAttributes
    {
        public SerializableCharacterAttributes()
        {
            Intelligence = Memory = Perception = Charisma = Willpower = 1;
            Booster = 0;
        }

        [XmlElement("intelligence")]
        public long Intelligence { get; set; }

        [XmlElement("memory")]
        public long Memory { get; set; }

        [XmlElement("perception")]
        public long Perception { get; set; }

        [XmlElement("willpower")]
        public long Willpower { get; set; }

        [XmlElement("charisma")]
        public long Charisma { get; set; }

        [XmlElement("booster")]
        public long Booster { get; set; }
    }
}
