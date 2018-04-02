using Hang.TencentSDK.WeiXin.Work.Entity;
using System;
using System.Threading.Tasks;

namespace Hang.TencentSDK.WeiXin.Work
{
    public class BatchApi
    {
        /// <summary>
        /// 邀请成员
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="user">成员ID列表, 最多支持1000个。</param>
        /// <param name="party">部门ID列表，最多支持100个。</param>
        /// <param name="tag">标签ID列表，最多支持100个。</param>
        /// <returns></returns>
        public static Task<ReturnBase> InviteAsync(string access_token, string[] user, int[] party, int[] tag)
        {
            if (user.Length > 1000)
                throw new ArgumentOutOfRangeException("user单次请求长度不超过1000");
            if (party.Length > 100)
                throw new ArgumentOutOfRangeException("party单次请求长度不超过100");
            if (tag.Length > 100)
                throw new ArgumentOutOfRangeException("tag单次请求长度不超过100");

            var url = "https://qyapi.weixin.qq.com/cgi-bin/batch/invite?access_token={access_token}";

            throw new NotImplementedException();
        }

    }
}
