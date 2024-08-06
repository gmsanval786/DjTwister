using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;
using Nop.Core;

namespace Nop.Web.Models.Package
{
    public partial record PackageModel : BaseNopModel
    {
        public PackageModel()
        {
            AvailableSongCounts = new List<SelectListItem>();
            AvailableRevisions = new List<SelectListItem>();
            AvailableDeliveryMethods = new List<SelectListItem>();
            AvailableDeliveryDays = new List<SelectListItem>();
            AvailableRecordingTimes = new List<SelectListItem>();
        }

        public int VendorId { get; set; }
        public decimal Price { get; set; }
        public string PriceVal { get; set; }
        public string Description { get; set; }
        public string RecordingTime { get; set; }
        public int PackageTypeId { get; set; }

        [NopResourceDisplayName("Account.Fields.SongCount")]
        public int SongCountId { get; set; }

        [NopResourceDisplayName("Account.Fields.Revision")]
        public int? RevisionId { get; set; }

        [NopResourceDisplayName("Account.Fields.RevisionAllowed")]
        public bool AllowRevisions { get; set; }
        public string StrAllowRevisions { get; set; }

        [NopResourceDisplayName("Account.Fields.DeliveryMethod")]
        public int DeliveryMethodId { get; set; }

        [NopResourceDisplayName("Account.Fields.DeliveryDay")]
        public int DeliveryTimeDays { get; set; }

        public IList<SelectListItem> AvailableSongCounts { get; set; }
        public IList<SelectListItem> AvailableRevisions { get; set; }
        public IList<SelectListItem> AvailableDeliveryMethods { get; set; }
        public IList<SelectListItem> AvailableDeliveryDays { get; set; }
        public IList<SelectListItem> AvailableRecordingTimes { get; set; }
        public PackageModel BasicPackage { get; set; }
        public PackageModel StandardPackage { get; set; }
        public PackageModel PremiumPackage { get; set; }
    }
}