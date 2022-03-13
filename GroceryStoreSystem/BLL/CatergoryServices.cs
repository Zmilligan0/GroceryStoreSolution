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
    public class CatergoryServices
    {
        #region Constructor and Context Dependency
        private readonly GrocerylistContext _context;

        internal CatergoryServices(GrocerylistContext context)
        {
            _context = context;
        }
        #endregion
        #region services and queries
        //obtain list of catergories to be used in a select list

        public List<SelectionList> GetAllCatergories() 
        {
            IEnumerable<SelectionList> info = _context.Categories.Select(c => new SelectionList
                                                {
                                                    ValueId = c.CategoryID,
                                                    DisplayText = c.Description
                                                });

            return info.ToList();
            //OrderBy(c => c.DisplayText)
        }
        #endregion
    }
}
