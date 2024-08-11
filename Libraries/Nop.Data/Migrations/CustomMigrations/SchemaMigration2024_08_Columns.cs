using FluentMigrator;
using Nop.Data.Mapping;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Vendors;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;

namespace Nop.Data.Migrations.CustomMigrations
{
    [NopMigration("2023-07-16 02:20:05:1037704", "SchemaMigration2024_08_Columns", MigrationProcessType.NoMatter)]
    public class SchemaMigration2024_08_Columns : MigrationBase
    {
        #region Fields

        private readonly IMigrationManager _migrationManager;
        private readonly INopDataProvider _dataProvider;

        #endregion

        #region Ctor

        public SchemaMigration2024_08_Columns(IMigrationManager migrationManager,
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
            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Customer))).Column(nameof(Customer.Token)).Exists())
            {
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Customer)))
                    .AddColumn(nameof(Customer.Token)).AsString().Nullable();
            }

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(Customer))).Column(nameof(Customer.TokenCreatedOnUtc)).Exists())
            {
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(Customer)))
                    .AddColumn(nameof(Customer.TokenCreatedOnUtc)).AsDateTime2().Nullable();
            }

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(ShoppingCartItem))).Column(nameof(ShoppingCartItem.PackageId)).Exists())
            {
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(ShoppingCartItem)))
                    .AddColumn(nameof(ShoppingCartItem.PackageId)).AsInt32().Nullable();
            }

            if (!Schema.Table(NameCompatibilityManager.GetTableName(typeof(OrderItem))).Column(nameof(OrderItem.PackageId)).Exists())
            {
                Alter.Table(NameCompatibilityManager.GetTableName(typeof(OrderItem)))
                    .AddColumn(nameof(OrderItem.PackageId)).AsInt32().Nullable();
            }
        }

        public override void Down()
        {
            //add the downgrade logic if necessary 
        }

        #endregion
    }
}
