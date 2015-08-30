using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class BankMoneyRecordOperate
    {
        readonly BankMoneyRecordManager manager=new BankMoneyRecordManager();
        public int AddRecord(BankMoneyRecord model)
        {
            return manager.insertRecord(model);
        }
   }
}
