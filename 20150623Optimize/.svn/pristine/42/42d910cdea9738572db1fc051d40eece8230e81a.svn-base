using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;

public partial class WeChatPlatManage_LandladysVoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initData();
        }
    }

    private void initData()
    {
        WechatLandladyVoiceOperate lvo = new WechatLandladyVoiceOperate();
        DataTable dt = lvo.GetLandladysVoiceInfo();
        GridView_Info.DataSource = dt;
        GridView_Info.DataBind();
        if (dt.Rows.Count == 0)
            Label_message.Text = "暂无数据";
        else
            Label_message.Text = "";
    }

    //protected void GridView_Info_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    WechatLandladyVoiceOperate lvo = new WechatLandladyVoiceOperate();
    //    if(lvo.Delete())
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('删除成功!');", true);
    //        initData();
    //    }
    //    else
    //        ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('删除失败!');", true);
    //}

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string fileName = FileUpload1.FileName;
        try
        {
            double filesize = FileUpload1.PostedFile.ContentLength / 1024.00;
            //HttpPostedFile httpPostedfile = FileUpload1.PostedFile;

            string fileExtend = fileName.Substring(fileName.LastIndexOf(".")).ToString(); //可接受的语音类型  AMR与MP3格式 
            if ((fileExtend.ToUpper() == ".AMR" || fileExtend.ToUpper() == ".MP3") && filesize < 256)
            {
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/UploadFiles/LandladyVoice/" + fileName));
                //添加数据
                WechatLandladyVoiceOperate wvo = new WechatLandladyVoiceOperate();
                WechatLandladyVoiceInfo landladyVoiceInfo = new WechatLandladyVoiceInfo();
                landladyVoiceInfo.fileName = fileName;
                landladyVoiceInfo.operaterID = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
                landladyVoiceInfo.pubDateTime = DateTime.Now.ToString();
                landladyVoiceInfo.remark = txtRemark.Text;

                if (wvo.Insert(landladyVoiceInfo) > 0)
                    ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('上传成功!');", true);
                else
                    ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('更新数据库失败!');", true);

                txtRemark.Text = "";
                initData();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('仅支持AMR与MP3格式,并且文件大小<256K 的语音文件.');", true);
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "js", "alert('上传失败!');", true);
        }
    }
    protected void chkSelec_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = (sender as CheckBox).Parent.Parent as GridViewRow;
        
        for (int i = 0; i < GridView_Info.Rows.Count; i++)
        {
            CheckBox chk = GridView_Info.Rows[i].Cells[5].FindControl("chkSelec") as CheckBox;
            if (gvr.RowIndex != i)
            {
                chk.Checked = false;
            }
            else
            {
                if (chk.Checked)
                    selectID.Value = gvr.Cells[0].Text;
                else
                    selectID.Value = "";
            }
        }
    }
}