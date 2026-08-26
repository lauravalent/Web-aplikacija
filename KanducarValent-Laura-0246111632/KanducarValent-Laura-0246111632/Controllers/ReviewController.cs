using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rad.DAL;
using Rad.Model;
using System.Security.Claims;

namespace KanducarValent_Laura_0246111632.Controllers
{
    [Authorize]
    public class ReviewController(
        GuestManagerDbContext _dbContext) : Controller
    {
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reviews = new List<Review>();
            if (User.IsInRole("Owner"))
            {
                reviews = _dbContext.Reviews
                    .Include(r => r.Accomodation)
                    .ToList();
            }
            else
            {
                reviews = _dbContext.Reviews
                    .Where(r => r.UserId == userId)
                    .Include(r => r.Accomodation)
                    .ToList();
            }

            return View(reviews);
        }
        [Authorize(Roles = "Guest")]
        public IActionResult Create(int? reservationId = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (reservationId.HasValue)
            {
                var reservation = _dbContext.Reservations
                    .Include(r => r.Accomodation)
                    .FirstOrDefault(r => r.ID == reservationId.Value);

                if (reservation == null)
                    return NotFound();

                if (reservation.UserId != userId)
                    return Forbid();

                ViewBag.ReservationValue = $"{reservation.Accomodation.Name} ({reservation.StartDate:dd.MM.yyyy} - {reservation.EndDate:dd.MM.yyyy})";

                var model = new Review
                {
                    ReservationID = reservationId.Value
                };

                return View(model);
            }

            ReservationDropdown(userId);
            return View();
        }
        [Authorize(Roles = "Guest")]
        [HttpPost]
        public IActionResult Create(Review model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            model.UserId = userId;

            ModelState.Remove("UserId");
            ModelState.Remove("User");
            ModelState.Remove("Accomodation");
            ModelState.Remove("AccomodationID");

            var reservation = _dbContext.Reservations
                .FirstOrDefault(r => r.ID == model.ReservationID && r.UserId == userId);

            if (reservation == null)
            {
                ModelState.AddModelError("ReservationID", "Rezervacija se ne može pronaći.");
            }

            else
            {
                model.AccomodationID = reservation.AccomodationID;
                bool alreadyReviewed = _dbContext.Reviews.Any(r => r.ReservationID == reservation.ID);
                if (alreadyReviewed)
                    ModelState.AddModelError("ReservationID", "Ovaj boravak ste već recenzirali.");
            }

            if (ModelState.IsValid)
            {
                _dbContext.Reviews.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            if (reservation != null)
                ViewBag.ReservationValue =
                    $"Kućica {reservation.Accomodation?.Name}, boravak od {reservation.StartDate:dd.MM.yyyy} do {reservation.EndDate:dd.MM.yyyy}";
            else
                ReservationDropdown(userId);

            return View(model);
        }


        private void ReservationDropdown(string userId)
        {
            var available = _dbContext.Reservations
                .Include(r => r.Accomodation)
                .Where(r => r.UserId == userId && r.EndDate < DateTime.Now)
                .Where(r => !_dbContext.Reviews.Any(rev => rev.ReservationID == r.ID))
                .ToList();

            var selectItems = available.Select(r => new SelectListItem(
                $"Kućica {r.Accomodation?.Name}, boravak od {r.StartDate:dd.MM.yyyy} do {r.EndDate:dd.MM.yyyy}",
                r.ID.ToString()
            )).ToList();

            ViewBag.NonreviewedReservations = selectItems;
        }
       

        public IActionResult ByAccomodation(int id)
        {
            var reviews = _dbContext.Reviews
                .Where(r => r.AccomodationID == id)
                .Include(r => r.User)
                .ToList();

            return PartialView("_ReviewsPartial", reviews);
        }
        [Authorize(Roles = "Guest")]
        [ActionName(nameof(Edit))]
        public IActionResult Edit(int id)
        {
            var model = _dbContext.Reviews
                .Include(r => r.Reservation)
                    .ThenInclude(res => res.Accomodation)
                .FirstOrDefault(c => c.ID == id);

            if (model == null)
                return RedirectToAction(nameof(Index));

            ViewBag.ReservationValue =
                $"Kućica {model.Reservation?.Accomodation?.Name}, boravak od {model.Reservation?.StartDate:dd.MM.yyyy} do {model.Reservation?.EndDate:dd.MM.yyyy}";

            return View(model);
        }
        [Authorize(Roles = "Guest")]
        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> EditPost(int id)
        {
            var res = _dbContext.Reviews
                .Include(r => r.Reservation)
                    .ThenInclude(res => res.Accomodation)
                .Single(c => c.ID == id);

            ModelState.Remove("User");
            ModelState.Remove("UserId");
            ModelState.Remove("Accomodation");
            ModelState.Remove("AccomodationID");
            ModelState.Remove("Reservation");
            ModelState.Remove("ReservationID");

            var ok = await this.TryUpdateModelAsync(res, "", r => r.Title, r => r.Content, r => r.Rating);

            if (ok && this.ModelState.IsValid)
            {
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
