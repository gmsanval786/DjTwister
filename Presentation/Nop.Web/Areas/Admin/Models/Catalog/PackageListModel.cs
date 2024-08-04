﻿using Nop.Web.Framework.Models;

namespace Nop.Web.Areas.Admin.Models.Catalog
{
    /// <summary>
    /// Represents a package list model
    /// </summary>
    public partial record PackageListModel : BasePagedListModel<PackageModel>
    {
    }
}