using System.Security.Principal;

namespace FubuMVC.Authentication.Basic
{
    public class BasicFubuAuthUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public bool Matches(LoginRequest request)
        {
            return UserName == request.UserName && Password == request.Password;
        }
    }

    public class BasicFubuPrincipalBuilder : IPrincipalBuilder
    {
        public IPrincipal Build(string userName)
        {
            return new GenericPrincipal(new GenericIdentity(userName), new string[0]);
        }
    }
}