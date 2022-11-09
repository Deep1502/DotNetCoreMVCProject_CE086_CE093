using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationCore.Models
{
    public interface IBookingRepository
    {
        Booking GetBooking(int Id);
        IEnumerable<Booking> GetAllBookings();
        IEnumerable<Booking> GetUserBookings(int UserId);
        Booking Add(Booking Booking);
    }
}
