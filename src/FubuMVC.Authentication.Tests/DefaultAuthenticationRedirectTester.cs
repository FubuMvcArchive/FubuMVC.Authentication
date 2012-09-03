using FubuTestingSupport;
using NUnit.Framework;


namespace FubuMVC.Authentication.Tests
{
	[TestFixture]
	public class DefaultAuthenticationRedirectTester : InteractionContext<DefaultAuthenticationRedirect>
	{
		[Test]
		public void always_matches()
		{
			ClassUnderTest.Applies().ShouldBeTrue();
		}
	}
}