using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace DummyVulnWebApp.Controllers
{
    public class LdapController2 : Controller
    {
        public ActionResult CheckItemIsAvailable(string item)
        {
            bool itemExists = false;
            string message = "";

            try
            {
                string ldapPath = "LDAP://ldapservermma";
                string ldapFilter = $"(sAMAccountName={item})";

                using (DirectoryEntry entry = new DirectoryEntry(ldapPath))
                using (DirectorySearcher searcher = new DirectorySearcher(entry))
                {
                    searcher.Filter = ldapFilter;
                    searcher.PropertiesToLoad.Add("sAMAccountName");

                    // Find the user in the directory
                    SearchResult result = searcher.FindOne();
                    itemExists = result != null;
                }

                message = itemExists ? $"{item} exists in the shelf." : $"{item} does not exist in the shelf.";
            }
            catch (Exception ex)
            {

                message = $"An error occurred for {item}: " + ex.Message;
            }

            // Pass the result to the view
            ViewBag.Message = message;
            ViewBag.UserExists = itemExists;

            return View();
        }
    }
}
