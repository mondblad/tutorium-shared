using Grpc.Core;
namespace Tutorium.Shared.Utils.Exceptions
{
    public class DefaultExceptionMapper : IExceptionMapper
    {
        public int Map(Exception ex) => ex switch
        {
            GrpcClientException ge => ge.Code,
            DomainException de => 400,
            ApplicationException ae => 422,
            InfrastructureException ie => 502,
            _ => 500
        };
    }
}
