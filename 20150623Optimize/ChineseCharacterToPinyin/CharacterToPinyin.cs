using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.International.Converters.PinYinConverter;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using System.Collections.ObjectModel; 

namespace ChineseCharacterToPinyin
{
    public class CharacterToPinyin
    {
        /// <summary>
        /// 将文本字符串转化为字符数组输出
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static char[] GetChars(string str)
        {
            char[] charList = str.ToCharArray();
            return charList;
        }
        /// <summary>
        /// 把汉字转换成拼音(全拼)
        /// </summary>
        /// <param name="hzString">汉字字符串</param>
        /// <returns>转换后的拼音(全拼)字符串</returns>
        public static string GetAllPYLetters(string hzString)
        {
            return GetPinyin(hzString, false);
        }
        /// <summary>
        /// 把汉字转换成拼音(首字母)
        /// </summary>
        /// <param name="hzString">汉字字符串</param>
        /// <returns>转换后的拼音(首字母)字符串</returns>
        public static string GetFirstPYLetters(string hzString)
        {
            return GetPinyin(hzString, true);
        }
        /// <summary>
        /// 汉字转拼音
        /// </summary>
        /// <param name="hzString"></param>
        /// <param name="isFirstLetter"></param>
        /// <returns></returns>
        private static string GetPinyin(string hzString, bool isFirstLetter)
        {
            string pyString = "";
            if (!string.IsNullOrEmpty(hzString))
            {
                char[] nowCharList = GetChars(hzString.Trim());
                int length = nowCharList.Length;
                for (int i = 0; i < length; i++)
                {
                    char nowChar = nowCharList[i];
                    if (ChineseChar.IsValidChar(nowChar))
                    {
                        ChineseChar chineseChar = new ChineseChar(nowChar);
                        //返回单个汉字拼音的所有集合，包括不同读音
                        //ReadOnlyCollection<string> roc = chineseChar.Pinyins;
                        string nowPinyin = "";
                        switch (nowChar)
                        {
                            #region 处理某些特殊的不常用的多音字
                            case '家':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '系':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '参':
                                nowPinyin = chineseChar.Pinyins[4];
                                break;
                            case '广':
                                nowPinyin = chineseChar.Pinyins[2];
                                break;
                            case '汤':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '万':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '红':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '秘':
                                nowPinyin = chineseChar.Pinyins[2];
                                break;
                            case '虾':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '腌':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '洋':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '芥':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '殖':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '石':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '奇':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '鸟':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '茄':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '合':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '粥':
                                nowPinyin = chineseChar.Pinyins[2];
                                break;
                            case '会':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '塔':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '选':
                                nowPinyin = chineseChar.Pinyins[3];
                                break;
                            case '伽':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '骑':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '单':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '咖':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '其':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '炮':
                                nowPinyin = chineseChar.Pinyins[3];
                                break;
                            case '落':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            case '叶':
                                nowPinyin = chineseChar.Pinyins[1];
                                break;
                            default:
                                nowPinyin = chineseChar.Pinyins[0];//获取第一个拼音
                                break;
                            #endregion
                        }
                        if (isFirstLetter)
                        {
                            pyString += nowPinyin.Substring(0, 1);//只截取首字母
                        } 
                        else
                        {
                            pyString += nowPinyin.Substring(0, nowPinyin.Length - 1);//去掉后面的数字（声调），只截取拼音
                        }
                    }
                    //不是汉字返回原来字符
                    else
                    {
                        pyString += nowChar.ToString();
                    }
                }
            }
            return pyString.ToLower();
        }
    }
}
