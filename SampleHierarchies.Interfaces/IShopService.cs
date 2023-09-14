using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Interfaces
{
    public interface IShopService
    {
        #region Interface Members

        /// <summary>
        /// Products collection.
        /// </summary>
        IShop? products { get; set; }

        /// <summary>
        /// Reads products from a json path.
        /// </summary>
        /// <param name="jsonPath"></param>
        /// <returns>True if success, false otherwise</returns>
        bool Read(string jsonPath);

        /// <summary>
        /// Writes products to a json path.
        /// </summary>
        /// <param name="jsonPath"></param>
        /// <returns>True if success, false otherwise</returns>
        bool Write(string jsonPath);

        #endregion // Interface Members
    }
}
