using Common.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.FluentValidators
{
    public class PractitionerRatingRequestValidator : AbstractValidator<PractitionerRatingRequest>
    {
        public PractitionerRatingRequestValidator()
        {
            // PractitionerId validation: It should not be empty
            RuleFor(x => x.PractitionerId)
                .NotEmpty().WithMessage("PractitionerId is required.");

            // Rating validation: It must be between 1 and 5
            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
        }
    }
}
