using Nop.Core.Domain.Vendors;
using Nop.Services.Caching;

namespace Nop.Services.Vendors.Caching
{
    /// <summary>
    /// Represents a vendor picture mapping cache event consumer
    /// </summary>
    public partial class VendorPictureCacheEventConsumer : CacheEventConsumer<VendorPicture>
    {
    }
}
