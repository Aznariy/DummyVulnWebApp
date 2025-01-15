using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace DummyVulnWebApp.Controllers
{
    public class LdapController3 : Controller
    {
        public ActionResult CheckIPersonIsemployed(string person)
        {
            bool personExists = false;
            string message = "";

            try
            {
                string ldapPath = "LDAP://ldapservermma";
                string ldapFilter = $"(sAMAccountName={person})";

                using (DirectoryEntry entry = new DirectoryEntry(ldapPath))
                using (DirectorySearcher searcher = new DirectorySearcher(entry))
                {
                    searcher.Filter = ldapFilter;
                    searcher.PropertiesToLoad.Add("sAMAccountName");

                    // Find the user in the directory
                    SearchResult result = searcher.FindOne();
                    personExists = result != null;
                }

                message = personExists ? $"{person} exists in the shelf." : $"{person} does not exist in the shelf.";
            }
            catch (Exception ex)
            {

                message = $"An error occurred for {person}: " + ex.Message;
            }

            // Pass the result to the view
            ViewBag.Message = message;
            ViewBag.UserExists = personExists;

            return View();
        }
    }
}
