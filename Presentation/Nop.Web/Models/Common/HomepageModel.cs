using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Models.Catalog;

namespace Nop.Web.Models.Common
{
    public partial record HomepageModel : BaseNopModel
    {
        public HomepageModel()
        {
            AvailableCategories = new List<SelectListItem>();
            Vendors = new List<VendorModel>();
        }

        [NopResourceDisplayName("Account.Homepage.Fields.Genres.Choose")]
        public int CategoryId { get; set; }
        public IList<SelectListItem> AvailableCategories { get; set; }
        public IList<VendorModel> Vendors { get; set; }

    }
}
