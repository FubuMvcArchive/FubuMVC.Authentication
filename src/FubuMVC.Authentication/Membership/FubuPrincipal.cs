using System;
using System.Security.Principal;
using System.Linq;

namespace FubuMVC.Authentication.Membership
{
    public class FubuPrincipal : GenericPrincipal
    {
        private readonly IUserInfo _user;

        public FubuPrincipal(IUserInfo user) : base(new GenericIdentity(user.UserName), user.Roles.ToArray())
        {
            _user = user;
        }

        public T Get<T>() where T : class
        {
            return _user.Get<T>();
        }

        public static FubuPrincipal Current()
        {
            var context = new ThreadPrincipalContext();
            return context.Current as FubuPrincipal;
        }

        public static void SetCurrent(Action<UserInfo> configuration)
        {
            var user = new UserInfo();
            configuration(user);

            var principal = new FubuPrincipal(user);
            new ThreadPrincipalContext().Current = principal;
        }
    }
}