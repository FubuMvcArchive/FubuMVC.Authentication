using NUnit.Framework;

using FubuTestingSupport;

namespace FubuMVC.Authentication.Tests
{
    [TestFixture]
    public class EncryptionTester
    {
        private Encryptor theEncryptor;

        [SetUp]
        public void SetUp()
        {
            theEncryptor = new Encryptor(new EncryptionSettings());
        }

        [Test]
        public void is_predictable()
        {
            var plain = "the rain in spain";
            var encrypted1 = theEncryptor.Encrypt(plain);
            var encrypted2 = theEncryptor.Encrypt(plain);

            encrypted1.ShouldNotEqual(plain);

            encrypted1.ShouldEqual(encrypted2);
        }

        [Test]
        public void can_round_trip()
        {
            var plain = "the rain in spain";
            var encrypted1 = theEncryptor.Encrypt(plain);

            theEncryptor.Decrypt(encrypted1).ShouldEqual(plain);
        }
    }
}