using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.Xmlfiles.Serialization
{
    public class SerialiazableRowset<T>
    {
        private readonly Collection<T> m_rows;

        public SerialiazableRowset()
        {
            m_rows = new Collection<T>();
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("columns")]
        public string Columns { get; set; }

        [XmlElement("row")]
        public Collection<T> Rows => m_rows;
    }
}