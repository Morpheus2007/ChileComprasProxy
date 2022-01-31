namespace ChileComprasProxy.Framework.Utilities.Errors
{
    public class InvalidRequestParameterErrorMessage : ErrorMessage
    {
        public InvalidRequestParameterErrorMessage()
        {
            Code = ErrorCode.InvalidParameter;
            Type = $"{ErrorCode.InvalidParameter.ToString()}";
        }
    }
}