using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VA.CacheLogic.OrderClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control;

public partial class RedEnvelope_SendRedEnvelope : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindActivity();
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        ImportResult result = ExcelHelper.ImportExcel(fileUploadPhone);
        if (result != null && result.dtPhone != null && result.dtPhone.Rows.Count > 0)
        {
            StringBuilder strPhone = new StringBuilder();
            foreach (DataRow dr in result.dtPhone.Rows)
            {
                strPhone.Append(dr[0]);
                strPhone.Append(',');
            }
            strPhone = strPhone.Remove(strPhone.Length - 1, 1);
            txbMobilePhoneNumber.Text = strPhone.ToString();
        }
        else
        {
            CommonPageOperate.AlterMsg(this, result.message);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txbMobilePhoneNumber.Text.Trim()) && rblActivity.SelectedValue == "0")
        {
            CommonPageOperate.AlterMsg(this, "请上传用户手机号码，选择活动");
            return;
        }
        string phoneList = txbMobilePhoneNumber.Text.Trim().Replace("，", ",");

        string[] strPhone = phoneList.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);

        int activityId = Common.ToInt32(rblActivity.SelectedValue);

        RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
        object[] result = redEnvelopeOperate.SendRedEnvelope(strPhone, activityId);
        if (result[0] == "true")
        {
            txbMobilePhoneNumber.Text = "";
        }
        CommonPageOperate.AlterMsg(this, result[1].ToString());
    }


    protected void btnCancle_Click(object sender, EventArgs e)
    {
        txbMobilePhoneNumber.Text = "";
    }

    private void BindActivity()
    {
        ActivityOperate activityOperate = new ActivityOperate();
        List<Activity> activities = activityOperate.QueryActivity(true);//赠送红包
        rblActivity.DataSource = activities;
        rblActivity.DataTextField = "name";
        rblActivity.DataValueField = "activityId";
        rblActivity.DataBind();
        if (rblActivity.Items.Count > 0)
        {
            rblActivity.SelectedIndex = 0;
        }
    }
}