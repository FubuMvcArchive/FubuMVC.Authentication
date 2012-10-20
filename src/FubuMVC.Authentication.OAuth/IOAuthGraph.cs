using System.IO;

namespace FubuMVC.Authentication.OAuth
{
    public interface IOAuthGraph
    {
        OAuthResponse Deserialize(Stream fromStream);
    }
}