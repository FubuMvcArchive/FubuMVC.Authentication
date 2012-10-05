using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FubuCore;
using FubuCore.Util;
using FubuMVC.Authentication.Tickets.Basic;
using FubuMVC.Authentication.Windows;
using FubuMVC.ContentExtensions;
using FubuMVC.Core;
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
        private CookieSettings _settings = new CookieSettings();
        private FubuPackageRegistry _internalRegistry = new FubuPackageRegistry();
        private readonly CompositeFilter<BehaviorChain> _filters = new CompositeFilter<BehaviorChain>();

        public ApplyAuthentication()
        {
            // Start by matching everything
            Include(x => true);

            // By default, we'll use the built-in endpoints
            _includeEndpoints = true;
        }

        public ApplyAuthentication Include(Expression<Func<BehaviorChain, bool>> filter)
        {
            _filters.Includes += filter;
            return this;
        }

        public ApplyAuthentication Exclude(Expression<Func<BehaviorChain, bool>> filter)
        {
            _filters.Excludes += filter;
            return this;
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
            registry.Services<AuthenticationServiceRegistry>();
            registry.Services(x => x.SetServiceIfNone(_settings));
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
            
            registry.Policies.Add(new ApplyAuthenticationPolicy(_filters.Matches));

            registry.Policies.Add(new ReorderBehaviorsPolicy{
                WhatMustBeBefore = node => node is AuthenticationFilterNode,
                WhatMustBeAfter = node => node is AuthorizationNode
            });
        }

        // TODO -- This goes away when the settings collection makes it into the service registry
        public void AlterSettings(Action<CookieSettings> configure)
        {
            configure(_settings);
        }

        public void AuthenticateWith<T>() where T : IAuthenticationService
        {
            _internalRegistry.Services(x => x.SetServiceIfNone<IAuthenticationService, T>());
            _useDefaults = false;
        }

        public void BuildPrincipalWith<T>() where T : IPrincipalBuilder
        {
            _internalRegistry.Services(x => x.SetServiceIfNone<IPrincipalBuilder, T>());
            _useDefaults = false;
        }

        public void RedirectWith<T>() where T : IAuthenticationRedirect
        {
            _internalRegistry.Services(x => x.AddService<IAuthenticationRedirect, T>());
        }
    }
}