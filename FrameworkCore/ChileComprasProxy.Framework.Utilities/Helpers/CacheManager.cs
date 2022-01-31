using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading;

namespace ChileComprasProxy.Framework.Utilities.Helpers
{
    public class CacheManager : ICacheManager
    {
        private static CancellationTokenSource _resetCacheToken = new CancellationTokenSource();
        private readonly IMemoryCache _memoryCache;
        private readonly int _horasCache;

        public CacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }


        public CacheManager(int horasCache)
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _horasCache = horasCache;

        }
        /// <summary>
        /// sets the cache entry T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private T Set<T>(object key, T value)
        {
            var options = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal).SetAbsoluteExpiration(TimeSpan.FromHours(_horasCache));
            options.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
            _memoryCache.Set(key, value, options);
            return value;
        }
        /// <summary>
        /// checks for cache entry existence
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool Contains(object key)
        {
            return _memoryCache.TryGetValue(key, out object result);
        }
        /// <summary>
        /// returns cache entry T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        private T Get<T>(object key)
        {
            return _memoryCache.TryGetValue(key, out T result) ? result : default(T);
        }


   /// <summary>
        /// Intenta obtener un elemento del caché y si no lo encuentra intenta obtenerlo invocando a la funcion pasada por parámetro.
        /// </summary>
        /// <remarks>
        /// Si el item es obtenido llamando a la función, es agregado al caché para obtenerlo desde allí en las futuras invocaciones. 
        /// </remarks>
        /// <param name="key">Clave del item</param>
        /// <param name="load">Función para obtener el elemento en caso de que no se encuentre en cache</param>
        /// <returns>Devuelve el item solicitado</returns>
        public T GetOrAdd<T>(object key, Func<T> load) where T : class
        {

            if (Contains(key))
            {
                return Get<T>(key);
            }

            T item = load();

            Set(key, item);

            return item;
        }


        /// <summary>
        /// expires cache entries T based on CancellationTokenSource cancel 
        /// </summary>
        public void Reset()
        {
            if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested &&
                _resetCacheToken.Token.CanBeCanceled)
            {
                _resetCacheToken.Cancel();
                _resetCacheToken.Dispose();
            }
            _resetCacheToken = new CancellationTokenSource();
        }
    }
}
