using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll.Messages;

namespace Web.Control.DDL
{
    public class MessageFirstTitleDropDownList
    {
        /// <summary>
        /// 获取消息标签
        /// </summary>
        /// <param name="ddl_MessageFirstTitle">标签下拉控件</param>
        /// <param name="CityID">城市ID</param>
        /// <param name="showMaster">是否显示master类型（所有类型）</param>
        public void BindMessageFirstTitle(DropDownList ddl_MessageFirstTitle,int CityID,bool showMaster)
        {
            MessageFirstTitleOperate mfto = new MessageFirstTitleOperate();
            var MessageFirstTitleViewModel = new MessageFirstTitleViewModel();
            List<MessageFirstTitleViewModel> data = mfto.GetHandleByCity(CityID);
            if (!showMaster)
            {
                foreach (MessageFirstTitleViewModel model in data)
                {
                    if (model.IsMaster == true)
                    {
                        data.Remove(model);
                        break;
                    }
                }
            }
            ddl_MessageFirstTitle.DataSource = data;
            ddl_MessageFirstTitle.DataTextField = "TitleName";
            ddl_MessageFirstTitle.DataValueField = "Id";
            ddl_MessageFirstTitle.DataBind();
        }
    }
}
