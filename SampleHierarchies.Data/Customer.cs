using SampleHierarchies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Data
{
    public class Customer : ICustomer 
    {
        public string Name { get; }

        public Customer(string name)
        {
            Name = name;      
        }

        public void Buy(IShopService shop, IProduct product, int quantity, double totalPrice, double discount)
        {
            if (shop == null)
            {
                throw new ArgumentNullException(nameof(shop));
            }

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            // Sprawdzanie dostępności produktu w sklepie
            IProduct productInShop = shop.products.Product.FirstOrDefault(p => p.Name == product.Name);

            if (productInShop == null)
            {
                Console.WriteLine($"Product {product.Name} not found in the shop.");
            }
            else
            {
                if (productInShop.Quantity >= quantity)
                {
                    // Zmniejszenie ilości produktów w sklepie
                    productInShop.Quantity -= quantity;

                    Console.WriteLine($"Sold {quantity} of {product.Name} to {Name}");
                    Console.WriteLine($"Total Price: {totalPrice + "$"}");
                    Console.WriteLine($"Discount: {discount}%");
                }
                else
                {
                    Console.WriteLine($"Not enough {product.Name} in stock to sell.");
                }
            }

        }
    }    
}
