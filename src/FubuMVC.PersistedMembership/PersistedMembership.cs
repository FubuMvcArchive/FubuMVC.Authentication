using FubuMVC.Authentication;
using FubuMVC.Authentication.Membership;
using FubuMVC.Core;

namespace FubuMVC.PersistedMembership
{
    public class PersistedMembership<T> : IFubuRegistryExtension where T : User
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Services(x => {
                x.ReplaceService<IMembershipRepository, MembershipRepository<T>>();
                x.SetServiceIfNone<IPasswordHash, PasswordHash>();
                x.SetServiceIfNone<ILoginAuditor, PersistedLoginAuditor>();
            });

            registry.AlterSettings<AuthenticationSettings>(x => {
                x.Strategies.AddToEnd(new MembershipNode(typeof(MembershipRepository<T>)));
            });
        }
    }
}