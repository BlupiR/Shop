using Newtonsoft.Json.Linq;
using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces;
using System.Collections.Generic;
using SampleHierarchies.Services;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;
using System.Xml.Linq;

namespace SampleHierarchies.Gui
{
    public sealed class ShopScreen : Screen
    {
        private IShopService _shopService;
        private IShop _shop;
        public ShopScreen(IShop shop, IShopService shopService)
        {

            _shopService = shopService;

        }

        public override void Show()
        {

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Your available choices are:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Add product");
                Console.WriteLine("2. Add discounted product");
                Console.WriteLine("3. List of products");
                Console.WriteLine("4. Sell product");
                Console.Write("Please enter your choice: ");

                string? choiceAsString = Console.ReadLine();

                // Validate choice
                try
                {
                    if (choiceAsString is null)
                    {
                        throw new ArgumentNullException(nameof(choiceAsString));
                    }

                    ShopScreenChoices choice = (ShopScreenChoices)Int32.Parse(choiceAsString);
                    switch (choice)
                    {
                        case ShopScreenChoices.AddProduct:
                            AddProduct();
                            break;
                        case ShopScreenChoices.AddDiscounted:
                            AddDiscountedProduct();
                            break;
                        case ShopScreenChoices.ShowProducts:
                            ShowingProducts();
                            break;
                        case ShopScreenChoices.SellProduct:
                            SellProduct();
                            break;
                        case ShopScreenChoices.Exit:
                            Console.WriteLine("Going to parent menu.");
                            return;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
            }

        }
        private void AddDiscountedProduct()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Console.Write("Enter product price: ");
            double price = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter product quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter discount percentage (e.g., 10 for 10% discount): ");
            double discount = Convert.ToDouble(Console.ReadLine());

            DiscountProduct newDiscountedProduct = new DiscountProduct(name, price, quantity, discount);

            _shopService?.products?.Product.Add(newDiscountedProduct);

            Console.WriteLine("Discounted product added successfully.");
        }
        private void AddProduct()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Console.Write("Enter product price: ");
            double price = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter product quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Product newProduct = new Product(name, price, quantity);

            _shopService?.products?.Product.Add(newProduct);

            Console.WriteLine("Product added successfully.");
        }
        private void ShowingProducts()
        {
            Console.WriteLine();
            if (_shopService?.products?.Product is not null && _shopService.products.Product.Count > 0)
            {
                Console.WriteLine("Available Products:");
                foreach (IProduct product in _shopService.products.Product)
                {
                    if (product.Quantity > 0)
                    {
                        if (product is DiscountProduct discountProduct)
                        {
                            Console.WriteLine($"Products with discount:");
                            Console.WriteLine($"{discountProduct.Name} - Price: {discountProduct.Price + "$"}, Quantity: {discountProduct.Quantity}, Discount: {discountProduct.Discount}%");
                        }
                        else
                        {
                            Console.WriteLine($"Products:");
                            Console.WriteLine($"{product.Name} - Price: {product.Price + "$"}, Quantity: {product.Quantity}");
                        }
                    }
                }
            }
        }
        private void SellProduct()
        {
            try
            {
                Console.Write("What is the name of the customer? ");
                string customerName = Console.ReadLine();
                Console.WriteLine("Enter product name to sell: ");
                string productName = Console.ReadLine();
                Console.WriteLine("Enter quantity to sell: ");
                int quantityToSell = Convert.ToInt32(Console.ReadLine());

                if (customerName is null || productName is null)
                {
                    throw new ArgumentNullException("Customer name and product name cannot be empty.");
                }
                productName = productName.ToLower();

                // Проверяем наличие продукта в магазине
                IProduct product = _shopService?.products?.Product.FirstOrDefault(p => p != null && p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
                if (product != null)
                {
                    // Создаем клиента
                    ICustomer customer = new Customer(customerName);

                    // Расчет итоговой стоимости и скидки (если продукт со скидкой)
                    double totalPrice;
                    double discount = 0; // Скидка по умолчанию

                    if (product is DiscountProduct discountProduct)
                    {
                        discount = discountProduct.Discount; // Получить скидку для продукта со скидкой
                        totalPrice = discountProduct.Price * quantityToSell * (1 - discount / 100);
                    }
                    else
                    {
                        totalPrice = product.Price * quantityToSell;
                    }

                    // Вызываем метод Buy у клиента, передавая итоговую стоимость и информацию о скидке
                    customer.Buy(_shopService, product, quantityToSell, totalPrice, discount);
                }
                else
                {
                    Console.WriteLine($"Product {productName} not found in the shop.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input. Try again.");
            }

        }
    }
}

