using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using System.Data;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Transactions;
using System.Web;


namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 二维码相关操作 逻辑层
    /// 20140115 jinyanni
    /// </summary>
    public partial class QRCodeOperate
    {
        QRCodeManage QRCodeManage = new QRCodeManage();

        /// <summary>
        /// 查询店铺可用的二维码类型
        /// </summary>
        /// <returns></returns>
        public DataTable QueryQRCodeShopType()
        {
            return QRCodeManage.QueryQRCodeShopType();
        }

        /// <summary>
        /// 查询所有二维码类型
        /// </summary>
        /// <returns></returns>
        public DataTable QueryQRCodeType()
        {
            return QRCodeManage.QueryQRCodeType();
        }

        /// <summary>
        /// 根据店铺Id查询相应的二维码图片
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public List<QRCodeConnShop> QueryQRByShopId(int shopId)
        {
            return QRCodeManage.QueryQRByShopId(shopId);
        }

        /// <summary>
        /// 保存二维码信息至数据库
        /// </summary>
        /// <param name="QRCodeConnShop"></param>
        /// <returns></returns>
        public bool InsertQRConnShop(QRCodeConnShop QRCodeConnShop)
        {
            return QRCodeManage.InsertQRCodeConnShop(QRCodeConnShop);
        }

        public bool UpdateQRConnShop(QRCodeConnShop QRCodeConnShop)
        {
            return QRCodeManage.UpdateQRConnShop(QRCodeConnShop);
        }

        public bool DeleteQRConnShop(QRCodeConnShop QRCodeConnShop)
        {
            return QRCodeManage.DeleteQRConnShop(QRCodeConnShop);
        }

        /// <summary>
        /// 先删除旧的再新增：二维码信息
        /// </summary>
        /// <param name="QRCodeConnShop"></param>
        /// <returns></returns>
        public bool SaveQRCodeConnShop(QRCodeConnShop QRCodeConnShop)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                bool delete = DeleteQRConnShop(QRCodeConnShop);
                bool insert = InsertQRConnShop(QRCodeConnShop);
                ts.Complete();

                if (delete && insert)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 生成相应类别的二维码
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="type"></param>
        /// <param name="imagePath">存储二维码图片的路劲</param>
        /// <returns>二维码图片URL</returns>
        public object[] CreateQRCode(string type, int shopId, string imagePath, string sourcePath)
        {
            object[] objResult = new object[] { false, "" };
            StringBuilder strURL = new StringBuilder();

            try
            {
                string URL = System.Configuration.ConfigurationManager.AppSettings["appdownloadURL"].ToString();
                strURL.Append(URL);

                if (!string.IsNullOrEmpty(type))
                {
                    strURL.Append("?t=" + type + "");//二维码类型
                }
                if (shopId > 0)
                {
                    strURL.Append("&s=" + shopId + "");//店铺
                }

                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeScale = 23;//像素1036*1036
                qrCodeEncoder.QRCodeVersion = 7;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

                System.Drawing.Image image;

                image = qrCodeEncoder.Encode(strURL.ToString());//生成的二维码图片

                image = CombinImage(image, sourcePath);//给中间加上悠先标志

                image.Save(imagePath);//保存图片至指定路径 ，参数：路径+图片名称

                objResult[0] = true;
            }
            catch (Exception ex)
            {
                objResult[1] = ex.Message;
            }
            return objResult;
        }

        /// <summary>  
        /// 调用此函数后使此两种图片合并，类似相册，有个  
        /// 背景图，中间贴自己的目标图片  
        /// </summary>  
        /// <param name="sourceImg">粘贴的源图片</param>  
        /// <param name="destImg">粘贴的目标图片</param>  
        public static System.Drawing.Image CombinImage(System.Drawing.Image imgBack, string destImg)
        {

            System.Drawing.Image img = System.Drawing.Image.FromFile(destImg);        //照片图片    
            if (img.Height != 50 || img.Width != 50)
            {
                img = KiResizeImage(img, 300, 300, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);   

            //g.FillRectangle(System.Drawing.Brushes.Red, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框  

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);  

            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            GC.Collect();
            return imgBack;
        }

        /// <summary>  
        /// Resize图片  
        /// </summary>  
        /// <param name="bmp">原始Bitmap</param>  
        /// <param name="newW">新的宽度</param>  
        /// <param name="newH">新的高度</param>  
        /// <param name="Mode">保留着，暂时未用</param>  
        /// <returns>处理以后的图片</returns>  
        public static System.Drawing.Image KiResizeImage(System.Drawing.Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                System.Drawing.Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);

                // 插值算法的质量  
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();

                return b;
            }
            catch
            {
                return null;
            }
        }
    }
}
