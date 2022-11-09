using AirlineReservationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineReservationCore.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public IActionResult Index()
        {
            var model = _userRepo.GetAllUsers();
            return View(model);
        }
        public ViewResult Details(int id)
        {
            User user = _userRepo.GetUser(id);
            if(user == null)
            {
                Response.StatusCode = 404;
                return View("userNotFound", id);
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult Register()
        {
            if (Request.Cookies["UserId"] != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                User existingUser = _userRepo.GetUser(user.Email);
                if(existingUser != null)
                {
                    ViewData["Error"] = "Email is already taken.";
                    return View();
                }
                else
                {
                    User newUser = _userRepo.Add(user);
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            User user = _userRepo.GetUser(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(User model)
        {
            if (ModelState.IsValid)
            {
                User user = _userRepo.GetUser(model.Id);
                user.Name = model.Name;
                user.DOB = model.DOB;
                user.Email = model.Email;
                user.Password = model.Password;
                user.ConfirmPassword = model.ConfirmPassword;
                User updateUser = _userRepo.Update(user);
                return RedirectToAction("Details", new { id = updateUser.Id});
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            if(Request.Cookies["UserId"] != null)
            {
                if(Request.Cookies["Role"] == "Admin")
                {
                    return RedirectToAction("AdminHome");
                }
                else
                {
                    return RedirectToAction("Home");
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                User user = _userRepo.GetUser(loginModel.Email, loginModel.Password);
                if(user == null)
                {
                    ViewData["Error"] = "Invalid email or password.";
                    return View();
                }
                else if(loginModel.Email == "admin@airline.com" && loginModel.Password == "Admin")
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(10);
                    Response.Cookies.Append("UserId", user.Id.ToString(), option);
                    Response.Cookies.Append("Role", "Admin", option);
                    return RedirectToAction("AdminHome");
                }
                else
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(10);
                    Response.Cookies.Append("UserId", user.Id.ToString(), option);
                    Response.Cookies.Append("Role", "Client", option);
                    return RedirectToAction("Home");
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("UserId");
            Response.Cookies.Delete("Role");
            return RedirectToAction("Login");
        }
        public IActionResult Home()
        {
            if (Request.Cookies["Role"] == null)
            {
                return RedirectToAction("Login");
            }
            else if (Request.Cookies["Role"] == "Admin")
            {
                return RedirectToAction("AdminHome");
            }
            int userId = Convert.ToInt32(Request.Cookies["UserId"]);
            User user = _userRepo.GetUser(userId);
            return View(user);
        }
        public IActionResult AdminHome()
        {
            if (Request.Cookies["Role"] == null)
            {
                return RedirectToAction("Login");
            }
            else if(Request.Cookies["Role"] == "Client")
            {
                return RedirectToAction("Home");
            }
            return View();
        }
    }
}
