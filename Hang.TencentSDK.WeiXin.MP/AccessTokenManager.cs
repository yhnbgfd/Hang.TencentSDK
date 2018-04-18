using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hang.TencentSDK.WeiXin.MP
{
    internal static class AccessTokenManager
    {
        private static ReaderWriterLockSlim _readerWriterLockSlim = new ReaderWriterLockSlim();
        private static Token _tokenCache = null;

        private class Token
        {
            internal string AccessToken { get; set; }
            internal DateTime Expires { get; set; }
        }

        /// <summary>
        /// 获取access_token
        /// </summary>
        /// <returns></returns>
        internal static async Task<string> GetAccessTokenAsync()
        {
            _readerWriterLockSlim.TryEnterUpgradeableReadLock(3000);
            try
            {
                if (_tokenCache != null && _tokenCache.Expires >= DateTime.Now)
                {
                    return _tokenCache.AccessToken;
                }
                else
                {
                    var url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={MP.AppID}&secret={MP.AppSecret}";
                    var ret = await HttpClientManager.GetAsync(url);
                    var json = JObject.Parse(ret);
                    if (json.ContainsKey("access_token"))
                    {
                        _readerWriterLockSlim.EnterWriteLock();
                        try
                        {
                            _tokenCache.AccessToken = json.Value<string>("access_token");
                            _tokenCache.Expires = DateTime.Now.AddSeconds(json.Value<int>("expires_in")).AddMinutes(-5);
                        }
                        finally
                        {
                            _readerWriterLockSlim.ExitWriteLock();
                        }
                        return _tokenCache.AccessToken;
                    }
                    else
                    {
                        var errcode = json.Value<int>("errcode");
                        var errmsg = json.Value<string>("errmsg");
                        throw new Exception($"[{errcode}]{errmsg}");
                    }
                }
            }
            finally
            {
                _readerWriterLockSlim.ExitUpgradeableReadLock();
            }
        }
    }
}
