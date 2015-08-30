using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Xml;
using System.Windows.Forms;
using LogDll;
using CloudStorage;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// Server上已有图片应用OSS操作类
    /// 创建时间：2014-4-17
    /// </summary>
    public class OSSImageOperate
    {
        OSSImageManager ossImageManager = new OSSImageManager();

        string ImagePath = ConfigurationManager.AppSettings["ImagePath"].ToString();
        string path = ConfigurationManager.AppSettings["path"].ToString();
        string bucketName = ConfigurationManager.AppSettings["ossBucket"].ToString();

        //public string path()
        //{
        //    string path = "";
        //    //path = Application.StartupPath;//获取当前应用程序所在目录的路径，最后不包含“/”
        //    //path = Application.ExecutablePath;//获取当前应用程序文件的路径，包含文件的名称；            
        //    //path = System.Threading.Thread.GetDomain().BaseDirectory;//获取当前应用程序所在目录的路径，最后包含“/”；
        //    //path = Environment.CurrentDirectory;//获取当前应用程序的路径，最后不包含“/”；
        //    //path = System.IO.Directory.GetCurrentDirectory();//获取当前应用程序的路径，最后不包含“/”；

        //    path = AppDomain.CurrentDomain.BaseDirectory;//获取当前应用程序所在目录的路径，最后包含“/”；
        //    path = path.Replace("Upload\\bin\\Debug", "WebSites");
        //    return path;
        //}

        //HttpServerUtility server = System.Web.HttpContext.Current.Server;
        //要上传的对象源路径
        //UploadFiles/Images/gangjichacantingwantangludian20121129092557462/gangjichacantingwantangludian20121129092700988/gangjichacantingwantangludiancaipu20121129092745159/0_22_20121129095730765.jpg
        //要上传的对象的Key（即名称）
        //UploadFiles/Images/gangjichacantingwantangludian20121129092557462/gangjichacantingwantangludian20121129092700988/gangjichacantingwantangludiancaipu20121129092745159/0_22_20121129095730765.jpg

        /// <summary>
        /// 上传公司Logo
        /// </summary>
        /// <param name="bucketName">Bucket名字</param>
        /// <returns></returns>
        public object[] UploadCompanyLogo()
        {
            object[] objResult = new object[] { false, "" };
            int cnt = 0;
            CloudStorageResult result = new CloudStorageResult();
            string filePath = "", objectKey = "";
            string companyImagePath = "", companyLogo = "";
            DataTable dtCompany = ossImageManager.GetCompanyLogo();
            if (dtCompany != null && dtCompany.Rows.Count > 0)
            {
                for (int i = 0; i < dtCompany.Rows.Count; i++)
                {
                    companyImagePath = dtCompany.Rows[i]["companyImagePath"].ToString();
                    companyLogo = dtCompany.Rows[i]["companyLogo"].ToString();

                    //要上传的对象源路径
                    filePath = ImagePath + companyImagePath + companyLogo;
                    filePath = filePath.Replace("/", "\\");
                    filePath = path + filePath;
                    if (File.Exists(filePath))//文件实体存在
                    {
                        //要上传的对象的Key（即名称）
                        objectKey = ImagePath + companyImagePath + companyLogo;
                        result = CloudStorageOperate.PutObject(objectKey, filePath);
                        if (result.code)
                        {
                            cnt++;//上传成功个数计算
                            objResult[0] = true;
                        }
                    }
                }
                objResult[1] = "DB中记录有" + dtCompany.Rows.Count + "个，上传成功" + cnt + "个";
            }
            return objResult;
        }

        /// <summary>
        /// 上传店铺Logo
        /// </summary>
        /// <param name="bucketName">Bucket名字</param>
        /// <returns></returns>
        public object[] UploadShopLogo()
        {
            object[] objResult = new object[] { false, "" };
            int cnt = 0;
            CloudStorageResult result = new CloudStorageResult();
            string filePath = "", objectKey = "";
            string shopImagePath = "", shopLogo = "";
            DataTable dtShop = ossImageManager.GetShopLogo();
            if (dtShop != null && dtShop.Rows.Count > 0)
            {
                for (int i = 0; i < dtShop.Rows.Count; i++)
                {
                    shopImagePath = dtShop.Rows[i]["shopImagePath"].ToString();
                    shopLogo = dtShop.Rows[i]["shopLogo"].ToString();

                    //要上传的对象源路径
                    filePath = ImagePath + shopImagePath + shopLogo;
                    filePath = filePath.Replace("/", "\\");
                    filePath = path + filePath;
                    if (File.Exists(filePath))
                    {
                        //要上传的对象的Key（即名称）
                        objectKey = ImagePath + shopImagePath + shopLogo;                        
                        result = CloudStorageOperate.PutObject(objectKey, filePath);

                        if (result.code)
                        {
                            cnt++;//上传成功个数计算
                            objResult[0] = true;
                        }
                    }
                }
                objResult[1] = "DB中记录有" + dtShop.Rows.Count + "个，上传成功" + cnt + "个";
            }
            return objResult;
        }

        /// <summary>
        /// 上传店铺形象展示照
        /// </summary>
        /// <param name="bucketName">Bucket名字</param>
        /// <returns></returns>
        public object[] UploadShopPublicityPhoto()
        {
            object[] objResult = new object[] { false, "" };
            int cnt = 0;
            CloudStorageResult result = new CloudStorageResult();
            string filePath = "", objectKey = "";
            string publicityPhotoPath = "";
            DataTable dtShop = ossImageManager.GetShopPublicityPhoto();
            if (dtShop != null && dtShop.Rows.Count > 0)
            {
                for (int i = 0; i < dtShop.Rows.Count; i++)
                {
                    publicityPhotoPath = dtShop.Rows[i]["publicityPhotoPath"].ToString();

                    //要上传的对象源路径
                    filePath = ImagePath + publicityPhotoPath;
                    filePath = filePath.Replace("/", "\\");
                    filePath = path + filePath;
                    if (File.Exists(filePath))
                    {
                        //要上传的对象的Key（即名称）
                        objectKey = ImagePath + publicityPhotoPath;
                        result = CloudStorageOperate.PutObject(objectKey, filePath);
                        if (result.code)
                        {
                            cnt++;//上传成功个数计算
                            objResult[0] = true;
                        }
                    }
                }
                objResult[1] = "DB中记录有" + dtShop.Rows.Count + "个，上传成功" + cnt + "个";
            }
            return objResult;
        }

        /// <summary>
        /// 上传店铺环境照片
        /// </summary>
        /// <param name="bucketName">Bucket名字</param>
        /// <returns></returns>
        public object[] UploadShopEnvironmentPhoto()
        {
            object[] objResult = new object[] { false, "" };
            int cnt = 0;
            CloudStorageResult result = new CloudStorageResult();
            string filePath = "", objectKey = "";
            string shopImagePath = "", revealImageName = "";
            DataTable dtShop = ossImageManager.GetShopEnvironmentPhoto();
            if (dtShop != null && dtShop.Rows.Count > 0)
            {
                for (int i = 0; i < dtShop.Rows.Count; i++)
                {
                    shopImagePath = dtShop.Rows[i]["shopImagePath"].ToString();
                    revealImageName = dtShop.Rows[i]["revealImageName"].ToString();

                    //要上传的对象源路径
                    filePath = ImagePath + shopImagePath + revealImageName;
                    filePath = filePath.Replace("/", "\\");
                    filePath = path + filePath;
                    if (File.Exists(filePath))
                    {
                        //要上传的对象的Key（即名称）
                        objectKey = ImagePath + shopImagePath + revealImageName;
                        result = CloudStorageOperate.PutObject(objectKey, filePath);
                        if (result.code)
                        {
                            cnt++;//上传成功个数计算
                            objResult[0] = true;
                        }
                    }
                }
                objResult[1] = "DB中记录有" + dtShop.Rows.Count + "个，上传成功" + cnt + "个";
            }
            return objResult;
        }

        /// <summary>
        /// 上传平台VIP等级图片
        /// </summary>
        /// <param name="bucketName">Bucket名字</param>
        /// <returns></returns>
        public object[] UploadPlatformVipImage()
        {
            object[] objResult = new object[] { false, "" };
            int cnt = 0;
            CloudStorageResult result = new CloudStorageResult();
            string filePath = "", objectKey = "";
            string vipImg = "";

            DataTable dtVip = ossImageManager.GetPlatformVipImage();
            if (dtVip != null && dtVip.Rows.Count > 0)
            {
                for (int i = 0; i < dtVip.Rows.Count; i++)
                {
                    vipImg = dtVip.Rows[i]["vipImg"].ToString();

                    //要上传的对象源路径
                    filePath = vipImg;
                    filePath = filePath.Replace("/", "\\");
                    filePath = path + filePath;
                    if (File.Exists(filePath))
                    {
                        //要上传的对象的Key（即名称）
                        objectKey = vipImg;
                        result = CloudStorageOperate.PutObject(objectKey, filePath);
                        if (result.code)
                        {
                            cnt++;//上传成功个数计算
                            objResult[0] = true;
                        }
                    }
                }
                objResult[1] = "DB中记录有" + dtVip.Rows.Count + "个，上传成功" + cnt + "个";
            }
            return objResult;
        }

        /// <summary>
        /// 上传积分商城商品图片
        /// </summary>
        /// <param name="bucketName">Bucket名字</param>
        /// <returns></returns>
        public object[] UploadPointGoodsImage()
        {
            object[] objResult = new object[] { false, "" };
            int cnt = 0;
            CloudStorageResult result = new CloudStorageResult();
            string filePath = "", objectKey = "";
            string pictureName = "";

            DataTable dtGoods = ossImageManager.GetPointGoodsImage();
            if (dtGoods != null && dtGoods.Rows.Count > 0)
            {
                for (int i = 0; i < dtGoods.Rows.Count; i++)
                {
                    pictureName = dtGoods.Rows[i]["pictureName"].ToString();

                    //要上传的对象源路径
                    filePath = ImagePath + pictureName;
                    filePath = filePath.Replace("/", "\\");
                    filePath = path + filePath;
                    if (File.Exists(filePath))
                    {
                        //要上传的对象的Key（即名称）
                        objectKey = ImagePath + pictureName;
                        result = CloudStorageOperate.PutObject(objectKey, filePath);
                        if (result.code)
                        {
                            cnt++;//上传成功个数计算
                            objResult[0] = true;
                        }
                    }
                }
                objResult[1] = "DB中记录有" + dtGoods.Rows.Count + "个，上传成功" + cnt + "个";
            }
            return objResult;
        }

        /// <summary>
        /// 上传所有菜图至阿里云
        /// </summary>
        /// <param name="bucketName">Bucket名字</param>
        /// <param name="companyId">公司ID</param>
        public object[] UploadDishImage(string shopId)
        {
            object[] objResult = new object[] { false, "" };
            int cnt = 0;
            CloudStorageResult result = new CloudStorageResult();
            string filePath = "", objectKey = "";
            string menuImagePath = "", ImageName = "";
            try
            {
                //先获取指定店铺所有菜单的菜图
                DataTable dtDishImage = ossImageManager.GetDishImage(shopId);

                if (dtDishImage != null && dtDishImage.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDishImage.Rows.Count; i++)
                    {
                        menuImagePath = dtDishImage.Rows[i]["menuImagePath"].ToString();
                        ImageName = dtDishImage.Rows[i]["ImageName"].ToString();

                        //要上传的对象源路径
                        filePath = ImagePath + menuImagePath + ImageName;
                        filePath = filePath.Replace("/", "\\");
                        filePath = path + filePath;
                        if (File.Exists(filePath))
                        {
                            //要上传的对象的Key（即名称）
                            objectKey = ImagePath + menuImagePath + ImageName;
                            result = CloudStorageOperate.PutObject(objectKey, filePath);
                            if (result.code)
                            {
                                cnt++;//上传成功个数计算
                                objResult[0] = true;
                            }
                        }
                    }
                    objResult[1] = "DB中记录有" + dtDishImage.Rows.Count + "个，上传成功" + cnt + "个";
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(LogFile.Error, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "---" + ex.Message);
            }
            return objResult;
        }

        /// <summary>
        /// 上传正常菜谱的Zip包
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public object[] UploadMenuZip()
        {
            object[] objResult = new object[] { false, "" };
            int cnt = 0;
            CloudStorageResult result = new CloudStorageResult();
            string filePath = "", objectKey = "";
            string zip = "", MenuID = "", MenuVersion = "", menuImagePath = "";

            DataTable dtMenuZip = ossImageManager.GetMenuZip();
            if (dtMenuZip != null && dtMenuZip.Rows.Count > 0)
            {
                for (int i = 0; i < dtMenuZip.Rows.Count; i++)
                {
                    MenuID = dtMenuZip.Rows[i]["MenuID"].ToString();
                    MenuVersion = dtMenuZip.Rows[i]["MenuVersion"].ToString();
                    menuImagePath = dtMenuZip.Rows[i]["menuImagePath"].ToString();
                    zip = MenuID + "_" + MenuVersion + ".zip";

                    //要上传的对象源路径
                    filePath = ImagePath + menuImagePath + zip;
                    filePath = filePath.Replace("/", "\\");
                    filePath = path + filePath;
                    if (File.Exists(filePath))
                    {
                        //要上传的对象的Key（即名称）
                        objectKey = ImagePath + menuImagePath + zip;
                        result = CloudStorageOperate.PutObject(objectKey, filePath);
                        if (result.code)
                        {
                            cnt++;//上传成功个数计算
                            objResult[0] = true;
                        }
                    }
                }
                objResult[1] = "DB中记录有" + dtMenuZip.Rows.Count + "个，上传成功" + cnt + "个";
            }
            return objResult;
        }

        /// <summary>
        /// 上传微信菜谱的Zip包
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public object[] UploadWechatMenuZip()
        {
            object[] objResult = new object[] { false, "" };
            int cnt = 0;
            CloudStorageResult result = new CloudStorageResult();
            string filePath = "", objectKey = "";
            string zip = "", MenuID = "", MenuVersion = "", menuImagePath = "";

            DataTable dtMenuZip = ossImageManager.GetMenuZip();
            if (dtMenuZip != null && dtMenuZip.Rows.Count > 0)
            {
                for (int i = 0; i < dtMenuZip.Rows.Count; i++)
                {
                    MenuID = dtMenuZip.Rows[i]["MenuID"].ToString();
                    MenuVersion = dtMenuZip.Rows[i]["MenuVersion"].ToString();
                    menuImagePath = dtMenuZip.Rows[i]["menuImagePath"].ToString();
                    zip = MenuID + "_" + MenuVersion + "_wechat.txt";

                    //要上传的对象源路径
                    filePath = ImagePath + menuImagePath + zip;
                    filePath = filePath.Replace("/", "\\");
                    filePath = path + filePath;
                    if (File.Exists(filePath))
                    {
                        //要上传的对象的Key（即名称）
                        objectKey = ImagePath + menuImagePath + zip;
                        result = CloudStorageOperate.PutObject(objectKey, filePath);
                        if (result.code)
                        {
                            cnt++;//上传成功个数计算
                            objResult[0] = true;
                        }
                    }
                }
                objResult[1] = "DB中记录有" + dtMenuZip.Rows.Count + "个，上传成功" + cnt + "个";
            }
            return objResult;
        }

        /// <summary>
        /// 上传公司官网图片
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public object[] UploadOfficialWebsiteImage()
        {
            object[] objResult = new object[] { false, "" };
            int cnt = 0, sourceCnt = 0;
            CloudStorageResult result = new CloudStorageResult();
            string filePath = "", objectKey = "";
            string webImage = "";

            string fileName = path + "web\\App_Data\\classiccase.xml";
            string xPath = "//classiccase//image";
            XmlNodeList List = GetXmlNodeListByXpath(fileName, xPath);
            XmlNodeList children = null;
            if (List != null)
            {
                sourceCnt += List.Count;
                foreach (XmlNode node in List)
                {
                    children = node.ChildNodes;
                    if (children != null)
                    {
                        webImage = children[3].InnerXml.Replace("../", "");

                        //要上传的对象源路径
                        filePath = webImage;
                        filePath = filePath.Replace("/", "\\");
                        filePath = path + filePath;
                        if (File.Exists(filePath))
                        {
                            //要上传的对象的Key（即名称）
                            objectKey = webImage;
                            result = CloudStorageOperate.PutObject(objectKey, filePath);
                            if (result.code)
                            {
                                cnt++;//上传成功个数计算
                                objResult[0] = true;
                            }
                        }
                    }
                }
            }

            fileName = path + "web\\App_Data\\cooperate.xml";
            xPath = "//cooperate//image";
            List = GetXmlNodeListByXpath(fileName, xPath);
            if (List != null)
            {
                sourceCnt += List.Count;
                foreach (XmlNode node in List)
                {
                    children = node.ChildNodes;
                    if (children != null)
                    {
                        webImage = children[3].InnerXml.Replace("../", "");

                        //要上传的对象源路径
                        filePath = webImage;
                        filePath = filePath.Replace("/", "\\");
                        filePath = path + filePath;
                        if (File.Exists(filePath))
                        {
                            //要上传的对象的Key（即名称）
                            objectKey = webImage;
                            result = CloudStorageOperate.PutObject(objectKey, filePath);
                            if (result.code)
                            {
                                cnt++;//上传成功个数计算
                                objResult[0] = true;
                            }
                        }
                    }
                }
                objResult[1] = "DB中记录有" + sourceCnt + "个，上传成功" + cnt + "个";
            }
            return objResult;
        }

        /// <summary>
        /// 选择匹配XPath表达式的节点列表XmlNodeList.
        /// </summary>
        /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
        /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
        /// <returns>返回XmlNodeList</returns>
        public static XmlNodeList GetXmlNodeListByXpath(string xmlFileName, string xpath)
        {
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(xmlFileName); //加载XML文档
                XmlNodeList xmlNodeList = xmlDoc.SelectNodes(xpath);
                return xmlNodeList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 待上传公司Logo总数
        /// </summary>
        /// <returns></returns>
        public int CompanyLogoCount()
        {
            DataTable dt = ossImageManager.GetCompanyLogo();
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows.Count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 待上传店铺Logo总数
        /// </summary>
        /// <returns></returns>
        public int ShopLogoCount()
        {
            DataTable dt = ossImageManager.GetShopLogo();
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows.Count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 待上传店铺形象展示照总数
        /// </summary>
        /// <returns></returns>
        public int ShopPublicityCount()
        {
            DataTable dt = ossImageManager.GetShopPublicityPhoto();
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows.Count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 待上传店铺环境总数
        /// </summary>
        /// <returns></returns>
        public int ShopEnvironmentCount()
        {
            DataTable dt = ossImageManager.GetShopEnvironmentPhoto();
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows.Count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 待上传VIP图片总数
        /// </summary>
        /// <returns></returns>
        public int VipCount()
        {
            DataTable dt = ossImageManager.GetPlatformVipImage();
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows.Count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 待上传积分商城商品总数
        /// </summary>
        /// <returns></returns>
        public int PointGoodsCount()
        {
            DataTable dt = ossImageManager.GetPointGoodsImage();
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows.Count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 待上传菜图总数
        /// </summary>
        /// <returns></returns>
        //public int DishImageCount(string companyId)
        //{
        //    DataTable dt = ossImageManager.GetDishImage(companyId);
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        return dt.Rows.Count;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        /// <summary>
        /// 待上传菜图压缩包总数
        /// </summary>
        /// <returns></returns>
        public int MenuCount()
        {
            DataTable dt = ossImageManager.GetMenuZip();
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows.Count;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据菜图数量区间计算公司
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public object[] Compute(string start, string end)
        {
            object[] result = new object[] { "", "" };
            DataTable dt = ossImageManager.Compute(start, end);
            if (dt != null && dt.Rows.Count > 0)
            {
                string shopCnt = dt.Rows.Count.ToString();
                string shopId = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == dt.Rows.Count - 1)
                    {
                        shopId += dt.Rows[i]["shopID"].ToString();
                    }
                    else
                    {
                        shopId += dt.Rows[i]["shopID"].ToString() + ",";
                    }
                }
                //companyId.TrimEnd(',');
                result[0] = shopCnt;
                result[1] = shopId;
            }
            return result;
        }

        // <summary>
        /// 指定区间待上传的菜图总数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public string UploadDishCount(string start, string end)
        {
            DataTable dt = ossImageManager.UploadDishCount(start, end);
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "0";
            }
        }
    }
}
