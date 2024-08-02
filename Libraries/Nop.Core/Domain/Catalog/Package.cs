using System;
using Nop.Core.Domain.Common;

namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Represents a package
    /// </summary>
    public partial class Package : BaseEntity, ISoftDeletedEntity
    {
        /// <summary>
        /// Gets or sets the package type identifier
        /// </summary>
        public int PackageTypeId { get; set; }

        /// <summary>
        /// Gets or sets the vendor identifier
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets the package type identifier
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a the song count
        /// </summary>
        public int SongCounts { get; set; }

        /// <summary>
        /// Gets or sets the recording time
        /// </summary>
        public string RecordingTime { get; set; }

        /// <summary>
        /// Gets or sets a the revisions
        /// </summary>
        public int Revisions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the revision allow
        /// </summary>
        public bool AllowRevisions { get; set; }

        /// <summary>
        /// Gets or sets the delivery method identifier
        /// </summary>
        public int DeliveryMethodId { get; set; }

        /// <summary>
        /// Gets or sets a the delivery time select in days
        /// </summary>
        public int DeliveryTimeDays { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time of product creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of product update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the package type
        /// </summary>
        public PackageType PackageType
        {
            get => (PackageType)PackageTypeId;
            set => PackageTypeId = (int)value;
        }

        /// <summary>
        /// Gets or sets the delivery method
        /// </summary>
        public DeliveryMethod DeliveryMethod
        {
            get => (DeliveryMethod)DeliveryMethodId;
            set => DeliveryMethodId = (int)value;
        }
    }
}