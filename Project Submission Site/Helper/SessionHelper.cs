using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Project_Submission_Site.Models;
using System.Security.Policy;

namespace ServiceProvider.Helpers
{
    public static class SessionHelper
    {
        private const string UserInfoKey = "UserId";

        public static void SetSession(Controller controller, int userid)
        {
            controller.HttpContext.Session.SetInt32(UserInfoKey, userid);
        }

        public static Account? GetSession(Controller controller, ApplicationContext context)
        {
            int? userid = controller.HttpContext.Session.GetInt32(UserInfoKey);
            if (userid == null || userid == 0)
                return null;

            Account? user = context.Users.FirstOrDefault(x => x.Id == userid);
            if (user == null)
                user = context.Referees.FirstOrDefault(x => x.Id == userid);
            if (user == null)
                user = context.Admins.FirstOrDefault(x => x.Id == userid);

            return user;
        }

        public static void DeleteSession(Controller controller)
        {
            controller.HttpContext.Session.Remove(UserInfoKey);
        }

        public static bool IsLoggedIn(Controller controller, ApplicationContext context, bool reqadmin = false)
        {
            return IsLoggedIn(GetSession(controller, context), reqadmin);
        }

        public static bool IsLoggedIn(Account? user, bool reqadmin = false)
        {
            if (user != null)
            {
                if (!reqadmin || user is Admin)
                    return true;
            }
            return false;
        }
    }
}
