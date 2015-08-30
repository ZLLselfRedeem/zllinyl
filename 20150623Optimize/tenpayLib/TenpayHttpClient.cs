using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

/**
 * http、https通信类
 * ============================================================================
 * api说明：
 * setReqContent($reqContent),设置请求内容，无论post和get，都用get方式提供
 * getResContent(), 获取应答内容
 * setMethod($method),设置请求方法,post或者get
 * getErrInfo(),获取错误信息
 * setCertInfo($certFile, $certPasswd, $certType="PEM"),设置证书，双向https时需要使用
 * setCaInfo($caFile), 设置CA，格式未pem，不设置则不检查
 * setTimeOut($timeOut)， 设置超时时间，单位秒
 * getResponseCode(), 取返回的http状态码
 * call(),真正调用接口
 * 
 * ============================================================================
 *
 */

namespace Tenpay
{
    public class TenpayHttpClient
    {
        //请求内容，无论post和get，都用get方式提供
        private string _reqContent;

        //应答内容
        private string _resContent;

        //请求方法
        private string _method;

        //错误信息        
        private string _errInfo;

        //证书文件 
        private string _certFile;

        //证书密码 
        private string _certPasswd;

        //ca证书文件 
        private string _caFile;

        //超时时间,以秒为单位 
        private int _timeOut;

        //http应答编码 
        private int _responseCode;

        //字符编码
        private string _charset;

        public TenpayHttpClient()
        {
            this._caFile = "";
            this._certFile = "";
            this._certPasswd = "";

            this._reqContent = "";
            this._resContent = "";
            this._method = "POST";
            this._errInfo = "";
            this._timeOut = 1 * 60;//5分钟

            this._responseCode = 0;
            this._charset = "gb2312";

        }

        /// <summary>
        /// 设置请求内容
        /// </summary>
        /// <param name="reqContent"></param>
        public void SetReqContent(string reqContent)
        {
            this._reqContent = reqContent;
        }

        /// <summary>
        /// 获取结果内容
        /// </summary>
        /// <returns></returns>
        public string GetResContent()
        {
            return this._resContent;
        }

        /// <summary>
        /// 设置请求方法post或者get
        /// </summary>
        /// <param name="method"></param>

        public void SetMethod(string method)
        {
            this._method = method;
        }

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <returns></returns>
        public string GetErrInfo()
        {
            return this._errInfo;
        }

        /// <summary>
        /// 设置证书信息
        /// </summary>
        /// <param name="certFile"></param>
        /// <param name="certPasswd"></param>
        public void SetCertInfo(string certFile, string certPasswd)
        {
            this._certFile = certFile;
            this._certPasswd = certPasswd;
        }

        /// <summary>
        /// 设置ca
        /// </summary>
        /// <param name="caFile"></param>
        public void SetCaInfo(string caFile)
        {
            this._caFile = caFile;
        }

        /// <summary>
        /// 设置超时时间,以秒为单位
        /// </summary>
        /// <param name="timeOut"></param>
        public void SetTimeOut(int timeOut)
        {
            this._timeOut = timeOut;
        }


        /// <summary>
        /// 获取http状态码
        /// </summary>
        /// <returns></returns>
        public int GetResponseCode()
        {
            return this._responseCode;
        }

        public void SetCharset(string charset)
        {
            this._charset = charset;
        }

        /// <summary>
        /// 验证服务器证书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// 执行http调用
        /// </summary>
        /// <returns></returns>
        public bool Call()
        {
            StreamReader sr = null;
            HttpWebResponse wr = null;

            HttpWebRequest hp = null;
            try
            {
                string postData = null;
                if (this._method.ToUpper() == "POST")
                {
                    string[] sArray = System.Text.RegularExpressions.Regex.Split(this._reqContent, "\\?");

                    hp = (HttpWebRequest)WebRequest.Create(sArray[0]);

                    if (sArray.Length >= 2)
                    {
                        postData = sArray[1];
                    }

                }
                else
                {
                    hp = (HttpWebRequest)WebRequest.Create(this._reqContent);
                }


                ServicePointManager.ServerCertificateValidationCallback =
                    new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                if (this._certFile != "")
                {
                    hp.ClientCertificates.Add(new X509Certificate2(this._certFile, this._certPasswd, X509KeyStorageFlags.MachineKeySet));
                }
                hp.Timeout = this._timeOut * 1000;

                System.Text.Encoding encoding = System.Text.Encoding.GetEncoding(this._charset);
                if (postData != null)
                {
                    byte[] data = encoding.GetBytes(postData);

                    hp.Method = "POST";

                    hp.ContentType = "application/x-www-form-urlencoded";

                    hp.ContentLength = data.Length;

                    Stream ws = hp.GetRequestStream();

                    // 发送数据

                    ws.Write(data, 0, data.Length);
                    ws.Close();


                }


                wr = (HttpWebResponse)hp.GetResponse();
                // ReSharper disable once AssignNullToNotNullAttribute
                sr = new StreamReader(wr.GetResponseStream(), encoding);



                this._resContent = sr.ReadToEnd();

            }
            catch (Exception exp)
            {
                this._errInfo += exp.ToString();
                if (wr != null)
                {
                    this._responseCode = Convert.ToInt32(wr.StatusCode);
                }

                return false;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
                if (wr != null)
                    wr.Close();

            }

            _responseCode = Convert.ToInt32(wr.StatusCode);

            return true;
        }
    }
}
