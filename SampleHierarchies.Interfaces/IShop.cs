using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Interfaces
{
    public interface IShop
    {
        List<IProduct> Product { get; set; }
        void AddProduct(IProduct product);
        void ShowProducts();
        void SellProduct(IProduct product, IShopService shopService, ICustomer customer);
    }
}
