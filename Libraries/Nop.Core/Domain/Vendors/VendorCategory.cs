namespace Nop.Core.Domain.Vendors
{
    /// <summary>
    /// Represents a vendor category mapping
    /// </summary>
    public partial class VendorCategory : BaseEntity
    {
        /// <summary>
        /// Gets or sets the vendor identifier
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets the category identifier
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}
