using System.Linq;
using FubuCore;
using FubuMVC.Authentication.Basic;
using FubuMVC.Core.Packaging;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Tests.Basic
{
    [TestFixture]
    public class FlatFileAuthenticationServiceTester
    {
        private FlatFileAuthenticationService theService;

        [SetUp]
        public void SetUp()
        {
            theService = new FlatFileAuthenticationService();
        }

        [TearDown]
        public void Teardown()
        {
            new FileSystem().DeleteFile(theService.PasswordConfigFile);
        }

        private bool authenticate(string user, string pass)
        {
            var request = new LoginRequest
                              {
                                  UserName = user,
                                  Password = pass
                              };

            return theService.Authenticate(request);
        }

        [Test]
        public void default_user_and_pass_exists_when_file_does_not_exist()
        {
            authenticate(FlatFileAuthenticationService.DefaultUser, FlatFileAuthenticationService.DefaultPass).ShouldBeTrue();
        }

        [Test]
        public void authenticates_successfully()
        {
            theService.WriteUser("test", "1234");
            authenticate("test", "1234").ShouldBeTrue();
        }

        [Test]
        public void authenticates_with_no_password_successfully()
        {
            theService.WriteUser("test", "");
            authenticate("test", "").ShouldBeTrue();
        }

        [Test]
        public void bad_username()
        {
            theService.WriteUser("test", "test");
            authenticate("someone", "else").ShouldBeFalse();
        }

        [Test]
        public void bad_password()
        {
            theService.WriteUser("test", "1324");
            authenticate("test", "blah").ShouldBeFalse();
        }

        [Test]
        public void bad_empty_password()
        {
            theService.WriteUser("blah", "1234");
            authenticate("blah", null).ShouldBeFalse();
        }
    }
}