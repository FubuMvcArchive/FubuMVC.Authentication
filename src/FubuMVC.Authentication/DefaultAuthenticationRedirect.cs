using FubuMVC.Core.Continuations;
using FubuMVC.Core.Http;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Authentication
{
	// TODO -- this is a bit hackey because of FubuMVC limitations. The ContinuationHandler bit is just too painful to test
	public class DefaultAuthenticationRedirect : IAuthenticationRedirect
	{
		private readonly IFubuRequest _request;
		private readonly ContinuationHandler _handler;
		private readonly ICurrentHttpRequest _currentRequest;

		public DefaultAuthenticationRedirect(IFubuRequest request, ContinuationHandler handler, ICurrentHttpRequest currentRequest)
		{
			_request = request;
			_handler = handler;
			_currentRequest = currentRequest;
		}

		public bool Applies()
		{
			return true;
		}

		public void Redirect()
		{
			var continuation = FubuContinuation.RedirectTo(new LoginRequest
			{
				Url = _currentRequest.RelativeUrl()
			});

			_request.Set(continuation);
			_handler.Invoke();
		}
	}
}