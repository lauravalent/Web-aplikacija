using KanducarValent_Laura_0246111632.Services;
using Microsoft.AspNetCore.Mvc;
using Rad.Model;

namespace KanducarValent_Laura_0246111632.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IEmailService _emailService;

        public ContactController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(Contact model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            await _emailService.SendEmailAsync(model);

            TempData["Success"] = "Poruka je uspješno poslana!";
            return RedirectToAction("Index", "Home");
        }
    }
}

