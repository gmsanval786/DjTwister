using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Vendors;
using Nop.Core.Http;
using Nop.Core.Infrastructure;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.ExportImport;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Services.Vendors;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Validators;

namespace Nop.Web.Areas.Admin.Controllers
{
    public partial class PackageController : BaseAdminController
    {
        #region Fields

        private readonly IAclService _aclService;
        private readonly IBackInStockSubscriptionService _backInStockSubscriptionService;
        private readonly ICategoryService _categoryService;
        private readonly ICopyProductService _copyProductService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ICustomerService _customerService;
        private readonly IDiscountService _discountService;
        private readonly IDownloadService _downloadService;
        private readonly IExportManager _exportManager;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IImportManager _importManager;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IManufacturerService _manufacturerService;
        private readonly INopFileProvider _fileProvider;
        private readonly INotificationService _notificationService;
        private readonly IPdfService _pdfService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly IProductAttributeFormatter _productAttributeFormatter;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductModelFactory _productModelFactory;
        private readonly IProductService _productService;
        private readonly IProductTagService _productTagService;
        private readonly ISettingService _settingService;
        private readonly IShippingService _shippingService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IStoreContext _storeContext;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IVideoService _videoService;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly VendorSettings _vendorSettings;
        private readonly IPackageModelFactory _packageModelFactory;
        private readonly IPackageService _packageService;
        private readonly IVendorService _vendorService;

        #endregion

        #region Ctor

        public PackageController(IAclService aclService,
            IBackInStockSubscriptionService backInStockSubscriptionService,
            ICategoryService categoryService,
            ICopyProductService copyProductService,
            ICustomerActivityService customerActivityService,
            ICustomerService customerService,
            IDiscountService discountService,
            IDownloadService downloadService,
            IExportManager exportManager,
            IGenericAttributeService genericAttributeService,
            IHttpClientFactory httpClientFactory,
            IImportManager importManager,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            IManufacturerService manufacturerService,
            INopFileProvider fileProvider,
            INotificationService notificationService,
            IPdfService pdfService,
            IPermissionService permissionService,
            IPictureService pictureService,
            IProductAttributeFormatter productAttributeFormatter,
            IProductAttributeParser productAttributeParser,
            IProductAttributeService productAttributeService,
            IProductModelFactory productModelFactory,
            IProductService productService,
            IProductTagService productTagService,
            ISettingService settingService,
            IShippingService shippingService,
            IShoppingCartService shoppingCartService,
            ISpecificationAttributeService specificationAttributeService,
            IStoreContext storeContext,
            IUrlRecordService urlRecordService,
            IVideoService videoService,
            IWebHelper webHelper,
            IWorkContext workContext,
            VendorSettings vendorSettings,
            IPackageModelFactory packageModelFactory,
            IPackageService packageService,
            IVendorService vendorService)
        {
            _aclService = aclService;
            _backInStockSubscriptionService = backInStockSubscriptionService;
            _categoryService = categoryService;
            _copyProductService = copyProductService;
            _customerActivityService = customerActivityService;
            _customerService = customerService;
            _discountService = discountService;
            _downloadService = downloadService;
            _exportManager = exportManager;
            _genericAttributeService = genericAttributeService;
            _httpClientFactory = httpClientFactory;
            _importManager = importManager;
            _languageService = languageService;
            _localizationService = localizationService;
            _localizedEntityService = localizedEntityService;
            _manufacturerService = manufacturerService;
            _fileProvider = fileProvider;
            _notificationService = notificationService;
            _pdfService = pdfService;
            _permissionService = permissionService;
            _pictureService = pictureService;
            _productAttributeFormatter = productAttributeFormatter;
            _productAttributeParser = productAttributeParser;
            _productAttributeService = productAttributeService;
            _productModelFactory = productModelFactory;
            _productService = productService;
            _productTagService = productTagService;
            _settingService = settingService;
            _shippingService = shippingService;
            _shoppingCartService = shoppingCartService;
            _specificationAttributeService = specificationAttributeService;
            _storeContext = storeContext;
            _urlRecordService = urlRecordService;
            _videoService = videoService;
            _webHelper = webHelper;
            _workContext = workContext;
            _vendorSettings = vendorSettings;
            _packageModelFactory = packageModelFactory;
            _packageService = packageService;
            _vendorService = vendorService;
        }

        #endregion

        #region Utilities

        protected virtual async Task UpdateLocalesAsync(Package package, PackageModel model)
        {
            foreach (var localized in model.Locales)
            {
                await _localizedEntityService.SaveLocalizedValueAsync(package,
                    x => x.Description,
                    localized.Description,
                    localized.LanguageId);
                await _localizedEntityService.SaveLocalizedValueAsync(package,
                    x => x.RecordingTime,
                    localized.RecordingTime,
                    localized.LanguageId);
            }
        }

        #endregion

        #region Methods

        #region Package list / create / edit / delete

        public virtual IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual async Task<IActionResult> List()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            //prepare model
            var model = await _packageModelFactory.PreparePackageSearchModelAsync(new PackageSearchModel());

            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> PackageList(PackageSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
                return await AccessDeniedDataTablesJson();

            //prepare model
            var model = await _packageModelFactory.PreparePackageListModelAsync(searchModel);

            return Json(model);
        }

        public virtual async Task<IActionResult> Create(bool showtour = false)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            //validate maximum number of package per vendor
            //var currentVendor = await _workContext.GetCurrentVendorAsync();
            //if (_vendorSettings.MaximumProductNumber > 0 && currentVendor != null
            //    && await _productService.GetNumberOfProductsByVendorIdAsync(currentVendor.Id) >= _vendorSettings.MaximumProductNumber)
            //{
            //    _notificationService.ErrorNotification(string.Format(await _localizationService.GetResourceAsync("Admin.Catalog.Products.ExceededMaximumNumber"),
            //        _vendorSettings.MaximumProductNumber));
            //    return RedirectToAction("List");
            //}

            //prepare model
            var model = await _packageModelFactory.PreparePackageModelAsync(new PackageModel(), null);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> Create(PackageModel model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            //validate maximum number of products per vendor
            var currentVendor = await _workContext.GetCurrentVendorAsync();
            //if (_vendorSettings.MaximumProductNumber > 0 && currentVendor != null
            //    && await _productService.GetNumberOfProductsByVendorIdAsync(currentVendor.Id) >= _vendorSettings.MaximumProductNumber)
            //{
            //    _notificationService.ErrorNotification(string.Format(await _localizationService.GetResourceAsync("Admin.Catalog.Products.ExceededMaximumNumber"),
            //        _vendorSettings.MaximumProductNumber));
            //    return RedirectToAction("List");
            //}

            if (ModelState.IsValid)
            {
                //a vendor should have access only to his products
                if (currentVendor != null)
                    model.VendorId = currentVendor.Id;

                //vendors cannot edit "Show on home page" property
                if (currentVendor != null && model.ShowOnHomepage)
                    model.ShowOnHomepage = false;

                //package
                var package = model.ToEntity<Package>();

                if (!package.AllowRevisions && package.Revisions != null)
                    package.Revisions = null;

                package.CreatedOnUtc = DateTime.UtcNow;
                package.UpdatedOnUtc = DateTime.UtcNow;
                await _packageService.InsertPackageAsync(package);

                //locales
                await UpdateLocalesAsync(package, model);

                //stores
                //await _productService.UpdateProductStoreMappingsAsync(package, model.SelectedStoreIds);

                //activity log
                await _customerActivityService.InsertActivityAsync("AddNewPackage",
                    string.Format(await _localizationService.GetResourceAsync("ActivityLog.AddNewPackage"), package.Description), package);

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Catalog.Packages.Added"));

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = package.Id });
            }

            //prepare model
            model = await _packageModelFactory.PreparePackageModelAsync(model, null, true);

            //if we got this far, something failed, redisplay form
            return View(model);
        }

        public virtual async Task<IActionResult> Edit(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            //try to get a package with the specified id
            var package = await _packageService.GetPackageByIdAsync(id);
            if (package == null || package.Deleted)
                return RedirectToAction("List");

            //a vendor should have access only to his products
            var currentVendor = await _workContext.GetCurrentVendorAsync();
            if (currentVendor != null && package.VendorId != currentVendor.Id)
                return RedirectToAction("List");

            //prepare model
            var model = await _packageModelFactory.PreparePackageModelAsync(null, package);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> Edit(PackageModel model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            //try to get a package with the specified id
            var package = await _packageService.GetPackageByIdAsync(model.Id);
            if (package == null || package.Deleted)
                return RedirectToAction("List");

            //a vendor should have access only to his products
            var currentVendor = await _workContext.GetCurrentVendorAsync();
            if (currentVendor != null && package.VendorId != currentVendor.Id)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                //a vendor should have access only to his products
                if (currentVendor != null)
                    model.VendorId = currentVendor.Id;

                //we do not validate maximum number of products per vendor when editing existing products (only during creation of new products)
                //vendors cannot edit "Show on home page" property
                //if (currentVendor != null && model.ShowOnHomepage != package.ShowOnHomepage)
                //    model.ShowOnHomepage = package.ShowOnHomepage;

                var previousProductType = package.PackageType;

                //package
                package = model.ToEntity(package);

                if (!package.AllowRevisions && package.Revisions != null)
                    package.Revisions = null;

                package.UpdatedOnUtc = DateTime.UtcNow;
                await _packageService.UpdatePackageAsync(package);

                //locales
                await UpdateLocalesAsync(package, model);

                //stores
                //await _productService.UpdateProductStoreMappingsAsync(package, model.SelectedStoreIds);

                //activity log
                await _customerActivityService.InsertActivityAsync("EditPackage",
                    string.Format(await _localizationService.GetResourceAsync("ActivityLog.EditPackage"), package.Description), package);

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Catalog.Packages.Updated"));

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = package.Id });
            }

            //prepare model
            model = await _packageModelFactory.PreparePackageModelAsync(model, package, true);

            //if we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            //try to get a package with the specified id
            var package = await _packageService.GetPackageByIdAsync(id);
            if (package == null)
                return RedirectToAction("List");

            //a vendor should have access only to his products
            var currentVendor = await _workContext.GetCurrentVendorAsync();
            if (currentVendor != null && package.VendorId != currentVendor.Id)
                return RedirectToAction("List");

            await _packageService.DeletePackageAsync(package);

            //activity log
            await _customerActivityService.InsertActivityAsync("DeletePackage",
                string.Format(await _localizationService.GetResourceAsync("ActivityLog.DeletePackage"), package.Description), package);

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Catalog.Packages.Deleted"));

            return RedirectToAction("List");
        }

        [HttpPost]
        public virtual async Task<IActionResult> DeleteSelected(ICollection<int> selectedIds)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            if (selectedIds == null || selectedIds.Count == 0)
                return NoContent();

            var currentVendor = await _workContext.GetCurrentVendorAsync();
            await _packageService.DeletePackagesAsync((await _packageService.GetPackagesByIdsAsync(selectedIds.ToArray()))
                .Where(p => currentVendor == null || p.VendorId == currentVendor.Id).ToList());

            return Json(new { Result = true });
        }

        public virtual async Task<IActionResult> GetPackagesByVendorId(int vendorId)
        {
            var packageTypeModel = new PackageTypeModel();

            // Define a method to populate package list
            async Task AddPackagesToModelAsync(IList<PackageModel> packageList, int packageTypeId)
            {
                var packages = await _packageService.GetAllPackagesAsync(vendorId: vendorId, packageTypeId: packageTypeId);

                foreach (var package in packages)
                {
                    var vendorEmail = "";
                    var vendor = await _vendorService.GetVendorByIdAsync(package.VendorId);
                    if (vendor != null)
                        vendorEmail = vendor.Email;

                    var packageType = await _localizationService.GetLocalizedEnumAsync(package.PackageType);

                    var fullPackageName = string.IsNullOrEmpty(vendorEmail)
                        ? $"{packageType} - {package.Price}"
                        : $"{packageType} - {package.Price} - {vendorEmail}";

                    packageList.Add(new PackageModel { Id = package.Id, Package = fullPackageName });
                }
            }

            await AddPackagesToModelAsync(packageTypeModel.BasicPackage, (int)PackageType.Basic);
            await AddPackagesToModelAsync(packageTypeModel.StandardPackage, (int)PackageType.Standard);
            await AddPackagesToModelAsync(packageTypeModel.PremiumPackage, (int)PackageType.Premium);

            return Json(packageTypeModel);
        }

        #endregion

        #endregion
    }
}