using System.Collections.Generic;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Urls;
using HtmlTags;

namespace FubuMVC.Authentication.Twitter
{
    public class DefaultTwitterLoginRequestWriter : IMediaWriter<TwitterLoginRequest>
    {
        private readonly IOutputWriter _writer;
        private readonly IUrlRegistry _urls;
        private readonly IFubuRequest _request;

        public DefaultTwitterLoginRequestWriter(IOutputWriter writer, IUrlRegistry urls, IFubuRequest request)
        {
            _writer = writer;
            _urls = urls;
            _request = request;
        }

        public void Write(string mimeType, TwitterLoginRequest resource)
        {
            var request = _request.Get<TwitterSignIn>();
            var tag = new HtmlTag("a").Attr("href", _urls.UrlFor(request)).Text(TwitterLoginKeys.LoginWithTwitter);
            _writer.WriteHtml(tag.ToString());
        }

        public IEnumerable<string> Mimetypes
        {
            get { yield return MimeType.Html.Value; }
        }
    }
}