using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project_Submission_Site.Models;
using ServiceProvider.Helpers;

namespace Project_Submission_Site.Controllers
{
	public class ProfileController : Controller
	{
		ApplicationContext _context;

        public ProfileController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Empty()
		{
			Account? account = SessionHelper.GetSession(this, _context);
			if (account == null)
				return RedirectToAction("Empty", "Login");

			return View(account);
		}

		[HttpPost]
		public IActionResult Update(Account user, string NewPassword, string ConfirmPassword)
		{
			Account? account = SessionHelper.GetSession(this, _context);
			if (account == null)
				return RedirectToAction("Empty", "Login");

            if (!string.IsNullOrEmpty(NewPassword) && NewPassword != ConfirmPassword)
				ModelState.AddModelError("Password", "Your password and confirmation do not match.");
            else if (ModelState.GetValidationState("Password") == ModelValidationState.Valid && user.Password != account.Password)
				ModelState.AddModelError("Password", "The password confirmation you entered is incorrect.");

			if (
				ModelState.GetValidationState("Email") == ModelValidationState.Valid
				&& ModelState.GetValidationState("Username") == ModelValidationState.Valid
                && ModelState.GetValidationState("Password") == ModelValidationState.Valid
                )
			{
				account.Email = user.Email;
				account.Username = user.Username;

				if (!string.IsNullOrEmpty(NewPassword))
					account.Password = NewPassword;

				_context.SaveChanges();

				return RedirectToAction("Empty");
			}
			return View("Empty");
		}
	}
}
