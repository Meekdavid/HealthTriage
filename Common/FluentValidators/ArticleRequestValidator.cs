using Common.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.FluentValidators
{
    public class ArticleRequestValidator : AbstractValidator<ArticleRequest>
    {
        public ArticleRequestValidator()
        {

            // Title validation: Title should be provided and cannot exceed 200 characters
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            // Content validation: Content should be provided and must be at least 10 characters long
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.");
                //.MinimumLength(10).WithMessage("Content must be at least 10 characters long.");

            // Category validation: Category should be a valid enum value
            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("Invalid Category.");

            // AuthorType validation: Should be either 'User' or 'Practitioner'
            RuleFor(x => x.AuthorType)
                .IsInEnum().WithMessage("AuthorType must be 'User' or 'Practitioner'.");

            // ArticleState validation: Should be one of 'Published', 'Draft', or 'Pending Review'
            RuleFor(x => x.ArticleState)
                .IsInEnum().WithMessage("ArticleState must be 'Published', 'Draft', or 'Pending Review'.");
        }
    }
}
