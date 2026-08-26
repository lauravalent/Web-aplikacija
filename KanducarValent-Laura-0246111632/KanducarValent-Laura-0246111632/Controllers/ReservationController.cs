using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rad.DAL;
using Rad.Model;
using System.Globalization;
using System.Security.Claims;

namespace KanducarValent_Laura_0246111632.Controllers
{
    [Authorize]
    public class ReservationController (
        GuestManagerDbContext _dbContext) : Controller
    {
        [Route("moje-rezervacije")]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reservations = new List<Reservation>();

            if (User.IsInRole("Owner"))
            {
                reservations = _dbContext.Reservations
                    .Include(r => r.Accomodation)
                    .ToList();
            }
            else
            {
                reservations = _dbContext.Reservations
                    .Where(r => r.UserId == userId)
                    .Include(r => r.Accomodation)
                    .ToList();
            }
            return View(reservations);
        }
        public IActionResult Create()
        {
            this.FillDropdownValues();
            return View();
        }

        [HttpGet]
        public IActionResult Create(int accomodationId, DateTime? startDate, DateTime? endDate, int? numberOfGuests)
        {
            var accomodation = _dbContext.Accomodations.Find(accomodationId);
            if (accomodation == null)
                return NotFound();
            if (numberOfGuests > accomodation.Capacity)
            {
                numberOfGuests = accomodation.Capacity;
            }
            var model = new Reservation
            {
                AccomodationID = accomodationId,
                StartDate = startDate ?? default,
                EndDate = endDate ?? default,
                NumberOfGuests = numberOfGuests ?? 1
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Reservation model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.UserId = userId;
            ModelState.Remove("UserId");

            var accomodation = _dbContext.Accomodations.FirstOrDefault(a => a.ID == model.AccomodationID);
            if (accomodation == null)
                return NotFound();


            if (model.NumberOfGuests > accomodation.Capacity)
            {
                ModelState.AddModelError("NumberOfGuests", $"Broj gostiju ne može biti veći od kapaciteta ({accomodation.Capacity}).");
            }

            var overlappingReservation = _dbContext.Reservations
                .Where(r => r.AccomodationID == model.AccomodationID)
                .Any(r =>
                    model.StartDate < r.EndDate &&
                    model.EndDate > r.StartDate
                );

            if (overlappingReservation)
            {
                ModelState.AddModelError("", "Odabrani termin je zauzet. Odaberite drugi.");
            }

            if (ModelState.IsValid)
            {
                _dbContext.Reservations.Add(model);
                _dbContext.SaveChanges();

                var acc = _dbContext.Accomodations.FirstOrDefault(a => a.ID == model.AccomodationID);

                if (acc != null)
                {
                    var days = (model.EndDate - model.StartDate)?.Days;
                    var total = days * acc.PricePerNight;

                    TempData["ReservationTotal"] =
                        total?.ToString("F2", CultureInfo.InvariantCulture) ?? "0.00";
                    TempData["ReservationDays"] = days.ToString();
                }

                return RedirectToAction("Confirmation");
            }


            FillDropdownValues();
            return View(model);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet]
        public IActionResult CreateManual()
        {
            var model = new Reservation();

            FillAccomodationDropdown();
            return View(model);
        }

        [Authorize(Roles = "Owner")]
        [HttpPost]
        public IActionResult CreateManual(Reservation model)
        {
            var accomodation = _dbContext.Accomodations.Find(model.AccomodationID);
            if (accomodation == null)
                return NotFound();

            ModelState.Remove("Accomodation");
            ModelState.Remove("User");
            ModelState.Remove("UserId");


            if (model.NumberOfGuests > accomodation.Capacity)
            {
                ModelState.AddModelError("NumberOfGuests", $"Broj gostiju ne može biti veći od kapaciteta ({accomodation.Capacity}).");
            }

            model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var overlapping = _dbContext.Reservations
                .Any(r => r.AccomodationID == model.AccomodationID &&
                          model.StartDate < r.EndDate &&
                          model.EndDate > r.StartDate);

            if (overlapping)
            {
                ModelState.AddModelError("", "Odabrani termin je zauzet. Odaberite drugi.");
            }

            if (ModelState.IsValid)
            {
                _dbContext.Reservations.Add(model);
                _dbContext.SaveChanges();
             
                return RedirectToAction(nameof(Index));
            }

            FillAccomodationDropdown();
            return View(model);
        }

        private void FillAccomodationDropdown()
        {
            ViewBag.Accomodations = new SelectList(_dbContext.Accomodations, "ID", "Name");
        }
    

        public IActionResult Confirmation()
        {
            return View();
        }


        private void FillDropdownValues(int? selectedId = null)
        {
            var selectItems = new List<SelectListItem>();

            var listItem = new SelectListItem();
            listItem.Text = "odaberite";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var category in _dbContext.Accomodations)
            {
                listItem = new SelectListItem(category.Name, category.ID.ToString())
                {
                    Selected = category.ID == selectedId
                };
                selectItems.Add(listItem);
            }

            ViewBag.Accomodations = selectItems;
        }
        [ActionName(nameof(Edit))]
        public IActionResult Edit(int id)
        {
            var model = _dbContext.Reservations
                .FirstOrDefault(c => c.ID == id);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId != model.UserId)
            {
                return Forbid();
            }
            return View(model);
        }
        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> EditPost(int id)
        {
            var res = _dbContext.Reservations
                .Single(c => c.ID == id);
            var ok = await this.TryUpdateModelAsync(res);

            if (ok && this.ModelState.IsValid)
            {
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [Authorize(Roles = "Owner")]
        [HttpGet]
        public IActionResult EditManual(int id)
        {
            var model = _dbContext.Reservations
                .FirstOrDefault(c => c.ID == id);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != model.UserId)
            {
                return Forbid();
            }
            FillDropdownValues(model.AccomodationID);
            return View(model);
        }
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [ActionName(nameof(EditManual))]
        public async Task<IActionResult> EditManualPost(int id)
        {
            var res = _dbContext.Reservations
                .Single(c => c.ID == id);
            var ok = await this.TryUpdateModelAsync(res);

            if (ok)
            {
                var accomodation = _dbContext.Accomodations.Find(res.AccomodationID);

                if (accomodation == null)
                {
                    return NotFound();
                }

                if (res.NumberOfGuests > accomodation.Capacity)
                {
                    ModelState.AddModelError("NumberOfGuests", $"Broj gostiju ne može biti veći od kapaciteta ({accomodation.Capacity}).");
                }

                var overlapping = _dbContext.Reservations
                    .Any(r => r.AccomodationID == res.AccomodationID &&
                              r.ID != res.ID &&
                              res.StartDate < r.EndDate &&
                              res.EndDate > r.StartDate);

                if (overlapping)
                {
                    ModelState.AddModelError("", "Odabrani termin je zauzet. Odaberite drugi.");
                }
            }


            if (ok && this.ModelState.IsValid)
            {
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            FillDropdownValues(res.AccomodationID);
            return View(res);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var reservation = _dbContext.Reservations
                .Include(r => r.Accomodation)
                .FirstOrDefault(r => r.ID == id);

            if (reservation == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isOwner = User.IsInRole("Owner");

            if (!isOwner && reservation.UserId != userId)
                return Forbid();

            return View(reservation);
        }

        [Authorize]
        [HttpPost]
        [ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var reservation = _dbContext.Reservations.Find(id);

            if (reservation == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isOwner = User.IsInRole("Owner");

            if (!isOwner && reservation.UserId != userId)
                return Forbid();

            _dbContext.Reservations.Remove(reservation);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
