using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.Transactions;

public partial class PointsManage_waiterQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //page初始化
            TextBox_TimeStr.Text = "2013-01-01";
            TextBox_TimeEnd.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            TextBox_LogStarTime.Text = "2013-01-01";
            TextBox_LogEndTime.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            ShowWaiterInfo();
        }
    }
    /// <summary>
    /// 详情信息
    /// </summary>
    protected void ShowWaiterInfo()
    {
        int employeeId = Common.ToInt32(Request.QueryString["employeeId"]);
        if (employeeId > 0)
        {
            #region 基本资料
            EmployeeOperate employeeOper = new EmployeeOperate();
            EmployeeInfo employeeInfo = employeeOper.QueryEmployee(employeeId);
            if (employeeInfo != null)
            {
                tb_Name.Text = employeeInfo.EmployeeFirstName;//姓名
                ddl_Sex.SelectedValue = Common.ToString(employeeInfo.EmployeeSex);
                tb_Phone.Text = employeeInfo.UserName;//用户名就是电话
                tb_Birthday.Text = Common.ToString(employeeInfo.birthday);//生日
                lb_RegisterTime.Text = Common.ToString(employeeInfo.registerTime);//注册时间
                //备注（数据库暂时没有存贮，待处理）
                tb_current_Point.Text = Common.ToString(employeeInfo.settlementPoint);//可用积分
                EmployeePointLogOperate logOper = new EmployeePointLogOperate();
                lb_ExchengeCount.Text = Common.ToString(logOper.QueryEmployeeExchangeCount(employeeId));//服务员兑换记录条数
                tb_Remark.Text = employeeInfo.remark;
            }
            #endregion
            #region 服务员服务信息查询
            WaiterWorkExperienceQuery(employeeId);
            #endregion
            #region 服务员积分日志信息查询
            WaiterPointLogQuery(0, 10);
            #endregion
        }
    }
    /// <summary>
    /// 服务员信息查询
    /// </summary>
    protected void TextBox_Time_WaiterWorkExperienceQuery_Changed(object sender, EventArgs e)
    {
        int employeeId = Common.ToInt32(Request.QueryString["employeeId"]);
        WaiterWorkExperienceQuery(employeeId);
    }
    /// <summary>
    /// 服务员信息查询绑定（封装）
    /// </summary>
    /// <param name="employeeId"></param>
    void WaiterWorkExperienceQuery(int employeeId)
    {
        string timeStar = Common.ToDateTime(TextBox_TimeStr.Text).ToString("yyyy-MM-dd") + " 00:00:00";
        string timerEnd = Common.ToDateTime(TextBox_TimeEnd.Text.Trim()).ToString("yyyy-MM-dd") + " 23:59:59";
        EmployeePointOperate pointOper = new EmployeePointOperate();
        DataTable dtWaiterWorkExperience = pointOper.QueryWaiterWorkExperience(timeStar, timerEnd, employeeId);
        GridView_WaiterWorkExperience.DataSource = dtWaiterWorkExperience;
        GridView_WaiterWorkExperience.DataBind();//绑定服务员服务信息
        if (GridView_WaiterWorkExperience.Rows.Count > 0)
        {
            for (int i = 0; i < GridView_WaiterWorkExperience.Rows.Count; i++)
            {
                int status = Common.ToInt32(GridView_WaiterWorkExperience.DataKeys[i].Values["status"].ToString());//是否开通
                bool isSupportEnterSyb = false;//是否可收银
                CheckBox cb_isSupportSyb = GridView_WaiterWorkExperience.Rows[i].FindControl("cb_isSupportSyb") as CheckBox;
                CheckBox cb_isOpen = GridView_WaiterWorkExperience.Rows[i].FindControl("cb_isOpen") as CheckBox;
                if (isSupportEnterSyb == true)
                {
                    cb_isSupportSyb.Checked = true;
                }
                else
                {
                    cb_isSupportSyb.Checked = false;
                }
                if (status == 1)
                {
                    cb_isOpen.Checked = true;
                }
                else
                {
                    cb_isOpen.Checked = false;
                }
                Label Label_serviceEndTime = GridView_WaiterWorkExperience.Rows[i].FindControl("Label_serviceEndTime") as Label;
                string serviceEndTime = Common.ToString(GridView_WaiterWorkExperience.DataKeys[i].Values["serviceEndTime"].ToString());
                if (string.IsNullOrEmpty(serviceEndTime))
                {
                    Label_serviceEndTime.Text = "至今";
                }
                else
                {
                    Label_serviceEndTime.Text = serviceEndTime;
                }
            }
        }
    }
    /// <summary>
    /// 服务员积分日志查询
    /// </summary>
    void WaiterPointLogQuery(int str, int end)
    {
        int employeeId = Common.ToInt32(Request.QueryString["employeeId"]);
        string logEndTime = Common.ToDateTime(TextBox_LogEndTime.Text).ToString("yyyy-MM-dd") + " 23:59:59";
        string logStarTime = Common.ToDateTime(TextBox_LogStarTime.Text.Trim()).ToString("yyyy-MM-dd") + " 00:00:00";
        bool showAdd = cb_Add.Checked == true ? true : false;//显示增加
        bool showReduce = cb_Reduce.Checked == true ? true : false;//显示减少
        EmployeePointLogOperate logOper = new EmployeePointLogOperate();
        DataTable dtWaiterPointLog = logOper.QueryWaiterPointLog(logStarTime, logEndTime, employeeId, showAdd, showReduce);
        if (dtWaiterPointLog.Rows.Count > 0)
        {
            int tableCount = dtWaiterPointLog.Rows.Count;
            AspNetPager_Point.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dtWaiterPointLog, str, end);//分页的DataTable
            GridView_WaiterPointLog.DataSource = dt_page;
        }
        GridView_WaiterPointLog.DataBind();
    }
    /// <summary>
    /// 服务员积分日志查询分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager_Point_PageChanged(object sender, EventArgs e)
    {
        WaiterPointLogQuery(AspNetPager_Point.StartRecordIndex - 1, AspNetPager_Point.EndRecordIndex);
    }
    /// <summary>
    /// 选择时间筛选日志信息
    /// </summary>
    protected void TextBox_LogTime_TextChanged(object sender, EventArgs e)
    {
        WaiterPointLogQuery(AspNetPager_Point.StartRecordIndex - 1, AspNetPager_Point.EndRecordIndex);
    }
    /// <summary>
    /// 编辑保存用户个人信息
    /// </summary>
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        using (TransactionScope scop = new TransactionScope())
        {
            int employeeId = Common.ToInt32(Request.QueryString["employeeId"]);
            EmployeeOperate employeeOper = new EmployeeOperate();
            EmployeeInfo employeeInfo = employeeOper.QueryEmployee(employeeId);
            EmployeeInfo newEmployeeInfo = new EmployeeInfo();
            newEmployeeInfo.EmployeeFirstName = tb_Name.Text;
            newEmployeeInfo.EmployeeSex = Common.ToInt32(ddl_Sex.SelectedValue);
            newEmployeeInfo.EmployeeStatus = 1;
            newEmployeeInfo.UserName = tb_Phone.Text;
            newEmployeeInfo.EmployeeID = employeeId;
            if (Common.ToDateTime(tb_Birthday.Text) == DateTime.MinValue)//匹配最小时间，不要用字符串匹配，每个pc时间格式不一样
            {
                newEmployeeInfo.birthday = Common.ToDateTime("2014/01/01 00:00:00");
            }
            else
            {
                newEmployeeInfo.birthday = Common.ToDateTime(tb_Birthday.Text);
            }
            newEmployeeInfo.settlementPoint = Common.ToDouble(tb_current_Point.Text);
            newEmployeeInfo.remark = tb_Remark.Text.Trim();
            //原数据
            newEmployeeInfo.EmployeeAge = employeeInfo.EmployeeAge;
            newEmployeeInfo.EmployeePhone = employeeInfo.EmployeePhone;
            newEmployeeInfo.EmployeeSequence = employeeInfo.EmployeeSequence;
            newEmployeeInfo.position = employeeInfo.position;
            newEmployeeInfo.defaultPage = employeeInfo.defaultPage;
            newEmployeeInfo.isViewAllocWorker = employeeInfo.isViewAllocWorker; //是否是友络工作人员
            EmployeeOperate employeeOperate = new EmployeeOperate();
            bool flag = false;
            bool flag1 = false;
            employeeInfo.settlementPoint = employeeInfo.settlementPoint.HasValue ? employeeInfo.settlementPoint.Value : 0;
            if (Common.ToDouble(tb_current_Point.Text.Trim()) != employeeInfo.settlementPoint.Value)//说明当前服务员的积分有变动
            {
                string desStr = (Common.ToDouble(tb_current_Point.Text) - employeeInfo.settlementPoint) > 0 ? "友络奖励" : "友络扣除";
                EmployeePointLog pointLog = new EmployeePointLog()
                {
                    customerId = 0,
                    employeeId = employeeId,
                    monetary = 0,
                    pointVariation = Common.ToDouble(tb_current_Point.Text.Trim()) - employeeInfo.settlementPoint.Value,
                    pointVariationMethods = (int)PointVariationMethods.VIEWALLOC_REWARDS,
                    preOrder19dianId = 0,
                    remark = desStr,
                    shopId = 0,
                    status = 1,
                    viewallocEmployeeId = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID,//当前后台管理员ID
                    operateTime = DateTime.Now
                };
                EmployeePointLogOperate pointLogOper = new EmployeePointLogOperate();
                flag = pointLogOper.Add(pointLog);//对账：增加积分变动记录，减少服务员未结算积分，增加服务员已结算积分
                EmployeePointOperate pointOper = new EmployeePointOperate();
                flag1 = pointOper.ModifyEmployeeSettlementPoint(employeeId, Common.ToDouble(tb_current_Point.Text) - employeeInfo.settlementPoint.Value);
            }
            else
            {
                flag = true;
                flag1 = true;
            }
            bool i = employeeOperate.ModifyEmployee(newEmployeeInfo);//修改个人信息
            if (i == true && flag == true & flag1 == true)
            {
                ShowWaiterInfo();//刷新页面
                scop.Complete();
                Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('修改成功');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('修改失败');</script>");
            }
        }
    }
    protected void lk_PointExchang_Click(object sender, EventArgs e)
    {
        int employeeId = Common.ToInt32(Request.QueryString["employeeId"]);
        Response.Redirect("exchangeQuery.aspx?employeeId=" + employeeId);
    }
    /// <summary>
    /// 显示增加
    /// </summary>
    protected void cb_Add_CheckedChanged(object sender, EventArgs e)
    {
        WaiterPointLogQuery(0, 10);
    }
    /// <summary>
    /// 显示减少
    /// </summary>
    protected void cb_Reduce_CheckedChanged(object sender, EventArgs e)
    {
        WaiterPointLogQuery(0, 10);
    }
}