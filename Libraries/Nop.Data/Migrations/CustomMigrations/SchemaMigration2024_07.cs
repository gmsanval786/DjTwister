using FluentMigrator;
using Nop.Data.Mapping;
using Nop.Data.Extensions;
using System.Collections.Generic;
using Nop.Core.Domain.Localization;
using System.Linq;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;

namespace Nop.Data.Migrations.CustomMigrations
{
    [NopMigration("2024-07-20 18:10:00:1037704", "SchemaMigration2024_07", MigrationProcessType.NoMatter)]
    public class SchemaMigration2024_07 : MigrationBase
    {
        #region Fields

        private readonly IMigrationManager _migrationManager;
        private readonly INopDataProvider _dataProvider;

        #endregion

        #region Ctor

        public SchemaMigration2024_07(IMigrationManager migrationManager,
            INopDataProvider dataProvider)
        {
            _migrationManager = migrationManager;
            _dataProvider = dataProvider;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Run new table queires
        /// </summary>
        protected virtual void RunNewTableQueries()
        {
            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Package))).Exists())
                Create.TableFor<Package>();

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(ProductPackage))).Exists())
                Create.TableFor<ProductPackage>();

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(VendorCategory))).Exists())
                Create.TableFor<VendorCategory>();

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(VendorPicture))).Exists())
                Create.TableFor<VendorPicture>();
        }

        /// <summary>
        /// Run locales queires
        /// </summary>
        protected virtual void RunLocalesQueries()
        {
            var resources = new Dictionary<string, string>();
            var languages = _dataProvider.QueryAsync<Language>($"Select * from {nameof(Language)}").Result;

            var localeStringResource = _dataProvider.QueryAsync<int>($"Select count(id) from {nameof(LocaleStringResource)} Where {nameof(LocaleStringResource.ResourceName)} " +
              $"= 'Account.Fields.DisplayName'").Result;
            if (localeStringResource.FirstOrDefault() == 0)
            {
                resources.Add("Account.Fields.DisplayName", "Display Dj Name");
                resources.Add("Account.Fields.DisplayName.Required", "Display Dj name is required");
                resources.Add("Account.Fields.ChooseLevel", "Choose Level");
                resources.Add("Account.Fields.Bio", "Bio");
                resources.Add("Account.Fields.Genre", "Genre");
                resources.Add("Account.Fields.FavoriteGenre", "Favorite Genre");
                resources.Add("Account.Fields.EventsInYear", "How many events do you host a year?");
                resources.Add("Account.Fields.Password.Required", "Password is required.");
                resources.Add("Account.Fields.Password.MinLength", "Password must be at least 8 characters long.");
                resources.Add("Account.Fields.Password.Number", "Password must contain at least one number.");
                resources.Add("Account.Fields.Password.Special", "Password must contain at least one special character.");
            }

            localeStringResource = _dataProvider.QueryAsync<int>($"Select count(id) from {nameof(LocaleStringResource)} Where {nameof(LocaleStringResource.ResourceName)} " +
              $"= 'Account.Fields.Package'").Result;
            if (localeStringResource.FirstOrDefault() == 0)
            {
                resources.Add("Account.Fields.Package", "Packages");

                resources.Add("Account.Fields.Package.Description", "Description");
                resources.Add("Account.Fields.Package.SelectSongCount", "Select Song Count");
                resources.Add("Account.Fields.Package.SongCount", "Song Count");
                resources.Add("Account.Fields.Package.RecordingTime", "Recording Time");
                resources.Add("Account.Fields.Package.Revisions", "Will Revisions be allowed?");
                resources.Add("Account.Fields.Package.HowMany", "How Many");
                resources.Add("Account.Fields.Package.HowManySelect", "How Many (select)");
                resources.Add("Account.Fields.Package.DeliveryMethod", "Delivery Method");
                resources.Add("Account.Fields.Package.DeliveryTime", "Delivery Time Select in Days");
            }

            localeStringResource = _dataProvider.QueryAsync<int>($"Select count(id) from {nameof(LocaleStringResource)} Where {nameof(LocaleStringResource.ResourceName)} " +
              $"= 'Admin.Customers.Customers.Fields.IsLock'").Result;
            if (localeStringResource.FirstOrDefault() == 0)
            {
                resources.Add("Admin.Customers.Customers.Fields.IsLock", "Lock");
                resources.Add("Admin.Customers.Customers.Fields.IsLock.hint", "Indicates whether the customer account is currently locked and cannot be accessed.");
            }

            localeStringResource = _dataProvider.QueryAsync<int>($"Select count(id) from {nameof(LocaleStringResource)} Where {nameof(LocaleStringResource.ResourceName)} " +
              $"= 'Admin.Vendors.Fields.Categories'").Result;
            if (localeStringResource.FirstOrDefault() == 0)
            {
                resources.Add("Admin.Vendors.Fields.Categories", "Genres");
                resources.Add("Admin.Vendors.Fields.Categories.hint", "Name of genres");
            }

            localeStringResource = _dataProvider.QueryAsync<int>($"Select count(id) from {nameof(LocaleStringResource)} Where {nameof(LocaleStringResource.ResourceName)} " +
              $"= 'Admin.Vendors.Fields.Categories.NoCategoriesAvailable'").Result;
            if (localeStringResource.FirstOrDefault() == 0)
            {
                resources.Add("Admin.Vendors.Fields.Categories.NoCategoriesAvailable", "No genres available.");
            }

            localeStringResource = _dataProvider.QueryAsync<int>($"Select count(id) from {nameof(LocaleStringResource)} Where {nameof(LocaleStringResource.ResourceName)} " +
              $"= 'Account.Homepage.Fields.Genres'").Result;
            if (localeStringResource.FirstOrDefault() == 0)
            {
                resources.Add("Account.Homepage.Fields.Genres.Choose", "Choose Genre");
            }

            localeStringResource = _dataProvider.QueryAsync<int>($"Select count(id) from {nameof(LocaleStringResource)} Where {nameof(LocaleStringResource.ResourceName)} " +
              $"= 'Admin.Vendors.Multimedia'").Result;
            if (localeStringResource.FirstOrDefault() == 0)
            {
                resources.Add("Admin.Vendors.Multimedia", "Multimedia");
                resources.Add("Admin.Vendors.Multimedia.Pictures", "Pictures");
                resources.Add("Admin.Vendors.Multimedia.Pictures.Fields.Picture", "Picture");
                resources.Add("Admin.Vendors.Multimedia.Pictures.Fields.Picture.hint", "You can choose multiple images to upload at once. If the picture size exceeds your stores max image size setting, it will be automatically resized.");
                resources.Add("Admin.Vendors.Multimedia.Pictures.Fields.DisplayOrder", "Display order");
                resources.Add("Admin.Vendors.Multimedia.Pictures.Fields.OverrideAltattribute", "Display order");
                resources.Add("Admin.Vendors.Multimedia.Pictures.Fields.OverrideTitleattribute", "Display order");
                resources.Add("Admin.Vendors.Multimedia.Pictures.AddNew", "Add a new picture");
            }

            localeStringResource = _dataProvider.QueryAsync<int>($"Select count(id) from {nameof(LocaleStringResource)} Where {nameof(LocaleStringResource.ResourceName)} " +
              $"= 'Admin.Catalog.Packages'").Result;
            if (localeStringResource.FirstOrDefault() == 0)
            {
                resources.Add("Admin.Catalog.Packages", "Packages");
                resources.Add("Admin.Catalog.Packages.Info", "Package info");
                resources.Add("Admin.Catalog.Packages.AddNew", "Add new package");
                resources.Add("Admin.Catalog.Packages.EditPackageDetails", "Edit package details");
                resources.Add("Admin.Catalog.Packages.BackToList", "Back to packages list");
                resources.Add("Admin.Catalog.Packages.Added", "Package added successfully.");
                resources.Add("Admin.Catalog.Packages.Updated", "Package updated successfully.");
                resources.Add("Admin.Catalog.Packages.Deleted", "Package deleted successfully.");

                resources.Add("Admin.Catalog.Packages.List.SearchVendor", "DJs");
                resources.Add("Admin.Catalog.Packages.List.SearchVendor.hint", "Search package by dj");

                resources.Add("Admin.Catalog.Packages.List.SearchPackageType", "Types");
                resources.Add("Admin.Catalog.Packages.List.SearchPackageType.hint", "Search package by type");

                resources.Add("Admin.Catalog.Packages.Fields.PackgeTypeName", "Type");
                resources.Add("Admin.Catalog.Packages.Fields.VendorName", "DJ");
                resources.Add("Admin.Catalog.Packages.Fields.DeliveryMethodName", "Delivery Method");

                resources.Add("Admin.Catalog.Packages.Fields.PackageType", "Type");
                resources.Add("Admin.Catalog.Packages.Fields.PackageType.hint", "Type of package");
                resources.Add("Admin.Catalog.Packages.Fields.Vendor", "DJ");
                resources.Add("Admin.Catalog.Packages.Fields.Vendor.hint", "Name of dj");
                resources.Add("Admin.Catalog.Packages.Fields.Description", "Description");
                resources.Add("Admin.Catalog.Packages.Fields.Description.hint", "Description of package");
                resources.Add("Admin.Catalog.Packages.Fields.RecordingTime", "Recording time");
                resources.Add("Admin.Catalog.Packages.Fields.RecordingTime.hint", "Songs recording time");
                resources.Add("Admin.Catalog.Packages.Fields.SongCount", "Song Count");
                resources.Add("Admin.Catalog.Packages.Fields.SongCount.hint", "Number of songs");
                resources.Add("Admin.Catalog.Packages.Fields.Price", "Price");
                resources.Add("Admin.Catalog.Packages.Fields.Price.hint", "Price of package");
                resources.Add("Admin.Catalog.Packages.Fields.RevisionAllowed", "Will Revisions be allowed?");
                resources.Add("Admin.Catalog.Packages.Fields.RevisionAllowed.hint", "Revisions be allowed?");
                resources.Add("Admin.Catalog.Packages.Fields.Revision", "Revision");
                resources.Add("Admin.Catalog.Packages.Fields.Revision.hint", "Number of revision");
                resources.Add("Admin.Catalog.Packages.Fields.DeliveryMethod", "Delivery method");
                resources.Add("Admin.Catalog.Packages.Fields.DeliveryMethod.hint", "Method of package delivery");
                resources.Add("Admin.Catalog.Packages.Fields.DeliveryDay", "Delivery Time Select in Days");
                resources.Add("Admin.Catalog.Packages.Fields.DeliveryDay.hint", "Days of delivery time");
            }

            localeStringResource = _dataProvider.QueryAsync<int>($"Select count(id) from {nameof(LocaleStringResource)} Where {nameof(LocaleStringResource.ResourceName)} " +
              $"= 'Admin.Catalog.Products.Fields.BasicPackage'").Result;
            if (localeStringResource.FirstOrDefault() == 0)
            {
                resources.Add("Admin.Catalog.Products.Fields.BasicPackage", "Basic Package");
                resources.Add("Admin.Catalog.Products.Fields.BasicPackage.hint", "Name of basic package");
                resources.Add("Admin.Catalog.Products.Fields.BasicPackage.None", "None");
                resources.Add("Admin.Catalog.Products.Fields.StandardPackage", "Standard Package");
                resources.Add("Admin.Catalog.Products.Fields.StandardPackage.hint", "Name of standard package");
                resources.Add("Admin.Catalog.Products.Fields.StandardPackage.None", "None");
                resources.Add("Admin.Catalog.Products.Fields.PremiumPackage", "Premium Package");
                resources.Add("Admin.Catalog.Products.Fields.PremiumPackage.hint", "Name of premium package");
                resources.Add("Admin.Catalog.Products.Fields.PremiumPackage.None", "None");
            }

            //insert new locale resources
            var locales = languages.SelectMany(language => resources.Select(resource => new LocaleStringResource
            {
                LanguageId = language.Id,
                ResourceName = resource.Key,
                ResourceValue = resource.Value
            })).ToList();

            foreach (var res in locales)
                _dataProvider.InsertEntityAsync(res);


            string[] updateresources = {
                    "Update  LocaleStringResource set ResourceValue='or continue with' where ResourceName='Account.AssociatedExternalAuth.Or' and LanguageId = 1",
                    "Update  LocaleStringResource set ResourceValue='Genres' where ResourceName='Account.Fields.Genre' and LanguageId = 1",
                    "Update  LocaleStringResource set ResourceValue='Cart' where ResourceName='ShoppingCart' and LanguageId = 1",
                    "Update  LocaleStringResource set ResourceValue='Your Account' where ResourceName='Account.MyAccount' and LanguageId = 1",
                    "Update  LocaleStringResource set ResourceValue='{0}' where ResourceName='ShoppingCart.HeaderQuantity' and LanguageId = 1",
                    "Update  LocaleStringResource set ResourceValue='Message DJ' where ResourceName='ContactVendor' and LanguageId = 1",
                    "Update  LocaleStringResource set ResourceValue='Choose Genre' where ResourceName='Search.SelectCategory' and LanguageId = 1",
                    "Update  LocaleStringResource set ResourceValue='Choose Genres' where ResourceName='Search.Category' and LanguageId = 1",
            };

            foreach (var res in updateresources)
            {
                _dataProvider.ExecuteNonQueryAsync(res);
            }

            string[] deleteresources = {
            };
            foreach (var res in deleteresources)
            {
                _dataProvider.ExecuteNonQueryAsync(res);
            }
        }

        protected virtual void RunOtherQueries()
        {
            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up()
        {
            RunNewTableQueries();

            RunLocalesQueries();

            RunOtherQueries();
        }

        public override void Down()
        {
            //add the downgrade logic if necessary 
        }

        #endregion
    }
}
