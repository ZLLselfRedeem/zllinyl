<%@ WebHandler Language="C#" Class="CN2PY" %>

using System;
using System.Web;

public class CN2PY : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string HzStr = context.Request["Hz"]; //汉字
        string typeID = context.Request["typeID"];  //1 汉字转简拼， 2 汉字转全拼,  其它 ,简拼+全拼
        string pyResult = string.Empty;
        if (!string.IsNullOrEmpty(HzStr))
        {
            switch (typeID)
            {
                case "1":
                    pyResult = HZ2PY.GetFirstPY(HzStr);
                    break;
                case "2":
                    pyResult = HZ2PY.ConvertPY(HzStr);
                    break;
                default:
                    pyResult = HZ2PY.GetFirstPY(HzStr) + "," + HZ2PY.ConvertPY(HzStr);
                    break;
            }
        }
        context.Response.Write(pyResult);
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}