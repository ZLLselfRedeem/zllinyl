using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class SystemConfig_foodPlazaMaintain : System.Web.UI.Page
{
    public FoodPlazaOperate FoodPlazaOperate { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrFoodPlaza(1, 10);
        }
    }
    public SystemConfig_foodPlazaMaintain()
    {
        FoodPlazaOperate = new FoodPlazaOperate();
    }
    /// <summary>
    /// 绑定列表数据
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    void BindGrFoodPlaza(int pageIndex, int pageSize)
    {
        int cityId = Common.ToInt32(ddlCity.SelectedValue);
        string strTime = String.IsNullOrWhiteSpace(txtStarTime.Text) ? "2014/08/01 00:00:00" : txtStarTime.Text + " 00:00:00";
        string endTime = String.IsNullOrWhiteSpace(txtEndTime.Text) ? "2020/01/01 23:59:59" : txtEndTime.Text + " 23:59:59";
        int totalCount = 0;
        //int pageSize
        //int pagsIndex

        List<FoodPlazaConfigPage> dataList = FoodPlazaOperate.PagingFoodPlaza(cityId, pageSize, pageIndex, strTime, endTime, out  totalCount);
        if (dataList.Any())
        {
            AspNetPager1.RecordCount = totalCount;
            grFoodPlaza.DataSource = dataList;
            grFoodPlaza.DataBind();
            for (int i = 0; i < grFoodPlaza.Rows.Count; i++)
            {
                LinkButton lnkListTop = grFoodPlaza.Rows[i].FindControl("lnkListTop") as LinkButton;
                lnkListTop.Text = dataList[i].isListTop == false ? "置顶" : "取消置顶";

                Image imgPerson = grFoodPlaza.Rows[i].FindControl("imgPerson") as Image;//用户头像控件
                imgPerson.ImageUrl = Common.ToString(grFoodPlaza.DataKeys[i].Values["personImgUrl"].ToString());

                Repeater rptDishImg = grFoodPlaza.Rows[i].FindControl("rptDishImg") as Repeater;
                List<FoodPlazaDish> list = grFoodPlaza.DataKeys[i].Values["dishImgs"] as List<FoodPlazaDish>;
                if (list.Any())
                {
                    rptDishImg.DataSource = list;
                    rptDishImg.DataBind();
                    for (int j = 0; j < rptDishImg.Items.Count; j++)
                    {
                        Image imgDish = rptDishImg.Items[j].FindControl("imgDish") as Image;
                        imgDish.ImageUrl = Common.ToString(list[j].dishImg) + "@100w_100h_1e_1c";
                    }
                }
            }
        }
        else
        {
            AspNetPager1.RecordCount = 0;
            grFoodPlaza.DataSource = dataList;
            grFoodPlaza.DataBind();
        }
    }
    /// <summary>
    /// 查询列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrFoodPlaza(1, 10);
    }
    /// <summary>
    /// 分页数据加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindGrFoodPlaza(((AspNetPager1.StartRecordIndex - 1) / 10) + 1, 10);//刷新当前页面数据
    }
    /// <summary>
    /// 事件群
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grFoodPlaza_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        long foodPlazaId = Common.ToInt32(grFoodPlaza.DataKeys[index].Values["foodPlazaId"].ToString());//主键
        int employeeID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
        bool falg = false;
        string des = "";
        switch (Common.ToString(e.CommandName))
        {
            case "ListTop"://置顶
                LinkButton lnkListTop = grFoodPlaza.Rows[index].FindControl("lnkListTop") as LinkButton;
                bool isListTop = lnkListTop.Text == "置顶" ? true : false;
                falg = FoodPlazaOperate.ListTopFoodPlaza(foodPlazaId, employeeID, isListTop);
                des = "置顶操作";
                break;
            case "UpdateData"://更新
                falg = FoodPlazaOperate.UpdateLatestOperate(foodPlazaId, employeeID);
                des = "更新操作";
                break;
            case "DeleteData"://删除
                falg = FoodPlazaOperate.DeleteFoodPlaza(foodPlazaId, employeeID);
                des = "删除操作";
                break;
            default:
                break;
        }
        if (falg == true)
        {
            BindGrFoodPlaza(((AspNetPager1.StartRecordIndex - 1) / 10) + 1, 10);//刷新当前页面数据
        }
        else
        {
            CommonPageOperate.AlterMsg(this, des + "失败");
        }
    }
}