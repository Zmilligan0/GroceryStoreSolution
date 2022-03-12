using GroceryStoreSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreSystem.BLL
{
    public class AboutServices
    {
        #region Constructor and Context Dependency
        private readonly GrocerylistContext _context;

        internal AboutServices(GrocerylistContext context)
        {
            _context = context;
        }
        #endregion

        #region Services

        //Query to obtain DbVersion data
        

        #endregion
    }
}
