﻿using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Vendors;
using Nop.Data.Extensions;

namespace Nop.Data.Mapping.Builders.Vendors
{
    /// <summary>
    /// Represents a vendor picture entity builder
    /// </summary>
    public partial class VendorPictureBuilder : NopEntityBuilder<VendorPicture>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(VendorPicture.PictureId)).AsInt32()
                .WithColumn(nameof(VendorPicture.VendorId)).AsInt32();
        }

        #endregion
    }
}