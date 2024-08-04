using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Vendors;
using Nop.Data;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Package service
    /// </summary>
    public partial class PackageService : IPackageService
    {
        #region Fields

        protected readonly IRepository<Package> _packageRepository;
        protected readonly IRepository<PackageProductMapping> _packageProductMapping;
        protected readonly IWorkContext _workContext;
        protected readonly LocalizationSettings _localizationSettings;
        protected readonly IRepository<Vendor> _vendorRepository;

        #endregion

        #region Ctor

        public PackageService(IRepository<Package> packageRepository,
            IWorkContext workContext,
            LocalizationSettings localizationSettings,
            IRepository<PackageProductMapping> packageProductMapping,
            IRepository<Vendor> vendorRepository)
        {
            _packageRepository = packageRepository;
            _workContext = workContext;
            _localizationSettings = localizationSettings;
            _packageProductMapping = packageProductMapping;
            _vendorRepository = vendorRepository;
        }

        #endregion

        #region Methods

        #region Products

        /// <summary>
        /// Delete a package
        /// </summary>
        /// <param name="package">Package</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeletePackageAsync(Package package)
        {
            await _packageRepository.DeleteAsync(package);
        }

        /// <summary>
        /// Delete packages
        /// </summary>
        /// <param name="packages">Packages</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeletePackagesAsync(IList<Package> packages)
        {
            await _packageRepository.DeleteAsync(packages);
        }

        /// <summary>
        /// Gets package
        /// </summary>
        /// <param name="productId">Package identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the package
        /// </returns>
        public virtual async Task<Package> GetPackageByIdAsync(int productId)
        {
            return await _packageRepository.GetByIdAsync(productId, cache => default);
        }

        /// <summary>
        /// Gets packages by identifier
        /// </summary>
        /// <param name="packageIds">Package identifiers</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the packages
        /// </returns>s
        public virtual async Task<IList<Package>> GetPackagesByIdsAsync(int[] packageIds)
        {
            return await _packageRepository.GetByIdsAsync(packageIds, cache => default, false);
        }

        /// <summary>
        /// Inserts a package
        /// </summary>
        /// <param name="package">Package</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertPackageAsync(Package package)
        {
            await _packageRepository.InsertAsync(package);
        }

        /// <summary>
        /// Updates the package
        /// </summary>
        /// <param name="package">Package</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdatePackageAsync(Package package)
        {
            await _packageRepository.UpdateAsync(package);
        }

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
        public virtual async Task<IPagedList<Package>> GetAllPackagesAsync(int vendorId = 0, int packageTypeId = 0, int storeId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var packages = await _packageRepository.GetAllPagedAsync(query =>
            {
                query = from p in query
                        join v in _vendorRepository.Table on p.VendorId equals v.Id into pv
                        from v in pv.DefaultIfEmpty()
                        where (vendorId == 0 || p.VendorId == vendorId)
                           && (packageTypeId == 0 || p.PackageTypeId == packageTypeId)
                           && (v == null || (!v.Deleted && v.Active))
                           && !p.Deleted
                        orderby p.PackageTypeId
                        select p;

                return query;
            }, pageIndex, pageSize);

            return packages;
        }

        #endregion

        #region Package product

        /// <summary>
        /// Add a package product mapping
        /// </summary>
        /// <param name="packProductMapping">Package-product mapping</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task AddPackageProductMappingAsync(PackageProductMapping packProductMapping)
        {
            await _packageProductMapping.InsertAsync(packProductMapping);
        }

        #endregion

        #endregion
    }
}
