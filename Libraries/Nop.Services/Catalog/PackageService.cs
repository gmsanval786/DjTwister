using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Localization;
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

        #endregion

        #region Ctor

        public PackageService(IRepository<Package> packageRepository,
            IWorkContext workContext,
            LocalizationSettings localizationSettings,
            IRepository<PackageProductMapping> packageProductMapping)
        {
            _packageRepository = packageRepository;
            _workContext = workContext;
            _localizationSettings = localizationSettings;
            _packageProductMapping = packageProductMapping;
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
        public virtual async Task<IPagedList<Package>> GetAllPackagesAsync(int vendorId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var vendors = await _packageRepository.GetAllPagedAsync(query =>
            {
                if (vendorId > 0)
                    query = query.Where(p => p.VendorId == vendorId);

                query = query.Where(p => !p.Deleted);
                query = query.OrderBy(p => p.PackageTypeId);

                return query;
            }, pageIndex, pageSize);

            return vendors;
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
