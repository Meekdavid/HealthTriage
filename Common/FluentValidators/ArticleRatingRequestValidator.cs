using Common.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.FluentValidators
{
    public class ArticleRatingRequestValidator : AbstractValidator<ArticleRatingRequest>
    {
        public ArticleRatingRequestValidator()
        {
            // ArticleId validation: It should not be empty
            RuleFor(x => x.ArticleId)
                .NotEmpty().WithMessage("ArticleId is required.");

            // UserId validation: It should not be empty
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            // Rating validation: It should be between 1 and 5 (inclusive)
            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
        }
    }
}
