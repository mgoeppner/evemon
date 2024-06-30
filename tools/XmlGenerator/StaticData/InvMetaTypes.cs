using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class InvMetaTypes : IRelation
    {
        [XmlElement("typeID")]
        public int ItemID { get; set; }

        [XmlElement("parentTypeID")]
        public int ParentItemID { get; set; }

        [XmlElement("metaGroupID")]
        public int MetaGroupID { get; set; }


        #region IRelation Members

        /// <summary>
        /// Gets the left column value.
        /// </summary>
        /// <value>The left.</value>
        int IRelation.Left => ItemID;

        /// <summary>
        /// Gets the center column value.
        /// </summary>
        /// <value>
        /// The center.
        /// </value>
        int IRelation.Center => ParentItemID;

        /// <summary>
        /// Gets the right.
        /// </summary>
        /// <value>The right column value.</value>
        int IRelation.Right => MetaGroupID;

        #endregion
    }
}