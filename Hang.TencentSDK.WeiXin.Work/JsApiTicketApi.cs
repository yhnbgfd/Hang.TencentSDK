using Hang.TencentSDK.WeiXin.Work.Common;
using Hang.TencentSDK.WeiXin.Work.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hang.TencentSDK.WeiXin.Work
{
    public class JsApiTicketApi
    {
        private static readonly object _ticketLocker = new object();
        private static List<TicketEntity> _ticketCache = new List<TicketEntity>();

        /// <summary>
        /// 获取jsapi_ticket
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="refresh"></param>
        /// <returns></returns>
        public static async Task<TicketResult> GetJsApiTicketAsync(string access_token, bool refresh = false)
        {
            if (refresh == false)
            {
                lock (_ticketLocker)
                {
                    var token = _ticketCache.FirstOrDefault(s => s.Access_Token == access_token && s.Expires > DateTime.Now);
                    if (token != null)
                    {
                        return new TicketResult { errcode = 0, ticket = token.Ticket };
                    }
                }
            }

            var url = $"https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={access_token}";
            var result = await HttpClientManager.GetAsync<TicketResult>(url);

            if (result.IsSuccess())
            {
                lock (_ticketLocker)
                {
                    var ticket = _ticketCache.FirstOrDefault(s => s.Access_Token == access_token);
                    if (ticket != null)
                    {
                        ticket.Ticket = result.ticket;
                        ticket.Expires = DateTime.Now.AddSeconds(result.expires_in).AddSeconds(-10);
                    }
                    else
                    {
                        _ticketCache.Add(new TicketEntity
                        {
                            Access_Token = access_token,
                            Ticket = result.ticket,
                            Expires = DateTime.Now.AddSeconds(result.expires_in).AddSeconds(-10),
                        });
                    }
                }
            }
            return result;
        }

        private class TicketEntity
        {
            public string Access_Token { get; set; }
            public string Ticket { get; set; }
            /// <summary>
            /// 过期时间
            /// </summary>
            public DateTime Expires { get; set; }
        }

        public class TicketResult : ReturnBase
        {
            /// <summary>
            /// 获取jsapi_ticket
            /// </summary>
            public string ticket { get; set; }
            /// <summary>
            /// 凭证的有效时间（秒）
            /// </summary>
            public int expires_in { get; set; }
        }
    }
}
