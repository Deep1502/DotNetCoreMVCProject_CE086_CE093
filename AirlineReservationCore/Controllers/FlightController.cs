using AirlineReservationCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationCore.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightRepository _flightRepo;
        public FlightController(IFlightRepository flightRepo)
        {
            _flightRepo = flightRepo;
        }
        public IActionResult Index()
        {
            var model = _flightRepo.GetAllFlights();
            return View(model);
        }
        public IActionResult ViewFlights()
        {
            var model = _flightRepo.GetAllFlights();
            return View(model);
        }
        public ViewResult Details(int id)
        {
            Flight flight = _flightRepo.GetFlight(id);
            if (flight == null)
            {
                Response.StatusCode = 404;
                return View("userNotFound", id);
            }
            return View(flight);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Flight flight)
        {
            if (ModelState.IsValid)
            {
                Flight newFlight = _flightRepo.Add(flight);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Flight flight = _flightRepo.GetFlight(id);
            return View(flight);
        }
        [HttpPost]
        public IActionResult Edit(Flight model)
        {
            if (ModelState.IsValid)
            {
                Flight flight = _flightRepo.GetFlight(model.Id);
                flight.Name = model.Name;
                flight.TotalSeats = model.TotalSeats;
                flight.AvailableSeats = model.AvailableSeats;
                flight.Price = model.Price;
                flight.Source = model.Source;
                flight.SourceTime = model.SourceTime;
                flight.Destination = model.Destination;
                flight.DestinationTime = model.DestinationTime;
                Flight updateFlight = _flightRepo.Update(flight);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Flight flight = _flightRepo.GetFlight(id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var flight = _flightRepo.GetFlight(id);
            _flightRepo.Delete(flight.Id);
            return RedirectToAction("Index");
        }
    }
}
