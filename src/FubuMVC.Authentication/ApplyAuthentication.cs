using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FubuCore;
using FubuCore.Descriptions;
using FubuCore.Util;
using FubuMVC.Authentication.Tickets.Basic;
using FubuMVC.Authentication.Windows;
using FubuMVC.ContentExtensions;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Security;

namespace FubuMVC.Authentication
{
    public class ApplyAuthentication : IFubuRegistryExtension
    {
        private bool _includeEndpoints;
        private bool _includeWindowsAuth;
        private bool _useDefaults = true;
        private FubuPackageRegistry _internalRegistry = new FubuPackageRegistry();

        public ApplyAuthentication()
        {
            // By default, we'll use the built-in endpoints
            _includeEndpoints = true;
        }

        /// <summary>
        /// By default, you get ~/login and ~/logout (with extensibility points). Use this method to turn them off.
        /// If you use the defaults, just create a view against <see cref="FubuMVC.Authentication.LoginRequest"/>.
        /// </summary>
        /// <returns></returns>
        public ApplyAuthentication DoNotIncludeEndpoints()
        {
            _includeEndpoints = false;
            return this;
        }

        public ApplyAuthentication IncludeWindowsAuthentication()
        {
            _includeWindowsAuth = true;
            return this;
        }

        void IFubuRegistryExtension.Configure(FubuRegistry registry)
        {
            // GOOD
            registry.Services<AuthenticationServiceRegistry>();
            registry.Policies.Add(new ApplyAuthenticationPolicy());




            _internalRegistry.As<IFubuRegistryExtension>().Configure(registry);

            if(_useDefaults)
            {
               registry.Services(x => x.SetServiceIfNone<IAuthenticationService, FlatFileAuthenticationService>()); 
            }

            if(_includeEndpoints)
            {
                registry.Actions.FindWith<BasicAuthenticationActionSource>();
                registry.Configure(graph => graph.Behaviors
                                            .Where(x => x.InputType() == typeof(LoginRequest))
                                            .Each(x => x.Prepend(Process.For<LoginBehavior>())));
                registry.Policies.Add<AttachDefaultLoginView>();
            }

            if(_includeWindowsAuth)
            {
                registry.Actions.FindWith<WindowsActionSource>();
                registry.Services<WindowsRegistry>();

                registry.Extensions().For(new WindowsLoginExtension());
            }
        }

    }
}