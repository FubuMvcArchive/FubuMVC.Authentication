using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FubuCore;
using FubuCore.Util;
using FubuMVC.Authentication.Basic;
using FubuMVC.Core;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Security;

namespace FubuMVC.Authentication
{
    public class ApplyAuthentication : IFubuRegistryExtension
    {
        private bool _includeEndpoints;
        private bool _useDefaults = true;
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

        void IFubuRegistryExtension.Configure(FubuRegistry registry)
        {
            registry.Services<AuthenticationServiceRegistry>();
            _internalRegistry.As<IFubuRegistryExtension>().Configure(registry);

            if(_useDefaults)
            {
               registry.Services(x =>
                                     {
                                         x.SetServiceIfNone<IAuthenticationService, FlatFileAuthenticationService>();
                                         x.SetServiceIfNone<IPrincipalBuilder, BasicFubuPrincipalBuilder>();
                                     }); 
            }

            if(_includeEndpoints)
            {
                registry.Actions.FindWith<BasicAuthenticationActionSource>();
                registry.Configure(graph => graph.Behaviors
                                            .Where(x => x.InputType() == typeof(LoginRequest))
                                            .Each(x => x.Prepend(Process.For<LoginBehavior>())));
            }
            
            registry.Policies.Add(new ApplyAuthenticationPolicy(_filters.Matches));

            registry.Policies.Add(new ReorderBehaviorsPolicy{
                WhatMustBeBefore = node => node is AuthenticationFilterNode,
                WhatMustBeAfter = node => node is AuthorizationNode
            });
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
    }
}