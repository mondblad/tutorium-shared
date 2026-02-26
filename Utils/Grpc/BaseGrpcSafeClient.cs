using Grpc.Core;
using Tutorium.Shared.Utils.Exceptions;

namespace Tutorium.Shared.Utils.Grpc
{
    public abstract class BaseGrpcSafeClient<TClient> where TClient : class
    {
        protected readonly TClient _client;

        public BaseGrpcSafeClient(TClient client)
        {
            _client = client;
        }

        protected Task ExecuteAsync(Func<Task> action, string errorMessage)
        {
            return ExecuteInternalAsync(action, errorMessage);
        }

        protected Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> action, string errorMessage)
        {
            return ExecuteInternalAsync(action, errorMessage);
        }

        private async Task ExecuteInternalAsync(Func<Task> action, string errorMessage)
        {
            try
            {
                await action();
            }
            catch (RpcException ex)
            {
                var cleanMessage = string.IsNullOrWhiteSpace(ex.Status.Detail)
                    ? errorMessage
                    : ex.Status.Detail;

                throw new GrpcClientException(cleanMessage, ex.StatusCode);
            }
        }

        private async Task<TResult> ExecuteInternalAsync<TResult>(Func<Task<TResult>> action, string errorMessage)
        {
            try
            {
                return await action();
            }
            catch (RpcException ex)
            {
                var cleanMessage = string.IsNullOrWhiteSpace(ex.Status.Detail)
                   ? errorMessage
                   : ex.Status.Detail;

                throw new GrpcClientException(cleanMessage, ex.StatusCode);
            }
        }
    }
}
