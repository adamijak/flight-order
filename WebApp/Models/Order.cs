using System;
using System.ComponentModel.DataAnnotations;
using Common;
using WebApp.Entities;

namespace WebApp.Models
{
    public class Order : IValidatableObject
    {
        [Required(ErrorMessage = "The First name field is required."), RegularExpression("[a-zA-Z]*", ErrorMessage = "The First name field is not a valid name.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "The Last name field is required."), RegularExpression("[a-zA-Z]*", ErrorMessage = "The Last name field is not a valid name.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "The Email field is required."), EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The Birth date field is required.")]
        public DateOnly? BirthDate { get; set; }

        [Required(ErrorMessage = "The Flight has to be selected."), RegularExpression("^(\\{){0,1}[0-9a-fA-F]{8}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{12}(\\}){0,1}$", ErrorMessage = "The Flight Id field is not a valid Guid.")]
        public string? FlightId { get; set; }

        public string? Coupon { get; set; }

        [Required(ErrorMessage = "The Discount field is required.")]
        public Discount Discount { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            if (now < BirthDate.Value.AddYears(15))
            {
                yield return new ValidationResult("Customer can not be younger than 15 years.");
            }
            
            if(BirthDate.Value.AddYears(150) < now)
            {
                yield return new ValidationResult("Customer can not be older than 150 years.");
            }

            if (Coupon is not null && !Globals.AcceptedCoupons.ContainsKey(Coupon))
            {
                yield return new ValidationResult("Invalid coupon.");
            }

            if (Discount == Discount.Student && BirthDate.Value.AddYears(26) < now)
            {
                yield return new ValidationResult("To apply student discount customer can not be older than 26 years.");
            }

            if (Discount == Discount.Senior && now < BirthDate.Value.AddYears(65))
            {
                yield return new ValidationResult("To apply senior discount customer can not be younger than 65 years.");
            }

        }
    }
}

