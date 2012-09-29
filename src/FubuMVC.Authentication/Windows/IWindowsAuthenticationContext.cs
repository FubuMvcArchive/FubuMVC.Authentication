namespace FubuMVC.Authentication.Windows
{
    // This is going to vary by the host (i.e., ASP.NET vs. Self Host)
    public interface IWindowsAuthenticationContext
    {
        string CurrentUser();
    }
}