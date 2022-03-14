#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional Namespaces
using GroceryStoreSystem.BLL;
using GroceryStoreSystem.ViewModels;
#endregion


namespace WebApp.Pages
{
    public class CRUDProductModel : PageModel
    {
        #region Service variable(s), FeedBack and DI constructor
        private readonly ProductServices _productservices;
        private readonly CatergoryServices _catergoryservices;

        [TempData]
        public string FeedBackMessage { get; set; }

        public string ErrorMessage { get; set; }

        public CRUDProductModel(ProductServices productservices,
            CatergoryServices catergoryservices)
        {
            _productservices = productservices;
            _catergoryservices = catergoryservices;

        }
        #endregion

        #region Form Variables
        [BindProperty(SupportsGet = true)]
        public int? productid { get; set; }

        [BindProperty]
        public List<SelectionList> Catergories { get; set; }

        [BindProperty]
        public ProductItem Product { get; set; }
        #endregion

        public void OnGet()
        {
            Catergories = _catergoryservices.GetAllCatergories();
            if (productid.HasValue && productid > 0)
            {

                Product = _productservices.Products_GetProductById((int)productid);
            }
        }

        public IActionResult OnPostNew()
        {
            try
            {
                //if you did not directly place the select value of your drop down list
                //  into your crud instance the you would have to manually move the value
                //  into your crud instance
                productid = _productservices.AddProduct(Product);
                FeedBackMessage = $"Product ({productid}) has been added";
                //the response to the browser  is a Post Redirect Get pattern 
                return RedirectToPage(new { productid = productid });
            }
            catch (Exception ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                Catergories = _catergoryservices.GetAllCatergories();
                //the response to the browser is the result of Post processing
                //this means the OnGet() will not be executed
                return Page();
            }

        }


        public IActionResult OnPostUpdate()
        {
            try
            {
                if (productid.HasValue)
                {
                    int rowaffected = _productservices.UpdateProduct(Product);
                    if (rowaffected > 0)
                    {
                        FeedBackMessage = "Product has been updated";
                    }
                    else
                    {
                        FeedBackMessage = "No product update. Product does not exist";
                    }
                }
                else
                {
                    FeedBackMessage = "Find an product to maintain before attempting the update";
                }
                //the response to the browser  is a Post Redirect Get pattern 
                return RedirectToPage(new { productid = productid });
            }
            catch (Exception ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                Catergories = _catergoryservices.GetAllCatergories();
                //the response to the browser is the result of Post processing
                //this means the OnGet() will not be executed
                return Page();
            }

        }
        public IActionResult OnPostDelete()
        {
            try
            {
                if (productid.HasValue)
                {
                    int rowaffected = _productservices.DeleteProduct(Product);
                    if (rowaffected > 0)
                    {
                        FeedBackMessage = "Product has been removed";
                    }
                    else
                    {
                        FeedBackMessage = "No product remove. Product does not exist";
                    }
                    //remove your pkey value you are hanging on to for Post Get Redirect
                    productid = null;
                }
                else
                {
                    FeedBackMessage = "Find an product to review before attempting the delete";
                }
                return RedirectToPage(new { productid = productid });
            }
            catch (Exception ex)
            {
                ErrorMessage = GetInnerException(ex).Message;
                Catergories = _catergoryservices.GetAllCatergories();
                return Page();
            }

        }

        public IActionResult OnPostSearch()
        {
            return RedirectToPage("/AlbumsByGenreQuery");
        }

        // this method will drill down into Exceptions to find the InnerException
        // often you may get an error message referring you to the InnerException
        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
            {
                //promote the InnerException to be the Exception
                //this loop will continue until to get to the most InnerException
                //      which is your actual error you wish to see
                ex = ex.InnerException;
            }
            return ex;
        }
    }
}
