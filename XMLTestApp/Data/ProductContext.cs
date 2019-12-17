using XMLTestApp.Contracts;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTestApp.Data
{
    public class ProductContext : ISQLiteContext<ProductSQLiteTable>
    {
        private static SQLiteConnection SQLiteConnection;
        public void Connect(string dbFilePath)
        {
            SQLiteConnection = new SQLiteConnection(dbFilePath);
        }

        public void CreateTable()
        {
            SQLiteConnection.CreateTable<ProductSQLiteTable>();
        }

        public List<ProductSQLiteTable> GetAll()
        {
            return SQLiteConnection.Table<ProductSQLiteTable>().ToList();
        }

        public void Insert(IEnumerable<ProductSQLiteTable> newObjects)
        {
            SQLiteConnection.InsertAll(newObjects, typeof(ProductSQLiteTable));
        }
    }
}
