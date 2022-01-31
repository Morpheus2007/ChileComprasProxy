using System;
using System.Net;
using System.Threading.Tasks;
using ChileComprasProxy.Framework.Utilities.Errors;
using ChileComprasProxy.Framework.Utilities.Exceptions;
using ChileComprasProxy.Framework.Utilities.Json;
using RestSharp;

namespace ChileComprasProxy.Framework.Services.Base
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
                    //case HttpStatusCode.Accepted:
                    //    break;
                    //case HttpStatusCode.AlreadyReported:
                    //    break;
                    //case HttpStatusCode.Ambiguous:
                    //    break;
                    //case HttpStatusCode.BadGateway:
                    //    break;
                    //case HttpStatusCode.Conflict:
                    //    break;
                    //case HttpStatusCode.Continue:
                    //    break;
                    //case HttpStatusCode.Created:
                    //    break;
                    //case HttpStatusCode.EarlyHints:
                    //    break;
                    //case HttpStatusCode.ExpectationFailed:
                    //    break;
                    //case HttpStatusCode.FailedDependency:
                    //    break;
                    //case HttpStatusCode.Forbidden:
                    //    break;
                    //case HttpStatusCode.Found:
                    //    break;
                    //case HttpStatusCode.GatewayTimeout:
                    //    break;
                    //case HttpStatusCode.Gone:
                    //    break;
                    //case HttpStatusCode.HttpVersionNotSupported:
                    //    break;
                    //case HttpStatusCode.IMUsed:
                    //    break;
                    //case HttpStatusCode.InsufficientStorage:
                    //    break;
                    //case HttpStatusCode.InternalServerError:
                    //    break;
                    //case HttpStatusCode.LengthRequired:
                    //    break;
                    //case HttpStatusCode.Locked:
                    //    break;
                    //case HttpStatusCode.LoopDetected:
                    //    break;
                    //case HttpStatusCode.MethodNotAllowed:
                    //    break;
                    //case HttpStatusCode.MisdirectedRequest:
                    //    break;
                    //case HttpStatusCode.Moved:
                    //    break;
                    //case HttpStatusCode.MultiStatus:
                    //    break;
                    //case HttpStatusCode.NetworkAuthenticationRequired:
                    //    break;
                    //case HttpStatusCode.NoContent:
                    //    break;
                    //case HttpStatusCode.NonAuthoritativeInformation:
                    //    break;
                    //case HttpStatusCode.NotAcceptable:
                    //    break;
                    //case HttpStatusCode.NotExtended:
                    //    break;
                    //case HttpStatusCode.NotImplemented:
                    //    break;
                    //case HttpStatusCode.NotModified:
                    //    break;
                    //case HttpStatusCode.OK:
                    //    break;
                    //case HttpStatusCode.PartialContent:
                    //    break;
                    //case HttpStatusCode.PaymentRequired:
                    //    break;
                    //case HttpStatusCode.PermanentRedirect:
                    //    break;
                    //case HttpStatusCode.PreconditionFailed:
                    //    break;
                    //case HttpStatusCode.PreconditionRequired:
                    //    break;
                    //case HttpStatusCode.Processing:
                    //    break;
                    //case HttpStatusCode.ProxyAuthenticationRequired:
                    //    break;
                    //case HttpStatusCode.RedirectKeepVerb:
                    //    break;
                    //case HttpStatusCode.RedirectMethod:
                    //    break;
                    //case HttpStatusCode.RequestedRangeNotSatisfiable:
                    //    break;
                    //case HttpStatusCode.RequestEntityTooLarge:
                    //    break;
                    //case HttpStatusCode.RequestHeaderFieldsTooLarge:
                    //    break;
                    //case HttpStatusCode.RequestTimeout:
                    //    break;
                    //case HttpStatusCode.RequestUriTooLong:
                    //    break;
                    //case HttpStatusCode.ResetContent:
                    //    break;
                    case HttpStatusCode.ServiceUnavailable:
                        break;
                        //case HttpStatusCode.SwitchingProtocols:
                        //    break;
                        //case HttpStatusCode.TooManyRequests:
                        //    break;
                        //case HttpStatusCode.Unauthorized:
                        //    break;
                        //case HttpStatusCode.UnavailableForLegalReasons:
                        //    break;
                        //case HttpStatusCode.UnprocessableEntity:
                        //    break;
                        //case HttpStatusCode.UnsupportedMediaType:
                        //    break;
                        //case HttpStatusCode.Unused:
                        //    break;
                        //case HttpStatusCode.UpgradeRequired:
                        //    break;
                        //case HttpStatusCode.UseProxy:
                        //    break;
                        //case HttpStatusCode.VariantAlsoNegotiates:
                        //    break;
                }

                if (error == null && restResponse.ErrorException != null)
                {
                    taskCompletionSource.SetException(new ApplicationException(restResponse.StatusDescription));
                }


            });

            return await taskCompletionSource.Task;
        }
        
        public async Task<IRestResponse<T>> ExternalServiceExecuteAsync<T>(IRestRequest request) where T : class, new()
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();
            Client.ExecuteAsync<T>(request, restResponse =>
            {

                if (restResponse.IsSuccessful)
                {
                    taskCompletionSource.SetResult(restResponse);
                }
               

                if (restResponse.ErrorException != null)
                {
                    taskCompletionSource.SetException(new ApplicationException(restResponse.StatusDescription));
                }


            });

            return await taskCompletionSource.Task;
        }
    }
}
