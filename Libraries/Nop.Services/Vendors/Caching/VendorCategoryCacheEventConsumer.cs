using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;
using Nop.Services.Caching;

namespace Nop.Services.Vendors.Caching
{
    /// <summary>
    /// Represents a vendor category cache event consumer
    /// </summary>
    public partial class VendorCategoryCacheEventConsumer : CacheEventConsumer<VendorCategory>
    {
        /// <summary>
        /// Clear cache data
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected override async Task ClearCacheAsync(VendorCategory entity)
        {
            await RemoveByPrefixAsync(NopVendorDefaults.CategoryVendorsNumberPrefix, entity.VendorId);
        }
    }
}
