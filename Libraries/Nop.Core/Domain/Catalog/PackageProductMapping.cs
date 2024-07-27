using Nop.Core.Domain.Localization;

namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Represents a product attribute mapping
    /// </summary>
    public partial class PackageProductMapping : BaseEntity, ILocalizedEntity
    {
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product attribute identifier
        /// </summary>
        public int ProductPackageId { get; set; }
    }
}