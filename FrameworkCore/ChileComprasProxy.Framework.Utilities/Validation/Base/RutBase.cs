using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ChileComprasProxy.Framework.Utilities.Validation.Base
{
    /// <summary>
    ///     Representa un RUT chileno.
    /// </summary>
    public class RutBase
    {
        private const char KChar = 'K';
        private const string RegexPattern = @"^(?!0)(?:\d+[kK]?|\d{1,3}(?:\.\d{3})+)(?:-\d|-[kK])?$";

        private readonly string _rut;

        /// <summary>
        ///     Crea una nueva instancia de la clase <see cref="RutBase" />.
        /// </summary>
        /// <param name="numero">Número.</param>
        /// <param name="dv">Dígito verificador.</param>
        public RutBase(int numero, char dv) : this($"{numero}-{dv}")
        {
        }

        /// <summary>
        ///     Crea una nueva instancia de la clase <see cref="RutBase" />.
        /// </summary>
        /// <param name="numero">Número.</param>
        /// <param name="dv">Dígito verificador.</param>
        public RutBase(string numero, char dv) : this($"{numero}-{dv}")
        {
        }

        /// <summary>
        ///     Crea una nueva instancia de la clase <see cref="RutBase" />.
        /// </summary>
        /// <param name="rut">RUT (incluyendo dígito verificador).</param>
        public RutBase(string rut)
        {
            if (rut.StartsWith("0"))
                for (var i = 0; i < rut.Length; i++)
                {
                    if (rut.StartsWith("0"))
                    {
                        rut = rut.Substring(1, rut.Length - 1);
                        continue;
                    }
                    break;
                }

            // Se elimina la validacion durante la ejecucion del constructor, para evitar excepciones ante datos no válidos.
            // La validacion se realiza de forma independiente a través del metodo estatico Validate(rut)
            //if (!Validate(rut))
            //    throw new ArgumentException("El valor especificado no es un RUT válido, verifique en la configuracion [DireccionTrabajo.Core.Common.Security.UserRut.Mock] si esta utilizando un Rut Mock, rut:[{0}]", rut);

            rut = Regex.Replace(rut, @"\.", string.Empty).Trim().ToUpperInvariant();

            if (rut.IndexOf('-') == -1)
                rut = rut.Substring(0, rut.Length - 1) + '-' + rut[rut.Length - 1];

            _rut = rut;
        }

        public static int MinLength { get; set; } = 8;

        public static int MaxLength { get; set; } = 9;

        /// <summary>
        ///     Devuelve el RUT sin dígito verificador.
        /// </summary>
        public string Numero => _rut.Substring(0, _rut.Length - 2);

        /// <summary>
        ///     Devuelve el dígito verificador.
        /// </summary>
        public char Dv => _rut[_rut.Length - 1];

        /// <summary>
        ///     Devuelve el RUT en formato <code>12345678-9</code>.
        /// </summary>
        /// <returns>RUT en formato <code>12345678-9</code>.</returns>
        public virtual string ToAccountName()
        {
            return ToString();
        }


        /// <summary>
        ///     Devuelve el RUT en formato <code>12345678-9</code>.
        /// </summary>
        /// <returns>RUT en formato <code>12345678-9</code>.</returns>
        public override string ToString()
        {
            return _rut;
        }

        /// <summary>
        ///     Valida el RUT especificado.
        /// </summary>
        /// <param name="rutString">RUT a validar.</param>
        /// <returns><c>true</c> si el RUT especificado es un RUT válido, <c>false</c> en caso contrario.</returns>
        public static bool Validate(string rutString)
        {
            //trim para no contar espacios en blanco
            var rut = rutString.Trim();

            //validar formato
            if (!ValidateFormat(rut))
                return false;

            //obtengo la parte numérica y dv del rut
            string rutNumericPart;
            string dv;
            GetRutParts(rut, out rutNumericPart, out dv);

            //si hubo error al obtener rutNumericPart o dv devolver false
            if (string.IsNullOrEmpty(rutNumericPart) || string.IsNullOrEmpty(dv))
                return false;

            //validar largo
            //if (!IsValidLength(rutNumericPart))
            //    return false;

            //validar dv
            var calculatedDv = CalculateDv(rutNumericPart);
            return calculatedDv == dv;
        }

        public static string CalculateDv(int numericPart)
        {
            var s = 1;
            for (var m = 0; numericPart != 0; numericPart /= 10)
                s = (s + numericPart % 10 * (9 - m++ % 6)) % 11;

            return s != 0 ? (s - 1).ToString() : KChar.ToString();
        }

        public static string CalculateDv(string numericPart)
        {
            var sb = new StringBuilder(numericPart);
            var s = 1;
            for (var m = 0; sb.Length != 0; sb.Remove(sb.Length - 1, 1))
            {
                var charLastDigit = sb[sb.Length - 1];
                if (!char.IsDigit(charLastDigit))
                    throw new ArgumentException("El argumento '{0}' debe contener sólo números.", numericPart);

                var lastDigit = int.Parse(charLastDigit.ToString(), NumberStyles.None);
                s = (s + lastDigit % 10 * (9 - m++ % 6)) % 11;
            }

            return s != 0 ? (s - 1).ToString() : KChar.ToString();
        }

        public static bool IsValidLength(int numericPart)
        {
            return IsValidLength(numericPart.ToString());
        }

        public static bool IsValidLength(string numericPart)
        {
            var rutLength = numericPart.Length + 1;
            return rutLength >= MinLength && rutLength <= MaxLength;
        }

        private static void GetRutParts(string rut, out string numericPart, out string dv)
        {
            dv = null;
            numericPart = null;

            if (string.IsNullOrWhiteSpace(rut)) return;

            var sb = new StringBuilder();
            foreach (var c in rut)
                if (char.IsDigit(c))
                {
                    sb.Append(c);
                }
                else if (char.ToUpper(c) == KChar)
                {
                    sb.Append(KChar);
                    break;
                }

            if (sb.Length <= 1)
                return;

            numericPart = sb.ToString(0, sb.Length - 1);
            dv = sb[sb.Length - 1].ToString();
        }

        /// <summary>
        ///     Valida que el RUT especificado tenga un formato válido.
        ///     Formatos válidos:
        ///     <list type="number">
        ///         <item>
        ///             <description>1.234.567-8</description>
        ///         </item>
        ///         <item>
        ///             <description>1234567-8</description>
        ///         </item>
        ///         <item>
        ///             <description>12345678</description>
        ///         </item>
        ///         <item>
        ///             <description>12.345.678-9</description>
        ///         </item>
        ///         <item>
        ///             <description>12345678-9</description>
        ///         </item>
        ///         <item>
        ///             <description>123456789</description>
        ///         </item>
        ///     </list>
        /// </summary>
        /// <remarks>No valida si el RUT especificado es válido o no, sólo valida su formato.</remarks>
        /// <param name="rut">RUT a validar.</param>
        /// <returns><c>truect</c> si el RUT especificado posee un formato válido, <c>false</c> en caso contrario.</returns>
        private static bool ValidateFormat(string rut)
        {
            if (string.IsNullOrWhiteSpace(rut))
                return false;

            var match = Regex.IsMatch(rut, RegexPattern, RegexOptions.IgnoreCase);

            return match;
        }
    }
}