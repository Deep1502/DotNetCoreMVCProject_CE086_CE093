using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationCore.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
        public DateTime BookingDate { get; set; }
        [Range(1, int.MaxValue)]
        public int Seats { get; set; }
        public int Cost { get; set; }
    }
}
