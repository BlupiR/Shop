using Newtonsoft.Json.Linq;
using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces;
using SampleHierarchies.Services;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;

namespace SampleHierarchies.Gui
{
    public sealed class MainScreen : Screen
    {

        private ShopScreen _shopScreen;

        private IShopService _shopService;

        public MainScreen(ShopScreen shopScreen, IShopService shopService)
        {
            _shopScreen = shopScreen;
            _shopService = shopService;
        }
        public override void Show()
        {

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Your available choices are:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Shop");
                Console.WriteLine("2. Read from file");
                Console.WriteLine("3. Save to file");
                Console.Write("Please enter your choice: ");

                string? choiceAsString = Console.ReadLine();

                // Validate choice
                try
                {
                    if (choiceAsString is null)
                    {
                        throw new ArgumentNullException(nameof(choiceAsString));
                    }

                    MainScreenChoices choice = (MainScreenChoices)Int32.Parse(choiceAsString);
                    switch (choice)
                    {
                        case MainScreenChoices.Shop:
                            _shopScreen.Show();
                            break;
                        case MainScreenChoices.ReadData:
                            ReadFromFile();
                            break;
                        case MainScreenChoices.WriteData:
                            SaveToFile();
                            break;
                        case MainScreenChoices.Exit:
                            Console.WriteLine("Goodbye.");
                            return;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
            }

        }

        /// <summary>
        /// Read data from file.
        /// </summary>
        private void ReadFromFile()
        {
            try
            {
                Console.Write("Read data from file: ");
                var fileName = Console.ReadLine();
                if (fileName is null)
                {
                    throw new ArgumentNullException(nameof(fileName));
                }
                _shopService.Write(fileName);
                Console.WriteLine("Data reading from: '{0}' was successful.", fileName);
            }
            catch
            {
                Console.WriteLine("Data reading from was not successful.");
            }
        }

        /// <summary>
        /// Save to file.
        /// </summary>
        private void SaveToFile()
        {
            try
            {
                Console.Write("Save data to file: ");
                var fileName = Console.ReadLine();
                if (fileName is null)
                {
                    throw new ArgumentNullException(nameof(fileName));
                }
                _shopService.Write(fileName);
                Console.WriteLine("Data saving to: '{0}' was successful.", fileName);
            }
            catch
            {
                Console.WriteLine("Data saving was not successful.");
            }

        }
    }
}

