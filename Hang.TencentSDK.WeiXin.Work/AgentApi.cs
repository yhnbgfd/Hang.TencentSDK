using Hang.TencentSDK.WeiXin.Work.Entity;
using System;
using System.Threading.Tasks;

namespace Hang.TencentSDK.WeiXin.Work
{
    /// <summary>
    /// 应用管理
    /// </summary>
    public class AgentApi
    {
        /// <summary>
        /// 获取应用
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="agentid">授权方应用id</param>
        /// <returns></returns>
        public static async Task<ReturnBase> GetAsync(string access_token, string agentid)
        {
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/agent/get?access_token={access_token}&agentid={agentid}";
            throw new NotImplementedException();
        }

        public static async Task<ReturnBase> SetAsync(string access_token)
        {
            throw new NotImplementedException();
        }

        public static async Task<ReturnBase> ListAsync(string access_token)
        {
            throw new NotImplementedException();
        }
    }
}
