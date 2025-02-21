using Common.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.FluentValidators
{
    public class UserRegisterRequestValidator : AbstractValidator<UserRegisterRequest>
    {
        public UserRegisterRequestValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is required.")
                .MaximumLength(100).WithMessage("Full Name must not exceed 100 characters.");

            RuleFor(x => x.ProfilePicture)
                .Must(BeAValidFile).WithMessage("Profile Picture must be a valid image file (.jpg, .jpeg, .png).")
                .When(x => x.ProfilePicture != null);

            RuleFor(x => x.DOB)
                .NotEmpty().WithMessage("Date of Birth is required.");
                //.Must(BeAValidDate).WithMessage("Invalid Date of Birth format. Use YYYY-MM-DD.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required.")
                .Must(g => g == "Male" || g == "Female" || g == "Other")
                .WithMessage("Gender must be 'Male', 'Female', or 'Other'.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid Email format.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.");
                //.Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("Zip Code is required.");
                //.Matches(@"^\d{5}(-\d{4})?$").WithMessage("Invalid Zip Code format.");

            RuleFor(x => x.Nickname)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.");

            RuleFor(x => x.BloodGroup)
                .NotEmpty().WithMessage("Blood Group is required.")
                .Must(b => new[] { "A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-" }.Contains(b))
                .WithMessage("Invalid Blood Group. Use A+, A-, B+, B-, O+, O-, AB+, or AB-.");

            RuleFor(x => x.Height)
                .NotEmpty().WithMessage("Height is required.")
                .Matches(@"^\d+(\.\d{1,2})?$").WithMessage("Height must be a numeric value (e.g., 170 or 170.5 cm).");

            RuleFor(x => x.Weight)
                .NotEmpty().WithMessage("Weight is required.")
                .Matches(@"^\d+(\.\d{1,2})?$").WithMessage("Weight must be a numeric value (e.g., 65 or 65.5 kg).");

            //RuleFor(x => x.EmergencyContact)
            //    .NotEmpty().WithMessage("Emergency Contact is required.")
            //    .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid Emergency Contact number format.");
        }

        private bool BeAValidFile(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(fileExtension);
        }

        private bool BeAValidDate(string date)
        {
            return DateTime.TryParse(date, out _);
        }
    }
}
