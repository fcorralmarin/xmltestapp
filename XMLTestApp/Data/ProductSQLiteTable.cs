using XMLTestApp.Contracts;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTestApp.Data
{
    [Table("configuration")]
    public class ProductSQLiteTable : ISQLiteTable
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public long _Id { get; set; }
        [NotNull, Column("id")]
        public long Id { get; set; }
        [Column("name"), MaxLength(100)]
        public string Name { get; set; }
        [Column("checksum"), MaxLength(100)]
        public string Checksum { get; set; }
        [NotNull, Column("number_of_subproducts")]
        public int NumberOfSubproducts { get; set; }
    }
}
