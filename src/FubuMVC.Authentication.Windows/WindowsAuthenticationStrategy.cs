using System.Security.Principal;

namespace FubuMVC.Authentication.Windows
{
    public class WindowsAuthenticationStrategy : IAuthenticationStrategy
    {
        private readonly IPrincipalContext _context;
        private readonly IWindowsPrincipalBuilder _builder;

        public WindowsAuthenticationStrategy(IPrincipalContext context, IWindowsPrincipalBuilder builder)
        {
            _context = context;
            _builder = builder;
        }

        public bool TryToApply()
        {
            var windowsPrincipal = _context.Current as WindowsPrincipal;
            if (windowsPrincipal == null) return false;

            if (windowsPrincipal.Identity.IsAuthenticated)
            {
                _context.Current = _builder.Build(windowsPrincipal);

                return true;
            }


            return false;
        }

        public bool Authenticate(LoginRequest request)
        {
            return TryToApply();
        }
    }

    public interface IWindowsPrincipalBuilder
    {
        IPrincipal Build(WindowsPrincipal principal);
    }

    public class PassthroughWindowsPrincipalBuilder : IWindowsPrincipalBuilder
    {
        public IPrincipal Build(WindowsPrincipal principal)
        {
            return principal;
        }
    }

}