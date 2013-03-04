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
        public void Remember_me()
        {
            runner.RunAndAssertTest("Logins/Remember me");
        }

        [TestFixtureTearDown]
        public void TeardownRunner()
        {
            runner.Dispose();
        }
    }
}