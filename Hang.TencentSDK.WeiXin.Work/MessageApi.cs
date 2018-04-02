using Hang.TencentSDK.WeiXin.Work.Common;
using Hang.TencentSDK.WeiXin.Work.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Hang.TencentSDK.WeiXin.Work
{
    public class MessageApi
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="touser">成员ID列表（消息接收者，多个接收者用‘|’分隔，最多支持1000个）。特殊情况：指定为@all，则向该企业应用的全部成员发送</param>
        /// <param name="toparty">部门ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数</param>
        /// <param name="totag">标签ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数</param>
        /// <param name="agentid">企业应用的id，整型。可在应用的设置页面查看</param>
        /// <param name="content">消息内容，最长不超过2048个字节</param>
        /// <param name="safe">表示是否是保密消息</param>
        /// <returns></returns>
        public static async Task<SendMessageResult> SendTextAsync(string access_token, IEnumerable<string> touser, IEnumerable<string> toparty, IEnumerable<string> totag, int agentid, string content, bool safe = false)
        {
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={access_token}";
            var safeInt = safe ? 1 : 0;

            var param = $@"
            {{
               ""touser"" : ""{touser.ToStitchingString('|')}"",
               ""toparty"" : ""{toparty.ToStitchingString('|')}"",
               ""totag"" : ""{totag.ToStitchingString('|')}"",
               ""msgtype"" : ""text"",
               ""agentid"" : {agentid},
               ""text"" : {{
                   ""content"" : ""{content}""
               }},
               ""safe"" : {safeInt}
            }}";

            return await HttpClientManager.PostAsync<SendMessageResult>(url, param);
        }

        /// <summary>
        /// 文本卡片消息
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="touser"></param>
        /// <param name="toparty"></param>
        /// <param name="totag"></param>
        /// <param name="agentid"></param>
        /// <param name="textCard"></param>
        /// <param name="safe"></param>
        /// <returns></returns>
        public static async Task<SendMessageResult> SendTextCardAsync(string corpID, string access_token, IEnumerable<string> touser, IEnumerable<string> toparty, IEnumerable<string> totag, int agentid, TextCard textCard)
        {
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={access_token}";

            var param = $@"
            {{
               ""touser"" : ""{touser.ToStitchingString('|')}"",
               ""toparty"" : ""{toparty.ToStitchingString('|')}"",
               ""totag"" : ""{totag.ToStitchingString('|')}"",
               ""msgtype"" : ""textcard"",
               ""agentid"" : {agentid},
               ""textcard"" : {textCard.ToString(corpID)}
            }}";

            return await HttpClientManager.PostAsync<SendMessageResult>(url, param);
        }

        /// <summary>
        /// 图文消息
        /// </summary>
        /// <param name="corpID"></param>
        /// <param name="access_token"></param>
        /// <param name="touser"></param>
        /// <param name="toparty"></param>
        /// <param name="totag"></param>
        /// <param name="agentid"></param>
        /// <param name="news"></param>
        /// <returns></returns>
        public static async Task<SendMessageResult> SendNewsAsync(string corpID, string access_token, IEnumerable<string> touser, IEnumerable<string> toparty, IEnumerable<string> totag, int agentid, News news)
        {
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={access_token}";

            var param = $@"
            {{
               ""touser"" : ""{touser.ToStitchingString('|')}"",
               ""toparty"" : ""{toparty.ToStitchingString('|')}"",
               ""totag"" : ""{totag.ToStitchingString('|')}"",
               ""msgtype"" : ""news"",
               ""agentid"" : {agentid},
               ""news"" : {news.ToString(corpID)}
            }}";

            return await HttpClientManager.PostAsync<SendMessageResult>(url, param);
        }


        /// <summary>
        /// 文本卡片(双括号括起的内容高亮并且自动换行)
        /// </summary>
        public class TextCard
        {
            /// <summary>
            /// 标题，不超过128个字节，超过会自动截断
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 描述，不超过512个字节，超过会自动截断
            /// </summary>
            public string description { get; set; }
            /// <summary>
            /// 点击后跳转的链接。
            /// </summary>
            public string url { get; set; }
            /// <summary>
            /// 按钮文字。 默认为“详情”， 不超过4个文字，超过自动截断。
            /// </summary>
            public string btntxt { get; set; } = "详情";

            public string ToString(string corpID)
            {
                description = $"<div class=\"normal\">{description}</div>"
                    .Replace("{{", "</div><div class=\"highlight\">")
                    .Replace("}}", "</div><div class=\"normal\">")
                    .Replace("<div class=\"normal\"></div>", "");
                description = $"<div class=\"gray\">{DateTime.Now.ToString("yyyy年M月d日")}</div> {description}";
                url = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={corpID}&redirect_uri={WebUtility.UrlEncode(url)}&response_type=code&scope=snsapi_base&agentid=&state=#wechat_redirect";
                return JsonConvert.SerializeObject(this);
            }
        }

        /// <summary>
        /// 图文信息
        /// </summary>
        public class News
        {
            public List<article> articles { get; set; } = new List<article>();

            public class article
            {
                public string title { get; set; }
                public string description { get; set; }
                public string url { get; set; }
                public string picurl { get; set; }
                public string btntxt { get; set; } = "阅读全文";
            }

            public string ToString(string corpID)
            {
                foreach (var item in articles)
                {
                    item.url = $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={corpID}&redirect_uri={WebUtility.UrlEncode(item.url)}&response_type=code&scope=snsapi_base&agentid=&state=#wechat_redirect";
                }
                return JsonConvert.SerializeObject(this);
            }
        }

        public class SendMessageResult : ReturnBase
        {
            /// <summary>
            /// 不区分大小写，返回的列表都统一转为小写
            /// </summary>
            public string invaliduser { get; set; }
            public string invalidparty { get; set; }
            public string invalidtag { get; set; }
        }
    }
}
