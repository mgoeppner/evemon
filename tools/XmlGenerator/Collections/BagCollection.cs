using EVEMon.Common.Extensions;
using EVEMon.XmlGenerator.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace EVEMon.XmlGenerator.Collections
{
    /// <summary>
    /// A dictionary-based implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class BagCollection<T> : IEnumerable<T>
        where T : IHasID
    {
        private readonly Dictionary<long, T> m_items;

        /// <summary>
        /// Initializes a new instance of the <see cref="BagCollection{T}" /> class.
        /// </summary>
        /// <param name="collection">The list.</param>
        /// <exception cref="System.ArgumentNullException">collection</exception>
        public BagCollection(IndexedCollection<T> collection)
        {
            collection.ThrowIfNull(nameof(collection));

            m_items = new Dictionary<long, T>();

            foreach (T item in collection.Items)
            {
                m_items[item.ID] = item;
            }
        }

        /// <summary>
        /// Determines whether the specified id has value.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// 	<c>true</c> if the specified id has value; otherwise, <c>false</c>.
        /// </returns>
        public bool HasValue(long id) => m_items.ContainsKey(id);

        /// <summary>
        /// Gets or sets the <see cref="T"/> with the specified id.
        /// </summary>
        /// <value></value>
        public T this[long id] => m_items[id];

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)m_items.Values).GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }


    /// <summary>
    /// A dictionary-based implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class LongBagCollection<T> : IEnumerable<T>
        where T : IHasLongID
    {
        private readonly Dictionary<long, T> m_items;

        /// <summary>
        /// Initializes a new instance of the <see cref="BagCollection{T}" /> class.
        /// </summary>
        /// <param name="collection">The list.</param>
        /// <exception cref="System.ArgumentNullException">collection</exception>
        public LongBagCollection(LongIndexedCollection<T> collection)
        {
            collection.ThrowIfNull(nameof(collection));

            m_items = new Dictionary<long, T>();

            foreach (T item in collection.Items)
            {
                m_items[item.ID] = item;
            }
        }

        /// <summary>
        /// Determines whether the specified id has value.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// 	<c>true</c> if the specified id has value; otherwise, <c>false</c>.
        /// </returns>
        public bool HasValue(long id) => m_items.ContainsKey(id);

        /// <summary>
        /// Gets or sets the <see cref="T"/> with the specified id.
        /// </summary>
        /// <value></value>
        public T this[long id] => m_items[id];

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)m_items.Values).GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
