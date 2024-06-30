using EVEMon.Common.Collections;
using EVEMon.Common.Constants;
using EVEMon.Common.Enumerations;
using EVEMon.Common.Serialization.Datafiles;
using EVEMon.XmlGenerator.Interfaces;
using EVEMon.XmlGenerator.Providers;
using EVEMon.XmlGenerator.StaticData;
using EVEMon.XmlGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EVEMon.XmlGenerator.Datafiles
{
    internal static class Skills
    {
        /// <summary>
        /// Generate the skills datafile.
        /// </summary>
        internal static async void GenerateDatafile()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Util.ResetCounters();

            Console.WriteLine();
            Console.Write(@"Generating skills datafile... ");

            // Export skill groups
            List<SerializableSkillGroup> listOfSkillGroups = new List<SerializableSkillGroup>();

            foreach (InvGroups group in Database.InvGroupsTable.Where(
                x => x.CategoryID == DBConstants.SkillCategoryID && x.ID != DBConstants.FakeSkillsGroupID).OrderBy(x => x.Name))
            {
                SerializableSkillGroup skillGroup = new SerializableSkillGroup
                {
                    ID = group.ID,
                    Name = group.Name,
                };

                var skills = await ExportSkillsInGroup(group);

                // Add skills in skill group
                skillGroup.Skills.AddRange(skills.OrderBy(x => x.Name));

                // Add skill group
                listOfSkillGroups.Add(skillGroup);
            }

            // Serialize
            SkillsDatafile datafile = new SkillsDatafile();
            datafile.SkillGroups.AddRange(listOfSkillGroups);

            Util.DisplayEndTime(stopwatch);

            Util.SerializeXml(datafile, DatafileConstants.SkillsDatafile);
        }

        /// <summary>
        /// Exports the skills in the skill group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        private static async Task<IEnumerable<SerializableSkill>> ExportSkillsInGroup(IHasID group)
        {
            List<SerializableSkill> listOfSkillsInGroup = new List<SerializableSkill>();

            var alphaLimit = await HoboleaksAlphaSkills.GetAlphaSkillLimits();
            var l5 = new SerializableSkillPrerequisite()
            {
                ID = 3348, // Leadership
                Level = 5,
                Name = Database.InvTypesTable[3348].Name
            };

            foreach (InvTypes skill in Database.InvTypesTable.Where(x => x.GroupID == group.ID))
            {
                Util.UpdatePercentDone(Database.SkillsTotalCount);

                int skillID = skill.ID;
                SerializableSkill singleSkill = new SerializableSkill
                {
                    ID = skillID,
                    Name = skill.Name,
                    Description = skill.Description,
                    Public = skill.Published,
                    Cost = (long)skill.BasePrice,
                    AlphaLimit = (alphaLimit.ContainsKey(skill.ID)) ? alphaLimit[skill.ID] : 0,
                };

                // Export skill atributes
                Dictionary<int, long> skillAttributes = Database.DgmTypeAttributesTable.Where(
                    x => x.ItemID == skill.ID).ToDictionary(
                        attribute => attribute.AttributeID, attribute => attribute.GetInt64Value);

                singleSkill.Rank = skillAttributes.ContainsKey(DBConstants.SkillTimeConstantPropertyID) &&
                                   skillAttributes[DBConstants.SkillTimeConstantPropertyID] > 0
                    ? skillAttributes[DBConstants.SkillTimeConstantPropertyID]
                    : 1;

                singleSkill.PrimaryAttribute = skillAttributes.ContainsKey(DBConstants.PrimaryAttributePropertyID)
                    ? IntToEveAttribute(skillAttributes[DBConstants.PrimaryAttributePropertyID])
                    : EveAttribute.None;
                singleSkill.SecondaryAttribute = skillAttributes.ContainsKey(DBConstants.SecondaryAttributePropertyID)
                    ? IntToEveAttribute(
                        skillAttributes[DBConstants.SecondaryAttributePropertyID])
                    : EveAttribute.None;

                // Export prerequisites
                List<SerializableSkillPrerequisite> listOfPrerequisites = new List<SerializableSkillPrerequisite>();

                for (int i = 0; i < DBConstants.RequiredSkillPropertyIDs.Count; i++)
                {
                    if (!skillAttributes.ContainsKey(DBConstants.RequiredSkillPropertyIDs[i]) ||
                        !skillAttributes.ContainsKey(DBConstants.RequiredSkillLevelPropertyIDs[i]))
                        continue;

                    InvTypes prereqSkill = Database.InvTypesTable[skillAttributes[DBConstants.RequiredSkillPropertyIDs[i]]];

                    SerializableSkillPrerequisite preReq = new SerializableSkillPrerequisite
                    {
                        ID = prereqSkill.ID,
                        Level =
                            skillAttributes[DBConstants.RequiredSkillLevelPropertyIDs[i]],
                        Name = prereqSkill.Name
                    };

                    // Add prerequisites
                    listOfPrerequisites.Add(preReq);
                }

                // Add prerequesites to skill
                singleSkill.SkillPrerequisites.AddRange(listOfPrerequisites);

                // Hack: Fleet formation skills have all NULL attributes. CCPlease.
                if (skillID == DBConstants.FleetCoordinationSkillID)
                {
                    singleSkill.Description = "Advanced fleet support skill allowing commanders to increase the size and spread of their fleet formations. Unlocks additional formation scaling options at each level of training.";
                    singleSkill.Rank = 8;
                    singleSkill.Cost = 40000000L;
                    singleSkill.PrimaryAttribute = EveAttribute.Charisma;
                    singleSkill.SecondaryAttribute = EveAttribute.Willpower;
                    singleSkill.AlphaLimit = 0;
                    singleSkill.SkillPrerequisites.Add(l5);
                    singleSkill.SkillPrerequisites.Add(new SerializableSkillPrerequisite()
                    {
                        ID = DBConstants.FleetFormationsSkillID,
                        Level = 1,
                        Name = Database.InvTypesTable[DBConstants.FleetFormationsSkillID].Name
                    });
                }
                else if (skillID == DBConstants.FleetFormationsSkillID)
                {
                    singleSkill.Description = "Fleet support skill allowing commanders to organize and warp fleets in formation. Unlocks additional formation types at each level of training.";
                    singleSkill.Rank = 5;
                    singleSkill.Cost = 40000000L;
                    singleSkill.PrimaryAttribute = EveAttribute.Charisma;
                    singleSkill.SecondaryAttribute = EveAttribute.Willpower;
                    singleSkill.AlphaLimit = 0;
                    singleSkill.SkillPrerequisites.Add(l5);
                }

                // Add skill
                listOfSkillsInGroup.Add(singleSkill);
            }
            return listOfSkillsInGroup;
        }

        /// <summary>
        /// Gets the Eve attribute.
        /// </summary>        
        private static EveAttribute IntToEveAttribute(long attributeValue)
        {
            switch (attributeValue)
            {
                case DBConstants.CharismaPropertyID:
                    return EveAttribute.Charisma;
                case DBConstants.IntelligencePropertyID:
                    return EveAttribute.Intelligence;
                case DBConstants.MemoryPropertyID:
                    return EveAttribute.Memory;
                case DBConstants.PerceptionPropertyID:
                    return EveAttribute.Perception;
                case DBConstants.WillpowerPropertyID:
                    return EveAttribute.Willpower;
                default:
                    return EveAttribute.None;
            }
        }
    }
}
