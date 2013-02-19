using FubuCore.Dates;
using FubuMVC.Authentication;
using FubuPersistence;
using FubuPersistence.RavenDb;
using NUnit.Framework;
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
                x.For<RavenDbSettings>().Use(new RavenDbSettings {RunInMemory = true, DataDirectory = null});

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
    }

    public class Something : AuditMessage
    {
    }
}