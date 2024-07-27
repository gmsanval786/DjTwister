﻿using FluentMigrator;
using Nop.Data.Mapping;
using Nop.Data.Extensions;
using System.Collections.Generic;
using Nop.Core.Domain.Localization;
using System.Linq;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;

namespace Nop.Data.Migrations.CustomMigrations
{
    [NopMigration("2024-07-20 16:20:00:1037704", "SchemaMigration2024_07", MigrationProcessType.NoMatter)]
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

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(PackageProductMapping))).Exists())
                Create.TableFor<PackageProductMapping>();

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(VendorCategory))).Exists())
                Create.TableFor<VendorCategory>();
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

                resources.Add("Account.Fields.Package.Price", "Price");
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
                                "Update  LocaleStringResource set ResourceValue='or continue with' where ResourceName='Account.AssociatedExternalAuth.Or' and LanguageId = 1"
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
