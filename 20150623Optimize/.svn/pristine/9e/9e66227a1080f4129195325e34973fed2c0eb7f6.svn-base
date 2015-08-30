using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;

public partial class PointsManage_htmlConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int cityId = Common.ToInt32(this.ddlCity.SelectedValue);//城市ID
            BindHtmlByCityId(cityId);
        }
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        int cityId = Common.ToInt32(this.ddlCity.SelectedValue);//城市ID
        BindHtmlByCityId(cityId);
        if (!string.IsNullOrEmpty(this.CKEditor1.Text))
        {
            this.btnDelete.Enabled = true;
        }
        else
        {
            this.btnDelete.Enabled = false;
        }
    }
    /// <summary>
    ///  根据城市ID查找对应的Html内容
    /// </summary>
    /// <param name="cityId"></param>
    private void BindHtmlByCityId(int cityId)
    {
        HtmlOperate _htmlOperate = new HtmlOperate();//逻辑层
       
        HtmlInfo htmlInfo = _htmlOperate.QueryHtmlByCityId(cityId);

        this.CKEditor1.Text = "";
        this.CKEditor1.Text = htmlInfo.html;//赋值
    }
    /// <summary>
    /// 保存按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.CKEditor1.Text))
        {
            HtmlInfo htmlInfo = new HtmlInfo();//Model
            htmlInfo.cityId = Common.ToInt32(this.ddlCity.SelectedValue);//城市ID
            htmlInfo.html = this.CKEditor1.Text;
            
            htmlInfo.html =Common.HtmlDiscodeForCKEditor(htmlInfo.html);

            htmlInfo.status = 1;

            HtmlOperate _htmlOperate = new HtmlOperate();//BLL

            object[] objResult = _htmlOperate.SaveHtml(htmlInfo);

            if (Common.ToBool(objResult[0]))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('保存成功')</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "fail", "<script>alert('保存失败，" + objResult[1].ToString() + "')</script>");
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('内容不能为空')</script>");
        }
    }
    
    /// <summary>
    /// 删除按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        HtmlOperate _htmlOperate = new HtmlOperate();//BLL

        int cityId = Common.ToInt32(this.ddlCity.SelectedValue);//城市ID

        object[] objResult = _htmlOperate.DeleteHtml(cityId);

        if (Common.ToBool(objResult[0]))
        {
            this.CKEditor1.Text = "";
            Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('删除成功')</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "fail", "<script>alert('删除失败，" + objResult[1].ToString() + "')</script>");
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.CKEditor1.Text = "";
    }
}