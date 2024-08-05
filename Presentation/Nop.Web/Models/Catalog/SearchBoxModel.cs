using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Models.Catalog
{
    public partial record SearchBoxModel : BaseNopModel
    {
        public SearchBoxModel()
        {
            AvailableCategories = new List<SelectListItem>();
        }
        public bool AutoCompleteEnabled { get; set; }
        public bool ShowProductImagesInSearchAutoComplete { get; set; }
        public int SearchTermMinimumLength { get; set; }
        public bool ShowSearchBox { get; set; }

        [NopResourceDisplayName("Search.Category")]
        public int cid { get; set; }
        public IList<SelectListItem> AvailableCategories { get; set; }
    }
}