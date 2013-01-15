using System.Collections.Generic;
using FubuMVC.Authentication.Endpoints;
using FubuMVC.ContentExtensions;
using FubuMVC.Core.UI;
using FubuMVC.Core.View;

namespace FubuMVC.Authentication.Windows
{
    public class WindowsLoginExtension : IContentExtension<LoginRequest>
    {
        public IEnumerable<object> GetExtensions(IFubuPage<LoginRequest> page)
        {
            yield return page.LinkTo<WindowsSignInRequest>().Text(LoginKeys.LoginWithWindows);
        }
    }
}