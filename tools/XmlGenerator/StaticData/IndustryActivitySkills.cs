using EVEMon.XmlGenerator.Interfaces;
using System.Xml.Serialization;

namespace EVEMon.XmlGenerator.StaticData
{
    public sealed class IndustryActivitySkills : IRelation
    {
        [XmlElement("typeID")]
        public int BlueprintTypeID { get; set; }

        [XmlElement("activityID")]
        public int ActivityID { get; set; }

        [XmlElement("skillID")]
        public int SkillID { get; set; }

        [XmlElement("level")]
        public int? Level { get; set; }

        #region IRelation Members

        /// <summary>
        /// Gets the left column value.
        /// </summary>
        /// <value>The left.</value>
        int IRelation.Left => BlueprintTypeID;

        /// <summary>
        /// Gets the center column value.
        /// </summary>
        /// <value>
        /// The center.
        /// </value>
        int IRelation.Center => ActivityID;

        /// <summary>
        /// Gets the right column value.
        /// </summary>
        /// <value>The right.</value>
        int IRelation.Right => SkillID;

        #endregion
    }
}


