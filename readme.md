## FubuMVC.Authentication

Provides a pluggable authentication infrastructure for your FubuMVC application.

## How do I use it?

### Step 1. Simply install it via NuGet:

> Install-Package FubuMVC.Authentication

### Step 2. Implement the services

If you run as-is, the bottle will blow up on you (on purpose) telling you that you're missing two things: 1) IAuthenticationService and 2) IPrincipalBuilder.

You will need to implement and register both of these custom-tailored services.

### Step 3. [Optional] Configuration

If you wish to configure how the Bottle is applied, you can do the following:

	Import<ApplyAuthentication>(x => {
		// Explictly state which behavior chains to authenticate
		x.Include(chain => ...);
		
		// Explicitly state which behavior chains to exclude from authentication
		x.Exclude(chain => ...);
		
		// By default, you get ~/login and ~/logout (with extensibility points). Use this method to turn them off.
		// If you use the defaults, just create a view that is typed against FubuMVC.Authentication.LoginRequest.
		x.DoNotIncludeEndpoints();
	});