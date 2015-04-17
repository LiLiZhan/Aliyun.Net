using System;
using System.IO;
using System.Xml;

namespace Aliyun
{
    /// <summary>
    /// Xml帮助类
    /// </summary>
    public class XmlHelper
    {
        /// <summary>
        /// 创建xml文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="rootName">根节点名</param>
        /// <returns></returns>
        public static XmlDocument CreateXml(string filePath, string rootName)
        {
            bool isExists = File.Exists(filePath);
            XmlDocument xmlDoc = new XmlDocument();
            if (isExists)
                xmlDoc.Load(filePath);
            else
            {
                xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", ""));
                xmlDoc.AppendChild(xmlDoc.CreateElement(rootName));
            }
            return xmlDoc;
        }

        /// <summary>
        /// 创建xml文件
        /// 默认根节点root
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static XmlDocument CreateXml(string filePath)
        {
            return CreateXml(filePath, "root");
        }

        /// <summary>
        /// 获取xml文档中节点
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="rootName">根节点名</param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static XmlNode GetNode(string filePath, string rootName, string xpath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            return GetNode(xmlDoc, rootName, xpath);
        }

        /// <summary>
        /// 获取xml文档中节点
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static XmlNode GetNode(string filePath, string xpath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            return GetNode(xmlDoc, xpath);
        }

        /// <summary>
        /// 获取xml文档中节点
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="rootName">根节点名</param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static XmlNode GetNode(XmlDocument xmlDoc, string rootName, string xpath)
        {
            if (xpath.StartsWith("/"))
                xpath = xpath.Substring(xpath.IndexOf("/") + 1);
            if (string.IsNullOrEmpty(rootName))
                return xmlDoc.SelectSingleNode(xpath);
            return xmlDoc.SelectSingleNode(string.Format("/{0}/{1}", rootName, xpath));
        }

        /// <summary>
        /// 获取xml文档中节点
        /// 默认根节点root
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static XmlNode GetNode(XmlDocument xmlDoc, string xpath)
        {
            return GetNode(xmlDoc, "root", xpath);
        }

        /// <summary>
        /// 获取元素属性、属性值和元素值
        /// </summary>
        /// <param name="xmlElement">元素</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        /// <param name="innerText">元素值</param>
        public static void GetAttribute(XmlElement xmlElement, string attributeName, ref string attributeValue, ref string innerText)
        {
            attributeValue = null;
            innerText = null;
            if (string.IsNullOrEmpty(attributeName))
                innerText = xmlElement.InnerText;
            else
            {
                if (xmlElement.HasAttribute(attributeName))
                {
                    attributeValue = xmlElement.GetAttribute(attributeName);
                    innerText = xmlElement.InnerText;
                }
            }
        }

        /// <summary>
        /// 获取元素属性、属性值
        /// </summary>
        /// <param name="xmlElement">元素</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        public static void GetAttribute(XmlElement xmlElement, string attributeName, ref string attributeValue)
        {
            string innerText = null;
            GetAttribute(xmlElement, attributeName, ref attributeValue, ref innerText);
        }

        public static void GetAttribute(XmlElement xmlElement, string attributeName, ref int attributeValue)
        {
            string innerText = null;
            string _attributeValue = null;
            GetAttribute(xmlElement, attributeName, ref _attributeValue, ref innerText);
            attributeValue = Convert.ToInt32(_attributeValue);
        }

        /// <summary>
        /// 获取元素值
        /// </summary>
        /// <param name="xmlElement">元素</param>
        /// <param name="innerText">元素值</param>
        public static void GetAttribute(XmlElement xmlElement, ref string innerText)
        {
            string attributeValue = null;
            GetAttribute(xmlElement, null, ref attributeValue, ref innerText);
        }

        /// <summary>
        /// 向元素节点添加指定元素名、属性名、属性值、元素值的子元素
        /// </summary>
        /// <param name="xmlElement">父元素节点</param>
        /// <param name="subElementName">子元素名</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        /// <param name="innerText">元素值</param>
        /// <returns></returns>
        public static XmlElement AddElement(XmlElement xmlElement, string subElementName, string attributeName, string attributeValue, string innerText)
        {
            XmlElement xmlSubElement = xmlElement.OwnerDocument.CreateElement(subElementName);
            if (!string.IsNullOrEmpty(attributeName))
                xmlSubElement.SetAttribute(attributeName, attributeValue);
            xmlSubElement.InnerText = innerText;
            xmlElement.AppendChild(xmlSubElement);
            return xmlSubElement;
        }

        public static XmlElement AddElement(XmlElement xmlElement, string subElementName, string attributeName, bool attributeValue, string innerText)
        {
            XmlElement xmlSubElement = xmlElement.OwnerDocument.CreateElement(subElementName);
            if (!string.IsNullOrEmpty(attributeName))
                xmlSubElement.SetAttribute(attributeName, attributeValue ? "1" : "0");
            xmlSubElement.InnerText = innerText;
            xmlElement.AppendChild(xmlSubElement);
            return xmlSubElement;
        }

        /// <summary>
        /// 向元素节点添加指定元素名、属性名、属性值、元素值的子元素
        /// </summary>
        /// <param name="xmlElement">父元素节点</param>
        /// <param name="subElementName">子元素名</param>
        /// <param name="innerText">元素值</param>
        /// <returns></returns>
        public static XmlElement AddElement(XmlElement xmlElement, string subElementName, string innerText)
        {
            return AddElement(xmlElement, subElementName, null, null, innerText);
        }

        public static XmlElement AddElement(XmlElement xmlElement, string subElementName, bool innerText)
        {
            return AddElement(xmlElement, subElementName, null, null, innerText ? "1" : "0");
        }

        /// <summary>
        /// 向指定元素节点添加属性
        /// </summary>
        /// <param name="xmlElement">元素节点</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        /// <returns></returns>
        public static XmlElement AddAttribute(XmlElement xmlElement, string attributeName, string attributeValue)
        {
            xmlElement.SetAttribute(attributeName, attributeValue);
            return xmlElement;
        }

        /// <summary>
        /// 移除当前节点的所有指定属性和子级。不移除默认属性。
        /// </summary>
        /// <param name="xmlElement">目标元素节点</param>
        /// <returns></returns>
        public static XmlElement ClearElement(XmlElement xmlElement)
        {
            xmlElement.RemoveAll();
            return xmlElement;
        }

        /// <summary>
        /// 删除元素节点
        /// </summary>
        /// <param name="xmlElement">元素节点</param>
        /// <returns></returns>
        public static bool DeleteElement(XmlElement xmlElement)
        {
            return DeleteElement(xmlElement, null, false);
        }

        /// <summary>
        /// 删除元素节点属性
        /// </summary>
        /// <param name="xmlElement">元素节点</param>
        /// <param name="attributeName">属性名</param>
        /// <returns></returns>
        public static bool DeleteElement(XmlElement xmlElement, string attributeName)
        {
            return DeleteElement(xmlElement, attributeName, true);
        }

        /// <summary>
        /// 删除元素节点或节点属性
        /// </summary>
        /// <param name="xmlElement">元素节点</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="isDeleteAttribute">是否删除仅属性</param>
        /// <returns></returns>
        private static bool DeleteElement(XmlElement xmlElement, string attributeName, bool isDeleteAttribute)
        {
            try
            {
                if (!isDeleteAttribute)
                    xmlElement.ParentNode.RemoveChild(xmlElement);
                else
                    if (xmlElement.HasAttribute(attributeName))
                        xmlElement.RemoveAttribute(attributeName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 设置元素节点属性、属性值和元素值
        /// </summary>
        /// <param name="xmlElement">元素节点</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        /// <param name="innerText">元素值</param>
        public static void SetAttribute(XmlElement xmlElement, string attributeName, string attributeValue, string innerText)
        {
            xmlElement.InnerText = innerText;
            if (!string.IsNullOrEmpty(attributeName))
                xmlElement.SetAttribute(attributeName, attributeValue);
        }

        /// <summary>
        /// 设置元素节点属性、属性值
        /// </summary>
        /// <param name="xmlElement">元素节点</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        public static void SetAttribute(XmlElement xmlElement, string attributeName, string attributeValue)
        {
            SetAttribute(xmlElement, attributeName, attributeValue, null);
        }

        /// <summary>
        /// 设置元素节点元素值
        /// </summary>
        /// <param name="xmlElement">元素节点</param>
        /// <param name="innerText">元素值</param>
        public static void SetAttribute(XmlElement xmlElement, string innerText)
        {
            SetAttribute(xmlElement, null, null, innerText);
        }
    }
}
