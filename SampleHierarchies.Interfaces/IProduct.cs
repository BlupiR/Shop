using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Interfaces
{
    public interface IProduct
    {
        string Name { get; }
        double Price { get; }
        int Quantity { get; set; }
    }
}
