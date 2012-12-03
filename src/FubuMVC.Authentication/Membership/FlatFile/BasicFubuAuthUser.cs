namespace FubuMVC.Authentication.Membership.FlatFile
{
    public class BasicFubuAuthUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public bool Matches(LoginRequest request)
        {
            return UserName == request.UserName && Password == request.Password;
        }
    }
}