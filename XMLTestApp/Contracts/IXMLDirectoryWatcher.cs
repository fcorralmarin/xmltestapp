using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTestApp.Contracts
{
    public interface IXMLDirectoryWatcher<T> where T : ISQLiteTable
    {
        public event EventHandler<IEnumerable<T>> XMLProcessed;
        public void WatchDirectory(string directory);
    }
}
