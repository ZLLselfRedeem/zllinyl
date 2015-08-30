using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;

public partial class ShopStatic_ShopLevelStatic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
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
        ShopInfoQueryObject queryObject = new ShopInfoQueryObject();
        if (!string.IsNullOrEmpty(this.TextBoxShopName.Text))
        {
            queryObject.ShopNameFuzzy = this.TextBoxShopName.Text;
        }
        if (!string.IsNullOrEmpty(this.DropDownListCity.SelectedValue))
        {
            queryObject.CityID = int.Parse(this.DropDownListCity.SelectedValue);
        }
        this.AspNetPager1.RecordCount = (int)ShopOperate.GetCountByQuery(queryObject);
        var list = ShopOperate.GetListByQuery(this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex, queryObject);
        this.GridViewShop.DataSource = list;
        this.GridViewShop.DataBind();
    }
    protected void GridViewShop_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ShopInfo shopInfo = e.Row.DataItem as ShopInfo;
        if (shopInfo != null)
        {
            ShopEvaluationDetailOperate shopEvaluationDetailOperate = new ShopEvaluationDetailOperate();
            ShopEvaluationDetailQueryObject queryObject = new ShopEvaluationDetailQueryObject()
            {
                ShopId = shopInfo.shopID
            };
            var list = shopEvaluationDetailOperate.GetShopEvaluationDetailByQuery(queryObject);
            var LabelScore = e.Row.FindControl("LabelScore") as Label;
            if (LabelScore != null)
            {
                int score = list.Sum(p => p.EvaluationValue * p.EvaluationCount);
                if (score > 0)
                {
                    LabelScore.Text = score.ToString();
                }
                else
                {
                    LabelScore.Text = "0";
                }
            }
            var LabelGoodCount = e.Row.FindControl("LabelGoodCount") as Label;
            if (LabelGoodCount != null)
            {
                var entity = list.FirstOrDefault(p => p.EvaluationValue == 1);
                if (entity != null)
                {
                    LabelGoodCount.Text = entity.EvaluationCount.ToString();
                }
                else
                {
                    LabelGoodCount.Text = "0";
                }
            }
            var LabelNormalCount = e.Row.FindControl("LabelNormalCount") as Label;
            if (LabelNormalCount != null)
            {
                var entity = list.FirstOrDefault(p => p.EvaluationValue == 0);
                if (entity != null)
                {
                    LabelNormalCount.Text = entity.EvaluationCount.ToString();
                }
                else
                {
                    LabelNormalCount.Text = "0";
                }
            }
            var LabelBadCount = e.Row.FindControl("LabelBadCount") as Label;
            if (LabelBadCount != null)
            {
                var entity = list.FirstOrDefault(p => p.EvaluationValue == -1);
                if (entity != null)
                {
                    LabelBadCount.Text = entity.EvaluationCount.ToString();
                }
                else
                {
                    LabelBadCount.Text = "0";
                }
            }
            PlaceHolder PlaceHolderLevel = e.Row.FindControl("PlaceHolderLevel") as PlaceHolder;
            if (PlaceHolderLevel != null)
            {
                //levelList :标志物升级对应的门店等级
                int[] levelList = new int[] { 1, 2, 3, 4, 5, 6, 12, 18, 24, 30, 36, 72, 108, 144, 180 };
                if (shopInfo.shopLevel == 0)
                {
                    shopInfo.shopLevel = 1;
                }
                int flagLevel = levelList.Where(p => p <= shopInfo.shopLevel).Max(); 
                int flag = 0;
                int flagCount = 0;
                for (int i = 0; i < levelList.Length; i++)
                {
                    if (flagLevel == levelList[i])
                    {
                        flag = ( i + 1) / 6;
                        flagCount =  i  % 5;
                        break;
                    }
                }
                string url = string.Empty;
                if (flag == 0)
                {
                    url = @"http://image.u-xian.com/UploadFiles/Images/15/grade/level-s.png";
                }
                else if (flag == 1)
                {
                    url = @"http://image.u-xian.com/UploadFiles/Images/15/grade/level-d.png";
                }
                else if (flag == 2)
                {
                    url = @"http://image.u-xian.com/UploadFiles/Images/15/grade/level-g.png";
                }
                for (int i = 0; i <= flagCount; i++)
                {
                    Image image = new Image();
                    image.Width = 24;
                    image.Height = 24;
                    image.ImageUrl = url;
                    PlaceHolderLevel.Controls.Add(image); 
                }
            }
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.DoQuery();
    }
}