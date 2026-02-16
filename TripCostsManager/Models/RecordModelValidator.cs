using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripCostsManager.Models
{
    public class RecordModelValidator : AbstractValidator<RecordModel>
    {
        public RecordModelValidator()
        {
            RuleFor(x => x.MarketName)
                .NotEmpty()
                .WithMessage("Market name is required")
                .MaximumLength(200)
                .WithMessage("Value too long, consider less than 200");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(200)
                .WithMessage("Value too long, consider less than 200");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Price is required")
                .Custom((x, context) =>
                {
                    if ((!(decimal.TryParse(x, out decimal value)) || value < 0))
                        context.AddFailure($"\"{x}\" is not a valid number or less than 0");
                });

            RuleFor(x => x.DateTime)
                .NotEmpty()
                .WithMessage("Dete time is required")
                .Custom((x, context) =>
                {
                    if ((!(DateTime.TryParse(x, out DateTime value)) || value < new DateTime(1950, 1, 1)))
                        context.AddFailure($"\"{x}\" is not a valid date or too old");
                });
        }
    }
}
