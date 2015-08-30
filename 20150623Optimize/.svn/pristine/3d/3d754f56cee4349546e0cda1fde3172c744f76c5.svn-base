using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;
using CloudStorage;

public partial class ViewAllocVip_articalManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int cityId = Common.ToInt32(this.ddlCity.SelectedValue);//城市ID
            BindArticleByCityId(cityId);
            BindVideo();
        }
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        int cityId = Common.ToInt32(this.ddlCity.SelectedValue);//城市ID
        BindArticleByCityId(cityId);
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
    private void BindArticleByCityId(int cityId)
    {
        ShopVipArticleOperate shopVipArticleOperate = new ShopVipArticleOperate();
        DataTable dt = shopVipArticleOperate.GetArticle(cityId);
        if (dt != null && dt.Rows.Count > 0)
        {
            this.CKEditor1.Text = "";
            this.CKEditor1.Text = dt.Rows[0]["Content"].ToString();//赋值
        }
        else
        {
            this.CKEditor1.Text = "";
        }
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
            ShopVipArticle shopVipArticle = new ShopVipArticle();
            shopVipArticle.City = Common.ToInt32(this.ddlCity.SelectedValue);//城市ID
            shopVipArticle.Content = this.CKEditor1.Text;
            shopVipArticle.CreateTime = DateTime.Now;
            shopVipArticle.Enable = 1;

            shopVipArticle.Content = Common.HtmlDiscodeForCKEditor(shopVipArticle.Content);

            ShopVipArticleOperate shopVipArticleOperate = new ShopVipArticleOperate(); //BLL

            int result = shopVipArticleOperate.InsertArticle(shopVipArticle);
            if (result > 0)
            {
                CommonPageOperate.AlterMsg(this, "保存成功");
            }
            else
            {
                CommonPageOperate.AlterMsg(this, "保存失败");
            }
        }
        else
        {
            CommonPageOperate.AlterMsg(this, "内容不能为空");
        }
    }

    /// <summary>
    /// 删除按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ShopVipArticleOperate shopVipArticleOperate = new ShopVipArticleOperate(); //BLL

        int cityId = Common.ToInt32(this.ddlCity.SelectedValue);//城市ID

        bool result = shopVipArticleOperate.DeleteArtile(cityId);

        if (result)
        {
            this.CKEditor1.Text = "";
            CommonPageOperate.AlterMsg(this, "删除成功");
        }
        else
        {
            CommonPageOperate.AlterMsg(this, "删除失败");
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.CKEditor1.Text = "";
    }
    protected void lnkbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        int id = Common.ToInt32(e.CommandArgument);

        switch (e.CommandName)
        {
            case "del":
                ShopVipArticleOperate shopVipArticleOperate = new ShopVipArticleOperate(); //BLL
                bool delete = shopVipArticleOperate.DeleteVideo(id);
                if (delete)
                {
                    string objectKey = shopVipArticleOperate.GetVideoPath(id);
                    CloudStorageOperate.DeleteObject(objectKey);
                    BindVideo();
                    CommonPageOperate.AlterMsg(this, "删除成功！");
                }
                else
                {
                    CommonPageOperate.AlterMsg(this, "删除失败！");
                }
                break;
            default:
                break;
        }
    }
    //上传视频
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ShopVipArticleOperate shopVipArticleOperate = new ShopVipArticleOperate(); //BLL
        string fileName = FileUploadVideo.FileName;
        try
        {
            if (string.IsNullOrEmpty(fileName))
            {
                CommonPageOperate.AlterMsg(this, "请先选择文件！");
            }
            else
            {
                string extension = System.IO.Path.GetExtension(fileName);//获取扩展名
                if (extension.ToLower() == ".mp4")
                {
                    fileName = "vipArticle" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;//重命名
                    string objectKey = WebConfig.UploadFiles + "video/" + fileName;
                    CloudStorageResult result = CloudStorageOperate.PutObject(objectKey, FileUploadVideo, fileName);
                    if (result.code)
                    {
                        int i = shopVipArticleOperate.InsertVideo(fileName, objectKey);
                        if (i > 0)
                        {
                            BindVideo();
                            CommonPageOperate.AlterMsg(this, "视频上传成功！");
                        }
                        else
                        {
                            CommonPageOperate.AlterMsg(this, "视频上传成功，数据保存失败！");
                        }
                    }
                    else
                    {
                        CommonPageOperate.AlterMsg(this, "视频上传失败！");
                    }
                }
                else
                {
                    CommonPageOperate.AlterMsg(this, "请上传mp4格式的视频！");
                }
            }
        }
        catch (Exception)
        { }
    }

    private void BindVideo()
    {
        ShopVipArticleOperate shopVipArticleOperate = new ShopVipArticleOperate(); //BLL
        DataTable dt = shopVipArticleOperate.GetVideo();
        if (dt != null && dt.Rows.Count > 0)
        {
            this.gdvVideo.DataSource = dt;
            this.gdvVideo.DataBind();
        }
        else
        {
            this.gdvVideo.DataSource = null;
            this.gdvVideo.DataBind();
        }
    }
    protected void gdvVideo_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gdvVideo.Rows.Count; i++)
        {
            gdvVideo.Rows[i].Cells[2].Text = WebConfig.OssDomain + gdvVideo.Rows[i].Cells[2].Text;
        }
    }
}