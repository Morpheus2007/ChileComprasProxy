using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace ChileComprasProxy.Framework.Utilities.Errors
{
    public static class ErrorService
    {
        public static ErrorMessage GetRequestContentMismatchErrorMessage()
        {
            return new RequestContentErrorMessage
            {
                Title = "ContentRequestError",
                Detail = ErrorToErrorList("Error en el contexto de la solicitud")
            };
        }

        public static ErrorMessage GetEntityNotFoundErrorMessage(Type entity, object param)
        {
            return new EntityNotFoundErrorMessage
            {
                Title = "Datos No Encontrados",
                Detail = ErrorToErrorList($"No se han encontrado datos del tipo {entity.Name} el parámetro {param}")
            };
        }

        public static ErrorMessage GetEntityNotFoundErrorMessage()
        {
            return new EntityNotFoundErrorMessage
            {
                Title = "Datos No Encontrados",
                Detail = ErrorToErrorList("No se han encontrado datos para los parametros especificados")
            };
        }


        public static ErrorMessage GetInvalidRequestParameterErrorMessage(object obj)
        {
            IList<string> errors = new List<string>();
            errors.Add($"El parámetro {obj} no tiene un valor o formato válido");
            return new InvalidRequestParameterErrorMessage
            {
                Title = "Parametro(s) de entrada inválido(s)",
                Detail = errors
            };
        }

        public static ErrorMessage GetInvalidRequestParameterErrorMessage(IList<ValidationFailure> errors)
        {

            IList<string> detailErrors = new List<string>();
            foreach (var error in errors)
            {
                detailErrors.Add(error.ErrorMessage);
            }

            return new InvalidRequestParameterErrorMessage
            {
                Title = "Parametro de entrada inválido",
                Detail = detailErrors,
                Type = "Model Validation"
            };
        }


        public static ErrorMessage GetDuplicateEntityRequestParameterErrorMessage(object obj)
        {
            return new DuplicateRequestParameterErrorMessage
            {
                Title = "Parametro de entrada duplicado",
                Detail = ErrorToErrorList($"El o los parametros de entrada {obj} ya se encuentrar registrados")
            };
        }

        public static ErrorMessage GetGenericErrorMessage(string info, string detail)
        {
            return new RequestContentErrorMessage
            {
                Title = "GenericError",
                Info = info,
                Detail = ErrorToErrorList(detail)
            };
        }

        public static ErrorMessage GetGenericErrorMessage(string info)
        {
            return new RequestContentErrorMessage
            {
                Title = "GenericError",
                Info = info
            };
        }


        private static IList<string> ErrorToErrorList(string msg)
        {
            IList<string> errors = new List<string>
            {
                msg
            };
            return errors;
        } 
    }
}