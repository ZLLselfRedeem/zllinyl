using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 商户打印机设置信息
    /// add by wangcheng 20140210
    /// </summary>
    public class MerchantsPrinterConfigInfo
    {
        public int id { get; set; }//唯一标识
        public string printerName { get; set; }//打印机名称
        public string printIp { get; set; }//打印机IP地址
        public string printInterface { get; set; }//打印机模式
        public int printCopies { get; set; }//打印份数
        public int serialPrintSecondTab { get; set; }//数量位置
        public int serialPrintThirdTab { get; set; }//单价位置
        public int serialPrintFourthTab { get; set; }//小计位置
        public int isPrintPrice { get; set; }//是否打印单价
        public int isPrintTotal { get; set; }//是否打印小计
        public int serialPrintLeftBlank { get; set; }//打印左边距品名位置
        public int serialPrintPaperWidth { get; set; }//纸张宽度
        public int thirdFontSize { get; set; }//大标题
        public int secondFontSize { get; set; }//桌号
        public int serialPrintLineFeed { get; set; }//行距
        public int fontSizeHeight { get; set; }//打印tab中间字体大小（菜名，数量字体大小）
        public int status { get; set; }//状态
        public int employeeId { get; set; }//用户编号
        public int shopId { get; set; }//门店编号
        public bool isOpen { get; set; }//是否开启（默认为关闭）

        public OperStatus editStatus { get; set; }//当前数据操作(页面传递)
    }
    /// <summary>
    /// 打印前展示给用户列表信息
    /// </summary>
    public class AvailablePrinterInfo
    {
        public int id { get; set; }
        public string printerName { get; set; }
        public bool isOpen { get; set; }
    }
}
