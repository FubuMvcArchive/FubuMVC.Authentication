using System.Collections.Generic;
using System.Net;
using FubuCore;
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

		protected override void beforeEach()
		{
			theUrl = "test/login";

			MockFor<IUrlRegistry>().Stub(x => x.UrlFor(typeof (LoginRequest)))
				.Return(theUrl);

			ClassUnderTest.Redirect();

			theValues = (IDictionary<string, object>) MockFor<IJsonWriter>()
			                                          	.GetArgumentsForCallsMadeOn(x => x.Write(null, null), x => x.IgnoreArguments())[0][0];
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
			MockFor<IJsonWriter>()
				.GetArgumentsForCallsMadeOn(x => x.Write(null, null), x => x.IgnoreArguments())[0][1]
				.ShouldEqual(MimeType.Json.ToString());
		}

		[Test]
		public void sets_the_status_code_to_unauthorized()
		{
			MockFor<IOutputWriter>().AssertWasCalled(x => x.WriteResponseCode(HttpStatusCode.Unauthorized));
		}
	}
}