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
        public HomeController()
        {

        }
        public virtual async Task<IActionResult> Index()
        {
            return View();
        }
    }
}