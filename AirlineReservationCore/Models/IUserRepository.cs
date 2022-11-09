using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationCore.Models
{
    public interface IUserRepository
    {
        User GetUser(int Id);
        User GetUser(string Email);
        User GetUser(string Email, string Password);
        IEnumerable<User> GetAllUsers();
        User Add(User User);
        User Update(User UserChanges);
    }
}
