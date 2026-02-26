using Grpc.Core;

namespace Tutorium.Shared.Utils.Exceptions
{
    public class GrpcClientException : AppException
    {
        public int Code { get; }

        public GrpcClientException(string message, StatusCode statusCode) : base(message, $"grpc.client.{statusCode}")
        {
            Code = statusCode switch
            {
                StatusCode.OK => 200,
                StatusCode.InvalidArgument => 400,
                StatusCode.NotFound => 404,
                StatusCode.AlreadyExists => 409,
                StatusCode.PermissionDenied => 403,
                StatusCode.Unauthenticated => 401,
                StatusCode.ResourceExhausted => 429,
                StatusCode.FailedPrecondition => 412,
                StatusCode.Aborted => 409,
                StatusCode.OutOfRange => 400,
                StatusCode.Unimplemented => 501,
                StatusCode.Internal => 500,
                StatusCode.Unavailable => 503,
                StatusCode.DeadlineExceeded => 504,
                _ => 500
            };
        }
    }
}
