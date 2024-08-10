using FluentMigrator;
using Nop.Data.Mapping;
using Nop.Data.Extensions;
using System.Collections.Generic;
using Nop.Core.Domain.Localization;
using System.Linq;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;
using Nop.Core.Domain.Logging;
using Nop.Core.Domain.Messages;

namespace Nop.Data.Migrations.CustomMigrations
{
    [NopMigration("2024-07-25 18:50:00:1037704", "SchemaMigration2024_08", MigrationProcessType.NoMatter)]
    public class SchemaMigration2024_08 : MigrationBase
    {
        #region Fields

        private readonly IMigrationManager _migrationManager;
        private readonly INopDataProvider _dataProvider;

        #endregion

        #region Ctor

        public SchemaMigration2024_08(IMigrationManager migrationManager,
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
            var messageTemplate = _dataProvider.QueryAsync<int>
                ($"Select count(id) from {nameof(MessageTemplate)} Where {nameof(MessageTemplate.Name)} = '{MessageTemplateSystemNames.CustomerAccountVerificationMessage}'").Result;
            if (messageTemplate.FirstOrDefault() == 0)
            {
                var template = new MessageTemplate
                {
                    Name = MessageTemplateSystemNames.CustomerAccountVerificationMessage,
                    Subject = "%Store.Name%. Account verification",
                    Body = $"<p><a href=\"%Store.URL%\">%Store.Name%</a><br/><br/><br/>Hello %Customer.FullName%,<br/>Your have received an email of account verification for %Customer.Email%. Please <a href=\"%Customer.UserAccountVerificationURL%\">click here</a> to account verification.<br/><br/></p>",
                    IsActive = true,
                };
                template.Body = template.ToBasicUIStyle();
                _dataProvider.InsertEntity(template);
            }
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
