using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Caching;
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
        protected readonly IRepository<ProductPackage> _productPackageRepository;
        protected readonly IRepository<Product> _productRepository;
        protected readonly IWorkContext _workContext;
        protected readonly LocalizationSettings _localizationSettings;
        protected readonly IRepository<Vendor> _vendorRepository;
        protected readonly IStoreContext _storeContext;
        protected readonly IStaticCacheManager _staticCacheManager;

        #endregion

        #region Ctor

        public PackageService(IRepository<Package> packageRepository,
            IWorkContext workContext,
            LocalizationSettings localizationSettings,
            IRepository<ProductPackage> packageProductMapping,
            IRepository<Vendor> vendorRepository,
            IRepository<Product> productRepository,
            IStoreContext storeContext,
            IStaticCacheManager staticCacheManager)
        {
            _packageRepository = packageRepository;
            _workContext = workContext;
            _localizationSettings = localizationSettings;
            _productPackageRepository = packageProductMapping;
            _vendorRepository = vendorRepository;
            _productRepository = productRepository;
            _storeContext = storeContext;
            _staticCacheManager = staticCacheManager;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Gets a product category mapping collection
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="storeId">Store identifier (used in multi-store environment). "showHidden" parameter should also be "true"</param>
        /// <param name="showHidden"> A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the product category mapping collection
        /// </returns>
        protected virtual async Task<IList<ProductPackage>> GetProductPackagesByProductIdAsync(int productId, int storeId,
            bool showHidden = false)
        {
            if (productId == 0)
                return new List<ProductPackage>();

            return await _productPackageRepository.GetAllAsync(async query =>
            {
                if (!showHidden)
                {
                    var packagesQuery = _packageRepository.Table.Where(c => !c.Deleted);

                    query = query.Where(pc => packagesQuery.Any(c => !c.Deleted && c.Id == pc.ProductPackageId));
                }

                return query
                    .Where(pc => pc.ProductId == productId);

            }, cache => _staticCacheManager.PrepareKeyForDefaultCache(NopCatalogDefaults.ProductPackagesByProductCacheKey,
                productId, showHidden, storeId));
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
        /// <param name="packageId">Package identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the package
        /// </returns>
        public virtual async Task<Package> GetPackageByIdAsync(int packageId)
        {
            return await _packageRepository.GetByIdAsync(packageId, cache => default);
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
        /// Gets a product package mapping collection
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="showHidden"> A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the product package mapping collection
        /// </returns>
        public virtual async Task<IList<ProductPackage>> GetProductPackagesByProductIdAsync(int productId, bool showHidden = false)
        {
            var store = await _storeContext.GetCurrentStoreAsync();

            return await GetProductPackagesByProductIdAsync(productId, store.Id, showHidden);
        }

        /// <summary>
        /// Gets a product package mapping 
        /// </summary>
        /// <param name="productPackageId">Product package mapping identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the product package mapping
        /// </returns>
        public virtual async Task<ProductPackage> GetProductPackageByIdAsync(int productPackageId)
        {
            return await _productPackageRepository.GetByIdAsync(productPackageId, cache => default);
        }

        /// <summary>
        /// Inserts a product package mapping
        /// </summary>
        /// <param name="productPackage">>Product package mapping</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertProductPackageAsync(ProductPackage productPackage)
        {
            await _productPackageRepository.InsertAsync(productPackage);
        }

        /// <summary>
        /// Updates the product package mapping 
        /// </summary>
        /// <param name="productPackage">>Product package mapping</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateProductPackageAsync(ProductPackage productPackage)
        {
            await _productPackageRepository.UpdateAsync(productPackage);
        }

        /// <summary>
        /// Deletes a product package mapping
        /// </summary>
        /// <param name="productPackage">Product category</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteProductPackageAsync(ProductPackage productPackage)
        {
            await _productPackageRepository.DeleteAsync(productPackage);
        }

        /// <summary>
        /// Returns a ProductCategory that has the specified values
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="productId">Product identifier</param>
        /// <param name="packageId">Package identifier</param>
        /// <returns>A ProductPackage that has the specified values; otherwise null</returns>
        public virtual ProductPackage FindProductPackage(IList<ProductPackage> source, int productId, int packageId)
        {
            foreach (var productPackage in source)
                if (productPackage.ProductId == productId && productPackage.ProductPackageId == packageId)
                    return productPackage;

            return null;
        }

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
        public virtual async Task<IPagedList<ProductPackage>> GetProductPackagesByPackageIdAsync(int packageId,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            if (packageId == 0)
                return new PagedList<ProductPackage>(new List<ProductPackage>(), pageIndex, pageSize);

            var query = from pc in _productPackageRepository.Table
                        join p in _productRepository.Table on pc.ProductId equals p.Id
                        where pc.ProductPackageId == packageId && !p.Deleted
                        orderby pc.Id
                        select pc;

            if (!showHidden)
            {
                var packagesQuery = _packageRepository.Table.Where(c => !c.Deleted);

                query = query.Where(pc => packagesQuery.Any(c => c.Id == pc.ProductPackageId));
            }

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }

        #endregion

        #endregion
    }
}
