using System.Collections.Generic;
using FubuMVC.Authentication.Endpoints;
using FubuMVC.Core.UI;
using FubuMVC.Core.UI.Extensions;
using FubuMVC.Core.View;

namespace FubuMVC.Authentication.Windows
{
    public class WindowsLoginExtension : IContentExtension<LoginRequest>
    {
        public IEnumerable<object> GetExtensions(IFubuPage<LoginRequest> page)
        {
            yield return page.LinkTo(new WindowsSignInRequest { Url = page.Model.Url }).Text(LoginKeys.LoginWithWindows);
        }
    }
}