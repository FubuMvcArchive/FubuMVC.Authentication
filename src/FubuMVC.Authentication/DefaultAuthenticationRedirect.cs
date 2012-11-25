using FubuMVC.Core.Continuations;
using FubuMVC.Core.Http;

namespace FubuMVC.Authentication
{
	public class DefaultAuthenticationRedirect : IAuthenticationRedirect
	{
		private readonly ICurrentHttpRequest _currentRequest;

		public DefaultAuthenticationRedirect(ICurrentHttpRequest currentRequest)
		{
			_currentRequest = currentRequest;
		}

		public bool Applies()
		{
			return true;
		}

		public FubuContinuation Redirect()
		{
			return FubuContinuation.RedirectTo(new LoginRequest
			{
				Url = _currentRequest.RelativeUrl()
			});
		}
	}
}