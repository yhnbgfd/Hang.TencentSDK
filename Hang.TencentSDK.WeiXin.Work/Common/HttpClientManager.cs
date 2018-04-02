using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hang.TencentSDK.WeiXin.Work.Common
{
    public static class HttpClientManager
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task<string> GetAsync(string url)
        {
            using (var response = await httpClient.GetAsync(url))
            {
                var httpRet = await response.Content.ReadAsStringAsync();
                return httpRet;
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

        public static async Task<string> PostAsync(string url, string content)
        {
            return await PostAsync(url, new StringContent(content));
        }

        public static async Task<string> PostAsync(string url, StringContent content)
        {
            using (var response = await httpClient.PostAsync(url, content))
            {
                var httpRet = await response.Content.ReadAsStringAsync();
                return httpRet;
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
            return await PostAsync<T>(url, new StringContent(content));
        }

        public static async Task<T> PostAsync<T>(string url, StringContent content) where T : new()
        {
            using (var response = await httpClient.PostAsync(url, content))
            {
                var httpRet = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(httpRet);
            }
        }

    }
}
