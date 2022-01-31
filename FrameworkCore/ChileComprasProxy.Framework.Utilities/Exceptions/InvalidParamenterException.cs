using System;
using ChileComprasProxy.Framework.Utilities.Errors;

namespace ChileComprasProxy.Framework.Utilities.Exceptions
{
    public class InvalidParamenterException : Exception
    {

        public InvalidRequestParameterErrorMessage Error;
        public InvalidParamenterException(InvalidRequestParameterErrorMessage error)
        {
            Error = error;
        }



        public InvalidParamenterException()
        {
        }

        public InvalidParamenterException(string message)
            : base(message)
        {
        }

        public InvalidParamenterException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}