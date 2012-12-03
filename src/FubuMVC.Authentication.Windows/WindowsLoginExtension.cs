using System.Collections.Generic;
using FubuMVC.Authentication.Endpoints;
using FubuMVC.ContentExtensions;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.View;
using HtmlTags;

namespace FubuMVC.Authentication.Windows
{
    public class WindowsLoginExtension : IContentExtension<LoginRequest>
    {
        public IEnumerable<object> GetExtensions(IFubuPage<LoginRequest> page)
        {
            var url = page.Urls.UrlFor(page.Get<IFubuRequest>().Get<WindowsSignInRequest>());
            yield return new HtmlTag("a").Attr("href", url).Text(LoginKeys.LoginWithWindows);
        }
    }
}