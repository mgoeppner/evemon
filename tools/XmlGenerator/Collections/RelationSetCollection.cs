using EVEMon.Common.Extensions;
using EVEMon.XmlGenerator.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace EVEMon.XmlGenerator.Collections
{
    public sealed class RelationSetCollection<T> : IEnumerable<T>
        where T : class, IRelation
    {
        /// <summary>
        /// Used to generate a unique, comparable key for the internal dictionary.
        /// </summary>
        private struct Relation : IRelation
        {
            int Left { get; set; }
            int Center { get; set; }
            int Right { get; set; }

            int IRelation.Left => Left;

            int IRelation.Center => Center;

            int IRelation.Right => Right;

            public Relation(int left, int center, int right)
            {
                Left = left;
                Center = center;
                Right = right;
            }

            public override bool Equals(object obj)
            {
                if (obj != null && obj is Relation r)
                {
                    return r.Left == Left && r.Center == Center && r.Right == Right;
                }

                return false;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 17;
                    hash = hash * 23 + Left.GetHashCode();
                    hash = hash * 23 + Center.GetHashCode();
                    hash = hash * 23 + Right.GetHashCode();
                    return hash;
                }
            }
        }

        private readonly Dictionary<Relation, T> m_dictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelationSetCollection{T}" /> class.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <exception cref="System.ArgumentNullException">src</exception>
        public RelationSetCollection(IEnumerable<T> src)
        {
            src.ThrowIfNull(nameof(src));

            m_dictionary = new Dictionary<Relation, T>();
            foreach (T item in src)
            {
                m_dictionary[GetKey(item)] = item;
            }
        }

        /// <summary>
        /// Determines whether [contains] [the specified left].
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="center">The center.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified left]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(int left, int center, int right)
            => m_dictionary.ContainsKey(GetKey(left, center, right));

        /// <summary>
        /// Determines whether the collection contains the specified item, based on the <see cref="IRelation"/> properties of the input.
        /// </summary>
        /// <param name="relation">The identifying properties of the item to check for.</param>
        /// <returns></returns>
        public bool Contains(IRelation relation)
            => m_dictionary.ContainsKey(GetKey(relation));

        /// <summary>
        /// Gets the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="center">The center.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public T Get(int left, int center, int right)
        {
            T value;
            m_dictionary.TryGetValue(GetKey(left, center, right), out value);
            return value;
        }

        /// <summary>
        /// Gets the specified item from the collection based on the <see cref="IRelation"/> properties of the input.
        /// </summary>
        /// <param name="relation">The identifying properties of the item to get.</param>
        /// <returns></returns>
        public T Get(IRelation relation)
            => Get(relation.Left, relation.Center, relation.Right);

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <param name="relation">The relation.</param>
        /// <returns></returns>
        private static Relation GetKey(IRelation relation)
            => GetKey(relation.Left, relation.Center, relation.Right);

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="center">The center.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        private static Relation GetKey(int left, int center, int right)
            => new Relation(left, center, right);

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)m_dictionary.Values).GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
