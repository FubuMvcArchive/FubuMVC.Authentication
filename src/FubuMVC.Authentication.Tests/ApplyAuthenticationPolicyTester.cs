using System;
using System.Linq;
using System.Linq.Expressions;
using FubuMVC.Core.Assets.Http;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class ApplyAuthenticationPolicyTester
    {
        [Test]
        public void exempt_includes_models_with_the_NotAuthenticatedAttribute()
        {
            var chain = chainFor<TestAuthenticationEndpoint<NotAuthenticatedModel>>(x => x.get_something(null));
            ApplyAuthenticationPolicy.ExemptedFromAuthentication(chain).ShouldBeTrue();
        }

        [Test]
        public void exempt_includes_calls_with_the_NotAuthenticationAttribute()
        {
            var chain = chainFor<NotAuthenticatedEndpoint>(x => x.get_something());
            ApplyAuthenticationPolicy.ExemptedFromAuthentication(chain).ShouldBeTrue();
        }

        [Test]
        public void exempt_includes_asset_writer_calls()
        {
            var chain = chainFor<AssetWriter>(x => x.Write(null));
            ApplyAuthenticationPolicy.ExemptedFromAuthentication(chain).ShouldBeTrue();
        }

        [Test]
        public void exempt_excludes_everything_else()
        {
            var chain = chainFor<TestAuthenticationEndpoint<AuthenticatedModel>>(x => x.get_something(null));
            ApplyAuthenticationPolicy.ExemptedFromAuthentication(chain).ShouldBeFalse();
        }

        [Test]
        public void prepends_the_authentication_node()
        {
            var thePolicy = new ApplyAuthenticationPolicy(x => true);
            
            var chain = chainFor<TestAuthenticationEndpoint<AuthenticatedModel>>(x => x.get_something(null));
            var graph = new BehaviorGraph();

            graph.AddChain(chain);

            thePolicy.Configure(graph);

            chain.First().ShouldBeOfType<AuthenticationFilterNode>();
        }

        [Test]
        public void uses_the_filter()
        {
            var thePolicy = new ApplyAuthenticationPolicy(x => false);

            var chain = chainFor<TestAuthenticationEndpoint<AuthenticatedModel>>(x => x.get_something(null));
            var graph = new BehaviorGraph();

            graph.AddChain(chain);

            thePolicy.Configure(graph);

            chain.ShouldHaveTheSameElementsAs(chain.FirstCall());
        }

        private static BehaviorChain chainFor<T>(Expression<Func<T, object>> expression)
        {
            var chain = new BehaviorChain();
            chain.AddToEnd(ActionCall.For(expression));
            return chain;
        }

        private static BehaviorChain chainFor<T>(Expression<Action<T>> expression)
        {
            var chain = new BehaviorChain();
            chain.AddToEnd(ActionCall.For(expression));
            return chain;
        }
    }
}