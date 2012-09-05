using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FubuCore.Util;
using FubuMVC.Core;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Security;

namespace FubuMVC.Authentication
{
    public class ApplyAuthentication : IFubuRegistryExtension
    {
        private bool _includeEndpoints;
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
        /// By default, you get ~/login and ~/logout with extensibility points. Use this method to turn them off.
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

            if(_includeEndpoints)
            {
                registry.Actions.FindWith<AuthenticationActionSource>();
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
    }
}