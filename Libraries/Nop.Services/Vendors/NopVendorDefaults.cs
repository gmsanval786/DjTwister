using Nop.Core.Caching;

namespace Nop.Services.Vendors
{
    /// <summary>
    /// Represents default values related to vendor services
    /// </summary>
    public static partial class NopVendorDefaults
    {
        /// <summary>
        /// Gets a generic attribute key to store vendor additional info
        /// </summary>
        public static string VendorAttributes => "VendorAttributes";

        /// <summary>
        /// Gets default prefix for vendor
        /// </summary>
        public static string VendorAttributePrefix => "vendor_attribute_";

        #region Caching defaults

        /// <summary>
        /// Gets a key for caching vendor attribute values of the vendor attribute
        /// </summary>
        /// <remarks>
        /// {0} : vendor attribute ID
        /// </remarks>
        public static CacheKey VendorAttributeValuesByAttributeCacheKey => new("Nop.vendorattributevalue.byattribute.{0}");

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : customer roles ID hash
        /// {1} : current store ID
        /// {2} : categories ID hash
        /// </remarks>
        public static CacheKey CategoryVendorsNumberCacheKey => new("Nop.vendorcategory.vendors.number.{0}", CategoryVendorsNumberPrefix);

        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        public static string CategoryVendorsNumberPrefix => "Nop.vendorcategory.vendors.number.";

        #endregion
    }
}