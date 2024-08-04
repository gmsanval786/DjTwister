using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Areas.Admin.Models.Settings;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Catalog
{
    /// <summary>
    /// Represents a package model
    /// </summary>
    public partial record PackageModel : BaseNopEntityModel, IStoreMappingSupportedModel
    {
        #region Ctor

        public PackageModel()
        {
            Locales = new List<PackageLocalizedModel>();

            SelectedStoreIds = new List<int>();
            AvailableStores = new List<SelectListItem>();
            AvailableVendors = new List<SelectListItem>();
            AvailablePackageTypes = new List<SelectListItem>();
            AvailableSongCounts = new List<SelectListItem>();
            AvailableRevisions = new List<SelectListItem>();
            AvailableDeliveryMethods = new List<SelectListItem>();
            AvailableDeliveryDays = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.Vendor")]
        public int VendorId { get; set; }
        public IList<SelectListItem> AvailableVendors { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.VendorName")]
        public string VendorName { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.PackageType")]
        public int PackageTypeId { get; set; }
        public IList<SelectListItem> AvailablePackageTypes { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.PackgeTypeName")]
        public string PackageTypeName { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.Description")]
        public string Description { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.RecordingTime")]
        public string RecordingTime { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.SongCount")]
        public int SongCounts { get; set; }
        public IList<SelectListItem> AvailableSongCounts { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.Price")]
        public decimal Price { get; set; }
        public string PriceVal { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.DeliveryMethod")]
        public int DeliveryMethodId { get; set; }
        public IList<SelectListItem> AvailableDeliveryMethods { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.DeliveryMethodName")]
        public string DeliveryMethod { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.DeliveryDay")]
        public int DeliveryTimeDays { get; set; }
        public IList<SelectListItem> AvailableDeliveryDays { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.RevisionAllowed")]
        public bool AllowRevisions { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.Revision")]
        public int? Revisions { get; set; }
        public IList<SelectListItem> AvailableRevisions { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.ShowOnHomepage")]
        public bool ShowOnHomepage { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.MetaKeywords")]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.MetaDescription")]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.MetaTitle")]
        public string MetaTitle { get; set; }

        public int Id { get; set; }
        public string Package { get; set; }

        public IList<PackageLocalizedModel> Locales { get; set; }

        //store mapping
        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.LimitedToStores")]
        public IList<int> SelectedStoreIds { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }

        #endregion
    }

    public partial record PackageTypeModel 
    {
        public PackageTypeModel()
        {
            BasicPackage = new List<PackageModel>();
            StandardPackage = new List<PackageModel>();
            PremiumPackage = new List<PackageModel>();
        }

        public IList<PackageModel> BasicPackage { get; set; }
        public IList<PackageModel> StandardPackage { get; set; }
        public IList<PackageModel> PremiumPackage { get; set; }
    }
    public partial record PackageLocalizedModel : ILocalizedLocaleModel
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.ShortDescription")]
        public string Description { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.RecordingTime")]
        public string RecordingTime { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.MetaKeywords")]
        public string MetaKeywords { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.MetaDescription")]
        public string MetaDescription { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.Fields.MetaTitle")]
        public string MetaTitle { get; set; }
    }
}