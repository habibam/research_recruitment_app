using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using loginregister.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;



namespace loginregister.Controllers
{
    public class HomeController : Controller
    {

        private MainContext _context;

        public HomeController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // LoginUser newuser = new LoginUser
            // {
       
            //     LogEmail = "habiba@habiba.com",
            //     LogPassword = "password",
            // };
            // return Login(newuser);
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // System.Console.WriteLine(registeredcheck);
                // System.Console.WriteLine("EMAILLL" + user.Email);
                // System.Console.WriteLine("THISSSSS****"+returnedid.id);

                User registeredcheck = _context.users.SingleOrDefault(str => str.Email == user.Email);


                // System.Console.WriteLine("THE EMAILLL", registeredcheck.Email);
                // string email = registeredcheck.Email;
                if (registeredcheck == null)
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user, user.Password);
                    User NewPerson = new User

                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        ConfirmPassword = user.ConfirmPassword,
                        ResearchRole = user.ResearchRole,
                    
                    };

                    _context.Add(NewPerson);
                    _context.SaveChanges();
                    System.Console.WriteLine("NEW PERSON", NewPerson.Name);
                    ViewBag.Success = "You have been added to the database! Please log in now!";
                    return View("Index");

                }
                else
                {
                    System.Console.WriteLine("ALREADY IN THE DATABASE");
                    return View("Index");
                }
            }
            return View("Index");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginUser user)
        {
            User userfound = new User
            {
                Name = "john",
                Email = user.LogEmail,
                Password = user.LogPassword,
                ResearchRole = "SOMET THING",
                
            };
            System.Console.WriteLine("FOUNDUESER" + userfound.Email);
            User loggeduser = _context.users.SingleOrDefault(str => str.Email == userfound.Email);
            if (loggeduser == null)
            {
                ViewBag.loginerror = "Login failed, email and password did not match the information in the database. If you haven't registered please register first!";
                return View("Index");
            }
            else{
                PasswordHasher<User> Hasher = new PasswordHasher<User>();

                if (0 != Hasher.VerifyHashedPassword(loggeduser, loggeduser.Password, userfound.Password))
                {

                    HttpContext.Session.SetInt32("loggedperson", (int)loggeduser.UserId);

                    return RedirectToAction("LandingPage", "Study");
                }
                else
                {

                    ViewBag.loginerror = "passwordhashing isnt workin";
                    return View("Index");
                }
            }
        }

        [HttpPost]
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {


            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }


    }
}
