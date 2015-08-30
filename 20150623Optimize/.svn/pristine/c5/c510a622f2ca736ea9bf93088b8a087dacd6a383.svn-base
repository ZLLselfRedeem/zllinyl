using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.HomeNew;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.SQLServerDAL.HomeNew;
using VAGastronomistMobileApp.WebPageDll;

public partial class HomeNew_HomeAdvertShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            int cityID = Common.ToInt32(Request.QueryString["cityID"]);
            int firstTitleID = Common.ToInt32(Request.QueryString["firstTitleID"]);
            int secondTitleID = Common.ToInt32(Request.QueryString["secondTitleID"]);
            InitShowAdvertImage(cityID, firstTitleID, secondTitleID);
        }
    }

    private void InitShowAdvertImage(int cityID,int firstTitleID,int secondTitleID)
    {
        // 根据secondTitleID判断类型 1、推荐 2、附近
        SubTitleManager subManager = new SubTitleManager();
        DataTable dt=subManager.SelectSubTitle(secondTitleID);
        if(dt!=null && dt.Rows.Count>0)
        {
            DataRow dr = dt.Rows[0];
            int ruleType = Common.ToInt32(dr["type"]);
            AdvertManager manager = new AdvertManager();
            var listAdvertShop = manager.SelectAdvertByKey(cityID, firstTitleID, secondTitleID, "");

            string advertPicUrl = Convert.ToString(Request.QueryString["advertPicUrl"]);
            if (string.IsNullOrEmpty(advertPicUrl))
            {
                listAdvertShop = listAdvertShop.FindAll(a => a.status == 1).ToList();
            }
            else
            {
                if(!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["advertID"])))
                {
                    listAdvertShop = listAdvertShop.FindAll(a => a.status == 1 || a.id == Common.ToInt32(Request.QueryString["advertID"])).Distinct().ToList();
                }
                else
                {
                    listAdvertShop = listAdvertShop.FindAll(a => a.status == 1).ToList();
                }
            }

            
            int advertIndex = Common.ToInt32(Request.QueryString["advertIndex"]);
            if (!string.IsNullOrEmpty(advertPicUrl))
            {
                if ((!listAdvertShop.Select(a => a.index).Contains(advertIndex) && string.IsNullOrEmpty(Convert.ToString(Request.QueryString["advertID"]))) || ruleType == 2)
                {
                    // 附近编辑
                    if (ruleType == 2 && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["advertID"])))
                    {
                        // continue;
                    }
                    else
                    {
                        listAdvertShop.Add(new AdvertShop() { index = advertIndex, yuanImageUrl = advertPicUrl, createTime = DateTime.Now });
                    }
                }

                // 编辑 推荐类型的栏目，修改顺序后， 更新 新的广告位置预览
                if(ruleType==1 && !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["advertID"])))
                {
                    foreach(var advertShop in listAdvertShop)
                    {
                        if(advertShop.id==Convert.ToInt32(Request.QueryString["advertID"]))
                        {
                            advertShop.index = advertIndex;
                            break;
                        }
                    }
                }
            }
            if(ruleType==1)
            {
                // 推荐排序
                listAdvertShop = listAdvertShop.OrderBy(a => a.index).ToList();
            }
            else
            {
                // 附近按时间排序
                listAdvertShop = listAdvertShop.OrderByDescending(a => a.createTime).ToList();
            }

            List<ShopAdvertImages> listImages = new List<ShopAdvertImages>();
            if (!string.IsNullOrEmpty(advertPicUrl))
            {
                bool isFirstIn = true;
                foreach (var advert in listAdvertShop)
                {
                    ShopAdvertImages obj = new ShopAdvertImages();
                    if (advert.index ==advertIndex && ruleType==1)
                    {
                        if (isFirstIn)
                        {
                            isFirstIn = false;
                            advert.yuanImageUrl = advertPicUrl;
                            obj.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + advert.yuanImageUrl;
                            listImages.Add(obj);
                        }
                        continue;
                    }

                    if (advert.id == Common.ToInt32(Request.QueryString["advertID"]) && isFirstIn)
                    {
                        isFirstIn = false;
                        advert.yuanImageUrl = advertPicUrl;
                    }
                    obj.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + advert.yuanImageUrl;
                    listImages.Add(obj);
                }
            }
            else
            {
                foreach (var advert in listAdvertShop)
                {
                    ShopAdvertImages obj = new ShopAdvertImages();
                    obj.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + advert.yuanImageUrl;
                    listImages.Add(obj);
                }
            }
            
            GridViewImage.DataSource = listImages;
            GridViewImage.DataBind();
        }
    }
}

/// <summary>
/// 店铺广告图片
/// </summary>
public class ShopAdvertImages
{
    public string ImageUrl
    {
        get;
        set;
    }
}