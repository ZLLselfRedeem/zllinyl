using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;

public partial class ShopStatic_EvaluationStatic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.TextBoxEvaluationTimeFrom.Text = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd");
            this.TextBoxEvaluationTimeTo.Text = DateTime.Today.ToString("yyyy-MM-dd");
            this.AspNetPager1.CurrentPageIndex = 1;
            this.DoQuery();
        }
    }
    protected void ButtonQuery_Click(object sender, EventArgs e)
    {
        this.AspNetPager1.CurrentPageIndex = 1;
        this.DoQuery();
    }

    private void DoQuery()
    {
        PreOrder19dianVQueryObject queryObject = new PreOrder19dianVQueryObject()
        {
            ShopNameFuzzy = this.TextBoxShopName.Text,
            MobilePhoneNumberFuzzy = this.TextBoxPhoneNumber.Text,
            IsEvaluation = 1
        }; 
        if (!string.IsNullOrEmpty(this.TextBoxEvaluationTimeFrom.Text))
        {
            queryObject.EvaluationTimeFrom = DateTime.Parse(this.TextBoxEvaluationTimeFrom.Text);
        }

        if (!string.IsNullOrEmpty(this.TextBoxEvaluationTimeTo.Text))
        {
            queryObject.EvaluationTimeTo = DateTime.Parse(this.TextBoxEvaluationTimeTo.Text).AddDays(0.999999);
        }
        //if (!string.IsNullOrEmpty(Request.Form[this.TextBoxEvaluationTimeFrom.ClientID]))
        //{
        //    queryObject.EvaluationTimeFrom = DateTime.Parse(Request.Form[this.TextBoxEvaluationTimeFrom.ClientID]);
        //}
        //if (!string.IsNullOrEmpty(Request.Form[this.TextBoxEvaluationTimeTo.ClientID]))
        //{
        //    queryObject.EvaluationTimeTo = DateTime.Parse(Request.Form[this.TextBoxEvaluationTimeTo.ClientID]).AddDays(0.999999);
        //}
        if (!string.IsNullOrEmpty(this.DropDownEvaluationValue.SelectedValue))
        {
            queryObject.EvaluationLevel = int.Parse(this.DropDownEvaluationValue.SelectedValue);
        }
        var list = PreOrder19dianVOperate.GetListByQuery(this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex, queryObject);
        this.GridViewShopEvaluation.DataSource = list;
        this.GridViewShopEvaluation.DataBind();
        this.AspNetPager1.RecordCount = (int)PreOrder19dianVOperate.GetCountByQuery(queryObject);
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.DoQuery();
    }
    protected void GridViewShopEvaluation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        var entity = e.Row.DataItem as PreOrder19dianV;
        if(entity != null )
        {
            var LabelEvaluationLevel = e.Row.FindControl("LabelEvaluationLevel") as Label;
            if (LabelEvaluationLevel != null && entity.EvaluationLevel.HasValue)
            {
                switch (entity.EvaluationLevel)
                {
                    case -1:
                        LabelEvaluationLevel.Text = "差评";
                        break;
                    case 0:
                        LabelEvaluationLevel.Text = "中评";
                        break;
                    case 1:
                        LabelEvaluationLevel.Text = "好评";
                        break;
                    default:
                        break;
                } 
            }
        }
             
    }
}