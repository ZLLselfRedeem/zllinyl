﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

public partial class Customer_CustomerList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //GetCustomer(0, 10);
        }
    }
    /// <summary>
    /// 获取客户信息
    /// </summary>
    protected void GetCustomer(int str, int end)
    {
        CustomerOperate CustomerOperate = new CustomerOperate();
        //DataTable dtCustomer = CustomerOperate.QueryCustomer();//总共的DataTable
        //DataView dvCustomer = dtCustomer.DefaultView;
        //string filter = "1=1";
        //if (TextBox_customerEmail.Text.Trim() != "")
        //{
        //    filter += " and customerEmail='" + TextBox_customerEmail.Text.Trim() + "'";
        //}
        //if (TextBox_mobilePhoneNumber.Text.Trim() != "")
        //{
        //    filter += " and mobilePhoneNumber='" + TextBox_mobilePhoneNumber.Text.Trim() + "'";
        //}

        ////根据当前时间选择获得符合条件的用户群
        ////2013-7-27 wangcheng
        //if (TextBox_TimeStr.Text.Trim() != "" && TextBox_TimeEnd.Text.Trim() != "")
        //{
        //    DateTime staTime = Convert.ToDateTime(TextBox_TimeStr.Text + " 00:00:00");
        //    DateTime endTime = Common.ToDateTime(TextBox_TimeEnd.Text + " 23:59:59");
        //    filter += " and RegisterDate >='" + staTime + "'";
        //    filter += " and RegisterDate <='" + endTime + "'";
        //}

        //dvCustomer.RowFilter = filter;
        //if (dvCustomer.Count > 0)
        //{
        //    int tableCount = dvCustomer.Count;//桌子总数
        //    Label_count.Text = tableCount.ToString();
        //    AspNetPager1.RecordCount = tableCount;
        //    DataTable dt_page = Common.GetPageDataTable(dvCustomer.ToTable(), str, end);//分页的DataTable
        //    GridViewCustomer.DataSource = dt_page;
        //}
        //else
        //{
        //    //没有数据的情况
        //    Label_count.Text = Common.ToString(0);
        //}
        //GridViewCustomer.DataBind();
    }
    /// <summary>
    /// 删除某行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewCustomer_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CustomerOperate CustomerOperate = new CustomerOperate();
        long CustomerID = Common.ToInt64(GridViewCustomer.DataKeys[e.RowIndex].Values["CustomerID"]);
        bool i = CustomerOperate.RemoveCustomer(CustomerID);
        if (i == true)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除成功！');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加失败！');</script>");
        }
        GetCustomer(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 点击修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridViewCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CustomerID = GridViewCustomer.DataKeys[GridViewCustomer.SelectedIndex].Values["CustomerID"].ToString();
        string cookie = GridViewCustomer.DataKeys[GridViewCustomer.SelectedIndex].Values["cookie"].ToString();
        Response.Redirect("CustomerManage.aspx?customerId=" + CustomerID + "&cookie=" + cookie);

    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetCustomer(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    protected void GridViewCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        long customerID = Common.ToInt64(GridViewCustomer.DataKeys[index].Values["CustomerID"]);
        if (e.CommandName == "ChangePassword")
        {
            string password = MD5Operate.getMd5Hash("123456");
            CustomerOperate customerOperate = new CustomerOperate();
            bool i = customerOperate.ModifyCustomerPassword(customerID, password);
            if (i == true)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('密码已经重置为123456！');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('密码重置失败！');</script>");
            }
        }
    }
    protected void Button_Check_Click(object sender, EventArgs e)
    {
        GetCustomer(0, 10);
    }
}