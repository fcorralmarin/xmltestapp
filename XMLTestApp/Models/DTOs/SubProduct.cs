using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XMLTestApp.Models.DTOs
{
    public class Subproduct
    {
        public int Lenght { get; set; }
        public int Area { get; set; }
        public NearPoint NearPoint { get; set; }
    }
}
