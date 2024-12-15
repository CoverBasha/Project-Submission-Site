using Microsoft.AspNetCore.Mvc;
using Project_Submission_Site.Models;
using Project_Submission_Site.Models.Accounts;

namespace Project_Submission_Site.Controllers
{
    public class LoginController : Controller
    {
        ApplicationContext _context;
        public LoginController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Empty()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginSignUpViewModel viewModel)
        {
            Account account = _context.Admins.SingleOrDefault(a => a.Email == viewModel.Email && a.Password == viewModel.Password);
            if (account == null)
                account = _context.Referees.SingleOrDefault(a => a.Email == viewModel.Email && a.Password == viewModel.Password);
            if (account == null)
                account = _context.Users.SingleOrDefault(a => a.Email == viewModel.Email && a.Password == viewModel.Password);

            if (account is User)
                return RedirectToAction("Home", "UserHome", (User)account);
            if (account is Referee)
                return RedirectToAction("Home", "RefereeHome", (Referee)account);
            if (account is Admin)
                return RedirectToAction("Home", "AdminHome", (Admin)account);

            return RedirectToAction("Empty");

        }

        [HttpPost]
        public ActionResult SignUp(LoginSignUpViewModel viewModel)
        {

            if (_context.Admins.SingleOrDefault(m => m.Email == viewModel.Email) != null)
                return RedirectToAction("Empty");
            if (_context.Referees.SingleOrDefault(m => m.Email == viewModel.Email) != null)
                return RedirectToAction("Empty");
            if (_context.Users.SingleOrDefault(m => m.Email == viewModel.Email) != null)
                return RedirectToAction("Empty");

            Account account;

            if (viewModel.Type == "user")
            {
                account = new User()
                {
                    Email = viewModel.Email,
                    Username = viewModel.Username,
                    Password = viewModel.Password
                };
                _context.Users.Add((User)account);
                _context.SaveChanges();
            }
            else
            {
                account = new Referee()
                {
                    Email = viewModel.Email,
                    Username = viewModel.Username,
                    Password = viewModel.Password
                };
                _context.Referees.Add((Referee)account);
                _context.SaveChanges();
            }


            return RedirectToAction("Index", "Home", account);
        }
    }
}
