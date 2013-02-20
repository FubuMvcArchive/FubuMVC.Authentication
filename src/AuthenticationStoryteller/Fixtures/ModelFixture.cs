using FubuMVC.Authentication;
using FubuMVC.PersistedMembership;
using FubuPersistence;
using FubuPersistence.Reset;
using StoryTeller;
using StoryTeller.Engine;

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