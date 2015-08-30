using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Configuration;
using System.IO;
using System.Data;
using CloudStorage;

public partial class PointsManage_supermarketGoods : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGoodsList("", 0, 5);
        }
    }

    /// <summary>
    /// 绑定所有商品列表
    /// </summary>
    private void BindGoodsList(string goodsName, int str, int end)
    {
        GoodsOperate _GoodsOperate = new GoodsOperate();
        DataTable dtGoods = new DataTable();

        dtGoods = _GoodsOperate.QueryGoodsDataTable(goodsName);
        if (dtGoods.Rows.Count > 0)
        {
            int tableCount = dtGoods.Rows.Count;//总数
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dtGoods, str, end);
            this.gdvGoodsList.DataSource = dt_page;
            this.gdvGoodsList.DataBind();
        }
        else
        {
            this.gdvGoodsList.DataSource = null;
            this.gdvGoodsList.DataBind();
        }
    }

    protected void lnkbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        int goodId = Common.ToInt32(e.CommandArgument);
        ViewState["id"] = goodId;

        switch (e.CommandName)
        {
            case "modify":
                BindGoodsInfo(goodId);
                this.divList.Attributes.Add("style", "display:none");
                this.divDetail.Attributes.Add("style", "display:''");
                break;
            case "del":
                object[] objResult = new object[] { false, "" };
                GoodsOperate _GoodsOperate = new GoodsOperate();
                Goods Goods = _GoodsOperate.QueryGoods(goodId);
                objResult = _GoodsOperate.DeleteGoods(goodId);
                if (Common.ToBool(objResult[0]))
                {
                    CloudStorageOperate.DeleteObject(WebConfig.Goods + Goods.pictureName);
                    BindGoodsList("", 0, 5);
                    Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('删除成功！')</script>");
                    Clear();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('删除失败，" + objResult[1].ToString() + "！'')</script>");
                }
                break;
            default:
                break;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string goodName = this.txbGoodsName.Text.Trim();
        BindGoodsList(goodName, 0, 5);
    }

    /// <summary>
    /// 分页操作
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindGoodsList("", AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.divList.Attributes.Add("style", "display:none");
        this.divDetail.Attributes.Add("style", "display:''");
    }
    protected void gdvGoodsList_DataBound(object sender, EventArgs e)
    {
        CheckBox ckb = new CheckBox();
        for (int i = 0; i < gdvGoodsList.Rows.Count; i++)
        {
            ckb = (CheckBox)gdvGoodsList.Rows[i].FindControl("ckbVisible");
            ckb.Checked = Common.ToBool(gdvGoodsList.DataKeys[i].Values[0]);

            Image img = (Image)gdvGoodsList.Rows[i].FindControl("imgGoods");
            img.ImageUrl = WebConfig.CdnDomain + WebConfig.Goods + gdvGoodsList.DataKeys[i].Values[1];
        }
    }

    #region 编辑模式
    private void BindGoodsInfo(int id)
    {
        GoodsOperate _GoodsOperate = new GoodsOperate();

        Goods Goods = _GoodsOperate.QueryGoods(id);
        this.lbGoodsId.Text = Goods.id.ToString();
        this.txbName.Text = Goods.name;
        this.imgGood.ImageUrl = WebConfig.CdnDomain + WebConfig.Goods + Goods.pictureName;
        ViewState["pictureName"] = Goods.pictureName;
        this.txbExchangePrice.Text = Goods.exchangePrice.ToString();
        this.txbResidueQuantity.Text = Goods.residueQuantity.ToString();
        this.txbHaveExchangeQuantity.Text = Goods.haveExchangeQuantity.ToString();
        this.txbRemark.Text = Goods.remark;
        this.ckbVisible.Checked = Goods.visible;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        object[] objResult = new object[] { false, "" };
        object[] objFile = new object[] { false, "" };
        GoodsOperate _GoodsOperate = new GoodsOperate();
        Goods Goods = new Goods();

        //如果有新选择的文件，则先上传图片
        string file = this.fileUpload.PostedFile.FileName;
        if (file != "")
        {
            objFile = UploadFile(this.fileUpload);
        }

        //有图片并且上传成功，或者没有上传图片，只更新其他数据
        if ((file != "" && Common.ToBool(objFile[0])) || (file == ""))
        {
            Goods.name = this.txbName.Text.Trim();
            if (ViewState["pictureName"] != null && ViewState["pictureName"].ToString() != "")
            {
                Goods.pictureName = ViewState["pictureName"].ToString();
            }
            else
            {
                Goods.pictureName = "";
            }
            Goods.exchangePrice = Common.ToDouble(this.txbExchangePrice.Text.Trim());
            Goods.residueQuantity = Common.ToInt32(this.txbResidueQuantity.Text.Trim());
            Goods.haveExchangeQuantity = Common.ToInt32(this.txbHaveExchangeQuantity.Text.Trim());
            Goods.remark = this.txbRemark.Text.Trim();
            Goods.visible = this.ckbVisible.Checked;
            Goods.status = 1;

            if (ViewState["id"] != null && ViewState["id"].ToString() != "")//修改
            {
                Goods.id = Common.ToInt32(ViewState["id"]);
                objResult = _GoodsOperate.UpdateGoods(Goods);
            }
            else
            {
                Goods.id = 0;
                objResult = _GoodsOperate.InsertGoods(Goods);
            }
            if (Common.ToBool(objResult[0]))
            {
                this.divDetail.Attributes.Add("style", "display:none");
                this.divList.Attributes.Add("style", "display:''");
                if (!string.IsNullOrEmpty(txbGoodsName.Text.Trim()))//修改
                {
                    BindGoodsList(txbGoodsName.Text.Trim(), AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
                }
                else
                {
                    BindGoodsList("", 0, 5);
                }
                Clear();
                Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('保存成功！');</script>");//window.location.href='supermarketGoods.aspx';
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('保存失败，" + objResult[1].ToString() + "！')</script>");
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('" + objFile[1].ToString() + "！')</script>");
        }
    }

    private object[] UploadFile(FileUpload fileUpload)
    {
        object[] objResult = new object[] { false, "" };
        string fileName = fileUpload.FileName;
        try
        {
            if (string.IsNullOrEmpty(fileName))
            {
                objResult[1] = "请先选择文件";
            }
            else
            {
                string extension = System.IO.Path.GetExtension(fileName);//获取扩展名
                fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;//重命名
                //ViewState["pictureName"] = fileName;

                System.Drawing.Bitmap originalBMP = new System.Drawing.Bitmap(fileUpload.FileContent);

                if (originalBMP.Width != 265 || originalBMP.Height != 265 || extension != ".png")//fileUpload.PostedFile.ContentLength > 100000 || 
                {
                    objResult[1] = "图片尺寸:265*265，且是png类型";
                }
                else
                {
                    string objectKey = WebConfig.Goods + fileName;
                    CloudStorageResult result = CloudStorageOperate.PutObject(objectKey, fileUpload, fileName);

                    if (result.code)
                    {
                        //检查下之前有没有图片，有的话先删除原图，没有则不作处理
                        if (ViewState["pictureName"] != null && ViewState["pictureName"].ToString() != "")
                        {
                            result = CloudStorageOperate.DeleteObject(WebConfig.Goods + ViewState["pictureName"]);
                        }
                        if (result.code)
                        {
                            ViewState["pictureName"] = fileName;
                            objResult[0] = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            objResult[1] = ex.Message;
        }
        return objResult;
    }
    #endregion
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        this.divDetail.Attributes.Add("style", "display:none");
        this.divList.Attributes.Add("style", "display:''");
        Clear();
    }

    private void Clear()
    {
        this.txbExchangePrice.Text = "";
        this.txbGoodsName.Text = "";
        this.txbHaveExchangeQuantity.Text = "";
        this.txbName.Text = "";
        this.txbRemark.Text = "";
        this.txbResidueQuantity.Text = "";
        this.lbGoodsId.Text = "";
        this.ckbVisible.Checked = false;
        this.imgGood.ImageUrl = "";
        ViewState["id"] = null;
        ViewState["pictureName"] = null;
    }
}