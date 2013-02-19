using System;
using FubuCore.Dates;
using FubuMVC.Authentication;
using FubuPersistence;
using FubuPersistence.RavenDb;
using NUnit.Framework;
using Raven.Client;
using StructureMap;
using System.Linq;
using FubuTestingSupport;

namespace FubuMVC.PersistedMembership.Testing
{
    [TestFixture]
    public class PersistedLoginAuditorIntegratedTester
    {
        private SettableClock theTime;
        private Container theContainer;

        [SetUp]
        public void SetUp()
        {
            theTime = new SettableClock();
            theTime.LocalNow(LocalTime.AtMachineTime("1200")); // doesn't matter what, only needs to be constant

            theContainer = new Container(x => {
                x.IncludeRegistry<RavenDbRegistry>();
                x.For<RavenDbSettings>().Use(new RavenDbSettings {RunInMemory = true, DataDirectory = null, Url = null, ConnectionString = null});

                x.For<ISystemTime>().Use(theTime);
            });


        }

        [Test]
        public void write_audit_message()
        {
            var auditor = theContainer.GetInstance<PersistedLoginAuditor>();
            auditor.Audit(new Something{UserName = "the something"});

            var theAudit = theContainer.GetInstance<IEntityRepository>().All<Audit>().Where(x => x.Type == "Something").Single();
            theAudit.Message.ShouldBeOfType<Something>().UserName.ShouldEqual("the something");
            theAudit.Timestamp.ShouldEqual(theTime.UtcNow());
            theAudit.Username.ShouldEqual("the something");
        }

        [Test]
        public void write_login_success()
        {
            var request = new LoginRequest
            {
                Status = LoginStatus.Succeeded,
                UserName = "somebody"
            };

            var auditor = theContainer.GetInstance<PersistedLoginAuditor>();
            auditor.Audit(request);

            var theAudit = theContainer.GetInstance<IEntityRepository>().All<Audit>()
                                       .Where(x => x.Type == "LoginSuccess").Single();

            theAudit.Message.ShouldBeOfType<LoginSuccess>();
            theAudit.Timestamp.ShouldEqual(theTime.UtcNow());
            theAudit.Username.ShouldEqual("somebody");
        }

        [Test]
        public void write_login_failure()
        {
            var request = new LoginRequest
            {
                Status = LoginStatus.Failed,
                UserName = "FailedGuy"
            };

            var auditor = theContainer.GetInstance<PersistedLoginAuditor>();
            auditor.Audit(request);

            var theAudit = theContainer.GetInstance<IEntityRepository>().All<Audit>()
                                       .Where(x => x.Type == "LoginFailure").Single();

            theAudit.Message.ShouldBeOfType<LoginFailure>();
            theAudit.Timestamp.ShouldEqual(theTime.UtcNow());
            theAudit.Username.ShouldEqual(request.UserName);
        }

        private void waitForHistoryToAppear(string username)
        {
            theContainer.GetInstance<IDocumentSession>().Query<LoginFailureHistory>().Customize(x => x.WaitForNonStaleResults())
                .Any(x => x.UserName == username).ShouldBeTrue();
        }

        private void waitForHistoryToDisappear(string username)
        {
            theContainer.GetInstance<IDocumentSession>().Query<LoginFailureHistory>().Customize(x => x.WaitForNonStaleResults())
                .Any(x => x.UserName == username).ShouldBeFalse();
        }

        [Test]
        public void when_logging_success_wipe_clean_the_login_failure_history()
        {
            var history = new LoginFailureHistory
            {
                UserName = "doofus",
                Attempts = 3
            };

            theContainer.GetInstance<ITransaction>().WithRepository(repo =>
            {
                repo.Update(history);
            });

            waitForHistoryToAppear(history.UserName);



            var request = new LoginRequest
            {
                Status = LoginStatus.Succeeded,
                UserName = history.UserName
            };

            var auditor = theContainer.GetInstance<PersistedLoginAuditor>();
            auditor.Audit(request);

            waitForHistoryToDisappear(history.UserName);

        }

        [Test]
        public void when_logging_failure_for_a_user_that_has_no_prior_failure_history()
        {
            var request = new LoginRequest
            {
                Status = LoginStatus.Failed,
                UserName = "NeverFailedBefore",
                NumberOfTries = 1,
                LockedOut = null
                
            };

            var auditor = theContainer.GetInstance<PersistedLoginAuditor>();
            auditor.Audit(request);

            waitForHistoryToAppear(request.UserName);

            var history = theContainer.GetInstance<IEntityRepository>()
                        .FindWhere<LoginFailureHistory>(x => x.UserName == request.UserName);


            history.ShouldNotBeNull();
            history.Attempts.ShouldEqual(1);
            history.LockedOutTime.ShouldBeNull();
        }

        [Test]
        public void when_logging_failure_for_a_user_that_is_locked_out()
        {
            var request = new LoginRequest
            {
                Status = LoginStatus.Failed,
                UserName = "NeverFailedBefore",
                NumberOfTries = 1,
                LockedOut = DateTime.Today.ToUniversalTime()

            };

            var auditor = theContainer.GetInstance<PersistedLoginAuditor>();
            auditor.Audit(request);

            waitForHistoryToAppear(request.UserName);

            var history = theContainer.GetInstance<IEntityRepository>()
                        .FindWhere<LoginFailureHistory>(x => x.UserName == request.UserName);



            history.LockedOutTime.ShouldEqual(request.LockedOut);
        }

        [Test]
        public void update_an_existing_history()
        {
            var history = new LoginFailureHistory
            {
                UserName = "AlreadyFailed",
                Attempts = 2
            };

            theContainer.GetInstance<ITransaction>().WithRepository(repo =>
            {
                repo.Update(history);
            });

            waitForHistoryToAppear(history.UserName);

            var request = new LoginRequest
            {
                Status = LoginStatus.Failed,
                UserName = history.UserName,
                NumberOfTries = 3,
                LockedOut = DateTime.Today.ToUniversalTime()

            };

            var auditor = theContainer.GetInstance<PersistedLoginAuditor>();
            auditor.Audit(request);

            waitForHistoryToAppear(request.UserName);

            var history2 = theContainer.GetInstance<IEntityRepository>()
                        .FindWhere<LoginFailureHistory>(x => x.UserName == request.UserName);

            history2.Attempts.ShouldEqual(request.NumberOfTries);
            history2.LockedOutTime.ShouldEqual(request.LockedOut);

        }

        [Test]
        public void apply_history_when_there_is_no_history()
        {
            var request = new LoginRequest
            {
                NumberOfTries = 5,
                UserName = "NoHistoryGuy"
            };


            var auditor = theContainer.GetInstance<PersistedLoginAuditor>();
            auditor.ApplyHistory(request);

            request.NumberOfTries.ShouldEqual(5); // Nothing gets replaced
        }

        [Test]
        public void apply_history_when_there_is_prior_history()
        {
            var history = new LoginFailureHistory
            {
                UserName = "AlreadyLockedOut",
                Attempts = 3,
                LockedOutTime = DateTime.Today.AddMinutes(30)
            };

            theContainer.GetInstance<ITransaction>().WithRepository(repo => repo.Update(history));
            waitForHistoryToAppear(history.UserName);

            var auditor = theContainer.GetInstance<PersistedLoginAuditor>();
            var request = new LoginRequest
            {
                UserName = history.UserName
            };

            auditor.ApplyHistory(request);

            request.NumberOfTries.ShouldEqual(history.Attempts);
            request.LockedOut.ShouldEqual(history.LockedOutTime.Value);
        }
    }

    public class Something : AuditMessage
    {
    }
}