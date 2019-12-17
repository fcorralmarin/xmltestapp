using XMLTestApp.Data;
using XMLTestApp.Models.Logic.XMLContainers;
using System.Collections.ObjectModel;
using XMLTestApp.Extensions;
using System.Collections.Generic;

namespace XMLTestApp.Models.DTOs
{
    public class ProductObservableCollection : ObservableCollection<ProductSQLiteTable>
    {
        public ProductObservableCollection()
        {
        }

        public ProductObservableCollection(IEnumerable<ProductSQLiteTable> collection) : base(collection)
        {
        }
    }
}
