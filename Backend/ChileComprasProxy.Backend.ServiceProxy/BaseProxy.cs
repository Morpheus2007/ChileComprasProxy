using System;
using System.Net;
using System.Threading.Tasks;
using ChileComprasProxy.Framework.Utilities.Errors;
using ChileComprasProxy.Framework.Utilities.Exceptions;
using ChileComprasProxy.Framework.Utilities.Json;
using RestSharp;

namespace ChileComprasProxy.PfiProxy.Backend.ServiceProxy
{

    public abstract class BaseProxy
    {
        protected readonly RestClient Client;

        protected BaseProxy(string endPoint)
        {
            Client = new RestClient(endPoint);
        }
        public async Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request) where T : class, new()
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();
            ErrorMessage error = null;
            Client.ExecuteAsync<T>(request, restResponse =>
            {

                if (restResponse.IsSuccessful)
                {
                    taskCompletionSource.SetResult(restResponse);
                }
                switch (restResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        error = JsonUtility.ConvertContentToObject<EntityNotFoundErrorMessage>(restResponse.Content);
                        taskCompletionSource.SetException(new EntityNotFoundException((EntityNotFoundErrorMessage)error));
                        break;
                    case HttpStatusCode.BadRequest:
                        error = JsonUtility.ConvertContentToObject<InvalidRequestParameterErrorMessage>(restResponse.Content);
                        taskCompletionSource.SetException(new InvalidParamenterException((InvalidRequestParameterErrorMessage)error));
                        break;
                    case HttpStatusCode.ServiceUnavailable:
                        break;
                }

                if (error == null && restResponse.ErrorException != null)
                {
                    taskCompletionSource.SetException(new ApplicationException(restResponse.StatusDescription));
                }


            });

            return await taskCompletionSource.Task;
        }

        public async Task<IRestResponse> ExecuteAsync(IRestRequest request) 
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            ErrorMessage error = null;
            Client.ExecuteAsync(request, restResponse =>
            {

                if (restResponse.IsSuccessful)
                {
                    taskCompletionSource.SetResult(restResponse);
                }
                switch (restResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        error = JsonUtility.ConvertContentToObject<EntityNotFoundErrorMessage>(restResponse.Content);
                        taskCompletionSource.SetException(new EntityNotFoundException((EntityNotFoundErrorMessage)error));
                        break;
                    case HttpStatusCode.BadRequest:
                        error = JsonUtility.ConvertContentToObject<InvalidRequestParameterErrorMessage>(restResponse.Content);
                        taskCompletionSource.SetException(new InvalidParamenterException((InvalidRequestParameterErrorMessage)error));
                        break;
                    case HttpStatusCode.ServiceUnavailable:
                        break;
                }

                if (error == null && restResponse.ErrorException != null)
                {
                    taskCompletionSource.SetException(new ApplicationException(restResponse.StatusDescription));
                }


            });

            return await taskCompletionSource.Task;
        }
    }
}
