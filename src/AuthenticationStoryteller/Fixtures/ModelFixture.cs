using System;
using System.Linq.Expressions;
using FubuCore.Dates;
using FubuMVC.Authentication;
using FubuMVC.PersistedMembership;
using FubuPersistence;
using FubuPersistence.Reset;
using StoryTeller;
using StoryTeller.Engine;
using FubuCore.Reflection;

namespace AuthenticationStoryteller.Fixtures
{
    public class ModelFixture : Fixture
    {
        private ICompleteReset _reset;
        private IUnitOfWork _unitOfWork;
        private IEntityRepository _repository;

        public ModelFixture()
        {
            Title = "The system state";
        }

        public override void SetUp(ITestContext context)
        {
            var clock = (Clock)Retrieve<IClock>();
            clock.Live();

            _reset = Retrieve<ICompleteReset>();
            _reset.ResetState();

            _unitOfWork = Retrieve<IUnitOfWork>();
            _repository = _unitOfWork.Start();
        }


        public override void TearDown()
        {
            _unitOfWork.Commit();
            _reset.CommitChanges(); // doesn't do anything now, but might later when we go to IIS
        }

        private IGrammar setter(Expression<Func<AuthenticationSettings, object>> property)
        {
            var accessor = property.ToAccessor();
            var grammar = new SetPropertyGrammar(accessor)
            {
                DefaultValue = accessor.GetValue(new AuthenticationSettings()).ToString()
            };

            return grammar;
        }

        public IGrammar SetAuthenticationSettings()
        {
            return Paragraph("The Authentication Settings are", x => {
                x += () => Context.CurrentObject = Retrieve<AuthenticationSettings>();
                x += setter(o => o.ExpireInMinutes);
                x += setter(o => o.SlidingExpiration);
                x += setter(o => o.MaximumNumberOfFailedAttempts);
                x += setter(o => o.CooloffPeriodInMinutes);
            });
        }

        [ExposeAsTable("The users are")]
        public void UsersAre(string UserName, string Password)
        {
            var user = new User
            {
                UserName = UserName,
                Password = Retrieve<IPasswordHash>().CreateHash(Password)
            };

            _repository.Update(user);
        }
    }
}