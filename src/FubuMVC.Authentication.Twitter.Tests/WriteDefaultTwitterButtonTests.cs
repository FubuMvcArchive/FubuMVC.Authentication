using FubuCore;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Urls;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Twitter.Tests
{
    [TestFixture]
    public class WriteDefaultTwitterButtonTester
    {
        private WriteDefaultTwitterButton theNode;

        [SetUp]
        public void SetUp()
        {
            theNode = new WriteDefaultTwitterButton();
        }

        [Test]
        public void applies_to_login_request()
        {
            theNode.ResourceType.ShouldEqual(typeof(TwitterLoginRequest));
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
            def.FindDependencyDefinitionFor(typeof(IMediaWriter<TwitterLoginRequest>)).Type.ShouldEqual(
                typeof(DefaultTwitterLoginRequestWriter));
        }
    }

    [TestFixture]
    public class DefaultTwitterLoginRequestWriterTester : InteractionContext<DefaultTwitterLoginRequestWriter>
    {
        private string theTag;
        private TwitterSignIn theSignIn;
        private TwitterLoginRequest theRequest;
        private string theUrl;

        protected override void beforeEach()
        {
            theUrl = "login/test";
            theRequest = new TwitterLoginRequest();
            theSignIn = new TwitterSignIn();

            MockFor<IFubuRequest>().Stub(x => x.Get<TwitterSignIn>()).Return(theSignIn);

            MockFor<IUrlRegistry>().Stub(x => x.UrlFor(theSignIn)).Return(theUrl);

            ClassUnderTest.Write(MimeType.Html.Value, theRequest);

            var html = MimeType.Html.ToString();
            theTag = MockFor<IOutputWriter>()
                .GetArgumentsForCallsMadeOn(x => x.Write(Arg<string>.Is.Same(html), Arg<string>.Is.NotNull))
                [0][1].As<string>();
        }

        [Test]
        public void writes_the_html_tag()
        {
            theTag.ShouldEqual("<a href=\"{0}\">{1}</a>".ToFormat(theUrl, TwitterLoginKeys.LoginWithTwitter.ToString()));
        }
    }
}