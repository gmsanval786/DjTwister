using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;
using Nop.Services.Caching;

namespace Nop.Services.Catalog.Caching
{
    /// <summary>
    /// Represents a product package cache event consumer
    /// </summary>
    public partial class ProductPackageCacheEventConsumer : CacheEventConsumer<ProductPackage>
    {
        /// <summary>
        /// Clear cache data
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected override async Task ClearCacheAsync(ProductPackage entity)
        {
            await RemoveByPrefixAsync(NopCatalogDefaults.ProductPackagesByProductPrefix, entity.ProductId);
        }
    }
}
