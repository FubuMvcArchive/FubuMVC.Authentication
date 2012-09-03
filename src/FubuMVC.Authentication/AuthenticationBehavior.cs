using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Behaviors;

namespace FubuMVC.Authentication
{
    public class AuthenticationBehavior : IActionBehavior
    {
        private readonly IAuthenticationFilter _filter;
        private readonly IActionBehavior _inner;
    	private readonly IEnumerable<IAuthenticationRedirect> _redirects;

        public AuthenticationBehavior(IAuthenticationFilter filter, IActionBehavior inner, IEnumerable<IAuthenticationRedirect> redirects)
        {
            _filter = filter;
            _inner = inner;
        	_redirects = redirects;
        }

        public void Invoke()
        {
			if (_inner == null) return;

        	var result = _filter.Authenticate();
			if(result == AuthenticationFilterResult.Continue)
			{
				_inner.Invoke();
				return;
			}

        	_redirects
				.Last(x => x.Applies())
				.Redirect();
        }

        public void InvokePartial()
        {
            _inner.InvokePartial();
        }
    }
}