using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model.Interface;

namespace VAGastronomistMobileApp.Model
{
    public class ShopHandleLog:IShopHandleLog
    {
        public long Id { get; set; }
        public string ShopName { get; set; }
        public int? ShopId { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int? HandleStatus { get; set; }
        public string HandleDesc { get; set; }
        public DateTime? OperateTime { get; set; }
        public int? CityId { get; set; }

        public override string ToString()
        {
            if (HandleStatus == (int)VAShopHandleStatus.SHOP_Pass)
            {
                HandleDesc = "审核通过";
            }
            else if (HandleStatus == (int)VAShopHandleStatus.SHOP_UnHandle)
            {
                HandleDesc = "未审核";
            }
            else if (HandleStatus == (int)VAShopHandleStatus.SHOP_Pass)
            {
                HandleDesc = "未审核通过";
            }
            return HandleDesc;
        }
    }
}
