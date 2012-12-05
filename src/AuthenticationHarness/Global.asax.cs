using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using FubuMVC.Authentication;
using FubuMVC.Authentication.Membership;
using FubuMVC.Core;
using FubuMVC.Diagnostics.Runtime.Tracing;
using FubuMVC.StructureMap;
using StructureMap;

namespace AuthenticationHarness
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var container = new Container();
            FubuApplication.For<HarnessRegistry>().StructureMap(container).Bootstrap();

            var membership = new InMemoryMembershipRepository();
            membership.Add(new UserInfo
            {
                UserName = "jeremy",
                Password = "carthage"
            });

            container.Inject<IMembershipRepository>(membership);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }

    public class HarnessRegistry : FubuRegistry
    {
        public HarnessRegistry()
        {
            AlterSettings<AuthenticationSettings>(x => {
                x.ExcludeChains.AnyActionMatches(call => call.HandlerType.Assembly == typeof (BehaviorTracer).Assembly);
            });
        }
    }
}