using Microsoft.AspNetCore.Mvc;
using GovSchedulaWeb.Models.ViewModels;
using System.Text.Json; 

namespace GovSchedulaWeb.Controllers
{
    public class ElectoralCommissionController : Controller
    {
        // GET: /ElectoralCommission/Register
        [HttpGet]
        public IActionResult Register()
        {
            var model = TempData.ContainsKey("VoterRegData")
                        && TempData["VoterRegData"] is string jsonData
                        && !string.IsNullOrEmpty(jsonData)
                ? JsonSerializer.Deserialize<VoterRegistrationViewModel>(jsonData)
                : new VoterRegistrationViewModel();

            if (model == null) model = new VoterRegistrationViewModel();
            
            TempData.Keep("VoterRegData"); 
            
            return View(model);
        }

        // POST: /ElectoralCommission/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(VoterRegistrationViewModel model)
        {
            // 4. VALIDATION IS COMMENTED OUT FOR TESTING
            /* if (!ModelState.IsValid)
            {
                return View(model);
            }
            */
            
            TempData["VoterRegData"] = JsonSerializer.Serialize(model);
            return RedirectToAction("ReviewVoterDetails");
        }

        // GET: /ElectoralCommission/ReviewVoterDetails
        [HttpGet]
        public IActionResult ReviewVoterDetails()
        {
            if (!TempData.ContainsKey("VoterRegData") || TempData["VoterRegData"] == null)
            {
                return RedirectToAction("Register"); 
            }

            var jsonData = TempData["VoterRegData"] as string;
            var model = JsonSerializer.Deserialize<VoterRegistrationViewModel>(jsonData ?? "{}");

            if (model == null)
            {
                 return RedirectToAction("Register");
            }
            
            TempData.Keep("VoterRegData"); 
                                 
            return View(model); 
        }

        // POST: /ElectoralCommission/SubmitRegistration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitRegistration()
        {
            if (!TempData.ContainsKey("VoterRegData") || TempData["VoterRegData"] == null)
            {
                return RedirectToAction("Register"); 
            }

            var jsonData = TempData["VoterRegData"] as string;
            var model = JsonSerializer.Deserialize<VoterRegistrationViewModel>(jsonData ?? "{}");

            // TODO: Pass the 'model' to your colleague's backend/database logic
            
            TempData.Remove("VoterRegData");
            return RedirectToAction("RegistrationSuccess");
        }

        // GET: /ElectoralCommission/RegistrationSuccess
        [HttpGet]
        public IActionResult RegistrationSuccess()
        {
            return View();
        }
    }
}