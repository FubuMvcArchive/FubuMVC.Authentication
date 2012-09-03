namespace FubuMVC.Authentication
{
    public interface IAuthenticationTicketEncryptor
    {
        string Encrypt(AuthenticationTicket ticket);
        AuthenticationTicket Decrypt(string text);
    }
}