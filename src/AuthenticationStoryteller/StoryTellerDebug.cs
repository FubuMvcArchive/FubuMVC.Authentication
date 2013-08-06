using NUnit.Framework;
using StoryTeller.Execution;

namespace StoryTellerTestHarness
{
    [TestFixture, Explicit]
    public class Template
    {
        private ProjectTestRunner runner;

        [TestFixtureSetUp]
        public void SetupRunner()
        {
            runner = new ProjectTestRunner(@"C:\code\FubuMVC.Authentication\src\AuthenticationStoryteller\storyteller.xml");
        }

        [Test]
        public void The_user_is_no_longer_frozen_after_the_lockout_period_is_over()
        {
            runner.RunAndAssertTest("Logins/The user is no longer frozen after the lockout period is over");
        }

        [TestFixtureTearDown]
        public void TeardownRunner()
        {
            runner.Dispose();
        }
    }
}