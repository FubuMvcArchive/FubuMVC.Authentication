using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Windows.Testing
{
    [TestFixture]
    public class setup_with_windows_authentication
    {
        private BehaviorGraph theGraph;

        [SetUp]
        public void SetUp()
        {
            var registry = new FubuRegistry();
            registry.Import<ApplyWindowsAuthentication>();

            theGraph = BehaviorGraph.BuildFrom(registry);
        }

        [Test]
        public void the_windows_action_call_is_registered()
        {
            theGraph.BehaviorFor<WindowsController>(x => x.Login(null)).ShouldNotBeNull();
        }

        [Test]
        public void registers_the_windows_authentication_context()
        {
            theGraph.Services.DefaultServiceFor<IWindowsAuthenticationContext>()
                                             .Type.ShouldEqual(typeof (AspNetWindowsAuthenticationContext));
        }

        [Test]
        public void registers_the_IWindowsPrincipalHandler()
        {
            theGraph
                .Services
                .DefaultServiceFor<IWindowsPrincipalHandler>()
                .Type.ShouldEqual(typeof(NulloWindowsPrincipalHandler));
        }

        [Test]
        public void registers_the_IWindowsAuthentication_service()
        {
            theGraph
                .Services
                .DefaultServiceFor<IWindowsAuthentication>()
                .Type.ShouldEqual(typeof (WindowsAuthentication));
        }
    }
}