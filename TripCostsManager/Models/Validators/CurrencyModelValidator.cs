using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripCostsManager.Models.Validators
{
    public class CurrencyModelValidator : AbstractValidator<CurrencyModel>
    {
        public CurrencyModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage("Name too long, consider less than 50");

            RuleFor(x => x.CurrencySymbol)
                .NotEmpty()
                .WithMessage("Currency symbol is required")
                .MaximumLength(50)
                .WithMessage("Currency symbol too long, consider up to 3 characters");

            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage("Value is required")
                .Custom((x, context) =>
                {
                    if (!decimal.TryParse(x, out decimal value) || value < 0)
                        context.AddFailure($"\"{x}\" is not a valid value or less than 0");
                });
        }
    }
}
