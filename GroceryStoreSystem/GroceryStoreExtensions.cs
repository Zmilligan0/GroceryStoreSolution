using GroceryStoreSystem.BLL;
using GroceryStoreSystem.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreSystem
{
    public static class GroceryStoreExtensions
    {
        public static void GroceryStoreSystemBackendDependencies(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            //register dbcontext
            services.AddDbContext<GrocerylistContext>(options);

            //add services with .AddTransient
            services.AddTransient<CatergoryServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<GrocerylistContext>();

                return new CatergoryServices(context);
            });
            services.AddTransient<ProductServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetRequiredService<GrocerylistContext>();

                return new ProductServices(context);
            });
        }
    }
}
