#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreSystem.ViewModels
{
    public class ProductsListBy
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double SalePrice => Price - Discount;
        public string UnitSize { get; set; }
        public bool Taxable { get; set; }
    }
}
