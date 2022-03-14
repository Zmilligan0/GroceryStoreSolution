#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations;
#endregion

namespace GroceryStoreSystem.ViewModels
{
	public class ProductItem
	{
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(160, ErrorMessage = "Description is limited to 160 characters")]
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int CatergoryId { get; set; }
        public string UnitSize { get; set; }
        public bool Taxable { get; set; }
    }
}