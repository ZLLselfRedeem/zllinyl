using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

public partial class AuthorizationManagement_RoleAuthorityManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllAuthority();
            GetRole();
            this.TreeView1.Attributes.Add("onclick", "CheckEvent()");
        }
    }
    /// <summary>
    /// 获取所有角色信息
    /// </summary>
    protected void GetRole()
    {
        RoleOperate RoleOperate = new RoleOperate();
        DataTable dt = RoleOperate.QueryRole();//总共的DataTable
        if (dt.Rows.Count > 0)
        {
            GridView_Role.DataSource = dt;
            GridView_Role.DataBind();
        }
    }
    /// <summary>
    /// 获取所有权限
    /// </summary>
    protected void GetAllAuthority()
    {
        TreeView1.Nodes.Clear();
        AuthorityOperate authorityOperate = new AuthorityOperate();
        DataTable dt = authorityOperate.QueryAuthority();
        DataRow[] dr_father = dt.Select("AuthorityRank=0");
        for (int i = 0; i < dr_father.Length; i++)
        {
            TreeNode NodeDept = new TreeNode();
            NodeDept.Text = dr_father[i]["AuthorityName"].ToString();
            NodeDept.Value = dr_father[i]["AuthorityID"].ToString();
            //获取子的
            DataView dv_son = dt.DefaultView;
            dv_son.RowFilter = "AuthorityRank=" + dr_father[i]["AuthorityID"].ToString();
            for (int j = 0; j < dv_son.Count; j++)
            {
                TreeNode NodeEmployee = new TreeNode();
                NodeEmployee.Text = dv_son[j]["AuthorityName"].ToString();
                NodeEmployee.Value = dv_son[j]["AuthorityID"].ToString();
                NodeDept.ChildNodes.Add(NodeEmployee);
            }
            TreeView1.Nodes.Add(NodeDept);
        }
        TreeView1.CollapseAll();
    }
    /// <summary>
    /// 角色选择权限
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_Role_SelectedIndexChanged(object sender, EventArgs e)
    {
        int roleID = Common.ToInt32(GridView_Role.DataKeys[GridView_Role.SelectedIndex].Values["RoleID"]);
        GetRoleAuthority(roleID);
    }
    /// <summary>
    /// 获取角色权限
    /// </summary>
    protected void GetRoleAuthority(int roleID)
    {
        RoleAuthorityOperate roleAuthorityOperate = new RoleAuthorityOperate();
        List<VARoleAuthority> list_VARoleAuthority = roleAuthorityOperate.QueryRoleAuthority(roleID);
        if (list_VARoleAuthority != null)
        {
            Check(TreeView1.Nodes, list_VARoleAuthority);
        }
        else
        {
            GetAllAuthority();
        }
    }
    /// <summary>
    /// 遍历treeview,查找用户已经选择的权限
    /// </summary>
    /// <param name="tvtemp"></param>
    /// <param name="list_role_menu"></param>
    protected void Check(TreeNodeCollection tvtemp, List<VARoleAuthority> list_VARoleAuthority)
    {
        foreach (TreeNode temp in tvtemp)
        {
            int count = 0;
            foreach (VARoleAuthority vARoleAuthority in list_VARoleAuthority)
            {
                if (vARoleAuthority.authorityID.ToString() == temp.Value)
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
            if (temp.ChildNodes.Count != 0)//
            {
                Check(temp.ChildNodes, list_VARoleAuthority);
            }
        }
    }
    /// <summary>
    /// 遍历treeview,保存checkbox
    /// </summary>
    /// <param name="tvtemp"></param>
    protected void Save(TreeNodeCollection tvtemp)
    {
        RoleAuthorityOperate roleAuthorityOperate = new RoleAuthorityOperate();
        RoleAuthority roleAuthority = new RoleAuthority();
        foreach (TreeNode temp in tvtemp)
        {
            if (temp.Checked == true)
            {
                roleAuthority.AuthorityID = Common.ToInt32(temp.Value);
                roleAuthority.RoleAuthorityStatus = 1;
                roleAuthority.RoleID = Common.ToInt32(GridView_Role.DataKeys[GridView_Role.SelectedIndex].Values["RoleID"]);
                roleAuthorityOperate.AddRoleAuthority(roleAuthority);
            }
            if (temp.ChildNodes.Count != 0)
            {
                Save(temp.ChildNodes);
            }
        }
    }
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Confirm_Click(object sender, EventArgs e)
    {
        if (GridView_Role.SelectedIndex == -1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('请先选择一个角色！');</script>");
            return;
        }
        else
        {
            RoleAuthorityOperate roleAuthorityOperate = new RoleAuthorityOperate();
            int roleID = Common.ToInt32(GridView_Role.DataKeys[GridView_Role.SelectedIndex].Values["RoleID"]);
            roleAuthorityOperate.RemoveRoleAuthority(roleID);
            Save(TreeView1.Nodes);
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('权限设定成功！');</script>");
        }
    }
}