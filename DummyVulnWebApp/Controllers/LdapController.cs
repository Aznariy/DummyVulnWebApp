using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace DummyVulnWebApp.Controllers
{
    public class LdapController: Controller
    {
        public ActionResult CheckUserExists(string username)
        {
            bool userExists = false;
            string message = "";

            try
            {
                // Define the LDAP path, domain, and filter with the username
                string ldapPath = "LDAP://ldapservermma";
                string ldapFilter = $"(sAMAccountName={username})"; // This assumes you're using sAMAccountName as the username

                using (DirectoryEntry entry = new DirectoryEntry(ldapPath))
                using (DirectorySearcher searcher = new DirectorySearcher(entry))
                {
                    searcher.Filter = ldapFilter;
                    searcher.PropertiesToLoad.Add("sAMAccountName");

                    // Find the user in the directory
                    SearchResult result = searcher.FindOne();
                    userExists = result != null;
                }

                message = userExists ? $"{username} exists in the directory." : $"{username} does not exist in the directory.";
            }
            catch (Exception ex)
            {

                message = $"An error occurred for {username}: " + ex.Message;
            }

            // Pass the result to the view
            ViewBag.Message = message;
            ViewBag.UserExists = userExists;

            return View();
        }
    }
}
