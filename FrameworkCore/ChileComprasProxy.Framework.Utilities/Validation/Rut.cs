using System.Globalization;
using ChileComprasProxy.Framework.Utilities.Validation.Base;

namespace ChileComprasProxy.Framework.Utilities.Validation
{
    public class Rut : RutBase
    {
        public Rut(string rut) : base(rut)
        {
        }

        public Rut(long rut) : this($"{rut}{GetDigit(rut)}")
        {
        }

        public Rut(int rut) : this($"{rut}{GetDigit(rut)}")
        {
        }

        public Rut(int numero, char dv) : base(numero, dv)
        {
        }

        public Rut(string numero, char dv)
            : base(numero, dv)
        {
        }


        public string Format()
        {
            return $"{DotToRut()}-{Dv}";
        }

        public string ToIdLong()
        {
            return ToAccountName().PadLeft(10, '0');
        }

        public int ToIntNumber()
        {
            return int.Parse(Numero);
        }

        public string ToIdShort()
        {
            return Numero.PadLeft(9, '0');
        }

        public string ToString(bool usarPuntosGuion)
        {
            if (!usarPuntosGuion)
                return ToString();

            var puntos = (Numero.Length - 1) / 3;
            var pos = Numero.Length - 1;

            var output = new char[Numero.Length + 2 + puntos];

            output[output.Length - 2] = '-';
            output[output.Length - 1] = Dv;

            while (puntos > 0)
            {
                var index = pos + puntos - 2;
                Numero.ToCharArray(pos - 2, 3).CopyTo(output, index);
                output[--index] = '.';
                pos -= 3;
                puntos--;
            }

            Numero.ToCharArray(0, pos + 1).CopyTo(output, 0);

            return new string(output);
        }

        private static string GetDigit(long rut)
        {
            var contador = 2;
            var acumulador = 0;

            while (rut != 0)
            {
                var multiplo = (int) rut % 10 * contador;
                acumulador = acumulador + multiplo;
                rut = rut / 10;
                contador = contador + 1;

                if (contador == 8)
                    contador = 2;
            }

            var digito = 11 - acumulador % 11;
            var rutDigito = digito.ToString().Trim();

            if (digito == 10)
                rutDigito = "K";

            if (digito == 11)
                rutDigito = "0";

            return rutDigito;
        }

        private string DotToRut()
        {
            var nfi = new CultureInfo("es-CL", false).NumberFormat;
            nfi.NumberDecimalDigits = 0;
            if (long.TryParse(Numero, out var num))
                return num.ToString("N", nfi);

            return Numero ?? string.Empty;
        }
    }
}