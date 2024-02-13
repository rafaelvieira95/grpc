using Grpc.Core;
using Microsoft.AspNetCore.Authorization;

namespace GrpcServiceAuthenticationJWT.Services
{
    [Authorize]
    public class CalcService: Calc.CalcBase
    {
        public override Task<CalcResponse> Add(CalcRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CalcResponse { Result = request.X + request.Y });
        }

        public override Task<CalcResponse> Subtract(CalcRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CalcResponse { Result = request.X - request.Y });
        }

        public override Task<CalcResponse> Multiply(CalcRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CalcResponse { Result = request.X * request.Y });
        }

        public override Task<CalcResponse> Division(CalcRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CalcResponse { Result = request.X / request.Y });
        }
    }
}
