using FubuCore;
using FubuMVC.Authentication.Endpoints;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;
using FubuTestingSupport;
using HtmlTags;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Tests.Endpoints
{
    [TestFixture]
    public class WriteDefaultLoginTester
    {
        private WriteDefaultLogin theNode;

        [SetUp]
        public void SetUp()
        {
            theNode = new WriteDefaultLogin();
        }

        [Test]
        public void applies_to_login_request()
        {
            theNode.ResourceType.ShouldEqual(typeof (LoginRequest));
        }

        [Test]
        public void applies_to_html()
        {
            theNode.Mimetypes.ShouldHaveTheSameElementsAs(MimeType.Html.Value);
        }

        [Test]
        public void builds_the_default_login_request_writer()
        {
            var def = theNode.As<IContainerModel>().ToObjectDef();
            def.FindDependencyDefinitionFor(typeof (IMediaWriter<LoginRequest>)).Type.ShouldEqual(
                typeof (DefaultLoginRequestWriter));
        }
    }

    [TestFixture]
    public class DefaultLoginRequestWriterTester : InteractionContext<DefaultLoginRequestWriter>
    {
        private HtmlDocument theDocument;
        private LoginRequest theRequest;

        protected override void beforeEach()
        {
            theDocument = new HtmlDocument();
            theDocument.Add(new DivTag("test"));

            theRequest = new LoginRequest();

            Services.PartialMockTheClassUnderTest();
            ClassUnderTest.Stub(x => x.BuildView(theRequest)).Return(theDocument);

            ClassUnderTest.Write(MimeType.Html.Value, theRequest);
        }

        [Test]
        public void writes_the_html_document()
        {
            MockFor<IOutputWriter>().AssertWasCalled(x => x.Write(MimeType.Html.ToString(), theDocument.ToString()));
        }
    }
}