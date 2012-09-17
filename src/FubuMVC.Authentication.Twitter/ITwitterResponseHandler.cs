namespace FubuMVC.Authentication.Twitter
{
    public interface ITwitterResponseHandler
    {
        void Success();
        void Failure();
    }
}