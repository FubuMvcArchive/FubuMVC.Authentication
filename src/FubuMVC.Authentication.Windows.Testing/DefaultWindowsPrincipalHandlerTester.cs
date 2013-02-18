using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Windows.Testing
{
    [TestFixture]
    public class DefaultWindowsPrincipalHandlerTester
    {
        [Test]
        public void authenticated_has_to_return_true()
        {
            new DefaultWindowsPrincipalHandler().Authenticated(null).ShouldBeTrue();
        }
    }
}