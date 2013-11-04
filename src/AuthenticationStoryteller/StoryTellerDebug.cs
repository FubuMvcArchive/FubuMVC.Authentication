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
        public void Sliding_expiration_is_OFF()
        {
            runner.RunAndAssertTest("Logins/Expiration/Sliding expiration is OFF");
        }

        [TestFixtureTearDown]
        public void TeardownRunner()
        {
            runner.Dispose();
        }
    }
}