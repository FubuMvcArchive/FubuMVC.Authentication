namespace FubuMVC.Authentication
{
    public interface ITicketSource
    {
        AuthenticationTicket CurrentTicket();
        void Persist(AuthenticationTicket ticket);
        void Delete();
    }
}