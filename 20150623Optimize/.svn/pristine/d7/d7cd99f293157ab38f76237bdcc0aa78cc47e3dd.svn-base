using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class SystemConfig_foodDiariesConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGr();
        }
    }
    void BindGr()
    {
        Panel_Add.Visible = false;
        Panel_List.Visible = true;
        FoodDiariesShareConfigOperate operate = new FoodDiariesShareConfigOperate();
        List<FoodDiariesShareConfig> list = operate.GetAllFoodDiariesShareConfig();
        Common commom = new Common();
        foreach (var item in list)
        {
            item.foodDiariesShareInfo = commom.HtmlDiscode(item.foodDiariesShareInfo);
            switch (item.type)
            {
                case 1:
                    item.typaName = "美食日记分享页面顶部描述";
                    break;
                case 2:
                    item.typaName = "美食日记分享页面底部描述(app)";
                    break;
                case 3:
                    item.typaName = "美食日记分享页面底部描述(pc)";
                    break;
                default:
                    break;
            }
        }
        GridView_List.DataSource = list;
        GridView_List.DataBind();
    }
    protected void btn_nva_Click(object sender, EventArgs e)
    {
        Panel_Add.Visible = true;
        Panel_List.Visible = false;
        txt_CKEditor.Text = "";
        ddl_type.SelectedValue = "1";
        Button_Save.CommandName = "add";
    }
    protected void Button_Back_Click(object sender, EventArgs e)
    {
        Panel_List.Visible = true;
        Panel_Add.Visible = false;
    }
    protected void Button_Save_Click(object sender, EventArgs e)
    {
        FoodDiariesShareConfigOperate operate = new FoodDiariesShareConfigOperate();
        if (ddl_type.SelectedValue == "1" && txt_CKEditor.Text.Length > 70)
        {
            Alert("字数太多啦，请修改！");
            return;
        }
        if (Button_Save.CommandName == "add")
        {
            FoodDiariesShareConfig model = new FoodDiariesShareConfig()
            {
                foodDiariesShareInfo = Common.HtmlDiscodeForCKEditor(txt_CKEditor.Text),
                id = 0,
                status = 1,
                type = Common.ToByte(ddl_type.SelectedValue)
            };
            if (operate.InsertFoodDiariesShareConfig(model))
            {
                BindGr();
            }
            else
            {
                Alert("保存失败");
            }
        }
        else
        {
            FoodDiariesShareConfig model = new FoodDiariesShareConfig()
            {
                foodDiariesShareInfo = Common.HtmlDiscodeForCKEditor(txt_CKEditor.Text),
                id = (int)ViewState["selectId"],
                status = 1,
                type = Common.ToByte(ddl_type.SelectedValue)
            };
            if (operate.UpdateFoodDiariesShareConfig(model))
            {
                BindGr();
            }
            else
            {
                Alert("保存失败");
            }
        }
    }

    private void Alert(string message)
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + message + "');</script>");
    }
    /// <summary>
    /// 修改事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_List_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = Common.ToInt32(GridView_List.DataKeys[GridView_List.SelectedIndex].Values["id"]);
        ViewState["selectId"] = id;
        Panel_Add.Visible = true;
        Panel_List.Visible = false;
        Button_Save.CommandName = "update";
        ddl_type.SelectedValue = GridView_List.DataKeys[GridView_List.SelectedIndex].Values["type"].ToString();
        txt_CKEditor.Text = GridView_List.DataKeys[GridView_List.SelectedIndex].Values["foodDiariesShareInfo"].ToString();
    }
    protected void GridView_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Common.ToInt32(GridView_List.DataKeys[e.RowIndex].Values["id"]);
        FoodDiariesShareConfigOperate operate = new FoodDiariesShareConfigOperate();
        if (operate.DeleteFoodDiariesShareConfig(id))
        {
            BindGr();
        }
        else
        {
            Alert("删除失败");
        }
    }
}