using System.Collections;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using EVEMon.Common.Resources.Skill_Select;

namespace EVEMon.Common.Helpers
{
    /// <summary>
    /// Provides the skill browser icon sets from the resources embedded in this assembly.
    /// </summary>
    public static class SkillIconSets
    {
        /// <summary>
        /// Gets the icon set for the given group index, using the default set for missing icons.
        /// </summary>
        /// <param name="index">The icon group index (1-based, see <see cref="IconSettings"/>).</param>
        /// <returns>The icon set, or null when the index or its resources are not available.
        /// The caller takes ownership of the returned list.</returns>
        public static ImageList GetIconSet(int index)
        {
            string groupname = string.Empty;

            if (index > 0 && index < IconSettings.Default.Properties.Count)
            {
                SettingsProperty groupProperty = IconSettings.Default.Properties["Group" + index];
                if (groupProperty != null)
                    groupname = groupProperty.DefaultValue.ToString();
            }

            if (string.IsNullOrEmpty(groupname))
                return null;

            ImageList tempImageList = null;
            try
            {
                tempImageList = new ImageList { ColorDepth = ColorDepth.Depth32Bit };

                if (!TryAddIcons(tempImageList, "Default") || !TryAddIcons(tempImageList, groupname))
                    return null;

                ImageList iconSet = tempImageList;
                tempImageList = null;
                return iconSet;
            }
            finally
            {
                tempImageList?.Dispose();
            }
        }

        /// <summary>
        /// Adds the icons of the given embedded resource set, replacing icons already present.
        /// </summary>
        /// <param name="imageList">The image list to fill.</param>
        /// <param name="baseName">The resource base name (each icon set resx is embedded
        /// with a flat logical name, e.g. "Pentagrams.resources").</param>
        /// <returns>False when the resource set does not exist.</returns>
        private static bool TryAddIcons(ImageList imageList, string baseName)
        {
            ResourceManager manager = new ResourceManager(baseName, typeof(IconSettings).Assembly);
            ResourceSet resourceSet = manager.GetResourceSet(CultureInfo.InvariantCulture,
                createIfNotExists: true, tryParents: false);
            if (resourceSet == null)
                return false;

            foreach (DictionaryEntry entry in resourceSet)
            {
                if (entry.Value is Icon icon)
                {
                    string key = entry.Key.ToString();
                    imageList.Images.RemoveByKey(key);
                    imageList.Images.Add(key, icon);
                }
            }

            return true;
        }
    }
}
