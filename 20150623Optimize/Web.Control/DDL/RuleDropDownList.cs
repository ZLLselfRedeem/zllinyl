using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.HomeNew;
using VAGastronomistMobileApp.WebPageDll;

namespace Web.Control.DDL
{
    public class RuleDropDownList
    {

        public void BindRule(DropDownList ddlRule)
        {
            IntegrationRuleOperate iro = new IntegrationRuleOperate();
            DataTable dt = iro.RuleTypes();
            ddlRule.DataSource = dt;
            ddlRule.DataTextField = "Name";
            ddlRule.DataValueField = "ID";
            ddlRule.DataBind();
        }

        public void BindIntegrationRule(DropDownList ddlIntegrationRule, Guid RuleType)
        {
            IntegrationRuleOperate iro = new IntegrationRuleOperate();
            DataTable dt = iro.IntegrationRules(RuleType);
            ddlIntegrationRule.DataSource = dt;
            ddlIntegrationRule.DataTextField = "Description";
            ddlIntegrationRule.DataValueField = "ID";
            ddlIntegrationRule.DataBind();
            ddlIntegrationRule.Items.Insert(0, new ListItem("所有", new Guid().ToString()));
        }
    }
}
