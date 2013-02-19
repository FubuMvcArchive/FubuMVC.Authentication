using System;
using FubuCore.Dates;
using FubuMVC.Authentication;
using FubuPersistence;

namespace FubuMVC.PersistedMembership
{
    public class Audit : Entity
    {
        public AuditMessage Message { get; set; }
        public DateTime Timestamp { get; set; }

        public string Type
        {
            get { return Message.GetType().Name; }
            set
            {
                // no-op;
            }
        }

        public string Username
        {
            get { return Message.UserName; }
            set
            {
                // do nothing
            }
        }
    }

    public class LoginFailureHistory : Entity
    {
        public string UserName { get; set; }
        public int Attempts { get; set; }
        public DateTime? LockedOutTime { get; set; }
    }

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
                history.LockedOutTime = request.LockedOut;

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
                request.LockedOut = history.LockedOutTime;
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