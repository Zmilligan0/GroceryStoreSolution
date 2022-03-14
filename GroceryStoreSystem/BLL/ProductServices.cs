#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GroceryStoreSystem.DAL;
using GroceryStoreSystem.Entities;
using GroceryStoreSystem.ViewModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        //New query here for crud
        public ProductItem Products_GetProductById(int ProductId)
        {
            //linq to entity therefore you need to access the DbSet in your
            //      context class

            ProductItem info = _context.Products
                            .Where(x => x.ProductID == ProductId)
                            .Select(x => new ProductItem
                            {
                                ProductId = x.ProductID,
                                Description = x.Description,
                                Price = (double)x.Price,
                                Discount = (double)x.Discount,
                                UnitSize = x.UnitSize,
                                Taxable = x.Taxable
                            }).FirstOrDefault();
            return info;
        }
        #endregion
        #region add update delete
        //update add delete
        public int AddProduct(ProductItem item)
        {
            //this method will return the new Product id if the add was successful
            //REMINDER: ProductItem is NOT an entity; it is a view model class
            //          This means you MUST move the data from the view model class
            //              to an instance of your desired entity

            //add a business rule to the method
            //     rule: no Product with the same Description, same year, same artist
            //     result: this will be considered a duplicate Product

            //how can one do such a test
            //1) use a search loop pattern: set a flag as found or not found
            //2) use can use Linq and test the result of a query: .FirstOrDefault()
            Product exist = _context.Products
                            .Where(x => x.Description.Equals(item.Description)
                                     && x.UnitSize == item.UnitSize)
                            .FirstOrDefault();
            if (exist != null)
            {
                throw new Exception("Product already exists on file");
            }

            //setup the entity instance with the data from the view model parameter
            //NOTE: Product has a identity pkey; therefore one does NOT need to set
            //      the ProductID
            exist = new Product
            {
                Description = item.Description,
                Price = (decimal)item.Price,
                Discount = (decimal)item.Discount,
                UnitSize = item.UnitSize,
                Taxable = item.Taxable
            };
            //stage add in local memory
            _context.Add(exist);
            //do any validation within the entity (validation anotation)
            //send stage request to the database for processing (transaction)
            _context.SaveChanges();
            return exist.ProductID;
        }
        public int UpdateProduct(ProductItem item)
        {
            Product exist = _context.Products
                            .Where(x => x.ProductID == item.ProductId)
                            .FirstOrDefault();
            if (exist == null)
            {
                throw new Exception("Product does not exist on file");
            }
            //setup the entity instance with the data from the view model parameter
            //NOTE: For an update you need the pkey value
            //if the Product was found, then you have a copy of that record (instance)
            //  in your  variable
            //You the pkey, you need to move your rest of the fields into the
            //  appropriate columns

            exist.Description = item.Description;
            exist.Price = (decimal)item.Price;
            exist.Discount = (decimal)item.Discount;
            exist.UnitSize = item.UnitSize;
            exist.Taxable = item.Taxable;

            //stage add in local memory
            EntityEntry<Product> updating = _context.Entry(exist);
            updating.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //do any validation within the entity (validation anotation)
            //send stage request to the database for processing
            //the returned value is the number of rows altered
            return _context.SaveChanges();

        }

        public int DeleteProduct(ProductItem item)
        {
            Product exist = _context.Products
                           .Where(x => x.ProductID == item.ProductId)
                           .FirstOrDefault();
            if (exist == null)
            {
                throw new Exception("Product does not exist on file");
            }
            //setup the entity instance with the data from the view model parameter
            //NOTE: For an update you need the pkey value
            //if the Product was found, then you have a copy of that record (instance)
            //  in your  variable
            //You the pkey, you need to move your rest of the fields into the
            //  appropriate columns

            exist.Description = item.Description;
            exist.Price = (decimal)item.Price;
            exist.Discount = (decimal)item.Discount;
            exist.UnitSize = item.UnitSize;
            exist.Taxable = item.Taxable;

            EntityEntry<Product> deleting = _context.Entry(exist);
            deleting.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            return _context.SaveChanges();
        }
        #endregion
    }
}
