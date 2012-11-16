using System.Collections.Generic;
using FubuMVC.ContentExtensions;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.View;

namespace FubuMVC.Authentication.OAuth
{
    public class OAuthContentExtension<TLoginRequest> : IContentExtension<LoginRequest> where TLoginRequest : IOAuthLoginRequest
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