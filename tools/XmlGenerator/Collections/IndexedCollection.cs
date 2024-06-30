using EVEMon.XmlGenerator.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.Collections
{
    /// <summary>
    /// A collection of indexed items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class IndexedCollection<T> : IEnumerable<T>
        where T : IHasID
    {
        private readonly Collection<T> m_items;

        /// <summary>
        /// Constructor.
        /// </summary>
        public IndexedCollection()
        {
            m_items = new Collection<T>();
        }

        /// <summary>
        /// List of items in the collection.
        /// </summary>
        [XmlElement("item")]
        public Collection<T> Items => m_items;

        /// <summary>
        /// Converts collection to a BagCollection.
        /// </summary>
        /// <returns>BagCollection of type T</returns>
        public BagCollection<T> ToBag() => new BagCollection<T>(this);

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator() => m_items.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary>
    /// A collection of indexed items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class LongIndexedCollection<T> : IEnumerable<T>
        where T : IHasLongID
    {
        private readonly Collection<T> m_items;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LongIndexedCollection()
        {
            m_items = new Collection<T>();
        }

        /// <summary>
        /// List of items in the collection.
        /// </summary>
        [XmlElement("item")]
        public Collection<T> Items => m_items;

        /// <summary>
        /// Converts collection to a BagCollection.
        /// </summary>
        /// <returns>BagCollection of type T</returns>
        public LongBagCollection<T> ToBag() => new LongBagCollection<T>(this);

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator() => m_items.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}