using XMLTestApp.Contracts;
using XMLTestApp.Models.DTOs;
using XMLTestApp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLTestApp.Models.Logic.XMLContainers
{
    [Serializable, XmlRoot("Configuration")]
    public class ConfigurationContainer : IXMLContainer
    {
        [XmlElement("ProductType1")]
        public List<Product> ProductsType1 { get; set; }
        [XmlElement("ProductType2")]
        public List<Product> ProductsType2 { get; set; }

        public void LoadFromXML(string inputPath)
        {
            ConfigurationContainer aux = inputPath.LoadFromXML<ConfigurationContainer>();
            foreach(var property in GetType().GetProperties())
            {
                property.SetValue(this, property.GetValue(aux));
            }
        }

        public void SaveAsXML(string outputPath)
        {
            this.SaveAsXML<ConfigurationContainer>(outputPath);
        }
    }
}
