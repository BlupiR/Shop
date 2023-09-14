using Newtonsoft.Json;
using SampleHierarchies.Data;
using SampleHierarchies.Interfaces;
using SampleHierarchies.Enums;
using System.Diagnostics;

namespace SampleHierarchies.Services;

/// <summary>
/// Shop service.
/// </summary>
/// 
public class ShopService : IShopService
{


    #region IDataService Implementation

    /// <inheritdoc/>
    public IShop? products { get; set; }



    /// <inheritdoc/>
    public bool Read(string jsonPath)
    {
        bool result = true;

        try
        {
            string jsonContent = File.ReadAllText(jsonPath);
            var jsonSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            products = JsonConvert.DeserializeObject<Shop>(jsonContent, jsonSettings);
            if (products is null)
            {
                result = false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            result = false;
        }

        return result;
    }

    /// <inheritdoc/>
    public bool Write(string jsonPath)
    {
        bool result = true;

        try
        {
            var jsonSettings = new JsonSerializerSettings();
            string jsonContent = JsonConvert.SerializeObject(products);
            string jsonContentFormatted = jsonContent.FormatJson();
            File.WriteAllText(jsonPath, jsonContentFormatted);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            result = false;
        }

        return result;
    }




    #endregion // IDataService Implementation

    #region Ctors

    /// <summary>
    /// Default ctor.
    /// </summary>
    public ShopService()
    {
        products = new Shop();
    }
}
    #endregion // Ctors


