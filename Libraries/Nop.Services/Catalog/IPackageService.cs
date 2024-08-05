using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Stores;
using Nop.Core.Domain.Vendors;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Package service
    /// </summary>
    public partial interface IPackageService
    {
        #region Packages

        /// <summary>
        /// Delete a package
        /// </summary>
        /// <param name="package">Package</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeletePackageAsync(Package package);

        /// <summary>
        /// Delete packages
        /// </summary>
        /// <param name="packages">Packages</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeletePackagesAsync(IList<Package> packages);

        /// <summary>
        /// Gets package
        /// </summary>
        /// <param name="productId">Package identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the package
        /// </returns>
        Task<Package> GetPackageByIdAsync(int productId);

        /// <summary>
        /// Gets packages by identifier
        /// </summary>
        /// <param name="packageIds">Package identifiers</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the packages
        /// </returns>
        Task<IList<Package>> GetPackagesByIdsAsync(int[] packageIds);

        /// <summary>
        /// Inserts a package
        /// </summary>
        /// <param name="package">Package</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertPackageAsync(Package package);

        /// <summary>
        /// Updates the package
        /// </summary>
        /// <param name="package">Package</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdatePackageAsync(Package package);

        /// <summary>
        /// Gets all packages
        /// </summary>
        /// <param name="vendorId">Vendor identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the packages
        /// </returns>
        Task<IPagedList<Package>> GetAllPackagesAsync(int vendorId = 0, int packageTypeId = 0, int storeId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        #endregion

        #region Package product


        /// <summary>
        /// Gets a product package mapping collection
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the product package mapping collection
        /// </returns>
        Task<IList<ProductPackage>> GetProductPackagesByProductIdAsync(int productId, bool showHidden = false);

        /// <summary>
        /// Gets a product package mapping 
        /// </summary>
        /// <param name="v">Product package mapping identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the product package mapping
        /// </returns>
        Task<ProductPackage> GetProductPackageByIdAsync(int productPackageId);

        /// <summary>
        /// Inserts a product package mapping
        /// </summary>
        /// <param name="productPackage">>Product package mapping</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertProductPackageAsync(ProductPackage productPackage);

        /// <summary>
        /// Updates the product package mapping 
        /// </summary>
        /// <param name="productPackage">>Product package mapping</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdateProductPackageAsync(ProductPackage productPackage);

        /// <summary>
        /// Deletes a product package mapping
        /// </summary>
        /// <param name="productPackage">Product category</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteProductPackageAsync(ProductPackage productPackage);

        /// <summary>
        /// Returns a ProductPackage that has the specified values
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="productId">Product identifier</param>
        /// <param name="packageId">Package identifier</param>
        /// <returns>A ProductPackage that has the specified values; otherwise null</returns>
        ProductPackage FindProductPackage(IList<ProductPackage> source, int productId, int packageId);

        /// <summary>
        /// Gets product package mapping collection
        /// </summary>
        /// <param name="packageId">Package identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the product a package mapping collection
        /// </returns>
        Task<IPagedList<ProductPackage>> GetProductPackagesByPackageIdAsync(int packageId,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        #endregion
    }
}