using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Windows.Testing
{
    [TestFixture]
    public class setup_with_windows_authentication
    {
        private BehaviorGraph theGraphWithWindowsAuthentication;

        [SetUp]
        public void SetUp()
        {
            var registry = new FubuRegistry();
            registry.Import<WindowsAuthFubuRegistryExtension>();

            theGraphWithWindowsAuthentication = BehaviorGraph.BuildFrom(registry);
        }

        [Test]
        public void the_windows_action_call_is_registered()
        {
            theGraphWithWindowsAuthentication.BehaviorFor<WindowsController>(x => x.Login(null)).ShouldNotBeNull();
        }

        [Test]
        public void registers_the_windows_authentication_context()
        {
            theGraphWithWindowsAuthentication.Services.DefaultServiceFor<IWindowsAuthenticationContext>()
                                             .Type.ShouldEqual(typeof (AspNetWindowsAuthenticationContext));
        }
    }
}