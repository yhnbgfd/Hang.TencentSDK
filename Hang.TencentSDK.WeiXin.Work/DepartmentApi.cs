using Hang.TencentSDK.WeiXin.Work.Common;
using Hang.TencentSDK.WeiXin.Work.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Hang.TencentSDK.WeiXin.Work
{
    /// <summary>
    /// 部门管理
    /// </summary>
    public class DepartmentApi
    {
        /// <summary>
        /// 创建部门【已完成】
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="name">部门名称。长度限制为1~64个字节</param>
        /// <param name="parentid">父部门id，32位整型</param>
        /// <param name="order">在父部门中的次序值。order值大的排序靠前。有效的值范围是[0, 2^32)</param>
        /// <returns></returns>
        public static async Task<(bool isSuccess, string msg, int id)> CreateAsync(string access_token, string name, int parentid, int order = 0)
        {
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token={access_token}";
            var content = JsonConvert.SerializeObject(new
            {
                name,
                parentid,
                order,
            });

            var httpRet = await HttpClientManager.PostAsync(url, content);

            var json = JObject.Parse(httpRet);
            var errcode = json.Value<long>("errcode");
            var errmsg = json.Value<string>("errmsg");
            if (errcode == 0)
            {
                return (true, errmsg, json.Value<int>("id"));
            }
            else
            {
                return (false, errmsg, 0);
            }
        }

        /// <summary>
        /// 更新部门【已完成】
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="id">部门id</param>
        /// <param name="name">部门名称。长度限制为1~64个字节</param>
        /// <param name="parentid">父部门id</param>
        /// <param name="order">在父部门中的次序值。order值大的排序靠前。有效的值范围是[0, 2^32)</param>
        /// <returns></returns>
        public static async Task<ReturnBase> UpdateAsync(string access_token, int id, string name, int parentid, int order = 0)
        {
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token={access_token}";
            var content = JsonConvert.SerializeObject(new
            {
                id,
                name,
                parentid,
                order,
            });

            return await HttpClientManager.PostAsync<ReturnBase>(url, content);
        }

        /// <summary>
        /// 删除部门【未完成】
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="id">部门id。（注：不能删除根部门；不能删除含有子部门、成员的部门）</param>
        /// <returns></returns>
        public static async Task<ReturnBase> DeleteAsync(string access_token, int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取部门列表【未完成】
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="id">部门id。获取指定部门及其下的子部门。 如果不填，默认获取全量组织架构</param>
        /// <returns></returns>
        public static async Task<(bool isSuccess, string msg)> ListAsync(string access_token, int? id = null)
        {
            var url = $"https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={access_token}&id={id}";
            var httpRet = await HttpClientManager.GetAsync(url);

            return (true, httpRet);
        }
    }
}
