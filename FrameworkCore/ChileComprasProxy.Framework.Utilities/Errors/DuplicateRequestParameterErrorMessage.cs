namespace ChileComprasProxy.Framework.Utilities.Errors
{
    public class DuplicateRequestParameterErrorMessage : ErrorMessage
    {
        public DuplicateRequestParameterErrorMessage()
        {
            Code = ErrorCode.DuplicateEntity;
            Type = $"{ErrorCode.DuplicateEntity.ToString()}";
        }
    }
}