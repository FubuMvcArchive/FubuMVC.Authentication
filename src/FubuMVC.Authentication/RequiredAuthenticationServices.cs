using System;
using System.Collections.Generic;
using Bottles.Configuration;
using StructureMap;

namespace FubuMVC.Authentication
{
    public class RequiredAuthenticationServices : IBottleConfigurationRule
    {
        private readonly IContainer _container;

        public RequiredAuthenticationServices(IContainer container)
        {
            _container = container;
        }

        public void Evaluate(BottleConfiguration configuration)
        {
            checkServices(configuration, typeof(IAuthenticationService), typeof(IPrincipalBuilder));
        }

        private void checkServices(BottleConfiguration configuration, params Type[] types)
        {
            types.Each(type =>
                           {
                               if (!_container.Model.HasDefaultImplementationFor(type))
                               {
                                   configuration.RegisterMissingService(type);
                               }
                           });
        }
    }
}