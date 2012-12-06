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

    public class MembershipRepository<T> : IMembershipRepository where T : User
    {
        private readonly IEntityRepository _repository;
        private readonly IPasswordHash _hash;

        public MembershipRepository(IEntityRepository repository, IPasswordHash hash)
        {
            _repository = repository;
            _hash = hash;
        }

        public bool MatchesCredentials(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public IUserInfo FindByName(string username)
        {
            throw new NotImplementedException();
        }
    }
}