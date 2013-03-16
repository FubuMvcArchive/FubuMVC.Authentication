using FubuCore;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.WindowsLive.Tests
{
    [TestFixture]
    public class WriteDefaultWindowsLiveButtonTester
    {
        #region Setup/Teardown

        [SetUp]
        public void SetUp()
        {
            theNode = new WriteDefaultWindowsLiveButton();
        }

        #endregion

        private WriteDefaultWindowsLiveButton theNode;

        [Test]
        public void applies_to_html()
        {
            theNode.Mimetypes.ShouldHaveTheSameElementsAs(MimeType.Html.Value);
        }

        [Test]
        public void applies_to_login_request()
        {
            theNode.ResourceType.ShouldEqual(typeof(WindowsLiveLoginRequest));
        }

        [Test]
        public void builds_the_default_login_request_writer()
        {
            var def = theNode.As<IContainerModel>().ToObjectDef();
            def.FindDependencyDefinitionFor(typeof(IMediaWriter<WindowsLiveLoginRequest>)).Type.ShouldEqual(
                typeof(DefaultWindowsLiveLoginRequestWriter));
        }
    }
}