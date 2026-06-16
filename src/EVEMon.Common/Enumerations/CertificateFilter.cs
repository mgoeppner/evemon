using System.ComponentModel;

namespace EVEMon.Common.Enumerations
{
    public enum CertificateFilter
    {
        [Description("All")]
        All = 0,

        [Description("Completed")]
        Completed = 1,

        [Description("Hide Completed")]
        HideMaxLevel = 2,

        [Description("Trainable Next Level")]
        NextLevelTrainable = 3,

        [Description("Untrainable Next Level")]
        NextLevelUntrainable = 4
    }
}
