using FubuMVC.Authentication;
using FubuPersistence;

namespace FubuMVC.PersistedMembership
{
    public class LoginPersistor
    {
        private readonly IEntityRepository _repository;

        public LoginPersistor(IEntityRepository repository)
        {
            _repository = repository;
        }

        public void LogFailure(LoginRequest request, Audit audit)
        {
            _repository.Update(audit);

            // TODO -- need to watch this w/ RavenDb's async nature
            var history = _repository.FindWhere<LoginFailureHistory>(x => x.UserName == request.UserName) ?? new LoginFailureHistory
            {
                UserName = request.UserName
            };

            history.Attempts = request.NumberOfTries;
            history.LockedOutTime = request.LockedOutUntil;

            _repository.Update(history);
        }

        public void LogSuccess(LoginRequest request, Audit audit)
        {
            _repository.Update(audit);

            // TODO -- need to watch this w/ RavenDb's async nature
            var history = _repository.FindWhere<LoginFailureHistory>(x => x.UserName == request.UserName);
            if (history != null)
            {
                _repository.Remove(history);
            }
        }

        public void ApplyHistory(LoginRequest request)
        {
            var history = _repository.FindWhere<LoginFailureHistory>(x => x.UserName == request.UserName);
            if (history == null) return;

            request.NumberOfTries = history.Attempts;
            request.LockedOutUntil = history.LockedOutTime;
        }

    }
}