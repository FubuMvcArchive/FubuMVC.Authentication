using System.Net;
using FubuCore.Binding;
using FubuMVC.Core;
using FubuMVC.Core.Ajax;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Urls;

namespace FubuMVC.Authentication
{
	public class AjaxAuthenticationRedirect : IAuthenticationRedirect
	{
		private readonly IRequestData _data;
		private readonly IJsonWriter _jsonWriter;
		private readonly IOutputWriter _outputWriter;
		private readonly IUrlRegistry _urls;

		public AjaxAuthenticationRedirect(IRequestData data, IJsonWriter jsonWriter, IOutputWriter outputWriter, IUrlRegistry urls)
		{
			_data = data;
			_jsonWriter = jsonWriter;
			_outputWriter = outputWriter;
			_urls = urls;
		}

		public bool Applies()
		{
			return _data.IsAjaxRequest();
		}

		public FubuContinuation Redirect()
		{
			var url = _urls.UrlFor(typeof (LoginRequest));
		    var continuation = new AjaxContinuation {Success = false, NavigatePage = url};

		    _jsonWriter.Write(continuation.ToDictionary(), MimeType.Json.ToString());

		    return FubuContinuation.EndWithStatusCode(HttpStatusCode.Unauthorized);
		}
	}
}