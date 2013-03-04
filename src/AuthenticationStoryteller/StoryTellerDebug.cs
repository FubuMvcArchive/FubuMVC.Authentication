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
            runner.Project.TimeoutInSeconds = 240;
        }

        [Test]
        public void Log_in_unsuccessfully()
        {
            runner.RunAndAssertTest("Logins/Log in unsuccessfully");
        }

        [TestFixtureTearDown]
        public void TeardownRunner()
        {
            runner.Dispose();
        }
    }
}