using System.Collections.Generic;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.UI.Extensions;
using FubuMVC.Core.View;

namespace FubuMVC.Authentication.Twitter
{
    // TODO -- Make a reusable piece to do partials
    public class TwitterContentExtension : IContentExtension<LoginRequest>
    {
        public IEnumerable<object> GetExtensions(IFubuPage<LoginRequest> page)
        {
            var chain = page.Get<BehaviorGraph>().BehaviorFor(typeof (TwitterLoginRequest));
            var output = page.Get<IOutputWriter>().Record(() =>
            {
                var partial = page.Get<IPartialFactory>().BuildPartial(chain);
                partial.InvokePartial();
            });

            yield return output.GetText();
        }
    }
}