namespace FubuMVC.PersistedMembership
{
    public interface IPasswordHash
    {
        string CreateHash(string password);
    }
}