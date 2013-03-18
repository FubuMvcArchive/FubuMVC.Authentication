using System.Collections.Generic;
using FubuMVC.ContentExtensions;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.View;

namespace FubuMVC.Authentication.OAuth2
{
    public class OAuth2ContentExtension<TLoginRequest> : IContentExtension<LoginRequest> where TLoginRequest : IOAuth2LoginRequest
    {
        public IEnumerable<object> GetExtensions(IFubuPage<LoginRequest> page)
        {
            var chain = page.Get<BehaviorGraph>().BehaviorFor(typeof(TLoginRequest));
            var output = page.Get<IOutputWriter>().Record(() =>
                {
                    var partial = page.Get<IPartialFactory>().BuildPartial(chain);
                    partial.InvokePartial();
                });

            yield return output.GetText();
        }
    }
}