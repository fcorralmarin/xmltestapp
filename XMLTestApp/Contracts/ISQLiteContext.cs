using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTestApp.Contracts
{
    public interface ISQLiteContext<ISQLiteTable>
    {
        void Connect(string dbFilePath);
        void CreateTable();
        List<ISQLiteTable> GetAll();
        void Insert(IEnumerable<ISQLiteTable> newObjects);
    }
}
