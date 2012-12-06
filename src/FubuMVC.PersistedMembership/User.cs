using System;
using FubuMVC.Authentication;
using FubuMVC.Authentication.Membership;
using FubuMVC.Core;
using FubuPersistence;
using System.Linq;

namespace FubuMVC.PersistedMembership
{
    public class User : UserInfo, IEntity
    {
        public Guid Id { get; set; }
    }

    public class PersistedMembership<T> : IFubuRegistryExtension where T : User
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Services(x => {
                x.ReplaceService<IMembershipRepository, MembershipRepository<T>>();
                x.SetServiceIfNone<IPasswordHash, PasswordHash>();
            });

            registry.AlterSettings<AuthenticationSettings>(x => {
                x.Strategies.AddToEnd(new MembershipNode(typeof(MembershipRepository<T>)));
            });
        }
    }
}