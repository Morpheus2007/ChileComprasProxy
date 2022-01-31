using System.Net.Http;
using System.Threading.Tasks;
using ChileComprasProxy.Framework.Utilities.Http;
using Newtonsoft.Json;

namespace ChileComprasProxy.Framework.Utilities.Json
{
    public static class JsonUtility
    {
        public static async Task<T> ConvertResponseToObject<T>(HttpResponseMessage response)
        {
            var task = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(task);
        }

        public static T ConvertContentToObject<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
        public static async Task<T> ConvertResponseWithBaseToObject<T>(HttpResponseMessage response)
        {
            var task = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<BaseResponse>(task);
            if (results.IsValid)
                return JsonConvert.DeserializeObject<T>(results.Data.ToString());
            throw new HttpRequestException("Error al establecer la conexión \n" +
                                           $"Status Code: {response.StatusCode}\n" +
                                           $"URL: {response.RequestMessage.RequestUri}" +
                                           $"ErrorMessage: {results.ErrorMessage}");
        }
    }
}