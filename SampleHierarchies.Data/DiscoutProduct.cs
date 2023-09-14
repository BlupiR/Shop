using SampleHierarchies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Data
{
    public class DiscountProduct : IProduct
    {
        public string Name { get; }
        public double Price { get; }
        public int Quantity { get; set; }
        public double Discount { get; }

        public DiscountProduct(string name, double price, int quantity, double discount)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            Discount = discount;
        }
    }
}
