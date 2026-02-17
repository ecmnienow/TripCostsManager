using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripCostsManager.Models.Validators
{
    public class ItemTypeModelValidator : AbstractValidator<ItemTypeModel>
    {
        public ItemTypeModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage("Value too long, consider less than 50");
        }
    }
}
