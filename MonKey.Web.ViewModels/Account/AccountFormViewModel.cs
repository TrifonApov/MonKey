namespace MonKey.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Services.AutoMapping;
    using Services.Models.Account;
    using static Data.Common.EntityValidation.Account;

    public class AccountFormViewModel : IMapTo<CreateAccountDto>, IHaveCustomMappings
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(AccountNameMaxLength, MinimumLength = AccountNameMinLength)]
        [Display(Name = "Account Name")]
        public string Name { get; set; } = null!;

        [StringLength(DescriptionNameMaxLength, MinimumLength = DescriptionNameMinLength)]
        [Display(Name = "Account Description (optional")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public Guid CurrencyId { get; set; }

        public Guid OwnerId { get; set; }

        [Required]
        [ModelBinder(BinderType = typeof(CultureAwareDecimalModelBinder))]
        [Range(typeof(decimal), MinBalanceValue, MaxBalanceValue, ParseLimitsInInvariantCulture = true)]
        [Display(Name = "Opening Account Balance")]
        public decimal Balance { get; set; }

        public string? CurrencyCode { get; set; }

        public bool IsEdit => Id.HasValue;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<CreateAccountDto, AccountFormViewModel>()
                .ForMember(m => m.Id, option => option.Ignore())
                .ForMember(m => m.CurrencyCode, option => option.Ignore())
                .ForMember(m => m.IsEdit, option => option.Ignore());
        }
    }
}
