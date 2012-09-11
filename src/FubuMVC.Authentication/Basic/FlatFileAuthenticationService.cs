using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuMVC.Core.Packaging;

namespace FubuMVC.Authentication.Basic
{
    public class FlatFileAuthenticationService : IAuthenticationService
    {
        public const string DefaultUser = "fubu";
        public const string DefaultPass = "world";

        public readonly string PasswordConfigFile;
        private readonly IFileSystem _fileSystem = new FileSystem();
        private readonly object _authLocker = new object();

        public FlatFileAuthenticationService()
        {
            PasswordConfigFile = FubuMvcPackageFacility.GetApplicationPath().AppendPath("fubu.auth.config");
        }

        private void writeDefaults()
        {
            WriteUser(DefaultUser, DefaultPass);
        }

        public void WriteUser(string username, string password)
        {
            lock (_authLocker)
            {
                _fileSystem.WriteToFlatFile(PasswordConfigFile, x => x.WriteLine("{0}={1}".ToFormat(username, password)));
            }
        }

        public IEnumerable<BasicFubuAuthUser> readUsersFromFile()
        {
            if (!_fileSystem.FileExists(PasswordConfigFile))
            {
                writeDefaults();
            }

            var users = new List<BasicFubuAuthUser>();
            _fileSystem.ReadTextFile(PasswordConfigFile, line =>
                                                             {
                                                                 var values = line.Split('=');
                                                                 var user = new BasicFubuAuthUser {UserName = values[0]};

                                                                 if(values.Length == 2)
                                                                 {
                                                                     user.Password = values[1];
                                                                 }

                                                                 users.Add(user);
                                                             });

            return users;
        }

        public bool Authenticate(LoginRequest request)
        {
            if(!_fileSystem.FileExists(PasswordConfigFile))
            {
                writeDefaults();
            }

            return readUsersFromFile().Any(user => user.Matches(request));
        }
    }
}