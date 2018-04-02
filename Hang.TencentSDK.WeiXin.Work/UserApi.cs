using Hang.TencentSDK.WeiXin.Work.Common;
using Hang.TencentSDK.WeiXin.Work.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hang.TencentSDK.WeiXin.Work
{
    /// <summary>
    /// 成员管理
    /// </summary>
    public class UserApi
    {
        /// <summary>
        /// 根据code获取成员信息
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static async Task<GetUserInfoResult> GetUserInfoAsync(string access_token, string code)
        {
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={access_token}&code={code}";
            return await HttpClientManager.GetAsync<GetUserInfoResult>(url);
        }

        /// <summary>
        /// 创建成员
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="userid"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="idCard"></param>
        /// <param name="department">部门ID</param>
        /// <returns></returns>
        public static async Task<ReturnBase> CreateAsync(string access_token, string userid, string name, string phone, string idCard, int department)
        {
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token={access_token}";
            var content = $@"
            {{
               ""userid"": ""{userid}"",
               ""name"": ""{name}"",
               ""english_name"": """",
               ""mobile"": ""{phone}"",
               ""department"": [{department}],
               ""position"": """",
               ""gender"": ""1"",
               ""email"": """",
               ""isleader"": 0,
               ""enable"":1,
               ""telephone"": """",
               ""extattr"": {{""attrs"":[{{""name"":""年级"",""value"":""""}},
                                         {{""name"":""院系"",""value"":""""}},
                                         {{""name"":""专业"",""value"":""""}},
                                         {{""name"":""身份证"",""value"":""{idCard}""}}]}}
            }}";

            return await HttpClientManager.PostAsync<ReturnBase>(url, content);
        }

        /// <summary>
        /// 读取成员
        /// </summary>
        public static void GetAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新成员
        /// </summary>
        public static void UpdateAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除成员
        /// </summary>
        public static async Task<ReturnBase> DeleteAsync(string access_token, string userid)
        {
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/user/delete?access_token={access_token}&userid={userid}";
            return await HttpClientManager.GetAsync<ReturnBase>(url);
        }

        /// <summary>
        /// 批量删除成员
        /// </summary>
        public static void BatchDeleteAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取部门成员
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="department_id">获取的部门id</param>
        /// <param name="fetch_child">1/0：是否递归获取子部门下面的成员</param>
        public static async Task<ListUserResult> SimpleListAsync(string access_token, string department_id, int fetch_child)
        {
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token={access_token}&department_id={department_id}&fetch_child={fetch_child}";
            var result = await HttpClientManager.GetAsync<ListUserResult>(url);
            return result;
        }

        /// <summary>
        /// 获取部门成员详情
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="department_id">获取的部门id</param>
        /// <param name="fetch_child">1/0：是否递归获取子部门下面的成员</param>
        public static async Task<ListUserResult> ListAsync(string access_token, string department_id, int fetch_child)
        {
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/user/list?access_token={access_token}&department_id={department_id}&fetch_child={fetch_child}";
            var result = await HttpClientManager.GetAsync<ListUserResult>(url);
            return result;
        }

        /// <summary>
        /// 验证成员信息成功后，调用如下接口即可让成员成功加入企业。
        /// https://work.weixin.qq.com/api/doc#11378
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static async Task<ReturnBase> Authsucc(string access_token, string userid)
        {
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/user/authsucc?access_token={access_token}&userid={userid}";
            return await HttpClientManager.GetAsync<ReturnBase>(url);
        }

        public class ListUserResult : ReturnBase
        {
            public List<User> userlist { get; set; }
        }

        public class GetUserInfoResult : ReturnBase
        {
            public string UserId { get; set; }
            public string DeviceId { get; set; }
            public string user_ticket { get; set; }
            public int expires_in { get; set; }
        }
    }
}
