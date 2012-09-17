using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Twitter.Tests
{
    [TestFixture]
    public class when_the_callback_action_is_invoked : InteractionContext<TwitterController>
    {
        [Test]
        public void it_just_calls_the_twitter_callback()
        {
            ClassUnderTest.Callback(new TwitterLoginCallback());
            MockFor<ITwitterCallback>().AssertWasCalled(x => x.Execute());
        }
    }
}