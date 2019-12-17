using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTestApp.Contracts
{
    public interface IXMLContainer
    {
        void SaveAsXML(string outputPath);
        void LoadFromXML(string inputPath);
    }
}
