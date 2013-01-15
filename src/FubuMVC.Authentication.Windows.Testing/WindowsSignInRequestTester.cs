using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Windows.Testing
{
    [TestFixture]
    public class WindowsSignInRequestTester
    {
        private WindowsSignInRequest theRequest;

        [SetUp]
        public void SetUp()
        {
            theRequest = new WindowsSignInRequest();
        }

        [Test]
        public void url_defaults_to_root()
        {
            theRequest.Url.ShouldEqual("~/");

        }

        [Test]
        public void uses_specified_url()
        {
            theRequest.Url = "123";
            theRequest.Url.ShouldEqual("123");
        }
    }
}