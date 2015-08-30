using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class DemoOperate
    {
        public int Update()
        {
            DataTable initData = new DemoManager().GetInitData();
            foreach (DataRow dr in initData.Rows)
            {
                dr["couponName"] = "修改2323";
                dr["couponDesc"] = "统一描述2323";
            }
            initData.TableName = "CouponInfo";
            var columnModels = new List<DemoModel>();
            columnModels.Add(new DemoModel()
            {
                Size = 100,
                ColumName = "couponName",
                SqlDbType = SqlDbType.NVarChar,
                IsWhere = false
            });
            columnModels.Add(new DemoModel()
            {
                Size = 500,
                ColumName = "couponDesc",
                SqlDbType = SqlDbType.NVarChar,
                IsWhere = false
            });
            columnModels.Add(new DemoModel()
            {
                Size = 8,
                ColumName = "couponID",
                SqlDbType = SqlDbType.BigInt,
                IsWhere = true
            });
            var columnsName = new string[] { "couponName" };
            string limitWhere = "";
            int onceUpdateNumber = 1000;
            return new DemoManager().BatchUpdate(initData, columnModels, limitWhere, onceUpdateNumber);
            //return new DemoManager().Update(initData);
        }
    }
}
