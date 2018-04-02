using Hang.TencentSDK.WeiXin.Work.Common;
using Hang.TencentSDK.WeiXin.Work.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Hang.TencentSDK.WeiXin.Work
{
    /// <summary>
    /// 标签管理
    /// </summary>
    public class TagApi
    {
        /// <summary>
        /// 创建标签
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="tagname">标签名称，长度限制为32个字（汉字或英文字母），标签名不可与其他标签重名。</param>
        /// <returns></returns>
        public static async Task<(bool isSuccess, string msg, int id)> CreateAsync(string access_token, string tagname)
        {
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/tag/create?access_token={access_token}";
            var content = JsonConvert.SerializeObject(new
            {
                tagname,
            });

            var httpRet = await HttpClientManager.PostAsync(url, content);

            var json = JObject.Parse(httpRet);
            var errcode = json.Value<long>("errcode");
            var errmsg = json.Value<string>("errmsg");
            if (errcode == 0)
            {
                return (true, errmsg, json.Value<int>("tagid"));
            }
            else
            {
                return (false, errmsg, 0);
            }
        }

        /// <summary>
        /// 更新标签名字
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="tagid">标签ID</param>
        /// <param name="tagname">标签名称，长度限制为32个字（汉字或英文字母），标签不可与其他标签重名。</param>
        /// <returns></returns>
        public static async Task<ReturnBase> UpdateAsync(string access_token, int tagid, string tagname)
        {
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/tag/update?access_token={access_token}";
            var content = JsonConvert.SerializeObject(new
            {
                tagid,
                tagname,
            });

            return await HttpClientManager.PostAsync<ReturnBase>(url, content);
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="id">标签ID</param>
        /// <returns></returns>
        public static async Task<ReturnBase> DeleteAsync(string access_token, int tagid)
        {
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/tag/delete?access_token={access_token}&tagid={tagid}";
            return await HttpClientManager.GetAsync<ReturnBase>(url);
        }

        /// <summary>
        /// 获取标签成员
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="tagid">标签ID</param>
        /// <returns></returns>
        public static async Task<ReturnBase> GetAsync(string access_token, int tagid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 增加标签成员
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="tagid">标签ID</param>
        /// <param name="userlist">企业成员ID列表，注意：userlist、partylist不能同时为空，单次请求长度不超过1000</param>
        /// <param name="partylist">企业部门ID列表，注意：userlist、partylist不能同时为空，单次请求长度不超过100</param>
        /// <returns></returns>
        public static async Task<ReturnBase> AddTagUsersAsync(string access_token, int tagid, string[] userlist, string[] partylist)
        {
            if (userlist.Length > 1000)
                throw new ArgumentOutOfRangeException("userlist单次请求长度不超过1000");
            if (partylist.Length > 100)
                throw new ArgumentOutOfRangeException("partylist单次请求长度不超过100");

            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除标签成员
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="tagid">标签ID</param>
        /// <param name="userlist">企业成员ID列表，注意：userlist、partylist不能同时为空</param>
        /// <param name="partylist">企业部门ID列表，注意：userlist、partylist不能同时为空</param>
        /// <returns></returns>
        public static async Task<(bool isSuccess, string msg)> DelTagUsersAsync(string access_token, int tagid, string[] userlist, string[] partylist)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static async Task<(bool isSuccess, string msg)> ListAsync(string access_token)
        {
            throw new NotImplementedException();
        }

    }
}
