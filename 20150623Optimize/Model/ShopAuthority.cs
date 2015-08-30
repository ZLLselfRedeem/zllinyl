﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class ShopAuthority
    {
        public int ShopAuthorityId { set; get; }

        public string ShopAuthorityName { set; get; }

        public string ShopAuthorityDescription { set; get; }

        public int ShopAuthorityStatus { set; get; }

        public int ShopAuthoritySequence { set; get; }

        /// <summary>
        /// <para>类型</para>
        /// <para>1-收银宝;2-悠先服务</para>
        /// </summary>
        public ShopAuthorityType ShopAuthorityType { set; get; }

        public bool IsClientShow { set; get; }

        public bool IsSYBShow { set; get; }
        public string AuthorityCode { set; get; }
        public bool IsViewAllocWorkerEnable { set; get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null)
                return false;
            ShopAuthority other = obj as ShopAuthority;
            if ((object)other == null)
                return false;
            return this.ShopAuthorityId.Equals(other.ShopAuthorityId);
        }

        public override int GetHashCode()
        {
            return this.ShopAuthorityId.GetHashCode();
        }
    }

    public enum ShopAuthorityType
    {
        收银宝 = 1,
        悠先服务 = 2,
        悠先服务内部 = 3,
        悠先服务统计=4
    }
}
