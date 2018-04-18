using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hang.TencentSDK.WeiXin.MP.Apis
{
    class MessageApi
    {
        public async Task SendTemplateMessageAsync(string touser, string template_id)
        {
            var token = await AccessTokenManager.GetAccessTokenAsync();
            var url = $"https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={token}";

            throw new NotImplementedException();
        }
    }
}
