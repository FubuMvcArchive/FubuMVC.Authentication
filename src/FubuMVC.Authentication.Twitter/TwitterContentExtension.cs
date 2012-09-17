using System.Collections.Generic;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.UI.Extensibility;
using FubuMVC.Core.View;

namespace FubuMVC.Authentication.Twitter
{
    // TODO -- Make a reusable piece to do partials
    public class TwitterContentExtension : IContentExtension<LoginRequest>
    {
        public IEnumerable<object> GetExtensions(IFubuPage<LoginRequest> page)
        {
            var partial = page.Get<IPartialFactory>().BuildPartial(typeof (TwitterLoginRequest));
            var output = page.Get<IOutputWriter>().Record(() => partial.InvokePartial());

            yield return output.GetText();
        }
    }
}