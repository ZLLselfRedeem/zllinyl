using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class SystemConfig_foodPlazaConfig : System.Web.UI.Page
{
    public FoodPlazaOperate FoodPlazaOperate { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (flag.Value == "true")
        {
            checkShop.Value = "";//回传页面显示公司名称
        }
        else
        {

            checkShop.Value = Common.ToString(Request.QueryString["name"]);//回传页面显示公司名称
        }
        if (!IsPostBack)
        {
            txtStarTime.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy/MM/dd");
            shopList.InnerHtml = "";
            BindGrFoodPlaza(1, 10);
        }
    }
    public SystemConfig_foodPlazaConfig()
    {
        FoodPlazaOperate = new FoodPlazaOperate();//注入实例
    }
    /// <summary>
    /// 切换城市
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        checkShop.Value = "";
        BindGrFoodPlaza(1, 10);
    }
    /// <summary>
    /// 查询刷新列表事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrFoodPlaza(1, 10);
    }

    private void BindGrFoodPlaza(int pageIndex, int pageSize)
    {
        int cityId = Common.ToInt32(ddlCity.SelectedValue);
        int shopId = 0;
        if (flag.Value == "true")
        {
            shopId = 0;
        }
        else
        {
            shopId = String.IsNullOrWhiteSpace(checkShop.Value) ? 0 : Common.ToInt32(Request.QueryString["id"]);
        }
        double preOrderSum = Common.ToDouble(txtOrderMount.Text);
        int isPaid = Common.ToInt32(ddlIsPay.SelectedValue);
        string strTime = Common.ToString(txtStarTime.Text + " 00:00:00");
        string endTime = Common.ToString(txtEndTime.Text + " 23:59:59");
        int totalCount = 0;
        //int pageSize
        //int pagsIndex
        List<FoodPlazaConfigPage> dataList = FoodPlazaOperate.PagingFoodPlazaOrder(cityId, shopId, preOrderSum,
             isPaid, strTime, endTime, pageSize, pageIndex, out  totalCount);
        if (dataList.Any())
        {
            AspNetPager1.RecordCount = totalCount;
            grFoodPlaza.DataSource = dataList;
            grFoodPlaza.DataBind();
            for (int i = 0; i < grFoodPlaza.Rows.Count; i++)
            {
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
                        imgDish.ImageUrl = list[j].dishImg.ToString() + "@100w_100h_1e_1c";

                        //CheckBox cbDish = rptDishImg.Items[j].FindControl("cbDish") as CheckBox;
                        //cbDish.Text = list[j].dishId.ToString();

                        HiddenField hidden_cbDish = rptDishImg.Items[j].FindControl("hidden_cbDish") as HiddenField;
                        hidden_cbDish.Value = list[j].dishId.ToString();
                        if (j > 0 && (j + 1) % 6 == 0)
                        {
                            rptDishImg.Controls.AddAt(j + 1, new LiteralControl("</tr><tr>"));
                        }
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
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindGrFoodPlaza(((AspNetPager1.StartRecordIndex - 1) / 10) + 1, 10);
    }
    /// <summary>
    /// 发布事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grFoodPlaza_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<int> cbDishs = new List<int>();
        Repeater rptDishImg = grFoodPlaza.Rows[grFoodPlaza.SelectedIndex].Cells[2].FindControl("rptDishImg") as Repeater;
        for (int j = 0; j < rptDishImg.Items.Count; j++)
        {
            CheckBox cbDish = rptDishImg.Items[j].FindControl("cbDish") as CheckBox;
            if (cbDish.Checked)
            {
                HiddenField hidden_cbDish = rptDishImg.Items[j].FindControl("hidden_cbDish") as HiddenField;
                // cbDishs.Add(Common.ToInt32(cbDish.Text));
                cbDishs.Add(Common.ToInt32(hidden_cbDish.Value));
            }
        }
        if (cbDishs.Count > 3)
        {
            CommonPageOperate.AlterMsg(this, "最多只能选择3张图片");
            BindGrFoodPlaza(((AspNetPager1.StartRecordIndex - 1) / 10) + 1, 10);//刷新当前页面数据
            return;
        }
        if (cbDishs.Count <= 0)
        {
            CommonPageOperate.AlterMsg(this, "请选择图片");
            BindGrFoodPlaza(((AspNetPager1.StartRecordIndex - 1) / 10) + 1, 10);//刷新当前页面数据
            return;
        }
        //        if (cbDishs.Count < 3)
        //        {
        //            ClientScript.RegisterStartupScript(this.GetType(), "message", @"<script type='text/javascript'>
        //                                                                        if(confirm('选择不足三张图片，确认要发布，请重新点击发布')) 
        //                                                                          document.getElementById('hiddenConfrim').value='true'; 
        //                                                                        else 
        //                                                                          document.getElementById('hiddenConfrim').value='false';
        //                                                                        </script>");
        //        }
        string dishIds = "";
        foreach (var item in cbDishs)
        {
            dishIds += item + ",";
        }
        // if (hiddenConfrim.Value.Equals("true"))
        //  {
        //发布美食广场图片信息
        var model = new FoodPlaza()
        {
            foodPlazaId = 0,
            personImgUrl = grFoodPlaza.DataKeys[grFoodPlaza.SelectedIndex].Values["personImgUrl"].ToString(),
            dishIds = dishIds.TrimEnd(','),
            shopName = grFoodPlaza.DataKeys[grFoodPlaza.SelectedIndex].Values["shopName"].ToString(),
            preOrder19DianId = Common.ToInt64(grFoodPlaza.DataKeys[grFoodPlaza.SelectedIndex].Values["preOrder19dianId"]),
            orderAmount = Common.ToDouble(grFoodPlaza.DataKeys[grFoodPlaza.SelectedIndex].Values["preOrderSum"]),
            customerId = Common.ToInt32(grFoodPlaza.DataKeys[grFoodPlaza.SelectedIndex].Values["customerId"]),
            cityId = Common.ToInt32(grFoodPlaza.DataKeys[grFoodPlaza.SelectedIndex].Values["cityId"]),//???
            shopId = Common.ToInt32(grFoodPlaza.DataKeys[grFoodPlaza.SelectedIndex].Values["shopId"]),
            status = true,
            latestUpdateTime = DateTime.Now,
            isListTop = false,
            customerName = grFoodPlaza.DataKeys[grFoodPlaza.SelectedIndex].Values["customerName"].ToString(),
            latestOperateEmployeeId = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID
        };
        long result = FoodPlazaOperate.InsertFoodPlaza(model);

        if (result > 0)
        {
            CommonPageOperate.AlterMsg(this, "发布成功");
            BindGrFoodPlaza(((AspNetPager1.StartRecordIndex - 1) / 10) + 1, 10);//刷新当前页面数据
        }
        else
        {
            CommonPageOperate.AlterMsg(this, "发布失败");
            BindGrFoodPlaza(((AspNetPager1.StartRecordIndex - 1) / 10) + 1, 10);//刷新当前页面数据
        }
        // }
        //  else
        //  {
        //    return;
        //  }
    }
}