using SampleHierarchies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.Design;

namespace SampleHierarchies.Data
{
    public class Shop : IShop
    {
        public List<IProduct> Product { get; set; }

    
        public Shop()
        {
            Product = new List<IProduct>();
        }
        //This method allows you to add products
        public void AddProduct(IProduct product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            Product.Add(product);
            Console.WriteLine($"Added product: {product.Name}");
        }
        //This method allows you to add products with discount
        public void AddDiscountedProduct(DiscountProduct product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            Product.Add(product);
            Console.WriteLine($"Added discounted product: {product.Name}");
        }

        public void ShowProducts()
        {
            Console.WriteLine("Available Products:");

            foreach (IProduct product in Product)
            {
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Price: {product.Price:C}");
                Console.WriteLine($"Quantity: {product.Quantity}");

                if (product is DiscountProduct discountProduct)
                {
                    Console.WriteLine($"Discount: {discountProduct.Discount}%");
                }

                Console.WriteLine(); // Pusty wiersz oddzielający informacje o produkcie.
            }
        }

        public void SellProduct(IProduct product, IShopService _shopService, ICustomer customer)
        {
            if (product == null)
            {
                Console.WriteLine("Invalid product.");
                return;
            }

            if (customer == null)
            {
                Console.WriteLine("Invalid customer.");
                return;
            }

            // Wyszukaj produkt w sklepie
            IProduct productInShop = _shopService.products.Product.FirstOrDefault(p => p.Name == product.Name);

            if (productInShop == null)
            {
                Console.WriteLine($"Product {product.Name} not found in the shop.");
                return;
            }

            if (productInShop.Quantity > 0)
            {
                // Zmniejsz ilość produktu w sklepie o 1 (zakładając, że sprzedaje się po jednym produkcie na raz)
                productInShop.Quantity--;

                // Logika biznesowa sprzedaży produktu klientowi
                // Tutaj możesz obliczyć całkowity koszt, rabat i wykonać inne działania, aby sprzedać produkt klientowi.

                Console.WriteLine($"Sold {product.Name} to {customer.Name}");
            }
            else
            {
                Console.WriteLine($"Not enough {product.Name} in stock to sell.");
            }
        }
    }
}

