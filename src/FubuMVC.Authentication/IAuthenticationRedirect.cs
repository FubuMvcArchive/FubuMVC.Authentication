namespace FubuMVC.Authentication
{
	public interface IAuthenticationRedirect
	{
		bool Applies();
		void Redirect();
	}
}