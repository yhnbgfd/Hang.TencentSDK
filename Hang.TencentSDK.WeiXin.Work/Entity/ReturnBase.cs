namespace Hang.TencentSDK.WeiXin.Work.Entity
{
    /// <summary>
    /// 企业微信所有接口，返回包里都有errcode、errmsg。
    /// </summary>
    public class ReturnBase
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 错误说明
        /// </summary>
        public string errmsg { get; set; }


        /// <summary>
        /// 是否成功（errcode==0）
        /// </summary>
        /// <returns></returns>
        public bool IsSuccess() => errcode == 0;
        /// <summary>
        /// 获取code+msg：[errcode] errmsg
        /// </summary>
        /// <returns></returns>
        public string GetCodeMsg() => $"[{errcode}] {errmsg}";
    }
}
