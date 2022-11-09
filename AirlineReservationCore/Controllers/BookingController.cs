using AirlineReservationCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationCore.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly AppDbContext context;
        private readonly IFlightRepository _flightRepo;
        public BookingController(IBookingRepository bookingRepo, AppDbContext context, IFlightRepository flightRepo)
        {
            _bookingRepo = bookingRepo;
            this.context = context;
            _flightRepo = flightRepo;
        }
        [HttpGet]
        public IActionResult BookFlight(int id)
        {
            ViewData["FlightId"] = id;
            ViewData["UserId"] = Convert.ToInt32(Request.Cookies["UserId"]);
            return View();
        }
        [HttpPost]
        public IActionResult BookFlight(Booking Booking)
        {
            if (ModelState.IsValid)
            {
                Flight flight = context.Flights.FirstOrDefault(m => m.Id == Booking.FlightId);
                if(flight.AvailableSeats == 0)
                {
                    ViewData["Error"] = "Flight is already full.";
                    return View();
                }
                else if(flight.AvailableSeats < Booking.Seats)
                {
                    ViewData["Error"] = "Flight does not have " + Booking.Seats.ToString() + " seats available.";
                    return View();
                }
                Booking.Cost = Booking.Seats * flight.Price;
                flight = _flightRepo.UpdateSeats(flight, Booking.Seats);
                Booking newBooking = _bookingRepo.Add(Booking);
                return RedirectToAction("Home", "User");
            }
            return View();
        }
        public IActionResult Index()
        {
            var model = _bookingRepo.GetAllBookings();
            return View(model);
        }
        public IActionResult Details(int id)
        {
            Booking booking = _bookingRepo.GetBooking(id);
            if (booking == null)
            {
                Response.StatusCode = 404;
                return View("userNotFound", id);
            }
            booking.User = context.Users.FirstOrDefault(m => m.Id == booking.UserId);
            booking.Flight = context.Flights.FirstOrDefault(m => m.Id == booking.FlightId);
            return View(booking);
        }
        public IActionResult UserBooking(int id)
        {
            IEnumerable<Booking> userBookings = _bookingRepo.GetUserBookings(id);
            List<Booking> temp = userBookings.ToList();
            foreach(var item in temp)
            {
                item.Flight = _flightRepo.GetFlight(item.FlightId);
            }
            return View(temp);
        }
    }
}
