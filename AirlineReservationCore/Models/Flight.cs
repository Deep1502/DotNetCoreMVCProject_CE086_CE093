using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationCore.Models
{
    public class Flight
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int TotalSeats { get; set; }
        [Required]
        [LessThan("TotalSeats", ErrorMessage = "AvailableSeats has to be less than TotalSeats.")]
        [Range(0, int.MaxValue)]
        public int AvailableSeats { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public DateTime SourceTime { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        [DateGreaterThan("SourceTime", ErrorMessage = "DestinationTime has to be after SourceTime.")]
        public DateTime DestinationTime { get; set; }
        ICollection<Booking> Bookings { get; set; }
    }
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var currentValue = (DateTime)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if (currentValue < comparisonValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
    public class LessThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public LessThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            int currentValue = (int)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (int)property.GetValue(validationContext.ObjectInstance);

            if (currentValue > comparisonValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
