using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.ComponentModel;
using System.Reflection;
using System.Transactions;
using Web.Control.Enum;

public partial class AuthorizationManagement_RoleManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetRole();
            this.TreeView_province_city.Attributes.Add("onclick", "CheckEvent()");
        }
    }
    /// <summary>
    /// 获取角色信息
    /// </summary>
    protected void GetRole()
    {
        RoleOperate RoleOperate = new RoleOperate();
        DataTable dt = RoleOperate.QueryRole();//总共的DataTable
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
    /// <summary>
    /// 删除某行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        using (TransactionScope scope = new TransactionScope())
        {
            RoleOperate RoleOperate = new RoleOperate();
            int RoleID = Common.ToInt32(GridView1.DataKeys[e.RowIndex].Values["RoleID"].ToString());
            bool i = RoleOperate.RemoveRole(RoleID);
            RoleAuthorityOperate roleAuthorityOperate = new RoleAuthorityOperate();
            roleAuthorityOperate.RemoveRoleAuthority(RoleID);//删除该角色对应的权限
            EmployeeRoleOperate oper = new EmployeeRoleOperate();
            oper.RemoveEmployeeRoleByRoleID(RoleID);//删除该角色对应所有员工的关联关系
            RoleOperate.UpdateSpecialAuthority(RoleID);//删除该角色管理的特殊权限关联关系
            if (i == true)
            {
                GetRole();
                scope.Complete();
                Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除成功！');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除失败！');</script>");
            }
        }
    }
    /// <summary>
    /// 点击修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox_RoleDescription.Text = GridView1.DataKeys[GridView1.SelectedIndex].Values["RoleDescription"].ToString();
        TextBox_RoleName.Text = GridView1.DataKeys[GridView1.SelectedIndex].Values["RoleName"].ToString();
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_Role');</script>");
        QuerySpecialAuthority(Common.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Values["RoleID"].ToString()));//查询显示枚举的权限信息
        GetAllAuthority();
    }
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_confirm_Click(object sender, EventArgs e)
    {
        RoleInfo RoleInfo = new RoleInfo();
        RoleInfo.RoleDescription = TextBox_RoleDescription.Text;
        RoleInfo.RoleName = TextBox_RoleName.Text;
        RoleInfo.RoleStatus = 1;
        RoleInfo.RoleID = Common.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Values["RoleID"].ToString());
        //(2013-8-2 wangcheng)
        using (TransactionScope scope = new TransactionScope())
        {
            RoleOperate RoleOperate = new RoleOperate();
            bool i = RoleOperate.ModifyRole(RoleInfo);
            if (i == true)
            {
                SpecialAuthorityInfo specialAuthorityInfo = new SpecialAuthorityInfo();
                specialAuthorityInfo.RoleId = RoleInfo.RoleID;
                for (int j = 0; j < GridView_SpecialAuthority.Rows.Count; j++)
                {
                    CheckBox chkVote = (CheckBox)GridView_SpecialAuthority.Rows[j].FindControl("CheckBox_SpecialAuthority");
                    DataTable dt = RoleOperate.QuerySpecialAuthorityInfo(specialAuthorityInfo.RoleId);
                    DataView dv = dt.DefaultView;
                    int specialAuthorityInfo_specialAuthorityId = Common.ToInt32(GridView_SpecialAuthority.DataKeys[j].Values["specialAuthorityId"]);
                    dv.RowFilter = "specialAuthorityId=" + specialAuthorityInfo_specialAuthorityId;
                    if (chkVote.Checked)
                    {
                        if (specialAuthorityInfo_specialAuthorityId == 17)
                        {
                            specialAuthorityInfo.status = true;
                            specialAuthorityInfo.specialAuthorityId = specialAuthorityInfo_specialAuthorityId;
                            int resultId = 0;
                            if (dv.Count > 0)//表示以前有数据，本次只是修改
                            {
                                if (RoleOperate.UpdateSpecialAuthority(specialAuthorityInfo.RoleId, specialAuthorityInfo_specialAuthorityId, true) > 0)
                                {
                                    //需要主键
                                    resultId = Common.ToInt32(Common.GetFieldValue("SpecialAuthority", "id", "RoleId=" + specialAuthorityInfo.RoleId + " and  specialAuthorityId=" + specialAuthorityInfo_specialAuthorityId));
                                }
                            }
                            else//表示是第一次添加，插入数据表
                            {
                                resultId = RoleOperate.InsertSpecialAuthorityInfo(specialAuthorityInfo);
                            }
                            Save(TreeView_province_city.Nodes, resultId);//保存关联省市表
                        }
                        else
                        {
                            if (dv.Count > 0)//表示以前有数据，本次只是修改
                            {
                                RoleOperate.UpdateSpecialAuthority(specialAuthorityInfo.RoleId, specialAuthorityInfo_specialAuthorityId, true);
                            }
                            else//表示是第一次添加，插入数据表
                            {
                                specialAuthorityInfo.status = true;
                                specialAuthorityInfo.specialAuthorityId = specialAuthorityInfo_specialAuthorityId;
                                RoleOperate.InsertSpecialAuthorityInfo(specialAuthorityInfo);
                            }
                        }
                    }
                    else
                    {
                        if (dv.Count > 0)//表示以前有数据，本次只是修改
                        {
                            RoleOperate.UpdateSpecialAuthority(specialAuthorityInfo.RoleId, specialAuthorityInfo_specialAuthorityId, false);
                        }
                        else
                        {
                            //表示对它没有任何操作
                        }
                    }
                }

                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改成功');</script>");
                GetRole();
                scope.Complete();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败');</script>");
            }
        }
    }
    /// <summary>
    /// 插入特殊权限省份关系表
    /// 递归操作
    /// </summary>
    /// <param name="id">特殊权限表主键</param>
    ///  <param name="tvtemp"></param>
    protected void Save(TreeNodeCollection tvtemp, int id)
    {
        RoleAuthorityOperate roleAuthorityOperate = new RoleAuthorityOperate();
        RoleOperate roleOperate = new RoleOperate();
        int status = 1;
        int provinceId = 0;
        int cityId = 0;
        int connSpecialAuthorityId = id;
        foreach (TreeNode temp in tvtemp)
        {
            //从子节点开始计算存入数据库
            if (temp.ChildNodes.Count == 0)
            {
                provinceId = Common.ToInt32(temp.Parent.Value);
                cityId = Common.ToInt32(temp.Value);
                if (temp.Checked == true)
                {
                    roleOperate.InsertSpecialAuthorityConnCity(provinceId, cityId, connSpecialAuthorityId, status);//add 到数据表中
                }
                else
                {
                    roleOperate.DeleteSpecialAuthorityConnCity(provinceId, cityId, connSpecialAuthorityId, status);//更新数据表
                }
            }
            if (temp.ChildNodes.Count != 0)
            {
                Save(temp.ChildNodes, id);//返回递归
            }
        }
    }
    /// <summary>
    /// (2013-8-2 wangcheng)获取所有特殊权限信息
    /// </summary>
    protected void QuerySpecialAuthority(int roleId)
    {
        GridView_SpecialAuthority.DataSource = EnumHelper.EnumToDataTable(typeof(VASpecialAuthority), "specialAuthorityName", "specialAuthorityId"); ;
        GridView_SpecialAuthority.DataBind();
        RoleOperate RoleOperate = new RoleOperate();
        DataTable dt = RoleOperate.QuerySpecialAuthorityInfo(roleId);
        if (dt.Rows.Count > 0)//有数据才可以操作
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "status=" + true;
            for (int i = 0; i < dv.Count; i++)
            {
                for (int j = 0; j < GridView_SpecialAuthority.Rows.Count; j++)
                {
                    if (Common.ToInt32(dv[i]["specialAuthorityId"]) == Common.ToInt32(GridView_SpecialAuthority.DataKeys[j].Values["specialAuthorityId"]))
                    {
                        CheckBox CheckBox_SpecialAuthority = GridView_SpecialAuthority.Rows[j].FindControl("CheckBox_SpecialAuthority") as CheckBox;
                        CheckBox_SpecialAuthority.Checked = true;
                        if (Common.ToInt32(GridView_SpecialAuthority.DataKeys[j].Values["specialAuthorityId"]) == 17)
                        {
                            Panel_province_city.Visible = true;
                        }
                    }
                }
            }
        }
    }

    #region 配置根据地区获取公司列表
    //查询当前用户具备特殊权限
    protected void Check_SpecialAuthority(object sender, EventArgs e)
    {
        int index = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        int specialAuthorityId = Common.ToInt32(GridView_SpecialAuthority.DataKeys[index].Values["specialAuthorityId"].ToString());
        //当点击操作选择给公司列表赋予特殊权限时，显示城市列表信息
        //if (specialAuthorityId == (int)VASpecialAuthority.CHECK_COMPANY_BY_PROVINCE_CITY)
        //{
        //    if (((CheckBox)sender).Checked == true)
        //    {
        //        Panel_province_city.Visible = true;
        //    }
        //    else
        //    {
        //        Panel_province_city.Visible = false;
        //    }
        //}
        //else
        //{
        Panel_province_city.Visible = false;
        // }
        //这里需要再次调用弹出窗体，以为此处操作checkbox页面执行了autopostback操作
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_Role');</script>");
        GetAllAuthority(true);
    }
    //绑定省份和城市列表信息
    protected void GetAllAuthority(bool flag = false)
    {
        TreeView_province_city.Nodes.Clear();
        RoleOperate RoleOperate = new RoleOperate();
        DataTable dt = RoleOperate.QuerySpecialAuthorityInfo(Common.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Values["RoleID"].ToString()));
        bool boolFLag = false;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Common.ToInt32(dt.Rows[i]["specialAuthorityId"]) == 17)
            {
                Panel_province_city.Visible = true;
                boolFLag = true;
                break;
            }
        }
        if (boolFLag || flag)
        {
            ShowTreeView();
        }
    }
    //查询和显示省市树结构信息
    protected void ShowTreeView()
    {
        ////展示树结构
        //DataTable dtProvince = Common.GetDataTableFieldValue("Province inner join City on City.provinceID=Province.provinceID inner join ShopInfo on ShopInfo.cityID=City.cityID", "distinct Province.provinceID,Province.provinceName ", "ShopInfo.isHandle=1");//查询获得省份信息
        //DataTable dtCity = Common.GetDataTableFieldValue("City inner join ShopInfo on ShopInfo.cityID=City.cityID", "distinct City.cityName,City.cityID,City.provinceID", "ShopInfo.isHandle=1");//查询获得城市信息
        //for (int i = 0; i < dtProvince.Rows.Count; i++)
        //{
        //    TreeNode nodeProvince = new TreeNode();
        //    nodeProvince.Text = dtProvince.Rows[i]["provinceName"].ToString();
        //    nodeProvince.Value = dtProvince.Rows[i]["provinceID"].ToString();
        //    DataView dv_City = dtCity.DefaultView;
        //    dv_City.RowFilter = " provinceID=" + Common.ToInt32(dtProvince.Rows[i]["provinceID"]);
        //    for (int j = 0; j < dv_City.Count; j++)
        //    {
        //        TreeNode nodeCity = new TreeNode();
        //        nodeCity.Text = dv_City[j]["cityName"].ToString();
        //        nodeCity.Value = dv_City[j]["cityID"].ToString();
        //        nodeProvince.ChildNodes.Add(nodeCity);
        //    }
        //    TreeView_province_city.Nodes.Add(nodeProvince);
        //}
        //TreeView_province_city.CollapseAll();
        ////选中存在的省市数据
        //int rolrId = Common.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Values["RoleID"].ToString());
        //int specialAuthorityInfo_specialAuthorityId = (int)VASpecialAuthority.CHECK_COMPANY_BY_PROVINCE_CITY;
        //int resultId = Common.ToInt32(Common.GetFieldValue("SpecialAuthority", "id", "RoleId=" + rolrId + " and  specialAuthorityId=" + specialAuthorityInfo_specialAuthorityId));
        //DataTable dt = Common.GetDataTableFieldValue("SpecialAuthorityConnCity", "provinceId,cityId", "connSpecialAuthorityId=" + resultId);
        //if (dt.Rows.Count > 0)
        //{
        //    Check(TreeView_province_city.Nodes, dt);
        //}
    }
    //省市树结构选中状态
    protected void Check(TreeNodeCollection tvtemp, DataTable dt)
    {
        foreach (TreeNode temp in tvtemp)
        {
            int count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["provinceId"].ToString() == temp.Value || dt.Rows[i]["cityId"].ToString() == temp.Value)
                {
                    count++;
                }
            }
            if (count != 0)
            {
                temp.Checked = true;
            }
            else
            {
                temp.Checked = false;
            }
            if (temp.ChildNodes.Count != 0)
            {
                Check(temp.ChildNodes, dt);
            }
        }
    }
    #endregion
}