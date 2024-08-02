using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Caching;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Vendors;
using Nop.Core;
using Nop.Services.Vendors;
using Nop.Web.Factories;
using Nop.Web.Models.Common;
using Nop.Web.Models.Catalog;
using System.Linq;
using DocumentFormat.OpenXml.EMMA;

namespace Nop.Web.Controllers
{
    public partial class HomeController : BasePublicController
    {
        #region Fields

        private readonly ICommonModelFactory _commonModelFactory;
        private readonly IVendorService _vendorService;

        #endregion

        public HomeController(ICommonModelFactory commonModelFactory, IVendorService vendorService)
        {
            _commonModelFactory = commonModelFactory;
            _vendorService = vendorService;
        }
        public virtual async Task<IActionResult> Index()
        {
            var model = await _commonModelFactory.PrepareVendorsModelAsync(new HomepageModel());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SearchVendors(int categoryId)
        {
            var model = await _commonModelFactory.PrepareVendorsModelAsync(new HomepageModel(), categoryId);
            
            return PartialView("_Vendors", model);
        }
    }
}