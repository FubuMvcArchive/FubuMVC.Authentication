using System.Collections.Generic;
using FubuCore;
using FubuMVC.ContentExtensions;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.UI;
using HtmlTags;

namespace FubuMVC.Authentication.Endpoints
{
    public class DefaultLoginRequestWriter : IMediaWriter<LoginRequest>
    {
        private readonly IServiceLocator _services;
        private readonly IFubuRequest _request;
        private readonly IOutputWriter _writer;

        public DefaultLoginRequestWriter(IServiceLocator services, IOutputWriter writer, IFubuRequest request)
        {
            _services = services;
            _writer = writer;
            _request = request;
        }

        public void Write(string mimeType, LoginRequest resource)
        {
            var document = BuildView(resource);
            _writer.WriteHtml(document.ToString());
        }

        public virtual HtmlDocument BuildView(LoginRequest request)
        {
            // TODO -- Revisit all of this when we get HTML conventions everywhere
            var view = new FubuHtmlDocument<LoginRequest>(_services, _request);
            var form = view.FormFor<LoginRequest>();
            form.Append(new HtmlTag("legend").Text(LoginKeys.Login));

            if(request.Message.IsNotEmpty())
            {
                form.Append(new HtmlTag("p").Text(request.Message).Style("color", "red"));
            }

            form.Append(view.Edit(x => x.UserName));
            form.Append(view.Edit(x => x.Password));
            form.Append(view.Edit(x => x.RememberMe));
            form.Append(view.DisplayFor(x => x.Message).Id("login-message"));

            form.Append(new HtmlTag("input").Attr("type", "submit").Attr("value", LoginKeys.Login).Id("login-submit"));
            
            view.Add(form);
            view.Add(new LiteralTag(view.WriteExtensions().ToHtmlString()));

            return view;
        }

        public IEnumerable<string> Mimetypes
        {
            get { yield return MimeType.Html.Value; }
        }
    }
}