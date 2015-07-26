using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SpaceShip.Classes.XML
{
    public class XMLParser
    {
        
        /// <summary>
        /// Serializes an object to an xml file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlObject"></param>
        /// <returns></returns>
        public static string SeralizeObjectToXML<T>(T xmlObject)
        {
            StringBuilder sbTR = new StringBuilder();
            XmlSerializer xmsTR = new XmlSerializer(xmlObject.GetType());
            //XmlWriterSettings xwsTR = new XmlWriterSettings() { OmitXmlDeclaration false, Indent = true };

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            string serResult;
            using (StringWriter writer = new IsoWriter())
            {
                xmsTR.Serialize(writer, xmlObject, namespaces);
                serResult = writer.ToString();
            }
            return serResult;
            //return sbTR.ToString();
        }


        public static string SeralizeObjectWithoutXMLDeclaration<T>(T xmlObject)
        {
            StringBuilder sbTR = new StringBuilder();
            XmlSerializer xmsTR = new XmlSerializer(xmlObject.GetType());
            var xwsTR = new System.Xml.XmlWriterSettings() { OmitXmlDeclaration = true, Indent = true };

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            string serResult;
            using (StringWriter writer = new IsoWriter())
            {
                using (var xmlWriter = System.Xml.XmlWriter.Create(writer, xwsTR))
                {
                    xmsTR.Serialize(xmlWriter, xmlObject, namespaces);
                    serResult = writer.ToString();
                }
            }
            return serResult;
        }

        /// <summary>
        /// Creates a class for the given xml file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                throw new Exception("XmlData is null");

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader rdr = new StringReader(xml);
            return (T)serializer.Deserialize(rdr);
        }

        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }

        public class IsoWriter : StringWriter
        {
            public override Encoding Encoding
            {
                get { return Encoding.GetEncoding("ISO-8859-1"); }
            }
        }

    }
}
