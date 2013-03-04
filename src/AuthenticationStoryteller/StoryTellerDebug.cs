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
            runner.Project.TimeoutInSeconds = 1000;
        }

        [Test]
        public void Sliding_expiration_success_and_failure()
        {
            runner.RunAndAssertTest("Logins/Expiration/Sliding expiration success and failure");
        }

        [TestFixtureTearDown]
        public void TeardownRunner()
        {
            runner.Dispose();
        }
    }
}