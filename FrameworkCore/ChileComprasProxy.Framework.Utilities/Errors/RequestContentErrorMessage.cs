namespace ChileComprasProxy.Framework.Utilities.Errors
{
    public class RequestContentErrorMessage : ErrorMessage
    {
        public RequestContentErrorMessage()
        {
            Code = ErrorCode.RequestContentMismatch;
            Type = $"{ErrorCode.RequestContentMismatch.ToString()}";
        }
    }
}