using Grpc.Core;

namespace GrpcServiceAuthenticationJWT.Services
{
    public class AuthenticationService: Authentication.AuthenticationBase
    {
        public override Task<AuthenticationResponse> Authenticate(AuthenticationRequest request, ServerCallContext context)
        {
            var authenticationResponse = JwtAuthenticationManager.Authenticate(request);
            if (authenticationResponse == null)
                throw new RpcException(new Status(StatusCode.Unauthenticated, "Invalid user credentials"));

            return Task.FromResult(authenticationResponse);
        }
    }
}
