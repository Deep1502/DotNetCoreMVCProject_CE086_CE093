using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationCore.Models
{
    public class SQLFlightRepository : IFlightRepository
    {
        private readonly AppDbContext context;
        public SQLFlightRepository(AppDbContext context)
        {
            this.context = context;
        }
        Flight IFlightRepository.Add(Flight Flight)
        {
            context.Flights.Add(Flight);
            context.SaveChanges();
            return Flight;
        }
        Flight IFlightRepository.Delete(int Id)
        {
            Flight flight = context.Flights.Find(Id);
            if (flight != null)
            {
                context.Flights.Remove(flight);
                context.SaveChanges();
            }
            return flight;
        }
        IEnumerable<Flight> IFlightRepository.GetAllFlights()
        {
            return context.Flights;
        }
        Flight IFlightRepository.GetFlight(int Id)
        {
            return context.Flights.FirstOrDefault(m => m.Id == Id);
        }
        Flight IFlightRepository.Update(Flight FlightChanges)
        {
            var flight = context.Flights.Attach(FlightChanges);
            flight.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return FlightChanges;
        }
        Flight IFlightRepository.UpdateSeats(Flight flight, int Seats)
        {
            flight.AvailableSeats -= Seats;
            var flight1 = context.Flights.Attach(flight);
            flight1.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return flight;
        }
    }
}
