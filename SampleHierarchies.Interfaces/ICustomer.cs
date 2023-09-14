using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Interfaces
{
    public interface ICustomer
    {
        string Name { get; }
        void Buy(IShopService shop, IProduct product, int quantity, double totalPrice, double discount);
    }
}
