using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hang.TencentSDK.WeiXin.Work.Common
{
    public class EncryptionHelper
    {
        /// <summary>
        /// 验签参数
        /// </summary>
        /// <param name="token"></param>
        /// <param name="msg_signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        public static bool Verify(string token, string msg_signature, string timestamp, string nonce, string echostr)
        {
            var list = new List<string> { token, timestamp, nonce, echostr };
            list.Sort(StringComparer.Ordinal);

            var sb = new StringBuilder();
            foreach (var str in list)
            {
                sb.Append(str);
            }

            using (SHA1 sha = new SHA1CryptoServiceProvider())
            {
                var dev_msg_signature = BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()))).Replace("-", "").ToLower();
                return dev_msg_signature == msg_signature;
            }
        }

        /// <summary>
        /// 解密内容（适用于验证URL，用在接收信息时会有问题）
        /// </summary>
        /// <param name="echostr"></param>
        /// <param name="encodingAESKey"></param>
        /// <returns></returns>
        public static async Task<string> DecryptMsgAsync(string echostr, string encodingAESKey)
        {
            var aes_msg = Convert.FromBase64String(echostr);

            var btmpMsg = await AesDecryptAsync(aes_msg, Convert.FromBase64String(encodingAESKey + "="));

            int len = BitConverter.ToInt32(btmpMsg, 16);
            len = IPAddress.NetworkToHostOrder(len);

            byte[] bMsg = new byte[len];
            byte[] bCorpid = new byte[btmpMsg.Length - 20 - len];
            Array.Copy(btmpMsg, 20, bMsg, 0, len);
            Array.Copy(btmpMsg, 20 + len, bCorpid, 0, btmpMsg.Length - 20 - len);

            return Encoding.UTF8.GetString(bMsg);
        }

        /// <summary>
        /// 解密内容（直接截取xml，适用于接收信息）
        /// </summary>
        /// <param name="echostr"></param>
        /// <param name="encodingAESKey"></param>
        /// <returns></returns>
        public static async Task<string> DecryptXmlMsgAsync(string echostr, string encodingAESKey)
        {
            var aes_msg = Convert.FromBase64String(echostr);

            var btmpMsg = await AesDecryptAsync(aes_msg, Convert.FromBase64String(encodingAESKey + "="));

            var str = Encoding.UTF8.GetString(btmpMsg);
            var start = str.IndexOf("<xml>");
            var end = str.LastIndexOf("</xml>") + 6;

            var xmlData = str.Substring(start, end - start);

            return xmlData;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="Key">Key=IV</param>
        /// <returns></returns>
        private static async Task<byte[]> AesDecryptAsync(byte[] cipherText, byte[] Key)
        {
            byte[] IV = new byte[16];
            Array.Copy(Key, IV, 16);

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.None;
                aes.Mode = CipherMode.CBC;
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            var plaintext = await sr.ReadToEndAsync();
                            return Encoding.UTF8.GetBytes(plaintext);
                        }
                    }
                }
            }
        }

    }
}
