using FubuCore;

namespace FubuMVC.Authentication
{
    public class LoginController
    {
        private readonly ITicketSource _ticketSource;
        private readonly ILoginFailureHandler _failureHandler;
        private readonly AuthenticationSettings _settings;

        public LoginController(ITicketSource ticketSource, ILoginFailureHandler failureHandler, AuthenticationSettings settings)
        {
            _ticketSource = ticketSource;
            _failureHandler = failureHandler;
            _settings = settings;
        }

        public LoginRequest Login(LoginRequest request)
        {
            var ticket = _ticketSource.CurrentTicket();

            if (request.UserName.IsEmpty())
            {
                var remembered = ticket.UserName;

                if (remembered.IsNotEmpty())
                {
                    request.UserName = remembered;
                    request.RememberMe = true;
                }
            }

            if (request.Status != LoginStatus.Failed)
            {
                return request;
            }

            _failureHandler.Handle(request, ticket, _settings);

            if (request.Message.IsEmpty())
            {
                request.Message = LoginKeys.Unknown.ToString();
            }

            return request;
        }
    }
}