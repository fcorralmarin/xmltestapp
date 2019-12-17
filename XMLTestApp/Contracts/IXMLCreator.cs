﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTestApp.Contracts
{
    public interface IXMLCreator<T> where T : IXMLContainer
    {
        Tuple<string, T> CreateRandomXML();
    }
}
