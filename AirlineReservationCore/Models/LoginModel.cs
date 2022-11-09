using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationCore.Models
{
    public class LoginModel
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
