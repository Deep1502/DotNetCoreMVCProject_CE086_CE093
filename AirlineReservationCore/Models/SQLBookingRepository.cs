using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationCore.Models
{
    public class SQLBookingRepository : IBookingRepository
    {
        private readonly AppDbContext context;
        public SQLBookingRepository(AppDbContext context)
        {
            this.context = context;
        }
        Booking IBookingRepository.GetBooking(int Id)
        {
            return context.Bookings.FirstOrDefault(m => m.Id == Id);
        }
        IEnumerable<Booking> IBookingRepository.GetAllBookings()
        {
            return context.Bookings;
        }
        IEnumerable<Booking> IBookingRepository.GetUserBookings(int UserId)
        {
            return context.Bookings.Where(x => x.UserId == UserId);
        }
        Booking IBookingRepository.Add(Booking Booking)
        {
            Booking b1 = new Booking();
            b1.UserId = Booking.UserId;
            b1.User = context.Users.FirstOrDefault(m => m.Id == b1.UserId);
            b1.FlightId = Booking.FlightId;
            b1.Flight = context.Flights.FirstOrDefault(m => m.Id == b1.FlightId);
            b1.BookingDate = DateTime.Now;
            b1.Seats = Booking.Seats;
            b1.Cost = Booking.Cost;
            context.Bookings.Add(b1);
            context.SaveChanges();
            return Booking;
        }
    }
}
