using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;
using System.Configuration;
using System.IO;
using VAGastronomistMobileApp.Model;
using CloudStorage;

public partial class platformVipConfig : System.Web.UI.Page
{
    protected static string vipImgName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }
    /// <summary>
    /// 添加按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Add_Click(object sender, EventArgs e)
    {
        Panel_VipAdd.Visible = true;
        Panel_VipList.Visible = false;
        Button_Add.Visible = false;
        TextBox_Count.Text = "";
        TextBox_Name.Text = "";
        Button_Save.CommandName = "add";
        Button_Save.CommandArgument = "";
        ButtonBig.CommandName = "add";//上传按钮
    }
    /// <summary>
    /// 返回按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel_VipAdd.Visible = false;
        Panel_VipList.Visible = true;
        Button_Add.Visible = true;
    }
    /// <summary>
    /// 绑定GridView信息列表
    /// </summary>
    protected void BindGridView()
    {
        Panel_VipList.Visible = true;
        Panel_VipAdd.Visible = false;
        Button_Add.Visible = true;
        ViewallocInfoOperate vaOperate = new ViewallocInfoOperate();
        DataTable dt = vaOperate.QueryViewAllocPlatformVipInfo();
        GridView_VipList.DataSource = dt;//dt为空直接绑定null，显示内容为空
        GridView_VipList.DataBind();
    }
    /// <summary>
    /// 新增保存数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        SaveData();
    }
    /// <summary>
    /// 删除某行数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_VipList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ViewallocInfoOperate operate = new ViewallocInfoOperate();
        int id = Common.ToInt32(GridView_VipList.DataKeys[e.RowIndex].Values["id"].ToString());
        if (operate.Delete(id))
        {
            BindGridView();
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除成功！');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除失败！');</script>");
        }
    }
    /// <summary>
    /// 修改某行数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_VipList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewallocInfoOperate operate = new ViewallocInfoOperate();
        int id = Common.ToInt32(GridView_VipList.DataKeys[GridView_VipList.SelectedIndex].Values["id"].ToString());
        string name = Common.ToString(GridView_VipList.DataKeys[GridView_VipList.SelectedIndex].Values["name"].ToString());
        double consumptionLevel = Common.ToDouble(GridView_VipList.DataKeys[GridView_VipList.SelectedIndex].Values["consumptionLevel"].ToString());
        if (Common.ToString(GridView_VipList.DataKeys[GridView_VipList.SelectedIndex].Values["consumptionLevel"]) != "")
        {
            img.ImageUrl = WebConfig.CdnDomain + WebConfig.UploadFiles + WebConfig.VipImg + Common.ToString(GridView_VipList.DataKeys[GridView_VipList.SelectedIndex].Values["vipImg"]);
            vipImgName = WebConfig.UploadFiles + WebConfig.VipImg + Common.ToString(GridView_VipList.DataKeys[GridView_VipList.SelectedIndex].Values["vipImg"]);
        }
        Panel_VipAdd.Visible = true;
        Panel_VipList.Visible = false;
        Button_Add.Visible = false;
        TextBox_Name.Text = name;
        TextBox_Count.Text = Common.ToString(consumptionLevel);
        Button_Save.CommandName = "update";
        Button_Save.CommandArgument = Common.ToString(id);
        ButtonBig.CommandName = "update";//上传按钮
    }

    protected void UploadImg(FileUpload fileUpload, Image image)
    {
        string fileName = Server.HtmlEncode(fileUpload.FileName);//上传的文件
        string extension = System.IO.Path.GetExtension(fileName);//获取扩展名
        if (!string.IsNullOrEmpty(fileName))
        {
            if (extension == ".png")
            {
                System.Drawing.Bitmap originalBMP = new System.Drawing.Bitmap(fileUpload.FileContent);
                if (fileUpload.PostedFile.ContentLength / 1024 <= 50)//上传图片尺寸限制为50k
                {
                    if (vipImgName.Contains(".png"))
                    {
                        CloudStorageOperate.DeleteObject(vipImgName);//删除原有的图片
                    }
                    vipImgName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                    string objectKey = WebConfig.UploadFiles + WebConfig.VipImg + vipImgName;//图片在数据库中的实际位置
                    CloudStorageResult result = CloudStorageOperate.PutObject(objectKey, fileUpload, vipImgName);
                    if (result.code)
                    {
                        image.ImageUrl = WebConfig.CdnDomain + WebConfig.UploadFiles + WebConfig.VipImg + vipImgName;//显示图片信息
                        File.Delete(Server.MapPath(WebConfig.Temp + vipImgName));
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('大小不超过50KB')</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片格式必须是png')</script>");
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请先选择图片')</script>");
        }
    }
    /// <summary>
    /// 上传事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonBig_Click(object sender, EventArgs e)
    {
        UploadImg(Big_File, img);
        if (Button_Save.CommandName == "update")
        {
            SaveData();
        }

    }
    /// <summary>
    /// 保存数据
    /// </summary>
    public void SaveData()
    {
        ViewallocInfoOperate operate = new ViewallocInfoOperate();
        string name = Common.ToString(TextBox_Name.Text.Trim());
        double consumptionLevel = Common.ToDouble(TextBox_Count.Text.Trim());
        if (name == "" || consumptionLevel == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('数据有误，无法保存');</script>");
        }
        else
        {
            if (Button_Save.CommandName == "add")
            {
                if (operate.Exists(name))
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('名称已存在');</script>");
                }
                else
                {
                    if (operate.Add(name, consumptionLevel, vipImgName) > 0)
                    {
                        //表示添加成功
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加成功');</script>");
                        BindGridView();
                    }
                    else
                    {
                        //添加失败
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加失败');</script>");
                    }
                }
            }
            else
            {
                if (operate.Update(Common.ToInt32(Button_Save.CommandArgument), name, consumptionLevel, vipImgName))
                {
                    //表示修改成功
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改成功');</script>");
                    BindGridView();
                }
                else
                {
                    //修改失败
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败');</script>");
                }
            }
        }
    }
}