using System.Linq;
using System.Reflection;
using FubuMVC.Core;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.WindowsLive.Tests
{
    [TestFixture]
    public class AssemblyNeedsTheFubuModuleAttribute
    {
        [Test]
        public void the_attribute_exists()
        {
            Assembly assembly = typeof (WindowsLiveController).Assembly;

            assembly.GetCustomAttributes(typeof (FubuModuleAttribute), true)
                .Any().ShouldBeTrue();
        }
    }
}