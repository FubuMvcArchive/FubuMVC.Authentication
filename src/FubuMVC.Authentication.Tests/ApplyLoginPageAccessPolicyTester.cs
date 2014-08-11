using System;
using System.Linq;
using System.Linq.Expressions;
using FubuMVC.Authentication.Endpoints;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class ApplyLoginPageAccessPolicyTester
    {
        [Test]
        public void prepends_login_page_access_filter_to_get_login()
        {
            var thePolicy = new ApplyLoginPageAccessPolicy();

            var chain = chainFor<LoginController>(x => x.get_login(null));
            var graph = new BehaviorGraph();

            graph.AddChain(chain);

            thePolicy.Configure(graph);

            var filter = chain.OfType<ActionFilter>().Single();
            filter.HandlerType.ShouldEqual(typeof (LoginPageAccessFilter));
        }

        [Test]
        public void does_not_prepend_login_page_access_filter_to_other_chains()
        {
            var thePolicy = new ApplyLoginPageAccessPolicy();

            var chain = chainFor<LoginController>(x => x.post_login(null));
            var graph = new BehaviorGraph();

            graph.AddChain(chain);
            
            thePolicy.Configure(graph);

            chain.OfType<ActionFilter>().FirstOrDefault().ShouldBeNull();
        }

        private static BehaviorChain chainFor<T>(Expression<Func<T, object>> expression)
        {
            var chain = new BehaviorChain();
            chain.AddToEnd(ActionCall.For(expression));
            return chain;
        }
    }
}
