using System.Security.Principal;

namespace FubuMVC.Authentication.Membership
{
    public class MembershipAuthenticationService : IAuthenticationService
    {
        private readonly IMembershipRepository _repository;

        public MembershipAuthenticationService(IMembershipRepository repository)
        {
            _repository = repository;
        }

        public bool Authenticate(LoginRequest request)
        {
            return _repository.MatchesCredentials(request);
        }

        public IPrincipal Build(string userName)
        {
            var user = _repository.FindByName(userName);

            return new FubuPrincipal(user);
        }
    }


}