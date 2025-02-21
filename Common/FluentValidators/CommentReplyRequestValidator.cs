using Common.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.FluentValidators
{
    public class CommentReplyRequestValidator : AbstractValidator<CommentReplyRequest>
    {
        public CommentReplyRequestValidator()
        {
            // CommentId validation: It should not be empty
            RuleFor(x => x.CommentId)
                .NotEmpty().WithMessage("CommentId is required.");

            // UserId validation: It should not be empty
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            // ReplyText validation: It should not be empty and have a minimum length
            RuleFor(x => x.ReplyText)
                .NotEmpty().WithMessage("ReplyText is required.");
                //.MinimumLength(10).WithMessage("ReplyText must be at least 10 characters long.");
        }
    }
}
