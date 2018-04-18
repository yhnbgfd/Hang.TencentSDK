using System;
using System.Collections.Generic;
using System.Text;

namespace Hang.TencentSDK.WeiXin.MP
{
    public static class MP
    {
        internal static string AppID { get; private set; }
        internal static string AppSecret { get; private set; }

        /// <summary>
        /// 注册微信公众号
        /// </summary>
        /// <param name="appID"></param>
        /// <param name="appSecret"></param>
        public static void Register(string appID, string appSecret)
        {
            AppID = appID;
            AppSecret = appSecret;
        }

    }
}
