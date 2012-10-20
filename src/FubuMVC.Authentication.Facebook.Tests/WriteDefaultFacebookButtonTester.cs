using FubuCore;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Facebook.Tests
{
    [TestFixture]
    public class WriteDefaultFacebookButtonTester
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            theNode = new WriteDefaultFacebookButton();
        }

        #endregion

        private WriteDefaultFacebookButton theNode;

        [Test]
        public void applies_to_html()
        {
            theNode.Mimetypes.ShouldHaveTheSameElementsAs(MimeType.Html.Value);
        }

        [Test]
        public void applies_to_login_request()
        {
            theNode.ResourceType.ShouldEqual(typeof(FacebookLoginRequest));
        }

        [Test]
        public void builds_the_default_login_request_writer()
        {
            var def = theNode.As<IContainerModel>().ToObjectDef();
            def.FindDependencyDefinitionFor(typeof(IMediaWriter<FacebookLoginRequest>)).Type.ShouldEqual(
                typeof(DefaultFacebookLoginRequestWriter));
        }
    }
}