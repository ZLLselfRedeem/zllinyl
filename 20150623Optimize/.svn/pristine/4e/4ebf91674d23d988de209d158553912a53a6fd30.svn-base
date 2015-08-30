using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;

public class SysXmlHelper
{

    public SysXmlHelper() { }

    #region XML文档节点查询和读取
    /// <summary>
    /// 选择匹配XPath表达式的第一个节点XmlNode.
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名")</param>
    /// <returns>返回XmlNode</returns>
    public static XmlNode GetXmlNodeByXpath(string xmlFileName, string xpath)
    {
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            return xmlNode;
        }
        catch (Exception)
        {
            return null;
            //throw ex; //这里可以定义你自己的异常处理
        }
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
            //throw ex; //这里可以定义你自己的异常处理
        }
    }

    /// <summary>
    /// 选择匹配XPath表达式的第一个节点的匹配xmlAttributeName的属性XmlAttribute.
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <param name="xmlAttributeName">要匹配xmlAttributeName的属性名称</param>
    /// <returns>返回xmlAttributeName</returns>
    public static XmlAttribute GetXmlAttribute(string xmlFileName, string xpath, string xmlAttributeName)
    {
        string content = string.Empty;
        XmlDocument xmlDoc = new XmlDocument();
        XmlAttribute xmlAttribute = null;
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                if (xmlNode.Attributes.Count > 0)
                {
                    xmlAttribute = xmlNode.Attributes[xmlAttributeName];
                }
            }
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return xmlAttribute;
    }

    /// <summary>
    /// New 选择匹配XPath表达式的第一个节点的匹配xmlAttributeName的属性XmlAttribute的节点名称.
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <param name="xmlAttributeName">要匹配xmlAttributeName的属性名称</param>
    /// <returns>返回xmlAttributeName</returns>
    public static XmlNode GetXmlNodeByAttribute(string xmlFileName, string xpath, string xmlAttributeName, string xmlAttributeValue)
    {
        XmlNode xmlNode = null;
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNodeList List = xmlDoc.SelectNodes(xpath);
            if (List != null)
            {
                foreach (XmlNode node in List)
                {
                    if (node != null)
                    {
                        if (node.Attributes.Count > 0)
                        {

                            foreach (XmlAttribute attr in node.Attributes)
                            {
                                if (attr.Name == xmlAttributeName && attr.Value == xmlAttributeValue)
                                {
                                    xmlNode = node;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return xmlNode;
    }

    /// <summary>
    /// 获取记录条数
    /// </summary>
    /// <param name="xmlFileName"></param>
    /// <param name="xpath"></param>
    /// <returns></returns>
    public static int GetXmlNodeCount(string xmlFileName, string xpath)
    {
        XmlDocument xmlDoc = new XmlDocument();

        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            return xmlDoc.SelectNodes(xpath).Count;
        }
        catch (Exception)
        {
            return 0;
            //throw ex; //这里可以定义你自己的异常处理
        }
    }

    /// <summary>
    /// 获取当前最大序号
    /// </summary>
    /// <param name="xmlFileName"></param>
    /// <param name="xpath"></param>
    /// <returns></returns>
    public static int GetXmlNodeMaxNumber(string xmlFileName, string xpath)
    {
        XmlDocument xmlDoc = new XmlDocument();

        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNodeList List = xmlDoc.SelectNodes(xpath);
            return Convert.ToInt32(List[List.Count - 1].InnerText);//找到最后一个节点的序号然后加1
        }
        catch (Exception)
        {
            return 0;
            //throw ex; //这里可以定义你自己的异常处理
        }
    }
    #endregion

    #region XML文档创建和节点或属性的添加、修改

    /// <summary>
    /// 创建一个XML文档
    /// </summary>
    /// <param Name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param Name="rootNodeName">XML文档根节点名称(须指定一个根节点名称)</param>
    /// <param Name="innerText">内容</param>
    /// <returns></returns>
    public static bool CreateXmlDocument(string xmlFileName, string rootNodeName)
    {
        return CreateXmlDocument(xmlFileName, rootNodeName, "1.0", "utf-8", null);
    }

    /// <summary>
    /// 创建一个XML文档
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="rootNodeName">XML文档根节点名称(须指定一个根节点名称)</param>
    /// <param name="version">XML文档版本号(必须为:"1.0")</param>
    /// <param name="encoding">XML文档编码方式</param>
    /// <param name="standalone">该值必须是"yes"或"no",如果为null,Save方法不在XML声明上写出独立属性</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool CreateXmlDocument(string xmlFileName, string rootNodeName, string version, string encoding, string standalone)
    {
        bool isSuccess = false;
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration(version, encoding, standalone);
            XmlNode root = xmlDoc.CreateElement(rootNodeName);
            xmlDoc.AppendChild(xmlDeclaration);
            xmlDoc.AppendChild(root);
            xmlDoc.Save(xmlFileName);
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }

    public static bool CreateXmlDocument(string xmlFileName, string rootNodeName, string innerXml)
    {
        return CreateXmlDocument(xmlFileName, rootNodeName, innerXml, "1.0", "utf-8", null);
    }

    public static bool CreateXmlDocument(string xmlFileName, string rootNodeName, string innerXml, string version, string encoding, string standalone)
    {
        bool isSuccess = false;
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration(version, encoding, standalone);
            XmlNode root = xmlDoc.CreateElement(rootNodeName);
            root.InnerXml = innerXml;
            xmlDoc.AppendChild(xmlDeclaration);
            xmlDoc.AppendChild(root);
            xmlDoc.Save(xmlFileName);
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }

    /// <summary>
    /// 依据匹配XPath表达式的第一个节点来创建它的子节点(如果此节点已存在则追加一个新的同名节点
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <param name="xmlNodeName">要匹配xmlNodeName的节点名称</param>
    /// <param name="innerText">节点文本值</param>
    /// <param name="xmlAttributeName">要匹配xmlAttributeName的属性名称</param>
    /// <param name="value">属性值</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool CreateXmlNodeByXPath(string xmlFileName, string xpath, string xmlNodeName, string innerText, string xmlAttributeName, string value)
    {
        bool isSuccess = false;
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNodeList List = xmlDoc.SelectNodes(xpath);
            if (List != null)
            {
                XmlNode xmlNode = List[List.Count - 1];

                if (xmlNode != null)
                {
                    //存不存在此节点都创建
                    XmlElement subElement = xmlDoc.CreateElement(xmlNodeName);
                    subElement.InnerXml = innerText;

                    //如果属性和值参数都不为空则在此新节点上新增属性
                    if (!string.IsNullOrEmpty(xmlAttributeName) && !string.IsNullOrEmpty(value))
                    {
                        XmlAttribute xmlAttribute = xmlDoc.CreateAttribute(xmlAttributeName);
                        xmlAttribute.Value = value;
                        subElement.Attributes.Append(xmlAttribute);
                    }

                    xmlNode.AppendChild(subElement);
                }
                xmlDoc.Save(xmlFileName); //保存到XML文档
                isSuccess = true;
            }
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }

    /// <summary>
    /// New 依据匹配XPath表达式的最后一个节点来创建它的子节点(如果此节点已存在则追加一个新的同名节点
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <param name="xmlNodeName">要匹配xmlNodeName的节点名称</param>
    /// <param name="innerText">节点文本值</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool CreateXmlNodeByXPath(string xmlFileName, string xpath, string xmlNodeName, string innerText)
    {
        bool isSuccess = false;
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNodeList List = xmlDoc.SelectNodes(xpath);
            if (List != null)
            {
                XmlNode xmlNode = List[List.Count - 1];

                if (xmlNode != null)
                {
                    //存不存在此节点都创建
                    XmlElement subElement = xmlDoc.CreateElement(xmlNodeName);
                    subElement.InnerXml = innerText;

                    xmlNode.AppendChild(subElement);
                }
            }
            xmlDoc.Save(xmlFileName); //保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }

    /// <summary>
    /// 依据匹配XPath表达式的第一个节点来创建或更新它的子节点(如果节点存在则更新,不存在则创建)
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <param name="xmlNodeName">要匹配xmlNodeName的节点名称</param>
    /// <param name="innerText">节点文本值</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool CreateOrUpdateXmlNodeByXPath(string xmlFileName, string xpath, string xmlNodeName, string innerText)
    {
        bool isSuccess = false;
        bool isExistsNode = false;//标识节点是否存在
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                //遍历xpath节点下的所有子节点
                foreach (XmlNode node in xmlNode.ChildNodes)
                {
                    if (node.Name.ToLower() == xmlNodeName.ToLower())
                    {
                        //存在此节点则更新
                        node.InnerXml = innerText;
                        isExistsNode = true;
                        break;
                    }
                }
                if (!isExistsNode)
                {
                    //不存在此节点则创建
                    XmlElement subElement = xmlDoc.CreateElement(xmlNodeName);
                    subElement.InnerXml = innerText;
                    xmlNode.AppendChild(subElement);
                }
            }
            xmlDoc.Save(xmlFileName); //保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }

    /// <summary>
    /// New 依据匹配XPath表达式的第一个节点来创建或更新它的子节点(如果节点存在则更新,不存在则创建)
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <param name="xmlNodeName">要匹配xmlNodeName的节点名称</param>
    /// <param name="innerText">节点文本值</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool CreateOrUpdateXmlNodeAndValueByXPath(string xmlFileName, string xpath, string xmlNodeName, string innerText)
    {
        bool isSuccess = false;
        bool isExistsNode = false;//标识节点是否存在
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                //遍历xpath节点下的所有子节点
                foreach (XmlNode node in xmlNode.ChildNodes)
                {
                    if (node.Name.ToLower() == xmlNodeName.ToLower() && node.InnerText.ToLower() == innerText)
                    {
                        //存在此节点则更新
                        node.InnerXml = innerText;
                        isExistsNode = true;
                        break;
                    }
                }
                if (!isExistsNode)
                {
                    //不存在此节点则创建
                    XmlElement subElement = xmlDoc.CreateElement(xmlNodeName);
                    subElement.InnerXml = innerText;
                    xmlNode.AppendChild(subElement);
                }
            }
            xmlDoc.Save(xmlFileName); //保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }


    /// <summary>
    /// 依据匹配XPath表达式的第一个节点来创建或更新它的属性(如果属性存在则更新,不存在则创建)
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <param name="xmlAttributeName">要匹配xmlAttributeName的属性名称</param>
    /// <param name="value">属性值</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool CreateOrUpdateXmlAttributeByXPath(string xmlFileName, string xpath, string xmlAttributeName, string value)
    {
        bool isSuccess = false;
        bool isExistsAttribute = false;//标识属性是否存在
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                //遍历xpath节点中的所有属性
                foreach (XmlAttribute attribute in xmlNode.Attributes)
                {
                    if (attribute.Name.ToLower() == xmlAttributeName.ToLower())
                    {
                        //节点中存在此属性则更新
                        attribute.Value = value;
                        isExistsAttribute = true;
                        break;
                    }
                }
                if (!isExistsAttribute)
                {
                    //节点中不存在此属性则创建
                    XmlAttribute xmlAttribute = xmlDoc.CreateAttribute(xmlAttributeName);
                    xmlAttribute.Value = value;
                    xmlNode.Attributes.Append(xmlAttribute);
                }
            }
            xmlDoc.Save(xmlFileName); //保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }

    /// <summary>
    /// New 依据匹配XPath表达式的指定节点来创建或更新它的属性(如果属性存在则更新,不存在则创建)
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <param name="xmlAttributeName">要匹配xmlAttributeName的属性名称</param>
    /// <param name="value">属性值</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool CreateOrUpdateXmlAttributeByXPath(string xmlFileName, string xpath, string innerText, string xmlAttributeName, string value)
    {
        bool isSuccess = false;
        bool isExistsAttribute = false;//标识属性是否存在
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNodeList List = xmlDoc.SelectNodes(xpath);
            if (List != null)
            {
                foreach (XmlNode node in List)
                {
                    if (node.InnerText == innerText)
                    {
                        //遍历xpath节点中的所有属性
                        foreach (XmlAttribute attribute in node.Attributes)
                        {
                            if (attribute.Name.ToLower() == xmlAttributeName.ToLower())
                            {
                                //节点中存在此属性则更新
                                attribute.Value = value;
                                isExistsAttribute = true;
                                break;
                            }
                        }

                        if (!isExistsAttribute)
                        {
                            //节点中不存在此属性则创建
                            XmlAttribute xmlAttribute = xmlDoc.CreateAttribute(xmlAttributeName);
                            xmlAttribute.Value = value;
                            node.Attributes.Append(xmlAttribute);
                        }
                    }
                }
            }
            xmlDoc.Save(xmlFileName); //保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }

    /// <summary>
    /// 依据匹配XPath表达式的第一个节点来更新它的子节点
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <param name="xmlNodeName">要匹配xmlNodeName的节点名称</param>
    /// <param name="innerText">节点文本值</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool UpdateXmlNodeByXPath(string xmlFileName, string xpath, string xmlNodeName, string innerText)
    {
        bool isSuccess = false;
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNodeList xmlNodeList = xmlDoc.SelectNodes(xpath);

            if (xmlNodeList != null)
            {
                //遍历xpath节点下的所有子节点
                foreach (XmlNode node in xmlNodeList)
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name == "" && child.InnerText == "")
                        {

                        }
                    }
                    if (node.Name.ToLower() == xmlNodeName.ToLower())
                    {
                        //存在此节点则更新
                        node.InnerXml = innerText;

                        break;
                    }
                }
            }
            xmlDoc.Save(xmlFileName); //保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }
    #endregion


    #region XML文档节点或属性的删除
    /// <summary>
    /// 删除匹配XPath表达式的第一个节点(节点中的子元素同时会被删除)
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool DeleteXmlNodeByXPath(string xmlFileName, string xpath)
    {
        bool isSuccess = false;
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                //删除节点
                xmlNode.ParentNode.RemoveChild(xmlNode);
            }
            xmlDoc.Save(xmlFileName); //保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }

    /// <summary>
    /// 删除匹配XPath表达式的指定节点(指定节点中的子元素同时会被删除)
    /// </summary>
    /// <param name="xmlFileName"></param>
    /// <param name="xpath"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool DeleteXmlNodeByXPathAndNode(string xmlFileName, string xpath, string number)
    {
        bool isSuccess = false;
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName);//加载XML
            XmlNodeList xmlNodeList = xmlDoc.SelectNodes(xpath);
            if (xmlNodeList != null)
            {
                foreach (XmlNode node in xmlNodeList)
                {
                    if (node.InnerText == number)
                    {
                        //删除节点
                        node.ParentNode.ParentNode.RemoveChild(node.ParentNode);
                        break;
                    }
                }
            }
            xmlDoc.Save(xmlFileName);//保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }

    /// <summary>
    /// 删除匹配XPath表达式的第一个节点中的匹配参数xmlAttributeName的属性
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <param name="xmlAttributeName">要删除的xmlAttributeName的属性名称</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool DeleteXmlAttributeByXPath(string xmlFileName, string xpath, string xmlAttributeName)
    {
        bool isSuccess = false;
        bool isExistsAttribute = false;
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            XmlAttribute xmlAttribute = null;
            if (xmlNode != null)
            {
                //遍历xpath节点中的所有属性
                foreach (XmlAttribute attribute in xmlNode.Attributes)
                {
                    if (attribute.Name.ToLower() == xmlAttributeName.ToLower())
                    {
                        //节点中存在此属性
                        xmlAttribute = attribute;
                        isExistsAttribute = true;
                        break;
                    }
                }
                if (isExistsAttribute)
                {
                    //删除节点中的属性
                    xmlNode.Attributes.Remove(xmlAttribute);
                }
            }
            xmlDoc.Save(xmlFileName); //保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }


    /// <summary>
    /// 删除匹配XPath表达式的匹配参数xmlAttributeName和Value的节点
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <param name="xmlAttributeName">要删除节点的属性名称</param>
    /// <param name="xmlAttributeValue">要删除节点的属性值</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool DeleteXmlByAttribute(string xmlFileName, string xpath, string xmlAttributeName, string xmlAttributeValue)
    {
        bool isSuccess = false;
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNodeList List = xmlDoc.SelectNodes(xpath);
            if (List != null)
            {
                foreach (XmlNode node in List)
                {
                    //遍历xpath节点中的所有属性
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        if (attribute.Name.ToLower() == xmlAttributeName.ToLower() && attribute.Value == xmlAttributeValue)
                        {
                            //节点中存在此属性
                            node.ParentNode.RemoveChild(node);
                            break;
                        }
                    }
                }
            }
            xmlDoc.Save(xmlFileName); //保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }


    /// <summary>
    /// 删除匹配XPath表达式的第一个节点中的所有属性
    /// </summary>
    /// <param name="xmlFileName">XML文档完全文件名(包含物理路径)</param>
    /// <param name="xpath">要匹配的XPath表达式(例如:"//节点名//子节点名</param>
    /// <returns>成功返回true,失败返回false</returns>
    public static bool DeleteAllXmlAttributeByXPath(string xmlFileName, string xpath)
    {
        bool isSuccess = false;
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName); //加载XML文档
            XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                //遍历xpath节点中的所有属性
                xmlNode.Attributes.RemoveAll();
            }
            xmlDoc.Save(xmlFileName); //保存到XML文档
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex; //这里可以定义你自己的异常处理
        }
        return isSuccess;
    }
    #endregion

    /// <summary>
    /// 打开xml文件
    /// </summary>
    /// <param Name="xmlFileName"></param>
    /// <returns></returns>
    public static XmlDocument OpenXml(string xmlFileName)
    {
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xmlFileName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return xmlDoc;
    }

    /// <summary>
    /// 保存xml文件
    /// </summary>
    /// <param Name="xmlDoc"></param>
    /// <param Name="xmlFileName"></param>
    /// <returns></returns>
    public static bool SaveXml(XmlDocument xmlDoc, string xmlFileName)
    {
        bool isSuccess = false;
        try
        {
            xmlDoc.Save(xmlFileName);

            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return isSuccess;
    }
}