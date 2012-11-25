using System;
using System.Collections.Generic;
using FubuCore;
using FubuMVC.Core.Continuations;
using System.Linq;

namespace FubuMVC.Authentication
{
    public interface IAuthenticationRedirector
    {
        FubuContinuation Redirect();
    }

    public class AuthenticationRedirector : IAuthenticationRedirector
    {
        private readonly RedirectLibrary _library;
        private readonly IServiceLocator _services;

        public AuthenticationRedirector(RedirectLibrary library, IServiceLocator services)
        {
            _library = library;
            _services = services;
        }

        public FubuContinuation Redirect()
        {
            var redirector = _library.GetRedirectTypes()
                .Select(x =>  _services.GetInstance(x).As<IAuthenticationRedirect>())
                .FirstOrDefault(x => x.Applies());

            return redirector.Redirect();
        }
    }

    public class RedirectLibrary
    {
        private readonly IList<Type> _redirectTypes = new List<Type>();

        public void Add<T>() where T : IAuthenticationRedirect
        {
            _redirectTypes.Add(typeof(T));
        }

        public IEnumerable<Type> GetRedirectTypes()
        {
            foreach (var redirectType in _redirectTypes)
            {
                yield return redirectType;
            }

            yield return typeof (AjaxAuthenticationRedirect);
            yield return typeof (DefaultAuthenticationRedirect);
        } 
    }

    public class AuthenticationFilter
    {
        private readonly IPrincipalBuilder _builder;
        private readonly IPrincipalContext _context;
        private readonly IAuthenticationRedirector _redirector;
        private readonly IAuthenticationSession _session;

        public AuthenticationFilter(IAuthenticationSession session, IPrincipalBuilder builder, IPrincipalContext context, IAuthenticationRedirector redirector)
        {
            _session = session;
            _builder = builder;
            _context = context;
            _redirector = redirector;
        }

        public FubuContinuation Authenticate()
        {
            var userName = _session.PreviouslyAuthenticatedUser();
            if (userName.IsNotEmpty())
            {
                _session.MarkAccessed();
                var principal = _builder.Build(userName);
                _context.Current = principal;

            	return FubuContinuation.NextBehavior();
            }

            return _redirector.Redirect();
        }

        
    }
}