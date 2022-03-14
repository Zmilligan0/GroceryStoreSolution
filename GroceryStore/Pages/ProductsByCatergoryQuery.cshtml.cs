#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using GroceryStoreSystem.BLL;
using GroceryStoreSystem.ViewModels;
using WebApp.Helpers;

namespace GroceryStore.Pages
{
    public class ProductsByCatergoryQueryModel : PageModel
    {
        #region varaibles and DI construction
        private readonly ILogger<IndexModel> _logger;
        private readonly ProductServices _productServices;
        private readonly CatergoryServices _catergoryServices;

        public ProductsByCatergoryQueryModel(ILogger<IndexModel> logger,
            ProductServices productServices,
            CatergoryServices catergoryServices)
        {
            _logger = logger;
            _productServices = productServices;
            _catergoryServices = catergoryServices;
        }
        #endregion
        #region Feedback and Errorhandling
        [TempData]
        public string FeedBack { get; set; }
        public bool hasFeedBack => !string.IsNullOrWhiteSpace(FeedBack);

        [TempData]
        public string ErrorMsg { get; set; }
        public bool hasErrorMsg => !string.IsNullOrWhiteSpace(ErrorMsg);
        #endregion
        [BindProperty]
        public List<SelectionList> CatergoryList { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CatergoryId { get; set; }

        [BindProperty]
        public List<ProductsListBy> ProductsByCatergory { get; set; }

        #region Paginator
        private const int PAGE_SIZE = 10;
        public Paginator Pager { get; set; }



        #endregion

        public void OnGet(int? currentPage)
        {
            CatergoryList = _catergoryServices.GetAllCatergories();
            //sorting
            CatergoryList.Sort((x,y) => x.DisplayText.CompareTo(y.DisplayText));


            if (CatergoryId.HasValue && CatergoryId.Value > 0)
            {

                //setup paginator
                int pageNumber = currentPage.HasValue ? currentPage.Value : 1;

                PageState current = new PageState(pageNumber, PAGE_SIZE);

                int totalrows = 0;


                ProductsByCatergory = _productServices.ProductsByCatergory((int)CatergoryId,
                    pageNumber, PAGE_SIZE, out totalrows);

                Pager = new Paginator(totalrows, current);
            }
        }
            

        public IActionResult OnPost()
        {
            if (CatergoryId == 0)
            {
                FeedBack = "You did not select a catergory";
            }
            else
            {
                FeedBack = $"You selected catergory id of {CatergoryId}";
            }
            return RedirectToPage(new { CatergoryId = CatergoryId});
        }
    }
}
