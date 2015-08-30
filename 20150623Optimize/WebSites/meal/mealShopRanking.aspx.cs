using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class meal_mealShopRanking : System.Web.UI.Page
{
    private readonly ShopSequenceOperate Operate = new ShopSequenceOperate();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 页面获取门店年夜饭排序数据
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string GetMealShopRankingData(int type)
    {
        var list = new ShopSequenceOperate().GetShopSequence(type);
        if (!list.Any())
        {
            return "";
        }
        return JsonOperate.JsonSerializer<List<ShopSequenceMedel>>(list);
    }

    /// <summary>
    /// 页面提交门店年夜饭排序数据
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string SubmitMealShopRankingData(string jsonstr)
    {
        var models = JsonOperate.JsonDeserialize<List<ShopSequenceMedel>>(jsonstr);
        if (!models.Any())
        {
            return "-1";
        }
        DataTable dt = ToDataTable(models);
        if (dt == null || dt.Rows.Count <= 0)
        {
            return "-1";
        }
        using (TransactionScope ts = new TransactionScope())
        {
            var Operate = new ShopSequenceOperate();
            bool delete = Operate.DeleteShopSequenceByType(models[0].type);
            bool insert = Operate.BatchAddShopSequence(dt);
            if (delete && insert)
            {
                ts.Complete();
                return "1";
            }
        }
        return "-1";
    }

    private static DataTable ToDataTable<T>(IEnumerable<T> collection)
    {
        var props = typeof(T).GetProperties();
        var dt = new DataTable();
        dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
        if (collection.Count() > 0)
        {
            for (int i = 0; i < collection.Count(); i++)
            {
                ArrayList tempList = new ArrayList();
                foreach (PropertyInfo pi in props)
                {
                    object obj = pi.GetValue(collection.ElementAt(i), null);
                    tempList.Add(obj);
                }
                object[] array = tempList.ToArray();
                dt.LoadDataRow(array, true);
            }
        }
        return dt;
    }
}