using FubuCore;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Urls;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Facebook.Tests
{
    [TestFixture]
    public class DefaultFacebookLoginRequestWriterTester : InteractionContext<DefaultFacebookLoginRequestWriter>
    {
        private string theTag;
        private FacebookSignIn theSignIn;
        private FacebookLoginRequest theRequest;
        private string theUrl;

        protected override void beforeEach()
        {
            theUrl = "login/test";
            theRequest = new FacebookLoginRequest();
            theSignIn = new FacebookSignIn();

            MockFor<IFubuRequest>().Stub(x => x.Get<FacebookSignIn>()).Return(theSignIn);

            MockFor<IUrlRegistry>().Stub(x => x.UrlFor(theSignIn)).Return(theUrl);

            ClassUnderTest.Write(MimeType.Html.Value, theRequest);

            string html = MimeType.Html.ToString();
            theTag = MockFor<IOutputWriter>()
                .GetArgumentsForCallsMadeOn(x => x.Write(Arg<string>.Is.Same(html), Arg<string>.Is.NotNull))
                [0][1].As<string>();
        }

        [Test]
        public void applies_to_html()
        {
            ClassUnderTest.Mimetypes.ShouldHaveTheSameElementsAs(MimeType.Html.Value);
        }

        [Test]
        public void writes_the_html_tag()
        {
            theTag.ShouldEqual("<a href=\"{0}\">{1}</a>".ToFormat(theUrl, FacebookLoginKeys.LoginWithFacebook.ToString()));
        }
    }
}