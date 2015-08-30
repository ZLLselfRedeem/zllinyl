using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

public partial class SystemConfig_businessDistrictConfig : System.Web.UI.Page
{
    public IShopTagService ShopTagService { get; set; }
    public SystemConfig_businessDistrictConfig()
    {
        ShopTagService = ServiceFactory.Resolve<IShopTagService>();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFirstBusinessDistrictDDLAndFirstBusinessDistrictGR();
        }
    }
    #region 新增商圈
    /// <summary>
    /// 添加一级商圈
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFirstBusinessDistrict_Click(object sender, EventArgs e)
    {
        string name = txtFirstBusinessDistrict.Text;
        if (String.IsNullOrWhiteSpace(name))
        {
            CommonPageOperate.AlterMsg(this, "请填写一级商圈名称");
            return;
        }
        if (name.Length > 10)
        {
            CommonPageOperate.AlterMsg(this, "请填写不超过10个字符一级商圈名称");
            return;
        }
        int cityId = Common.ToInt32(ddlCity.SelectedValue);
        if (cityId <= 0)
        {
            CommonPageOperate.AlterMsg(this, "请选择商圈所属城市");
            return;
        }
        if (ShopTagService.AddShopTag(1, cityId, name))
        {
            CommonPageOperate.AlterMsg(this, "一级商圈添加成功");
            BindFirstBusinessDistrictDDLAndFirstBusinessDistrictGR();//刷新列表数据
            txtFirstBusinessDistrict.Text = "";
            return;
        }
        CommonPageOperate.AlterMsg(this, "一级商圈添加失败");
    }
    /// <summary>
    /// 添加二级商圈
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSecondBusinessDistrict_Click(object sender, EventArgs e)
    {
        string name = txtSecondBusinessDistrict.Text;
        if (String.IsNullOrWhiteSpace(txtSecondBusinessDistrict.Text))
        {
            CommonPageOperate.AlterMsg(this, "请填写二级商圈名称");
            return;
        }
        if (name.Length > 10)
        {
            CommonPageOperate.AlterMsg(this, "请填写不超过10个字符二级商圈名称");
            return;
        }
        int cityId = Common.ToInt32(ddlCity.SelectedValue);
        if (cityId <= 0)
        {
            CommonPageOperate.AlterMsg(this, "请选择商圈所属城市");
            return;
        }
        int tagId = Common.ToInt32(ddlFirstBusinessDistrict.SelectedValue);
        if (tagId <= 0)
        {
            CommonPageOperate.AlterMsg(this, "请选择所属一级商圈");
            return;
        }
        if (ShopTagService.AddShopTag(tagId, cityId, name))
        {
            CommonPageOperate.AlterMsg(this, "二级商圈添加成功");
            txtSecondBusinessDistrict.Text = "";
            return;
        }
        CommonPageOperate.AlterMsg(this, "二级商圈添加失败");
    }
    #endregion
    /// <summary>
    /// 绑定一级商圈下拉列表和Gr列表信息
    /// </summary>
    private void BindFirstBusinessDistrictDDLAndFirstBusinessDistrictGR()
    {
        int cityId = Common.ToInt32(ddlCity.SelectedValue);
        if (cityId <= 0)
        {
            return;
        }
        List<ShopTag> list = ShopTagService.GetFirstGradeShopTagByCityId(cityId);
        ddlFirstBusinessDistrict.DataSource = list;
        ddlFirstBusinessDistrict.DataTextField = "Name";
        ddlFirstBusinessDistrict.DataValueField = "TagId";
        ddlFirstBusinessDistrict.DataBind();
        //绑定GR
        grFirstBusinessDistrict.DataSource = list;
        grFirstBusinessDistrict.DataBind();
    }

    /// <summary>
    /// 切换城市，刷新一级商圈信息列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindFirstBusinessDistrictDDLAndFirstBusinessDistrictGR();
    }
    /// <summary>
    /// 选择操作一级商圈列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grFirstBusinessDistrict_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
        int tagId = Common.ToInt32(grFirstBusinessDistrict.DataKeys[index].Values["TagId"].ToString());//父商圈编号
        int flag = Common.ToInt32(grFirstBusinessDistrict.DataKeys[index].Values["Flag"].ToString());
        grFirstBusinessDistrict.SelectedIndex = index;
        int cityId = Common.ToInt32(ddlCity.SelectedValue);
        ViewState["flag"] = flag;
        List<ShopTag> list = ShopTagService.GetSecondGradeShopTagByCityId(cityId, flag);
        switch (e.CommandName)
        {
            case "QuerySecond"://查询子商圈
                if (!list.Any())
                {
                    CommonPageOperate.AlterMsg(this, "无任何二级商圈信息");
                }
                grSecondBusinessDistrict.DataSource = list;
                grSecondBusinessDistrict.DataBind();
                break;
            case "UpdateFirst"://保存
                TextBox textBox = grFirstBusinessDistrict.Rows[index].FindControl("grtxtFirstBusinessDistrict") as TextBox;
                if (String.IsNullOrWhiteSpace(textBox.Text))
                {
                    CommonPageOperate.AlterMsg(this, "请填写需要修改的一级商圈名称");
                    return;
                }
                else
                {
                    if (ShopTagService.UpdateShopTagName(tagId, textBox.Text))
                    {
                        CommonPageOperate.AlterMsg(this, "当前一级商圈名称修改成功");
                        BindFirstBusinessDistrictDDLAndFirstBusinessDistrictGR();
                    }
                    else
                    {
                        CommonPageOperate.AlterMsg(this, "当前一级商圈名称修改失败");
                    }
                }
                break;
            case "DeleteFirst"://删除
                string tagIds = "(" + tagId;
                foreach (var item in list)
                {
                    tagIds += item.TagId + ",";
                }
                tagIds = tagIds.TrimEnd(',') + ")";
                if (ShopTagService.DeleteShopTag(tagIds))
                {
                    CommonPageOperate.AlterMsg(this, "当前一级商圈删除成功");
                    BindFirstBusinessDistrictDDLAndFirstBusinessDistrictGR();
                }
                else
                {
                    CommonPageOperate.AlterMsg(this, "当前一级商圈删除失败");
                }
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 选择操作二级商圈列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grSecondBusinessDistrict_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
        int tagId = Common.ToInt32(grSecondBusinessDistrict.DataKeys[index].Values["TagId"].ToString());//父商圈编号
        grSecondBusinessDistrict.SelectedIndex = index;
        switch (e.CommandName)
        {
            case "UpdateSecond"://保存
                TextBox textBox = grSecondBusinessDistrict.Rows[index].FindControl("grtxtSecondBusinessDistrict") as TextBox;
                if (String.IsNullOrWhiteSpace(textBox.Text))
                {
                    CommonPageOperate.AlterMsg(this, "请填写需要修改的二级商圈名称");
                    return;
                }
                else
                {
                    if (ShopTagService.UpdateShopTagName(tagId, textBox.Text))
                    {
                        CommonPageOperate.AlterMsg(this, "当前二级商圈名称修改成功");
                    }
                    else
                    {
                        CommonPageOperate.AlterMsg(this, "当前二级商圈名称修改失败");
                    }
                }
                break;
            case "DeleteSecond"://删除
                if (ShopTagService.DeleteShopTag("(" + tagId + ")"))
                {
                    CommonPageOperate.AlterMsg(this, "当前二级商圈删除成功");
                    List<ShopTag> list = ShopTagService.GetSecondGradeShopTagByCityId(Common.ToInt32(ddlCity.SelectedValue), Common.ToInt32(ViewState["flag"]));
                    grSecondBusinessDistrict.DataSource = list;
                    grSecondBusinessDistrict.DataBind();
                }
                else
                {
                    CommonPageOperate.AlterMsg(this, "当前二级商圈删除失败");
                }
                break;
            default:
                break;
        }
    }
}