using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using HtmlTags;
using NUnit.Framework;
using FubuCore.Reflection;
using FubuTestingSupport;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class AuthenticationSettingsTester
    {
        [Test]
        public void has_to_be_application_level()
        {
            typeof(AuthenticationSettings).HasAttribute<ApplicationLevelAttribute>()
                .ShouldBeTrue();
        }

        [Test]
        public void excludes_is_always_false_with_no_exclusions()
        {
            var settings = new AuthenticationSettings();
            settings.ShouldBeExcluded(new BehaviorChain()).ShouldBeFalse();
        }

        [Test]
        public void automatically_excludes_the_NotAuthenticated_attribute()
        {
            var chain = new BehaviorChain();
            chain.AddToEnd(ActionCall.For<AuthenticatedEndpoints>(x => x.get_notauthenticated()));

            new AuthenticationSettings().ShouldBeExcluded(chain)
                .ShouldBeTrue();
        }

        [Test]
        public void apply_a_custom_exclusion()
        {
            var chain = new BehaviorChain();
            chain.AddToEnd(ActionCall.For<AuthenticatedEndpoints>(x => x.get_tag()));


            var settings = new AuthenticationSettings();

            settings.ShouldBeExcluded(chain).ShouldBeFalse();

            settings.ExcludeChains.ResourceTypeIs<HtmlTag>();

            settings.ShouldBeExcluded(chain).ShouldBeTrue();

        }
    }

    public class AuthenticatedEndpoints
    {
        [NotAuthenticated]
        public string get_notauthenticated()
        {
            return "anything";
        }
        
        public string get_authenticated()
        {
            return "else";
        }

        public HtmlTag get_tag()
        {
            return new HtmlTag("div");
        }
    }
}