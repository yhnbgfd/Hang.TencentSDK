using System;
using System.Collections.Generic;

namespace Hang.TencentSDK.WeiXin.Work.Entity
{
    [Serializable]
    public class User
    {
        public string userid { get; set; }
        public string name { get; set; }
        public int[] department { get; set; }
        public int[] order { get; set; }
        public string position { get; set; }
        public string mobile { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public int isleader { get; set; }
        public string avatar { get; set; }
        public string telephone { get; set; }
        public string english_name { get; set; }
        public int status { get; set; }
        public Extattr extattr { get; set; }
    }

    [Serializable]
    public class Extattr
    {
        public List<NameValue> attrs { get; set; }
    }

    [Serializable]
    public class NameValue
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
