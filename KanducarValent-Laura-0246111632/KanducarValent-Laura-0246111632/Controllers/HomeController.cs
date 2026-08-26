using System.Diagnostics;
using System.Security.Claims;
using KanducarValent_Laura_0246111632.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rad.DAL;

namespace KanducarValent_Laura_0246111632.Controllers
{
    public class HomeController (GuestManagerDbContext _dbContext) : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("Guest"))
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                var forReview = _dbContext.Reservations
                    .Include(r => r.Accomodation)
                    .Where(r => r.UserId == userId.Value && r.EndDate < DateTime.Now && !_dbContext.Reviews.Any(review => review.ReservationID == r.ID))
                    .OrderByDescending(r => r.EndDate)
                    .FirstOrDefault();

                ViewBag.ForReview = forReview;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
