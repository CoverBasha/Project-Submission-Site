using Microsoft.AspNetCore.Mvc;
using Project_Submission_Site.Models;
using Project_Submission_Site.Models.Accounts;
using System.Net.Mail;
using VerificationService;

namespace Project_Submission_Site.Controllers
{
    public class LoginController : Controller
    {
        ApplicationContext _context;
        GmailVerifyClient _gmailClient;

		public LoginController(ApplicationContext context)
        {
            _context = context;
			_gmailClient = new GmailVerifyClient();
		}

        [NonAction]
        private IActionResult? IsLoggedIn()
        {
			int? userid = HttpContext.Session.GetInt32("UserId");
			if (userid != null && userid > 0)
			{
				Account? user = _context.Admins.SingleOrDefault(a => a.Id == userid);
				if (user == null)
					user = _context.Referees.SingleOrDefault(a => a.Id == userid);
				if (user == null)
					user = _context.Users.SingleOrDefault(a => a.Id == userid);

                if (user != null)
                {
                    if (user is User)
                        return RedirectToAction("Home", "UserHome");
                    if (user is Referee)
                        return RedirectToAction("Home", "RefereeHome");
                    if (user is Admin)
                        return RedirectToAction("Home", "AdminHome");
                }
			}
			return null;
        }

		[HttpGet]
        public IActionResult Empty()
        {
            IActionResult? result = IsLoggedIn();
            if (result != null)
                return result;

			return View();
        }

        [HttpGet]
		public IActionResult Verify()
		{
			IActionResult? result = IsLoggedIn();
			if (result != null)
				return result;

			return View();
		}

		[HttpPost]
        public IActionResult Login(LoginSignUpViewModel viewModel)
        {
			IActionResult? result = IsLoggedIn();
			if (result != null)
				return result;

			Account? account = _context.Admins.SingleOrDefault(a => a.Email == viewModel.Email && a.Password == viewModel.Password);
            if (account == null)
                account = _context.Referees.SingleOrDefault(a => a.Email == viewModel.Email && a.Password == viewModel.Password);
            if (account == null)
                account = _context.Users.SingleOrDefault(a => a.Email == viewModel.Email && a.Password == viewModel.Password);

            if (account != null)
            {
                HttpContext.Session.SetInt32("UserId", account.Id);

                if (account is User)
                    return RedirectToAction("Home", "UserHome");
                if (account is Referee)
                    return RedirectToAction("Home", "RefereeHome");
                if (account is Admin)
                    return RedirectToAction("Home", "AdminHome");
            }

            return RedirectToAction("Empty");

        }

        [HttpPost]
        public IActionResult SignUp(LoginSignUpViewModel viewModel)
        {
			IActionResult? result = IsLoggedIn();
			if (result != null)
				return result;

			if (_context.Admins.SingleOrDefault(m => m.Email == viewModel.Email) != null)
                return RedirectToAction("Empty");
            if (_context.Referees.SingleOrDefault(m => m.Email == viewModel.Email) != null)
                return RedirectToAction("Empty");
            if (_context.Users.SingleOrDefault(m => m.Email == viewModel.Email) != null)
                return RedirectToAction("Empty");

            Account account;

            string key = _gmailClient.SendMail(viewModel.Email, viewModel.Username);
            if (key == null)
				return RedirectToAction("Empty");

			if (viewModel.Type == "user")
            {
                account = new User()
                {
                    Email = viewModel.Email,
                    Username = viewModel.Username,
                    Password = viewModel.Password,
                    Verified = false,
                    VerifyCode = key,
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
                    Password = viewModel.Password,
                    Verified = false,
					VerifyCode = key,
				};

                _context.Referees.Add((Referee)account);
                _context.SaveChanges();
            }

            HttpContext.Session.SetInt32("UserId", account.Id);

			ViewBag.VerifyEmail = account.Email;
            return View("Empty");
        }

        [HttpPost]
        public IActionResult PostVerify(string code)
        {
			int? userid = HttpContext.Session.GetInt32("UserId");
            Account? user = null;

			if (userid != null && userid > 0)
			{
				user = _context.Admins.SingleOrDefault(a => a.Id == userid);
				if (user == null)
					user = _context.Referees.SingleOrDefault(a => a.Id == userid);
				if (user == null)
					user = _context.Users.SingleOrDefault(a => a.Id == userid);
			}

            if (user != null)
            {
                if (!user.VerifyCode.Equals(code, StringComparison.OrdinalIgnoreCase))
                {
					ModelState.AddModelError(string.Empty, "Verification code is incorrect.");

					ViewBag.VerifyEmail = user.Email;
					return View("Empty");
				}

                user.Verified = true;
                _context.SaveChanges();

                if (user is User)
                    return RedirectToAction("Home", "UserHome");
                if (user is Referee)
                    return RedirectToAction("Home", "RefereeHome");
                if (user is Admin)
                    return RedirectToAction("Home", "AdminHome");
            }
			return RedirectToAction("Empty");
		}
	}
}
