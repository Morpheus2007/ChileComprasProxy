using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Validators;

namespace ChileComprasProxy.Framework.Utilities.Validator
{
    public class ValidRutAttribute : PropertyValidator
    {

        public ValidRutAttribute()
            : base("{PropertyName} debe ingresar un Rut válido.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            throw new NotImplementedException();
        }
    }
}
