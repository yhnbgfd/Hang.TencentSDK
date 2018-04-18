using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hang.TencentSDK.WeiXin.MP
{
    /// <summary>
    /// HttpClient管理器
    /// </summary>
    internal class HttpClientManager
    {
        private static readonly HttpClient httpClient = new HttpClient();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url)
        {
            using (var response = await httpClient.GetAsync(url))
            {
                return await response.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// HTTP Get 得内容并序列化成 T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(string url) where T : new()
        {
            using (var response = await httpClient.GetAsync(url))
            {
                var httpRet = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(httpRet);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, string content)
        {
            using (var response = await httpClient.PostAsync(url, new StringContent(content)))
            {
                return await response.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// HTTP Post 得内容并序列化成 T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static async Task<T> PostAsync<T>(string url, string content) where T : new()
        {
            using (var response = await httpClient.PostAsync(url, new StringContent(content)))
            {
                var httpRet = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(httpRet);
            }
        }
    }
}
