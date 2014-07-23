using System.Collections.Generic;
using System.Net;
using FubuCore;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Urls;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;


namespace FubuMVC.Authentication.Tests
{
	[TestFixture]
	public class when_redirecting_an_unauthenticated_ajax_request : InteractionContext<AjaxAuthenticationRedirect>
	{
		private string theUrl;
		private IDictionary<string, object> theValues;
	    private FubuContinuation theContinuation;

	    protected override void beforeEach()
		{
			theUrl = "test/login";

			MockFor<IUrlRegistry>().Stub(x => x.UrlFor(new LoginRequest(), "GET"))
				.Return(theUrl);


	        theValues = ClassUnderTest.BuildAjaxContinuation().ToDictionary();
			theContinuation = ClassUnderTest.Redirect();

		}

	    [Test]
	    public void the_continuation_should_be_stop()
	    {
            theContinuation.AssertWasEndedWithStatusCode(HttpStatusCode.Unauthorized);
	    }

		[Test]
		public void sets_the_navigate_page_property()
		{
			theValues["navigatePage"].ShouldEqual(theUrl);
		}

		[Test]
		public void sets_the_success_flag()
		{
			theValues["success"].As<bool>().ShouldBeFalse();
		}

		[Test]
		public void writes_the_json_mimetype()
		{
			MockFor<IOutputWriter>()
				.GetArgumentsForCallsMadeOn(x => x.Write(MimeType.Json, null), x => x.IgnoreArguments())[0][0]
				.ShouldEqual(MimeType.Json.Value);
		}

	}
}