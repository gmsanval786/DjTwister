using FluentValidation;
using Nop.Core.Domain.Customers;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.Customer;

namespace Nop.Web.Validators.Customer
{
    public partial class PasswordRecoveryConfirmValidator : BaseNopValidator<PasswordRecoveryConfirmModel>
    {
        public PasswordRecoveryConfirmValidator(ILocalizationService localizationService, CustomerSettings customerSettings)
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Account.Fields.Password.Required"))
                .MinimumLength(8).WithMessageAwait(localizationService.GetResourceAsync("Account.Fields.Password.MinLength"))
                .Matches(@"[0-9]").WithMessageAwait(localizationService.GetResourceAsync("Account.Fields.Password.Number"))
                .Matches(@"[!@#$%^&*(),.?"":{}|<>]").WithMessageAwait(localizationService.GetResourceAsync("Account.Fields.Password.Special"));

            RuleFor(x => x.ConfirmNewPassword).NotEmpty().WithMessageAwait(localizationService.GetResourceAsync("Account.PasswordRecovery.ConfirmNewPassword.Required"));
            RuleFor(x => x.ConfirmNewPassword).Equal(x => x.NewPassword).WithMessageAwait(localizationService.GetResourceAsync("Account.PasswordRecovery.NewPassword.EnteredPasswordsDoNotMatch"));
        }
    }
}