using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Qin.Blog.Common
{
    public class XmlHelper
    {
        XmlDocument XmlDoc;
        private static object DocLock = new object();

        /// <summary>
        /// 获取XML
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        // xmlFile = HttpContext.Server.MapPath("~/Student.xml")
        public string GetXml(string xmlFile)
        {
            string id = "";
            string Info = "";

            if (XmlDoc == null)
            {
                lock (DocLock)
                {
                    if (XmlDoc == null)
                    {
                        XmlDoc = new XmlDocument();
                        XmlDoc.Load(xmlFile);
                    }
                }
            }

            string Name = string.Empty;
            string _id = string.Empty;
            XmlElement root = XmlDoc.DocumentElement;
            XmlNodeList personNodes = root.GetElementsByTagName("person");
            foreach (XmlNode node in personNodes)
            {
                if (((XmlElement)node).GetAttribute("id") == "2" || ((XmlElement)node).GetAttribute("id") == "4")
                {
                    Name += ((XmlElement)node).InnerText;
                    _id += ((XmlElement)node).GetAttribute("id");
                    var str = node.GetEnumerator();
                }
            }

            XmlNodeReader ParaReader = new XmlNodeReader(XmlDoc);
            while (ParaReader.Read())
            {
                if (ParaReader.NodeType == XmlNodeType.Element && ParaReader.Name == "person")
                {
                    if (!string.IsNullOrEmpty(ParaReader.GetAttribute("id")))
                    {
                        id += ParaReader.GetAttribute("id") + "+";
                        Info += ParaReader.ReadInnerXml() + "+";

                    }
                    //if (f == "PaymentDate" && f == ParaReader.GetAttribute(0)) Info = ParaReader.GetAttribute(1);//Info = ParaReader.GetAttribute(1).Replace("{2}", Member.ValidBeginDate + "");//缴费
                    //if (f == "ReplacementDate" && f == ParaReader.GetAttribute(0)) Info = ParaReader.GetAttribute("value");//Info = ParaReader.GetAttribute("value").Replace("{2}", Member.ValidBeginDate + "").Replace("{3}", Member.ReplacementDate + "");  //换证 
                    //if (f == "ContributionsDate" && f == ParaReader.GetAttribute(0)) Info = ParaReader.GetAttribute("value"); //体检 
                }
                string str = ParaReader.GetAttribute("id") + ParaReader.GetAttribute("sex") + ParaReader.ReadInnerXml();
            }
            ParaReader.Close();
            return System.Text.Encoding.GetEncoding("gb2312").GetString(System.Text.Encoding.Default.GetBytes(id + "\n" + Info));
        }
    }
}
