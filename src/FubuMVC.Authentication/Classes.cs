using System;
using FubuCore;
using FubuMVC.Authentication.Endpoints;
using FubuMVC.Authentication.Membership;
using FubuMVC.Core;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.ObjectGraph;

namespace FubuMVC.Authentication
{
    public class ApplyAuthentication : IFubuRegistryExtension
    {
        // TODO -- go grab AuthenticationSettings here.  
        // 

        void IFubuRegistryExtension.Configure(FubuRegistry registry)
        {
            registry.Policies.Add<RegisterAuthenticationStrategies>();

            registry.Services<AuthenticationServiceRegistry>();
            registry.Policies.Add(new ApplyAuthenticationPolicy());
            registry.Policies.Add<FormsAuthenticationEndpointsRegistration>();
            registry.Policies.Add<AttachLoginBehaviorToLoginController>();
            registry.Policies.Add<AttachDefaultLoginView>();
        }

    }

    [ConfigurationType(ConfigurationType.Discovery)]
    public class RegisterAuthenticationStrategies : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            // TODO -- 
            // pull in AuthenticationSettings, grab each AuthenticationNode
            // if none, then use the default
            throw new NotImplementedException();
        }
    }

    public class AuthenticationChain : Chain<AuthenticationNode, AuthenticationChain>
    {
    }

    public class AuthenticationNode : Node<AuthenticationNode, AuthenticationChain>, IContainerModel
    {
        private readonly Type _authType;

        public AuthenticationNode(Type authType)
        {
            if (!authType.CanBeCastTo<IAuthenticationStrategy>())
            {
                throw new ArgumentOutOfRangeException("authType", "authType must be assignable to IAuthenticationStrategy");
            }

            _authType = authType;
        }

        public Type AuthType
        {
            get { return _authType; }
        }

        ObjectDef IContainerModel.ToObjectDef()
        {
            var def = new ObjectDef(_authType);

            configure(def);

            return def;
        }

        protected virtual void configure(ObjectDef def)
        {

        }
    }


    // TODO -- build the BasicAuthenticationNode
    // TODO -- add the session
    // TODO -- add the principal builder strategy
    // TODO -- go to windows and add the node, but it has to be first
    // TODO -- go to windows and make sure there's a way to disable it
    // TODO -- windows needs its own IWindowsPrincipalSource.Build(WindowsPrincipal)
}