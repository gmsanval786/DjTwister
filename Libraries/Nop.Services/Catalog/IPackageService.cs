﻿using System;
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
        /// Gets package
        /// </summary>
        /// <param name="productId">Package identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the package
        /// </returns>
        Task<Package> GetPackageByIdAsync(int productId);

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
        Task<IPagedList<Package>> GetAllPackagesAsync(int vendorId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        #endregion

        #region Package product

        /// <summary>
        /// Add a package product mapping
        /// </summary>
        /// <param name="packProductMapping">Package-product mapping</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task AddPackageProductMappingAsync(PackageProductMapping packProductMapping);

        #endregion
    }
}