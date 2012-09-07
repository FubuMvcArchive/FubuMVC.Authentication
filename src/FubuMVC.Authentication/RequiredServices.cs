using System;
using System.Collections.Generic;
using Bottles.Configuration;
using FubuCore;

namespace FubuMVC.Authentication
{
    // TODO -- Maybe we make this configurable/reusable (that's a tomorrow thing)
    public class RequiredServices : IBottleConfigurationRule
    {
        private readonly IServiceLocator _services;

        public RequiredServices(IServiceLocator services)
        {
            _services = services;
        }

        public void Evaluate(BottleConfiguration configuration)
        {
            checkServices(configuration, typeof(IAuthenticationService), typeof(IPrincipalBuilder));
        }

        private void checkServices(BottleConfiguration configuration, params Type[] types)
        {
            types.Each(type =>
                           {
                               try
                               {
                                   _services.GetInstance(type);
                               }
                               catch
                               {
                                   // Yep, swallowing this one
                                   configuration.RegisterMissingService(type);
                               }
                           });
        }
    }
}