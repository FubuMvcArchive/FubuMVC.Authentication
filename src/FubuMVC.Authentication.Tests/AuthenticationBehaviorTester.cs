using FubuMVC.Core.Behaviors;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Tests.Tests
{
	[TestFixture]
	public class AuthenticationBehaviorTester : InteractionContext<AuthenticationBehavior>
	{
		private IAuthenticationRedirect r1;
		private IAuthenticationRedirect r2;

		protected override void beforeEach()
		{
			r1 = MockRepository.GenerateStub<IAuthenticationRedirect>();
			r2 = MockRepository.GenerateStub<IAuthenticationRedirect>();

			r1.Stub(x => x.Applies()).Return(true);
			r2.Stub(x => x.Applies()).Return(true);

			Services.Inject(typeof(IAuthenticationRedirect), r1);
			Services.Inject(typeof(IAuthenticationRedirect), r2);
		}

		[Test]
		public void invokes_the_inner_behavior_as_partial()
		{
			ClassUnderTest.InvokePartial();
			MockFor<IActionBehavior>().AssertWasCalled(x => x.InvokePartial());
		}

		[Test]
		public void should_invoke_the_next_behavior_if_the_filter_continues()
		{
			MockFor<IAuthenticationFilter>()
				.Stub(x => x.Authenticate())
				.Return(AuthenticationFilterResult.Continue);

			ClassUnderTest.Invoke();

			MockFor<IActionBehavior>().AssertWasCalled(x => x.Invoke());
		}

		[Test]
		public void should_not_invoke_the_next_behavior_if_the_filter_redirects()
		{
			MockFor<IAuthenticationFilter>()
				.Stub(x => x.Authenticate())
				.Return(AuthenticationFilterResult.Redirect);

			ClassUnderTest.Invoke();

			MockFor<IActionBehavior>().AssertWasNotCalled(x => x.Invoke());
		}

		[Test]
		public void no_redirects_are_invoked_for_a_continue()
		{
			MockFor<IAuthenticationFilter>()
				.Stub(x => x.Authenticate())
				.Return(AuthenticationFilterResult.Continue);

			ClassUnderTest.Invoke();

			r2.AssertWasNotCalled(x => x.Redirect());
		}

		[Test]
		public void invokes_the_last_matching_redirect()
		{
			MockFor<IAuthenticationFilter>()
				.Stub(x => x.Authenticate())
				.Return(AuthenticationFilterResult.Redirect);

			ClassUnderTest.Invoke();

			r2.AssertWasCalled(x => x.Redirect());
		}
	}
}