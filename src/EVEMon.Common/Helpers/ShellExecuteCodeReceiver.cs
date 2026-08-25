using System;
using Google.Apis.Auth.OAuth2;

namespace EVEMon.Common.Helpers
{
    /// <summary>
    /// A <see cref="LocalServerCodeReceiver"/> that opens the authorization URL through
    /// <see cref="Util.OpenURL"/> (a plain shell-execute) instead of the library's
    /// "cmd /c start" workaround, which launches nothing when running under Wine.
    /// </summary>
    public sealed class ShellExecuteCodeReceiver : LocalServerCodeReceiver
    {
        protected override bool OpenBrowser(string url)
        {
            Util.OpenURL(new Uri(url));
            return true;
        }
    }
}
