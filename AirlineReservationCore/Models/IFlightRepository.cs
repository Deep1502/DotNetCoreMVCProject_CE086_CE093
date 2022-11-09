using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationCore.Models
{
    public interface IFlightRepository
    {
        Flight GetFlight(int Id);
        IEnumerable<Flight> GetAllFlights();
        Flight Add(Flight Flight);
        Flight Update(Flight FlightChanges);
        Flight Delete(int Id);
        Flight UpdateSeats(Flight flight, int Seats);
    }
}
