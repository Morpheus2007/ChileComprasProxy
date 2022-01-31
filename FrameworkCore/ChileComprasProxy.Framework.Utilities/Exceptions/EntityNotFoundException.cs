using System;
using ChileComprasProxy.Framework.Utilities.Errors;

namespace ChileComprasProxy.Framework.Utilities.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundErrorMessage Error;
        public EntityNotFoundException(EntityNotFoundErrorMessage error)
        {
            Error = error;
        }



        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}