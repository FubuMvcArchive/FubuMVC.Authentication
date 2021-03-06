using System.Security.Principal;
using System.Threading;
using NUnit.Framework;

using FubuTestingSupport;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class ThreadPrincipalContextTester
    {
        [Test]
        public void set_the_principal()
        {
            var principal = new GenericPrincipal(new GenericIdentity("somebody"), new string[0]);

            var context = new ThreadPrincipalContext();

            context.Current = principal;

            Thread.CurrentPrincipal.ShouldBeTheSameAs(principal);

            
        }

        [Test]
        public void get_the_principal()
        {
            var principal = new GenericPrincipal(new GenericIdentity("somebody"), new string[0]);

            var context = new ThreadPrincipalContext();

            Thread.CurrentPrincipal = principal;

            context.Current.ShouldBeTheSameAs(principal);
        }

    }
}