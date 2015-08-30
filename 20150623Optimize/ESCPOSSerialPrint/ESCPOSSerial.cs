using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Xiaobai Xu(Jason's big boss) on 2013-04-21(节日哦).
//  Modified by jlf on 2013-7-30加入网口打印机
//
namespace ESCPOSSerialPrint
{
    public static class ESCPOSSerial
    {
        static SerialPort serialPort;
        static bool serialOpened = false;
        static int port = 9100;
        static Socket c = null;
        static string stylePrint = "";

        private struct OVERLAPPED
        {
            int Internal;
            int InternalHigh;
            int Offset;
            int OffSetHigh;
            int hEvent;
        }
        [DllImport("kernel32.dll")]
        private static extern int CreateFile(
         string lpFileName,
         uint dwDesiredAccess,
         int dwShareMode,
         int lpSecurityAttributes,
         int dwCreationDisposition,
         int dwFlagsAndAttributes,
         int hTemplateFile
         );
        [DllImport("kernel32.dll")]
        private static extern bool WriteFile(
         int hFile,
         byte[] lpBuffer,
         int nNumberOfBytesToWrite,
         ref int lpNumberOfBytesWritten,
         ref OVERLAPPED lpOverlapped
         );
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(
         int hObject
         ); //C#LPT端口打印类的操作  
        private static int iHandle;



        /// <summary>
        /// 打开串口,网口
        /// </summary>
        /// <param name="printSerialPort"></param>
        /// <returns></returns>
        public static bool OpenComPort(ref SerialPort printSerialPort, string httpsite)
        {

            bool success = false;
            serialPort = printSerialPort;
            stylePrint = serialPort.PortName;

            if (stylePrint.Contains("COM"))
            {

                #region 串口

                // Set the read/write timeouts
                serialPort.ReadTimeout = 500;
                serialPort.WriteTimeout = 500;

                //_serialPort.RtsEnable = true;
                serialPort.DtrEnable = true;   // Epson TM 打印机出场默认都是DTR/DSR，非常重要！！

                try
                {
                    serialPort.Open();
                    serialOpened = true;
                    success = true;
                }

                catch (System.Exception)
                {
                    success = false;
                }

                // Epson TM打印机在国内销售的包含GB18030大字库的
                serialPort.Encoding = Encoding.GetEncoding("gb18030");

                return success;
                #endregion
            }
            else if (stylePrint == "IP4")
            {
                #region 网口
                // IP地址检查
                string ipsite = httpsite;

                IPAddress ip = IPAddress.Parse(ipsite);

                try
                {
                    //把ip和端口转化为IPEndPoint实例
                    IPEndPoint ip_endpoint = new IPEndPoint(ip, port);

                    //创建一个Socket
                    c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


                    //连接到服务器
                    c.Connect(ip_endpoint);
                    //应对同步Connect超时过长的办法，猜测应该是先用异步方式建立以个连接然后，
                    //确认连接是否可用，然后报错或者关闭后，重新建立一个同步连接                    

                    c.SendTimeout = 300;
                    //string str_send = "\x1b\x40打开TCP/IP连接!\n-------------------------------\n\n";
                    //Byte[] byte_send = Encoding.GetEncoding("gb18030").GetBytes(str_send);
                    //ipWrite(byte_send, 0, byte_send.Length);

                    success = true;

                }
                catch (Exception)
                {
                    success = false;
                }
                return success;
                #endregion
            }
            else if (stylePrint.Contains("LPT"))
            {
                #region 并口

                iHandle = CreateFile("lpt1", 0x40000000, 0, 0, 3, 0, 0);
                if (iHandle != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                #endregion
            }
            return success;
        }
        /// <summary>
        /// 关闭串口，网口
        /// </summary>
        /// <returns></returns>
        public static bool ClosePrinterPort()
        {
            bool success = false;
            if (stylePrint.Contains("COM"))
            {
                #region 串口
                if (serialOpened)
                {
                    try
                    {
                        serialPort.Close();
                        serialOpened = false;
                        success = true;
                    }
                    catch (System.Exception)
                    {
                        success = false;
                    }
                }
                #endregion
            }
            else if (stylePrint == "IP4")
            {
                #region 网口
                try
                {
                    c.Close();
                    success = true;
                }
                catch (Exception)
                {
                    success = false;
                }
                #endregion
            }
            else if (stylePrint.Contains("LPT"))
            {
                #region 并口
                try
                {
                    CloseHandle(iHandle);
                    success = true;
                }
                catch (Exception)
                {
                    success = false;
                }
                #endregion

            }
            return success;
        }
        /// <summary>
        /// 设置行间距
        /// </summary>
        /// <param name="nDistance"></param>
        /// <returns></returns>
        public static bool POS_SetLineSpacing(uint nDistance)
        {
            bool success = false;
            try
            {
                byte[] lineSpace = new byte[] { 0x1b, 0x33, 0x00 };
                lineSpace[2] = (byte)nDistance;
                espWrite(lineSpace, 0, lineSpace.Length);
                success = true;
            }
            catch (System.Exception)
            {
                success = false;
            }
            return success;
        }
        /// <summary>
        /// 切刀
        /// </summary>
        /// <returns></returns>
        public static bool POS_CutPaper()
        {
            bool success = false;
            try
            {
                // Feed and cut paper
                byte[] cutPaper = new byte[] { 0x1D, 0x56, 0x42, 0x00 };
                espWrite(cutPaper, 0, cutPaper.Length);
                success = true;
            }
            catch (System.Exception)
            {
                success = false;
            }
            return success;
        }
        /// <summary>
        /// 打印机初始化
        /// </summary>
        public static void POS_Init()
        {
            byte[] initPrinter = new byte[] { 0x1B, 0x40 };
            espWrite(initPrinter, 0, initPrinter.Length);

        }
        /// <summary>
        /// 把将要打印的字符串数据发送到打印缓冲区中，并指定X 方向（水平）上的绝对起始点位置，
        /// 指定每个字符宽度和高度方向上的放大倍数、类型和风格。
        /// </summary>
        /// <param name="pszString">待打印的文本信息，可以使用'\n'进行换行</param>
        /// <param name="nOrgx">打印的左边起始位置</param>
        /// <param name="widthTimes">指定字符的宽度方向上的放大倍数。可以为 1到 6。</param>
        /// <param name="heightTimes">指定字符高度方向上的放大倍数。可以为 1 到 6。</param>
        /// <returns></returns>
        public static bool POS_S_TextOut(string pszString, uint nOrgx, byte widthTimes, byte heightTimes)
        {
            bool success = false;
            try
            {
                //ESC $ nL nH, 缩进
                //byte[] leftBlank = new byte[] { 0x1D, 0x4C, 0x00, 0x00 };
                //leftBlank[2] = (byte)nOrgx;
                //serialPort.Write(leftBlank, 0, leftBlank.Length);
                //byte[] abPosition = new byte[] { 0x1b, 0x24, 0x00, 0x00 };
                byte[] abPosition = new byte[] { 0x1D, 0x4C, 0x00, 0x00 };
                abPosition[2] = Convert.ToByte(nOrgx % 256);
                abPosition[3] = Convert.ToByte(nOrgx / 256);
                espWrite(abPosition, 0, abPosition.Length);

                byte[] fontSize = new byte[] { 0x1D, 0x21, 0x00 };
                fontSize[2] = (byte)(((widthTimes - 1) << 4) + (heightTimes - 1));
                espWrite(fontSize, 0, fontSize.Length);

                espWrite(pszString);

                success = true;
            }
            catch (System.Exception)
            {
                success = false;
            }
            return success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pszString"></param>
        /// <param name="widthTimes"></param>
        /// <param name="heightTimes"></param>
        /// <returns></returns>
        public static bool POS_S_TabOut(string pszString, uint nOrgx, byte widthTimes, byte heightTimes)
        {
            bool success = false;
            try
            {
                //ESC $ nL nH, 缩进
                //byte[] leftBlank = new byte[] { 0x1D, 0x4C, 0x00, 0x00 };
                //leftBlank[2] = (byte)nOrgx;
                //serialPort.Write(leftBlank, 0, leftBlank.Length);
                byte[] abPosition = new byte[] { 0x1b, 0x24, 0x00, 0x00 };
                //byte[] abPosition = new byte[] { 0x1b, 0x44, 0x0A, 0x24, 0x00 };
                abPosition[2] = Convert.ToByte(nOrgx % 256);
                abPosition[3] = Convert.ToByte(nOrgx / 256);
                espWrite(abPosition, 0, abPosition.Length);

                byte[] fontSize = new byte[] { 0x1D, 0x21, 0x00 };
                fontSize[2] = (byte)(((widthTimes - 1) << 4) + (heightTimes - 1));
                espWrite(fontSize, 0, fontSize.Length);

                espWrite(pszString);

                success = true;
            }
            catch (System.Exception)
            {
                success = false;
            }
            return success;
        }
        public static void POS_PrintSize(uint pagesize)
        {

            byte[] printWidth = new byte[] { 0x1D, 0x57, 0x00, 0x00 };
            printWidth[2] = Convert.ToByte(pagesize % 256);
            printWidth[3] = Convert.ToByte(pagesize / 256);
            espWrite(printWidth, 0, printWidth.Length);
        }

        /// <summary>
        /// 打印位图 nOrgx为缩进宽度
        /// 打印位图时会自动将行间距设置为默认值，如果下面还要打印文字需要手动调整行间距
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="nOrgx"></param>
        /// <returns></returns>
        public static bool POS_S_BitmapOut(Bitmap bmp, uint nOrgx)
        {
            bool success = false;
            try
            {
                byte[] data = new byte[] { 0x1B, 0x33, 0x00 };
                espWrite(data, 0, data.Length);
                data[0] = (byte)'\x00';
                data[1] = (byte)'\x00';
                data[2] = (byte)'\x00';    // Clear to Zero.

                Color pixelColor;

                // ESC * m nL nH
                byte[] escBmp = new byte[] { 0x1B, 0x2A, 0x00, 0x00, 0x00 };
                escBmp[2] = (byte)'\x21';
                //nL, nH
                escBmp[3] = (byte)(bmp.Width % 256);
                escBmp[4] = (byte)(bmp.Width / 256);

                // data
                for (int i = 0; i < (bmp.Height / 24) + 1; i++)
                {
                    POS_S_TextOut("", nOrgx, 1, 1);

                    espWrite(escBmp, 0, escBmp.Length);

                    for (int j = 0; j < bmp.Width; j++)
                    {
                        for (int k = 0; k < 24; k++)
                        {
                            if (((i * 24) + k) < bmp.Height)   // if within the BMP size
                            {
                                pixelColor = bmp.GetPixel(j, (i * 24) + k);
                                if (pixelColor.R == 0)
                                {
                                    data[k / 8] += (byte)(128 >> (k % 8));
                                }
                            }
                        }

                        espWrite(data, 0, 3);
                        data[0] = (byte)'\x00';
                        data[1] = (byte)'\x00';
                        data[2] = (byte)'\x00';    // Clear to Zero.
                    }

                    espWrite("\n");
                }// data
                success = true;
            }
            catch (System.Exception)
            {
                success = false;
            }
            return success;
        }
        /// <summary>
        /// 网口打印机打印方法
        /// </summary>
        /// <param name="byte_send"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        public static void ipWrite(Byte[] byte_send, int start, int length)
        {
            try
            {
                //发送测试信息
                c.Send(byte_send, length, 0);
            }
            catch (SocketException)
            {

            }
        }
        /// <summary>
        /// esp打印机打印方法（网口，串口）
        /// </summary>
        /// <param name="bt_send"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        public static void espWrite(Byte[] bt_send, int start, int length)
        {
            if (stylePrint.Contains("COM"))
            {
                try
                {
                    serialPort.Write(bt_send, 0, bt_send.Length);
                }
                catch (Exception)
                {

                }
            }
            else if (stylePrint == "IP4")
            {
                try
                {

                    ipWrite(bt_send, 0, bt_send.Length);
                }
                catch (SocketException)
                {

                }

            }
            else if (stylePrint.Contains("LPT"))
            {
                if (iHandle != -1)
                {
                    OVERLAPPED x = new OVERLAPPED();
                    int i = 0;
                    //byte[] mybyte = System.Text.Encoding.Default.GetBytes(Mystring);

                    WriteFile(iHandle, bt_send, bt_send.Length, ref i, ref x);

                }
                else
                {
                    throw new Exception("不能连接到打印机!");
                }

            }
        }
        /// <summary>
        /// esp打印机打印方法（网口，串口）
        /// </summary>
        /// <param name="str_send"></param>
        public static void espWrite(String str_send)
        {
            Byte[] byte_send = Encoding.GetEncoding("gb18030").GetBytes(str_send);
            espWrite(byte_send, 0, byte_send.Length);
        }
    }
}
