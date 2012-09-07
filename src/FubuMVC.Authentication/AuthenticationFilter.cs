using FubuCore;

namespace FubuMVC.Authentication
{
    public class AuthenticationFilter : IAuthenticationFilter
    {
        private readonly IPrincipalBuilder _builder;
        private readonly IPrincipalContext _context;
    	private readonly IAuthenticationSession _session;

        public AuthenticationFilter(IAuthenticationSession session, IPrincipalBuilder builder, IPrincipalContext context)
        {
            _session = session;
            _builder = builder;
            _context = context;
        }

        public AuthenticationFilterResult Authenticate()
        {
            var userName = _session.PreviouslyAuthenticatedUser();
            if (userName.IsNotEmpty())
            {
                _session.MarkAccessed();
                var principal = _builder.Build(userName);
                _context.Current = principal;

            	return AuthenticationFilterResult.Continue;
            }

            return AuthenticationFilterResult.Redirect;
        }
    }
}