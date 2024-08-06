using FluentMigrator;
using Nop.Data.Mapping;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Vendors;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Migrations.CustomMigrations
{
    [NopMigration("2023-07-16 02:05:00:1037704", "SchemaMigration2024_07_Columns", MigrationProcessType.NoMatter)]
    public class SchemaMigration2024_07_Columns : MigrationBase
    {
        #region Fields

        private readonly IMigrationManager _migrationManager;
        private readonly INopDataProvider _dataProvider;

        #endregion

        #region Ctor

        public SchemaMigration2024_07_Columns(IMigrationManager migrationManager,
            INopDataProvider dataProvider)
        {
            _migrationManager = migrationManager;
            _dataProvider = dataProvider;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up()
        {
            //add new columns
            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Customer))).Column(nameof(Customer.EventHostCount)).Exists())
            {
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Customer)))
                    .AddColumn(nameof(Customer.EventHostCount)).AsInt32().Nullable();
            }

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Customer))).Column(nameof(Customer.GenreId)).Exists())
            {
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Customer)))
                    .AddColumn(nameof(Customer.GenreId)).AsInt32().Nullable();
            }

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Customer))).Column(nameof(Customer.IsLock)).Exists())
            {
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Customer)))
                    .AddColumn(nameof(Customer.IsLock)).AsBoolean().NotNullable().WithDefaultValue(false);
            }

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Vendor))).Column(nameof(Vendor.LevelId)).Exists())
            {
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Vendor)))
                    .AddColumn(nameof(Vendor.LevelId)).AsInt32().Nullable();
            }

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Package))).Column(nameof(Package.VendorId)).Exists())
            {
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Package)))
                    .AddColumn(nameof(Package.VendorId)).AsInt32().Nullable();
            }

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Package))).Column(nameof(Package.AllowRevisions)).Exists())
            {
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Package)))
                    .AddColumn(nameof(Package.AllowRevisions)).AsBoolean().Nullable();
            }

            if (Schema.Table(NameCompatibilityManager.GetTableName(typeof(Package))).Column(nameof(Package.Revisions)).Exists())
            {
                //alter column
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Package)))
                    .AlterColumn(nameof(Package.Revisions)).AsInt32().Nullable();
            }

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Vendor))).Column(nameof(Vendor.Experience)).Exists())
            {
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Vendor)))
                    .AddColumn(nameof(Vendor.Experience)).AsInt32().Nullable();
            }
        }

        public override void Down()
        {
            //add the downgrade logic if necessary 
        }

        #endregion
    }
}
