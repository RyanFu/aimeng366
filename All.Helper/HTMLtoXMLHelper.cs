using Sgml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace All.Helper
{
    public class HTMLtoXMLHelper
    {

        /// <summary>
        /// 获取xml中的数据  根据
        /// </summary>
        /// <param name="htmlString"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static string GetWellFormedHTML(string htmlString, string xpath)
        {
            if (htmlString.Trim().Length<10)
            {
                return "";
            }
            htmlString = htmlString.Replace("xmlns", "buyao");
            StringWriter sw = null;
            SgmlReader reader = null;
            XmlTextWriter writer = null;
            try
            {
                reader = new SgmlReader();
                reader.DocType = "HTML";
                reader.InputStream = new StringReader(htmlString);
                sw = new StringWriter();
                writer = new XmlTextWriter(sw);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                while (reader.Read())
                {
                    if (reader.NodeType != XmlNodeType.Whitespace)
                    {
                        try
                        {
                            //如果出错 抛弃此节点
                            writer.WriteNode(reader, true);
                        }
                        catch (Exception e)
                        {
                        }
                    }
                }
                if (xpath == null)
                {
                    return sw.ToString();
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    XPathDocument doc = new XPathDocument(new StringReader(sw.ToString()));
                    XPathNavigator nav = doc.CreateNavigator();
                    XPathNodeIterator nodes = nav.Select(xpath);
                    while (nodes.MoveNext())
                    {
                        sb.Append(nodes.Current.OuterXml + " ");
                    }
                    return sb.ToString();
                }
            }
            catch (Exception exp)
            {
                writer.Close();
                reader.Close();
                sw.Close();
                return "";
            }
        }

        public static string PartHtml(string htmlString, string xpath)
        {
            if (xpath != null)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    XPathDocument doc = new XPathDocument(new StringReader(htmlString));
                    XPathNavigator nav = doc.CreateNavigator();
                    XPathNodeIterator nodes = nav.Select(xpath);
                    while (nodes.MoveNext())
                    {
                        sb.Append(nodes.Current.Value + " ");
                    }
                    return sb.ToString();
                }
                catch (Exception)
                {
                    return "";
                }
            }
            return "";
        }

    }
}
