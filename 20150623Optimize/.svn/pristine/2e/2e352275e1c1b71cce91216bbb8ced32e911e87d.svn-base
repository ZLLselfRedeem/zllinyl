using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using LogDll;
using System.Web.UI.WebControls;
using Aliyun.OpenServices.OpenStorageService;
using System.IO;

namespace CloudStorage
{
    public class CloudStorageOperate
    {
        static AliyunOpenStorageService aliyun = new AliyunOpenStorageService();
        static string cloudStorageMode = ConfigurationManager.AppSettings["cloudStorageMode"].ToString();

        /// <summary>
        /// 上传指定文件流到指定Bucket中
        /// </summary>
        /// <param name="objectKey">要上传的对象的Key（即名称）
        /// 若是多层目录，请在此处指定，eg. childTest/grandChildTest/osstestimage.png</param>
        /// <param name="content">要上传的流文件
        /// <returns></returns>
        public static CloudStorageResult PutObject(string objectKey, Stream content)
        {
            CloudStorageResult result = new CloudStorageResult();
            try
            {
                if (!string.IsNullOrEmpty(cloudStorageMode))
                {
                    switch (cloudStorageMode)
                    {
                        case "aliyun":
                            AliyunOSSResult aliyunResult = aliyun.PutObject(objectKey, content);
                            result.code = aliyunResult.code;
                            result.msg = aliyunResult.msg;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "--CloudStoragePutObjectException:" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 上传指定文件到指定Bucket中
        /// </summary>
        /// <param name="objectKey">要上传的对象的Key（即名称）</param>
        /// 若是多层目录，请在此处指定，eg. childTest/grandChildTest/osstestimage.png</param>
        /// <param name="filePath">要上传的对象源路径
        /// eg.C:\Users\Gianna\Pictures\image\osstestimage.png</param>
        /// <returns></returns>
        public static CloudStorageResult PutObject(string objectKey, string filePath)
        {
            CloudStorageResult result = new CloudStorageResult();
            try
            {
                if (!string.IsNullOrEmpty(cloudStorageMode))
                {
                    switch (cloudStorageMode)
                    {
                        case "aliyun":
                            AliyunOSSResult aliyunResult = aliyun.PutObject(objectKey, filePath);
                            result.code = aliyunResult.code;
                            result.msg = aliyunResult.msg;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "--CloudStoragePutObjectException:" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 使用UploadFile服务器控件上传文件
        /// </summary>
        /// <param name="objectKey">待上传对象</param>
        /// <param name="userFileUpload">用户FileUpload控件</param>
        /// <param name="fileName">待上传对象的物理路径</param>
        /// <returns></returns>
        public static CloudStorageResult PutObject(string objectKey, FileUpload userFileUpload, string fileName)
        {
            CloudStorageResult result = new CloudStorageResult();
            try
            {
                if (!string.IsNullOrEmpty(cloudStorageMode))
                {
                    switch (cloudStorageMode)
                    {
                        case "aliyun":
                            AliyunOSSResult aliyunResult = aliyun.PutObject(objectKey, userFileUpload, fileName);
                            result.code = aliyunResult.code;
                            result.msg = aliyunResult.msg;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "--CloudStoragePutObjectException:" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="objectKey">要删除的对象的Key（即名称）
        /// 若是多层目录，请在此处指定，eg. childTest/grandChildTest/osstestimage.png</param>
        /// <returns></returns>
        public static CloudStorageResult DeleteObject(string objectKey)
        {
            CloudStorageResult result = new CloudStorageResult();
            try
            {
                if (!string.IsNullOrEmpty(cloudStorageMode))
                {
                    switch (cloudStorageMode)
                    {
                        case "aliyun":
                            AliyunOSSResult aliyunResult = aliyun.DeleteObject(objectKey);
                            result.code = aliyunResult.code;
                            result.msg = aliyunResult.msg;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "--CloudStorageDeleteObjectException:" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 获取指定文件
        /// </summary>
        /// <param name="objectKey">要获取的对象的Key（即名称）</param>
        /// <returns></returns>
        public static CloudStorageObject GetObject(string objectKey)
        {
            CloudStorageObject cloudStorageObject = new CloudStorageObject(objectKey);
            try
            {
                if (!string.IsNullOrEmpty(cloudStorageMode))
                {
                    switch (cloudStorageMode)
                    {
                        case "aliyun":
                            OssObject ossObject = aliyun.GetObject(objectKey);
                            cloudStorageObject.BucketName = ossObject.BucketName;
                            cloudStorageObject.Content = ossObject.Content;
                            cloudStorageObject.Key = ossObject.Key;
                            cloudStorageObject.Metadata = ossObject.Metadata;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "--CloudStorageGetObjectException:" + ex.Message);
            }
            return cloudStorageObject;
        }
    }
}
