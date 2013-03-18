using FubuLocalization;

namespace FubuMVC.Authentication.WindowsLive
{
    public class WindowsLiveLoginKeys : StringToken
    {
        public static readonly WindowsLiveLoginKeys LoginWithWindowsLive = new WindowsLiveLoginKeys("Login with Windows Live");

        protected WindowsLiveLoginKeys(string defaultValue)
            : base(null, defaultValue, namespaceByType: true)
        {
        }
    }
}