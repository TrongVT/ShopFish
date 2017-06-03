using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShopFish.Common
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { set; get; }
        public string USerName { set; get; }
    }
}