using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GroceryStoreSystem.DAL;
using GroceryStoreSystem.Entities;
using GroceryStoreSystem.ViewModels;

namespace GroceryStoreSystem.BLL
{
    public class ProductServices
    {
        #region Constructor and Context Dependency
        private readonly GrocerylistContext _context;

        internal ProductServices(GrocerylistContext context)
        {
            _context = context;
        }
        #endregion
        #region Queries and services
        public List<ProductsListBy> ProductsByCatergory(int catergoryid,
            int pageNumber,
            int pageSize,
            out int totalrows)
        {
            //return raw data and order on presentation layer
            IEnumerable<ProductsListBy> info = _context.Products
                .Where(p => p.CategoryID == catergoryid)
                .Select(p => new ProductsListBy
                { 
                    ProductId = p.ProductID,
                    Description = p.Description,
                    Price = (double)p.Price,
                    Discount = (double)p.Discount,
                    UnitSize = p.UnitSize,
                    Taxable = p.Taxable

                })
                .OrderBy(x => x.Description);
            totalrows = info.Count();

            int skipRows = (pageNumber - 1) * pageSize;

            

            return info.Skip(skipRows).Take(pageSize).ToList();
        }
        #endregion

    }
}
