using Common.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.FluentValidators
{
    public class ArticleCommentRequestValidator : AbstractValidator<ArticleCommentRequest>
    {
        public ArticleCommentRequestValidator()
        {
            // ArticleId validation: It should not be empty
            RuleFor(x => x.ArticleId)
                .NotEmpty().WithMessage("ArticleId is required.");

            // CommentText validation: It should not be empty and should have a minimum length of 10 characters
            RuleFor(x => x.CommentText)
                .NotEmpty().WithMessage("CommentText is required.");
                //.MinimumLength(10).WithMessage("CommentText must be at least 10 characters long.");
        }
    }
}
