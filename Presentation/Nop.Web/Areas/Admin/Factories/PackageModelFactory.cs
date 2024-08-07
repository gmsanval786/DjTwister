using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Tax;
using Nop.Core.Domain.Vendors;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Discounts;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Areas.Admin.Models.Common;
using Nop.Web.Framework.Extensions;
using Nop.Web.Framework.Factories;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Web.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the package model factory implementation
    /// </summary>
    public partial class PackageModelFactory : IPackageModelFactory
    {
        #region Fields

        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedModelFactory _localizedModelFactory;
        private readonly IPackageService _packageService;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IWorkContext _workContext;
        private readonly CatalogSettings _catalogSettings;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IVendorService _vendorService;

        #endregion

        #region Ctor

        public PackageModelFactory(IBaseAdminModelFactory baseAdminModelFactory,
            ILocalizationService localizationService,
            ILocalizedModelFactory localizedModelFactory,
            IPackageService packageService,
            IStoreContext storeContext,
            IStoreService storeService,
            IUrlRecordService urlRecordService,
            IWorkContext workContext,
            CatalogSettings catalogSettings,
            IPriceFormatter priceFormatter,
            IVendorService vendorService)
        {
            _baseAdminModelFactory = baseAdminModelFactory;
            _localizationService = localizationService;
            _localizedModelFactory = localizedModelFactory;
            _packageService = packageService;
            _storeContext = storeContext;
            _storeService = storeService;
            _urlRecordService = urlRecordService;
            _workContext = workContext;
            _catalogSettings = catalogSettings;
            _priceFormatter = priceFormatter;
            _vendorService = vendorService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare package search model
        /// </summary>
        /// <param name="searchModel">Product search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the package search model
        /// </returns>
        public virtual async Task<PackageSearchModel> PreparePackageSearchModelAsync(PackageSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare available stores
            await _baseAdminModelFactory.PrepareStoresAsync(searchModel.AvailableStores);

            await _baseAdminModelFactory.PrepareVendorsAsync(searchModel.AvailableVendors);

            await _baseAdminModelFactory.PreparePackageTypesAsync(searchModel.AvailablePackageTypes);

            searchModel.HideStoresList = _catalogSettings.IgnoreStoreLimitations || searchModel.AvailableStores.SelectionIsNotPossible();

            //prepare grid
            searchModel.SetGridPageSize();

            return searchModel;
        }

        /// <summary>
        /// Prepare paged package list model
        /// </summary>
        /// <param name="searchModel">Product search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the package list model
        /// </returns>
        public virtual async Task<PackageListModel> PreparePackageListModelAsync(PackageSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get packages
            var packages = await _packageService.GetAllPackagesAsync(vendorId: searchModel.SearchVendorId, packageTypeId: searchModel.SearchPackageTypeId,
                showHidden: true, storeId: searchModel.SearchStoreId, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = await new PackageListModel().PrepareToGridAsync(searchModel, packages, () =>
            {
                return packages.SelectAwait(async package =>
                {
                    //fill in model values from the entity
                    var packageModel = package.ToModel<PackageModel>();

                    var packageVendor = await _vendorService.GetVendorByIdAsync(package.VendorId);
                    if (packageVendor is not null)
                        packageModel.VendorName = packageVendor.Name;

                    packageModel.PackageTypeName = await _localizationService.GetLocalizedEnumAsync(package.PackageType);
                    packageModel.PriceVal = await _priceFormatter.FormatPriceAsync(package.Price, true, false);
                    packageModel.DeliveryMethod = await _localizationService.GetLocalizedEnumAsync(package.DeliveryMethod);
                    
                    return packageModel;
                });
            });

            return model;
        }

        /// <summary>
        /// Prepare package model
        /// </summary>
        /// <param name="model">Product model</param>
        /// <param name="package">Product</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the package model
        /// </returns>
        public virtual async Task<PackageModel> PreparePackageModelAsync(PackageModel model, Package package, bool excludeProperties = false)
        {
            Func<PackageLocalizedModel, int, Task> localizedModelConfiguration = null;

            if (package != null)
            {
                //fill in model values from the entity
                if (model == null)
                {
                    model = package.ToModel<PackageModel>();

                    var vendor = await _vendorService.GetVendorByIdAsync(package.VendorId);
                    if (vendor is not null)
                        model.VendorName = vendor.Name;

                    model.PackageTypeName = await _localizationService.GetLocalizedEnumAsync(package.PackageType);
                }
            }

            await _baseAdminModelFactory.PrepareVendorsAsync(model.AvailableVendors, false);

            await _baseAdminModelFactory.PreparePackageTypesAsync(model.AvailablePackageTypes);

            await _baseAdminModelFactory.PrepareSongCountAsync(model.AvailableSongCounts);

            await _baseAdminModelFactory.PrepareRevisionAsync(model.AvailableRevisions);

            await _baseAdminModelFactory.PrepareDeliveryMethodsAsync(model.AvailableDeliveryMethods);

            await _baseAdminModelFactory.PrepareDeliveryDaysAsync(model.AvailableDeliveryDays);

            //prepare localized models
            if (!excludeProperties)
                model.Locales = await _localizedModelFactory.PrepareLocalizedModelsAsync(localizedModelConfiguration);

            return model;
        }        

        #endregion
    }
}