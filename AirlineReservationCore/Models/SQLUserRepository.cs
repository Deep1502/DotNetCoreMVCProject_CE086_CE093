using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationCore.Models
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly AppDbContext context;
        public SQLUserRepository(AppDbContext context)
        {
            this.context = context;
        }
        User IUserRepository.Add(User User)
        {
            context.Users.Add(User);
            context.SaveChanges();
            return User;
        }
        IEnumerable<User> IUserRepository.GetAllUsers()
        {
            return context.Users;
        }
        User IUserRepository.GetUser(int Id)
        {
            return context.Users.FirstOrDefault(m => m.Id == Id);
        }
        User IUserRepository.GetUser(string Email)
        {
            return context.Users.FirstOrDefault(m => m.Email == Email);
        }
        User IUserRepository.GetUser(string Email, string Password)
        {
            return context.Users.FirstOrDefault(m => m.Email == Email && m.Password == Password);
        }
        User IUserRepository.Update(User UserChanges)
        {
            var user = context.Users.Attach(UserChanges);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UserChanges;
        }
    }
}
