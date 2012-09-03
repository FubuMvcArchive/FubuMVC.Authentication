using NUnit.Framework;

using FubuTestingSupport;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class LoginRequestTester
    {
        [Test]
        public void status_is_not_authenticated_by_default()
        {
            new LoginRequest().Status.ShouldEqual(LoginStatus.NotAuthenticated);
        }
    }
}