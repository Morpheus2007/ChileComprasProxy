using System;

namespace ChileComprasProxy.Framework.Utilities.Helpers
{
    public interface ICacheManager
    {

        void Reset();

        /// <summary>
        /// Intenta obtener un elemento del caché y si no lo encuentra intenta obtenerlo invocando a la funcion pasada por parámetro.
        /// </summary>
        /// <remarks>
        /// Si el item es obtenido llamando a la función, es agregado al caché para obtenerlo desde allí en las futuras invocaciones. 
        /// </remarks>
        /// <param name="key">Clave del item</param>
        /// <param name="load">Función para obtener el elemento en caso de que no se encuentre en cache</param>
        /// <returns>Devuelve el item solicitado</returns>
        T GetOrAdd<T>(object key, Func<T> load) where T : class;
    }
}