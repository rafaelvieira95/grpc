using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceAuthenticationJWT;

namespace GrpcClientJWT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Delay(5000);

            var channel = GrpcChannel.ForAddress("http://localhost:5069");
            var authenticationClient = new Authentication.AuthenticationClient(channel);
            var authenticationResponse = authenticationClient.Authenticate(new AuthenticationRequest
            {
                UserName = "admin",
                Password = "admin"
            });

            Console.WriteLine($"Receive Auth Response | Token: {authenticationResponse.AccessToken} | Expires In: {authenticationResponse.ExpiresIn}");

            var calculationClient = new Calc.CalcClient(channel);
            var headers = new Metadata();
            headers.Add("Authorization", $"Bearer {authenticationResponse.AccessToken}");

            var result = calculationClient.Add(new CalcRequest { X = 30.0, Y = 44.0 }, headers);
            Console.WriteLine($"Sum Result: {result.Result}");  

            channel.ShutdownAsync();
        }
    }
}   
