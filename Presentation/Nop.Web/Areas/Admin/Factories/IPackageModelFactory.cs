using System.Threading.Tasks;
using Nop.Core.Domain.Catalog;
using Nop.Web.Areas.Admin.Models.Catalog;

namespace Nop.Web.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the package model factory
    /// </summary>
    public partial interface IPackageModelFactory
    {
        /// <summary>
        /// Prepare package search model
        /// </summary>
        /// <param name="searchModel">Package search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the package search model
        /// </returns>
        Task<PackageSearchModel> PreparePackageSearchModelAsync(PackageSearchModel searchModel);

        /// <summary>
        /// Prepare paged package list model
        /// </summary>
        /// <param name="searchModel">Package search model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the package list model
        /// </returns>
        Task<PackageListModel> PreparePackageListModelAsync(PackageSearchModel searchModel);

        /// <summary>
        /// Prepare package model
        /// </summary>
        /// <param name="model">Package model</param>
        /// <param name="package">Package</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the package model
        /// </returns>
        Task<PackageModel> PreparePackageModelAsync(PackageModel model, Package package, bool excludeProperties = false);
    }
}