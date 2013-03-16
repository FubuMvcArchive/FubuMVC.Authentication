using System.Collections.Generic;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Urls;
using HtmlTags;

namespace FubuMVC.Authentication.Google
{
    public class DefaultGoogleLoginRequestWriter : IMediaWriter<GoogleLoginRequest>
    {
        private readonly IFubuRequest _request;
        private readonly IUrlRegistry _urls;
        private readonly IOutputWriter _writer;

        public DefaultGoogleLoginRequestWriter(IOutputWriter writer, IUrlRegistry urls, IFubuRequest request)
        {
            _writer = writer;
            _urls = urls;
            _request = request;
        }

        public void Write(string mimeType, GoogleLoginRequest resource)
        {
            var request = _request.Get<GoogleSignIn>();
            HtmlTag tag = new HtmlTag("a").Attr("href", _urls.UrlFor(request)).Text(GoogleLoginKeys.LoginWithGoogle);
            _writer.WriteHtml(tag.ToString());
        }

        public IEnumerable<string> Mimetypes
        {
            get { yield return MimeType.Html.Value; }
        }
    }
}