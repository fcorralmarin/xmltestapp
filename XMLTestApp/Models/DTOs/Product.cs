using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLTestApp.Models.DTOs
{
    public class Product
    {
        public string Name { get; set; }
        public long Id { get; set; }
        [XmlElement("checksum")]
        public string Checksum { get; set; }
        public int Var1 { get; set; }
        public int Var2 { get; set; }
        public string Var3 { get; set; }
        [XmlElement("Subproduct")]
        public List<Subproduct> SubProducts { get; set; }
    }
}
