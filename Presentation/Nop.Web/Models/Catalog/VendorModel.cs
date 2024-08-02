using System.Collections.Generic;
using Nop.Core.Domain.Vendors;
using Nop.Web.Areas.Admin.Models.Vendors;
using Nop.Web.Framework.Models;
using Nop.Web.Models.Media;
using Nop.Web.Models.Package;

namespace Nop.Web.Models.Catalog
{
    public partial record VendorModel : BaseNopEntityModel
    {
        public VendorModel()
        {
            PictureModel = new PictureModel();
            PackageModel = new List<PackageModel>();
            VendorPictureModel = new List<VendorPictureModel>();
            CatalogProductsModel = new CatalogProductsModel();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string SeName { get; set; }
        public bool AllowCustomersToContactVendors { get; set; }
        public string Categories { get; set; }

        public PictureModel PictureModel { get; set; }
        public IList<PackageModel> PackageModel { get; set; }
        public IList<VendorPictureModel> VendorPictureModel { get; set; }

        public CatalogProductsModel CatalogProductsModel { get; set; }
    }
}