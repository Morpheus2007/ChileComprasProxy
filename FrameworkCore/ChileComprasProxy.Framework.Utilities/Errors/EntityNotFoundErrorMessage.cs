namespace ChileComprasProxy.Framework.Utilities.Errors
{
    public class EntityNotFoundErrorMessage : ErrorMessage
    {
        public EntityNotFoundErrorMessage()
        {
            Code = ErrorCode.EntityNotFound;
            Type = $"{ErrorCode.EntityNotFound.ToString()}";
        }
    }
}