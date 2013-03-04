using FubuCore.Dates;
using FubuMVC.Authentication;
using FubuMVC.Authentication.Auditing;
using FubuPersistence;

namespace FubuMVC.PersistedMembership
{
    public class PersistedLoginAuditor : ILoginAuditor
    {
        private readonly ISystemTime _systemTime;
        private readonly ITransaction _transaction;

        public PersistedLoginAuditor(ISystemTime systemTime, ITransaction transaction)
        {
            _systemTime = systemTime;
            _transaction = transaction;
        }

        public void Audit(LoginRequest request)
        {
            if (request.Status == LoginStatus.Succeeded)
            {
                logSuccess(request);
            }
            else
            {
                logFailure(request);
            }
        }

        private void logFailure(LoginRequest request)
        {
            var audit = new Audit
            {
                Message = new LoginFailure { UserName = request.UserName },
                Timestamp = _systemTime.UtcNow()
            };

            _transaction.WithRepository(repo =>
            {
                repo.Update(audit);

                // TODO -- need to watch this w/ RavenDb's async nature
                var history = repo.FindWhere<LoginFailureHistory>(x => x.UserName == request.UserName) ?? new LoginFailureHistory
                {
                    UserName = request.UserName
                };

                history.Attempts = request.NumberOfTries;
                history.LockedOutTime = request.LockedOutUntil;

                repo.Update(history);
            });
        }

        private void logSuccess(LoginRequest request)
        {
            var audit = new Audit
            {
                Message = new LoginSuccess {UserName = request.UserName},
                Timestamp = _systemTime.UtcNow()
            };

            _transaction.WithRepository(repo => {
                repo.Update(audit);

                // TODO -- need to watch this w/ RavenDb's async nature
                var history = repo.FindWhere<LoginFailureHistory>(x => x.UserName == request.UserName);
                if (history != null)
                {
                    repo.Remove(history);
                }
            });
        }

        public void ApplyHistory(LoginRequest request)
        {
            _transaction.WithRepository(repo => {
                var history = repo.FindWhere<LoginFailureHistory>(x => x.UserName == request.UserName);
                if (history == null) return;

                request.NumberOfTries = history.Attempts;
                request.LockedOutUntil = history.LockedOutTime;
            });
        }

        public void Audit<T>(T log) where T : AuditMessage
        {
            persistAudit(log);
        }

        private void persistAudit(AuditMessage message)
        {
            var audit = new Audit
            {
                Message = message,
                Timestamp = _systemTime.UtcNow()
            };

            _transaction.WithRepository(repo => repo.Update(audit));
        }
    }
}