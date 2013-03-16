using System.Linq;
using System.Reflection;
using FubuMVC.Core;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Facebook.Tests
{
    [TestFixture]
    public class AssemblyNeedsTheFubuModuleAttribute
    {
        [Test]
        public void the_attribute_exists()
        {
            Assembly assembly = typeof (FacebookController).Assembly;

            assembly.GetCustomAttributes(typeof (FubuModuleAttribute), true)
                .Any().ShouldBeTrue();
        }
    }
}