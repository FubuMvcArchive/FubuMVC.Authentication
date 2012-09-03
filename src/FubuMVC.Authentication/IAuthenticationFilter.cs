namespace FubuMVC.Authentication
{
	public interface IAuthenticationFilter
	{
		AuthenticationFilterResult Authenticate();
	}
}