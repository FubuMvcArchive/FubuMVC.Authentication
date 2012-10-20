using FubuMVC.Core.Registration;

namespace FubuMVC.Authentication.OAuth
{
    public class OAuthServiceRegistry : ServiceRegistry
    {
        public OAuthServiceRegistry()
        {
            SetServiceIfNone<ISystemUrls, SystemUrls>();
        }
    }
}