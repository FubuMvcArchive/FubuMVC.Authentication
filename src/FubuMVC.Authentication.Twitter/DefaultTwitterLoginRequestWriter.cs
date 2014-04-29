using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Urls;
using HtmlTags;

namespace FubuMVC.Authentication.Twitter
{
    public class DefaultTwitterLoginRequestWriter : IMediaWriter<TwitterLoginRequest>
    {
        public void Write(string mimeType, IFubuRequestContext context, TwitterLoginRequest resource)
        {
            var request = context.Models.Get<TwitterSignIn>();
            var tag = new HtmlTag("a").Attr("href", context.Services.GetInstance<IUrlRegistry>().UrlFor(request)).Text(TwitterLoginKeys.LoginWithTwitter);
            context.Writer.WriteHtml(tag.ToString());
        }

        public IEnumerable<string> Mimetypes
        {
            get { yield return MimeType.Html.Value; }
        }
    }
}