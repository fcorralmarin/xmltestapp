using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMLTestApp.Models.DTOs
{
    public class NearPoint : IXmlSerializable
    {
        public int X { get; set; }
        public int Y { get; set; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            string xmlValue = reader.ReadString();
            if(Regex.IsMatch(xmlValue, @"-?\d;-?\d"))
            {
                X = int.Parse(xmlValue.Split(';')[0]);
                Y = int.Parse(xmlValue.Split(';')[1]);
            }
            reader.Read();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteValue($"{X};{Y}");
        }
    }
}
