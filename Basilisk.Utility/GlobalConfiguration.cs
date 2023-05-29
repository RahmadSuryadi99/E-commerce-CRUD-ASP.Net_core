using Basilisk.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Utility
{
    public class GlobalConfiguration
    {
        public static string _passowrdDefault = "indocyber";
        public static IEnumerable<MenuViewModel> GetMenuByRole(string roleName)
        {
            var data = new List<MenuViewModel>()
            {
                new MenuViewModel{Role="Administrator",Controller="Account",Action="Index",Title="Account" },
                new MenuViewModel{Role="Administrator",Controller="Category",Action="Index",Title="Category" },
                new MenuViewModel{Role="Administrator",Controller="Supplier",Action="Index",Title="Supplier" },
                new MenuViewModel{Role="Administrator",Controller="ProdSupCat",Action="Index",Title="Product" },
                new MenuViewModel{Role="Administrator",Controller="Delivery",Action="Index",Title="Delivery" },
                new MenuViewModel{Role="Administrator",Controller="Salesman",Action="Index",Title="Salesman" },
                new MenuViewModel{Role="Administrator",Controller="Region",Action="Index",Title="Region" },
                new MenuViewModel{Role="Administrator",Controller="Order",Action="Index",Title="Order" },
                new MenuViewModel{Role="Administrator",Controller="Customer",Action="Index",Title="Customer" },

                new MenuViewModel{Role="Salesman",Controller="Order",Action="Index",Title="Order" },
                new MenuViewModel{Role="Salesman",Controller="ProdSupCat",Action="Index",Title="Product" },

                new MenuViewModel{Role="Customer",Controller="Customer",Action="CheckCart",Title="Keranjang" },
                new MenuViewModel{Role="Customer",Controller="Customer",Action="ShowOrders",Title="Pesanan" },

            };
            return data;
        }
    }
}
