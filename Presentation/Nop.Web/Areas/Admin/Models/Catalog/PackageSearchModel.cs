using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Areas.Admin.Models.Common;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.Catalog
{
    /// <summary>
    /// Represents a package search model
    /// </summary>
    public partial record PackageSearchModel : BaseSearchModel
    {
        #region Ctor

        public PackageSearchModel()
        {
            AvailableStores = new List<SelectListItem>();
            AvailablePackageTypes = new List<SelectListItem>();
            AvailableVendors = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [NopResourceDisplayName("Admin.Catalog.Packages.List.SearchVendor")]
        public int SearchVendorId { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.List.SearchStore")]
        public int SearchStoreId { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Packages.List.SearchPackageType")]
        public int SearchPackageTypeId { get; set; }

        public bool HideStoresList { get; set; }

        public IList<SelectListItem> AvailableStores { get; set; }
        public IList<SelectListItem> AvailablePackageTypes { get; set; }
        public IList<SelectListItem> AvailableVendors { get; set; }

        #endregion
    }
}