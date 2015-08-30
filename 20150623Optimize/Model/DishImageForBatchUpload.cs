using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class DishImageForBatchUpload
    {
        //"ID":"session_id","x":x,"y":y,"w":w,"h":h
        public List<DishImageJson> DishImageJsonBatch { get; set; }
    }
    //批量传图时，对图片做完编辑，保存裁剪的坐标，以便后台保存该图片
    public class DishImageJson
    {
        //session_id
        public string ID
        { get; set; }
        //坐标x
        public string X
        { get; set; }
        //坐标y
        public string Y
        { get; set; }
        //宽度
        public string W
        { get; set; }
        //高度
        public string H
        { get; set; }
        //图片容器实际宽度 320...
        public string ControlW
        { get; set; }
    }
}
