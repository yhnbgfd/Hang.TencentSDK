using Hang.TencentSDK.WeiXin.Work.Common;
using Hang.TencentSDK.WeiXin.Work.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hang.TencentSDK.WeiXin.Work
{
    public class AccessTokenApi
    {
        private static readonly object _tokenLocker = new object();
        private static List<Token> _tokenCache = new List<Token>();

        /// <summary>
        /// 获取access_token
        /// </summary>
        /// <param name="corpid"></param>
        /// <param name="corpsecret"></param>
        /// <param name="refresh">是否强制刷新access_token</param>
        /// <returns></returns>
        public static async Task<AccessTokenReturn> GetTokenAsync(string corpid, string corpsecret, bool refresh = false)
        {
            if (refresh == false)
            {
                lock (_tokenLocker)
                {
                    var token = _tokenCache.FirstOrDefault(s => s.Corpid == corpid && s.Corpsecret == corpsecret && s.Expires > DateTime.Now);
                    if (token != null)
                    {
                        return new AccessTokenReturn { errcode = 0, access_token = token.Access_Token };
                    }
                }
            }

            var url = $"https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={corpid}&corpsecret={corpsecret}";
            var result = await HttpClientManager.GetAsync<AccessTokenReturn>(url);

            if (result.IsSuccess())
            {
                lock (_tokenLocker)
                {
                    var token = _tokenCache.FirstOrDefault(s => s.Corpid == corpid && s.Corpsecret == corpsecret);
                    if (token != null)
                    {
                        token.Access_Token = result.access_token;
                        token.Expires = DateTime.Now.AddSeconds(result.expires_in).AddSeconds(-10);
                    }
                    else
                    {
                        _tokenCache.Add(new Token
                        {
                            Corpid = corpid,
                            Corpsecret = corpsecret,
                            Access_Token = result.access_token,
                            Expires = DateTime.Now.AddSeconds(result.expires_in).AddSeconds(-10),
                        });
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取企业access_token
        /// </summary>
        /// <param name="corpid"></param>
        /// <param name="corpsecret"></param>
        /// <param name="refresh">是否强制刷新access_token</param>
        /// <returns></returns>
        public AccessTokenReturn GetCorpTokenAsync()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 保存AccessToken缓存的实体
        /// </summary>
        private class Token
        {
            public string Corpid { get; set; }
            public string Corpsecret { get; set; }
            public string Access_Token { get; set; }
            /// <summary>
            /// 过期时间
            /// </summary>
            public DateTime Expires { get; set; }
        }

        /// <summary>
        /// 获取access_token返回值
        /// </summary>
        public class AccessTokenReturn : ReturnBase
        {
            /// <summary>
            /// 获取到的凭证。长度为64至512个字节
            /// </summary>
            public string access_token { get; set; }
            /// <summary>
            /// 凭证的有效时间（秒）
            /// </summary>
            public int expires_in { get; set; }
        }
    }
}
