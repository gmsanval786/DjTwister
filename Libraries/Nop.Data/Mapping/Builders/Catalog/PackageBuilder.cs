using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Builders.Catalog
{
    /// <summary>
    /// Represents a package entity builder
    /// </summary>
    public partial class PackageBuilder : NopEntityBuilder<Package>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(Package.Description)).AsString(int.MaxValue).Nullable()
                .WithColumn(nameof(Package.SongCounts)).AsInt32().Nullable()
                .WithColumn(nameof(Package.Revisions)).AsInt32().Nullable()
                .WithColumn(nameof(Package.DeliveryTimeDays)).AsInt32().Nullable();
        }

        #endregion
    }
}