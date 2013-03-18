using System.Linq;
using System.Reflection;
using FubuMVC.Core;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Github.Tests
{
    [TestFixture]
    public class AssemblyNeedsTheFubuModuleAttribute
    {
        [Test]
        public void the_attribute_exists()
        {
            Assembly assembly = typeof (GithubController).Assembly;

            assembly.GetCustomAttributes(typeof (FubuModuleAttribute), true)
                .Any().ShouldBeTrue();
        }
    }
}