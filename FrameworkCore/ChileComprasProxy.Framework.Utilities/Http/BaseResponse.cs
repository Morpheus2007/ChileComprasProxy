using System.Collections.Generic;

namespace ChileComprasProxy.Framework.Utilities.Http
{
    //Clase Base de respuesta usado en sistemas como Sirela2, Dtplus u otro
    public class BaseResponse
    {
        public BaseResponse() : this(default(object))
        {
        }

        public BaseResponse(object data)
        {
            Data = data;
            IsValid = false;
        }

        public BaseResponse(object data, bool isValid)
        {
            Data = data;
            IsValid = isValid;
        }

        public BaseResponse(IDictionary<string, string> modelErrors, string errorMessage)
        {
            ErrorMessage = errorMessage;
            ModelErrors = modelErrors;
            IsValid = false;
        }

        public bool IsValid { get; set; }

        public string ErrorMessage { get; set; }

        public IDictionary<string, string> ModelErrors { get; }

        public object Data { get; set; }
    }
}