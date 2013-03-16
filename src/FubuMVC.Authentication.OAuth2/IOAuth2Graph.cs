using System.IO;

namespace FubuMVC.Authentication.OAuth2
{
    public interface IOAuth2Graph
    {
        OAuth2Response Deserialize(Stream fromStream);
    }
}