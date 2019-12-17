using XMLTestApp.Data;
using XMLTestApp.Models.DTOs;
using XMLTestApp.Models.Logic.XMLContainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTestApp.Extensions
{
    public static class ModelExtensions
    {
        public static IEnumerable<ProductSQLiteTable> ToDBModel(this ConfigurationContainer configurationContainer)
        {
            List<ProductSQLiteTable> productDBModels = new List<ProductSQLiteTable>();
            if(configurationContainer != null)
            {
                productDBModels.AddRange(configurationContainer.ProductsType1.ToDBModelList());
                productDBModels.AddRange(configurationContainer.ProductsType2.ToDBModelList());
            }
            return productDBModels;
        }

        public static List<ProductSQLiteTable> ToDBModelList(this List<Product> products)
        {
            List<ProductSQLiteTable> productDBModels = new List<ProductSQLiteTable>();
            productDBModels = products.Select(x => x.ToDBModel()).ToList();
            return productDBModels;
        }
        public static ProductSQLiteTable ToDBModel(this Product product)
        {
            return new ProductSQLiteTable()
            {
                Checksum = product.Checksum,
                Name = product.Name,
                Id = product.Id,
                NumberOfSubproducts = product.SubProducts != null ? product.SubProducts.Count : 0
            };
        }
    }
}
