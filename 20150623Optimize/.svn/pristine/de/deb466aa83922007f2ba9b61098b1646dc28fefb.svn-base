using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aliyun.OpenServices.OpenStorageService;
using System.IO;
using System.Configuration;
using LogDll;
using System.Data;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using System.Web;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 阿里云开放存储服务
    /// 创建时间：2014-4-15
    /// </summary>
    public class AliyunOpenStorageService
    {
        //string bucketName = ConfigurationManager.AppSettings["ossBucket"].ToString();

        ///// <summary>
        ///// 创建OSS入口类
        ///// </summary>
        ///// <returns></returns>
        //private OssClient CreateOssClient()
        //{
        //    string endpoint = ConfigurationManager.AppSettings["ossEndPoint"].ToString();
        //    string accessId = ConfigurationManager.AppSettings["ossAccessID"].ToString();
        //    string accessKey = ConfigurationManager.AppSettings["ossAccessKey"].ToString();

        //    OssClient ossClient = new OssClient(endpoint, accessId, accessKey);
        //    return ossClient;
        //}

        ///// <summary>
        ///// 在OSS中创建一个Bucket
        ///// </summary>
        ///// <param name="bucketName">Bucket名字</param>
        ///// 只能包括小写字母，数字，短横线（-）
        ///// 必须以小写字母或者数字开头和结尾
        ///// 长度必须在 3-63 字节之间
        ///// <returns></returns>
        //public AliyunOSSResult CreateBucket()
        //{
        //    AliyunOSSResult result = new AliyunOSSResult();
        //    OssClient ossClient = CreateOssClient();
        //    try
        //    {
        //        ossClient.CreateBucket(bucketName);
        //        result.code = true;
        //        result.msg = "创建Bucket" + bucketName + "成功";
        //    }
        //    catch (OssException ex)
        //    {
        //        result.code = false;
        //        switch (ex.ErrorCode)
        //        {
        //            case OssErrorCode.BucketAlreadyExists:
        //                result.msg = "Bucket已经存在";
        //                break;
        //            case OssErrorCode.InvalidBucketName:
        //                result.msg = "无效的Bucket名字";
        //                break;
        //            case OssErrorCode.AccessDenied:
        //                result.msg = "访问被拒绝";
        //                break;
        //            case OssErrorCode.TooManyBuckets:
        //                result.msg = "用户的Bucket数目超过限制（10个）";
        //                break;
        //            default:
        //                // RequestID和HostID可以在有问题时用于联系客服诊断异常。
        //                result.msg = string.Format("创建Bucket失败。错误代码：{0}; 错误消息：{1}。\nRequestID:{2}\tHostID:{3}",
        //                                            ex.ErrorCode,
        //                                            ex.Message,
        //                                            ex.RequestId,
        //                                            ex.HostId);
        //                break;
        //        }
        //        LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "--" + bucketName + "--" + result.msg);
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 设置Bucket权限
        ///// </summary>
        ///// <param name="bucketName">Bucket名字</param>
        ///// <param name="authority">用户访问权限</param>
        ///// <returns></returns>
        //public AliyunOSSResult SetBucketAcl(CannedAccessControlList authority)
        //{
        //    AliyunOSSResult result = new AliyunOSSResult();
        //    OssClient ossClient = CreateOssClient();
        //    try
        //    {
        //        ossClient.SetBucketAcl(bucketName, authority);
        //        result.code = true;
        //        result.msg = "设置" + bucketName + "权限成功";
        //    }
        //    catch (OssException ex)
        //    {
        //        result.code = false;
        //        switch (ex.ErrorCode)
        //        {
        //            case OssErrorCode.InvalidBucketName:
        //                result.msg = "无效的Bucket名字";
        //                break;
        //            case OssErrorCode.AccessDenied:
        //                result.msg = "访问被拒绝";
        //                break;
        //            default:
        //                // RequestID和HostID可以在有问题时用于联系客服诊断异常。
        //                result.msg = string.Format("设置Bucket权限失败。错误代码：{0}; 错误消息：{1}。\nRequestID:{2}\tHostID:{3}",
        //                                            ex.ErrorCode,
        //                                            ex.Message,
        //                                            ex.RequestId,
        //                                            ex.HostId);
        //                break;
        //        }
        //        LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "--" + bucketName + "--" + result.msg);
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 删除指定的Bucket
        ///// </summary>
        ///// <param name="bucketName">Bucket名字</param>
        ///// <returns></returns>
        //public AliyunOSSResult DeleteBucket()
        //{
        //    AliyunOSSResult result = new AliyunOSSResult();
        //    OssClient ossClient = CreateOssClient();
        //    try
        //    {
        //        ossClient.DeleteBucket(bucketName);
        //        result.code = true;
        //        result.msg = "删除Bucket" + bucketName + "成功";
        //    }
        //    catch (OssException ex)
        //    {
        //        result.code = false;
        //        switch (ex.ErrorCode)
        //        {
        //            case OssErrorCode.NoSuchBucket:
        //                result.msg = "Bucket不存在";
        //                break;
        //            case OssErrorCode.BucketNotEmtpy:
        //                result.msg = "Bucket不为空";
        //                break;
        //            case OssErrorCode.AccessDenied:
        //                result.msg = "访问被拒绝";
        //                break;
        //            default:
        //                // RequestID和HostID可以在有问题时用于联系客服诊断异常。
        //                result.msg = string.Format("删除Bucket失败。错误代码：{0}; 错误消息：{1}。\nRequestID:{2}\tHostID:{3}",
        //                                            ex.ErrorCode,
        //                                            ex.Message,
        //                                            ex.RequestId,
        //                                            ex.HostId);
        //                break;
        //        }
        //        LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "--" + bucketName + "--" + result.msg);
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 上传指定文件到指定Bucket中
        ///// </summary>
        ///// <param name="objectKey">要上传的对象的Key（即名称）
        ///// 若是多层目录，请在此处指定，eg. childTest/grandChildTest/osstestimage.png</param>
        ///// <param name="filePath">要上传的对象源路径
        ///// eg.C:\Users\Gianna\Pictures\image\osstestimage.png</param>
        ///// <returns></returns>
        //public AliyunOSSResult PutObject(string objectKey, string filePath)
        //{
        //    AliyunOSSResult result = new AliyunOSSResult();
        //    OssClient ossClient = CreateOssClient();
        //    try
        //    {
        //        using (var fs = File.Open(filePath, FileMode.Open))
        //        {
        //            ObjectMetadata metadata = new ObjectMetadata();
        //            metadata.ContentLength = fs.Length;
        //            // 可以设定自定义的metadata。
        //            //metadata.UserMetadata.Add("myfield", "test");

        //            ossClient.PutObject(bucketName, objectKey, fs, metadata);
        //            result.code = true;
        //            result.msg = "上传成功！";

        //            fs.Dispose();//释放流文件
        //            //File.Delete(filePath);//删除刚才寄存的文件
        //        }
        //    }
        //    catch (OssException ex)
        //    {
        //        result.code = false;
        //        switch (ex.ErrorCode)
        //        {
        //            case OssErrorCode.RequestTimeout:
        //                result.msg = "请求超时";
        //                break;
        //            case OssErrorCode.NoSuchBucket:
        //                result.msg = "Bucket不存在";
        //                break;
        //            case OssErrorCode.AccessDenied:
        //                result.msg = "访问被拒绝";
        //                break;
        //            case OssErrorCode.InvalidArgument:
        //                result.msg = "参数格式错误";
        //                break;
        //            case OssErrorCode.InvalidObjectName:
        //                result.msg = "无效的Object名字，长度不能大于1023";
        //                break;
        //            default:
        //                // RequestID和HostID可以在有问题时用于联系客服诊断异常。
        //                result.msg = string.Format("上传Object失败。错误代码：{0}; 错误消息：{1}。\nRequestID:{2}\tHostID:{3}",
        //                                            ex.ErrorCode,
        //                                            ex.Message,
        //                                            ex.RequestId,
        //                                            ex.HostId);
        //                break;
        //        }
        //        LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "--" + objectKey + "--" + result.msg);
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 使用UploadFile服务器控件上传文件
        ///// </summary>
        ///// <param name="objectKey">待上传对象</param>
        ///// <param name="userFileUpload">用户FileUpload控件</param>
        ///// <param name="fileName">待上传对象的物理路径</param>
        ///// <returns></returns>
        //public AliyunOSSResult PutObject(string objectKey, FileUpload userFileUpload, string fileName)
        //{
        //    AliyunOSSResult result = new AliyunOSSResult();
        //    OssClient ossClient = CreateOssClient();
        //    try
        //    {
        //        HttpServerUtility server = System.Web.HttpContext.Current.Server;
        //        string filePath = server.MapPath(WebConfig.Temp + fileName);
        //        userFileUpload.PostedFile.SaveAs(filePath);//先把文件寄存在Server上
        //        FileStream fs = new FileStream(filePath, FileMode.Open);//读流文件

        //        ObjectMetadata metadata = new ObjectMetadata();
        //        metadata.ContentLength = fs.Length;
        //        // 可以设定自定义的metadata。
        //        //metadata.UserMetadata.Add("myfield", "test");

        //        ossClient.PutObject(bucketName, objectKey, fs, metadata);
        //        result.code = true;
        //        result.msg = "上传成功！";

        //        fs.Dispose();//释放流文件
        //        File.Delete(filePath);//删除刚才寄存的文件
        //    }
        //    catch (OssException ex)
        //    {
        //        result.code = false;
        //        switch (ex.ErrorCode)
        //        {
        //            case OssErrorCode.RequestTimeout:
        //                result.msg = "请求超时";
        //                break;
        //            case OssErrorCode.NoSuchBucket:
        //                result.msg = "Bucket不存在";
        //                break;
        //            case OssErrorCode.AccessDenied:
        //                result.msg = "访问被拒绝";
        //                break;
        //            case OssErrorCode.InvalidArgument:
        //                result.msg = "参数格式错误";
        //                break;
        //            case OssErrorCode.InvalidObjectName:
        //                result.msg = "无效的Object名字，长度不能大于1023";
        //                break;
        //            default:
        //                // RequestID和HostID可以在有问题时用于联系客服诊断异常。
        //                result.msg = string.Format("上传Object失败。错误代码：{0}; 错误消息：{1}。\nRequestID:{2}\tHostID:{3}",
        //                                            ex.ErrorCode,
        //                                            ex.Message,
        //                                            ex.RequestId,
        //                                            ex.HostId);
        //                break;
        //        }
        //        LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "--" + objectKey + "--" + result.msg);
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 删除指定文件
        ///// </summary>
        ///// <param name="objectKey">要删除的对象的Key（即名称）
        ///// 若是多层目录，请在此处指定，eg. childTest/grandChildTest/osstestimage.png</param>
        ///// <returns></returns>
        //public AliyunOSSResult DeleteObject(string objectKey)
        //{
        //    AliyunOSSResult result = new AliyunOSSResult();
        //    OssClient ossClient = CreateOssClient();
        //    try
        //    {
        //        ossClient.DeleteObject(bucketName, objectKey);
        //        result.code = true;
        //        result.msg = "删除Object" + objectKey + "成功";
        //    }
        //    catch (OssException ex)
        //    {
        //        result.code = false;
        //        switch (ex.ErrorCode)
        //        {
        //            case OssErrorCode.NoSuchBucket:
        //                result.msg = "Bucket不存在";
        //                break;
        //            case OssErrorCode.AccessDenied:
        //                result.msg = "访问被拒绝";
        //                break;
        //            default:
        //                // RequestID和HostID可以在有问题时用于联系客服诊断异常。
        //                result.msg = string.Format("删除Object失败。错误代码：{0}; 错误消息：{1}。\nRequestID:{2}\tHostID:{3}",
        //                                            ex.ErrorCode,
        //                                            ex.Message,
        //                                            ex.RequestId,
        //                                            ex.HostId);
        //                break;
        //        }
        //        LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "--" + objectKey + "--" + result.msg);
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 获取指定文件
        ///// </summary>
        ///// <param name="objectKey">要获取的对象的Key（即名称）</param>
        ///// <returns></returns>
        //public OssObject GetObject(string objectKey)
        //{
        //    OssClient ossClient = CreateOssClient();
        //    OssObject ossObject = null;
        //    try
        //    {
        //        ossObject = ossClient.GetObject(bucketName, objectKey);
        //    }
        //    catch (OssException ex)
        //    {
        //        string msg = string.Format("获取Object失败。错误代码：{0}; 错误消息：{1}。\nRequestID:{2}\tHostID:{3}",
        //                                           ex.ErrorCode,
        //                                           ex.Message,
        //                                           ex.RequestId,
        //                                           ex.HostId);
        //        LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "--" + objectKey + "--" + msg);
        //    }
        //    return ossObject;
        //}

        ///// <summary>
        ///// 拷贝一个在 OSS 上已经存在的 object 成另外一个 object
        ///// </summary>
        ///// <param name="sourceBucketName"> 需要拷贝的OssObject所在的Bucket</param>
        ///// <param name="sourceKey">需要拷贝的OssObject名称</param>
        ///// <param name="destinationBucketName">要拷贝到的目的OssObject所在的Bucket</param>
        ///// <param name="destinationKey">要拷贝到的目的OssObject的名称</param>
        ///// <returns></returns>
        //public AliyunOSSResult CopyObject(string sourceBucketName, string sourceKey, string destinationBucketName, string destinationKey)
        //{
        //    AliyunOSSResult result = new AliyunOSSResult();
        //    OssClient ossClient = CreateOssClient();
        //    try
        //    {
        //        CopyObjectRequest request = new CopyObjectRequest(sourceBucketName, sourceKey, destinationBucketName, destinationKey);
        //        ossClient.CopyObject(request);
        //        result.code = true;
        //        result.msg = "复制Object成功";
        //    }
        //    catch (OssException ex)
        //    {
        //        result.code = false;
        //        switch (ex.ErrorCode)
        //        {
        //            case OssErrorCode.AccessDenied:
        //                result.msg = "访问被拒绝";
        //                break;
        //            default:
        //                // RequestID和HostID可以在有问题时用于联系客服诊断异常。
        //                result.msg = string.Format("复制Object失败。错误代码：{0}; 错误消息：{1}。\nRequestID:{2}\tHostID:{3}",
        //                                            ex.ErrorCode,
        //                                            ex.Message,
        //                                            ex.RequestId,
        //                                            ex.HostId);
        //                break;
        //        }
        //        LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "--" + result.msg + "--sourceKey:" + sourceKey + "--destinationKey:" + destinationKey);
        //    }
        //    return result;
        //}
    }
}
