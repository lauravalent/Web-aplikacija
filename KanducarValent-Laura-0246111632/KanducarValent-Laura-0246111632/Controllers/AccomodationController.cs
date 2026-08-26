using KanducarValent_Laura_0246111632.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Rad.DAL;
using Rad.Model;

namespace KanducarValent_Laura_0246111632.Controllers
{
    public class AccomodationController(GuestManagerDbContext _dbContext) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Filter()
        {
            return View(new AccomodationFilterModel());
        }

        [HttpGet]
        public IActionResult Index(AccomodationFilterModel filter)
        {
            bool filterPrazan = filter.StartDate == null && filter.EndDate == null && filter.Capacity == 0;

            if (filterPrazan)
            {
                filter.Results = _dbContext.Accomodations.ToList();
                return View(filter);
            }

            if (!ModelState.IsValid)
            {
                filter.Results = _dbContext.Accomodations.ToList();
                return View(filter);
            }
            if (filter.StartDate.HasValue && filter.StartDate.Value.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError(nameof(filter.StartDate), "Datum dolaska ne može biti u prošlosti.");
            }

            if (filter.EndDate.HasValue && filter.EndDate.Value.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError(nameof(filter.EndDate), "Datum odlaska ne može biti u prošlosti.");
            }

            if (filter.StartDate.HasValue && filter.EndDate.HasValue && filter.EndDate <= filter.StartDate)
            {
                ModelState.AddModelError(nameof(filter.EndDate), "Datum odlaska mora biti nakon datuma dolaska.");
            }

            if (filter.StartDate.HasValue && filter.EndDate.HasValue && filter.EndDate <= filter.StartDate)
            {
                ModelState.AddModelError(nameof(filter.EndDate), "Datum odlaska mora biti nakon datuma dolaska");
                //filter.Results = _dbContext.Accomodations.ToList();
                //return View(filter);
            }

            var query = _dbContext.Accomodations.AsQueryable();

            if (filter.StartDate.HasValue && filter.EndDate.HasValue)
            {
                query = query.Where(a => !_dbContext.Reservations.Any(r =>
                    r.AccomodationID == a.ID &&
                    filter.StartDate < r.EndDate &&
                    filter.EndDate > r.StartDate));
            }

            var available = query.OrderByDescending(a => a.Capacity).ToList();

            filter.Results = available;
            filter.NoResult = available.Any() && !available.Any(a => a.Capacity >= filter.Capacity);

            return View(filter);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Accomodation model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Accomodations.Add(model);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            else
            {
                return View();
            }


        }
        [Authorize(Roles = "Owner")]
        [ActionName(nameof(Edit))]
        public IActionResult Edit(int id)
        {
            var model = _dbContext.Accomodations
                .FirstOrDefault(c => c.ID == id);
            return View(model);
        }
        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> EditPost(int id)
        {
            var acco = _dbContext.Accomodations
                .Single(c => c.ID == id);
            var ok = await this.TryUpdateModelAsync(acco);

            if (ok && this.ModelState.IsValid)
            {
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Details(int id, DateTime? startDate, DateTime? endDate, int? numberOfGuests)
        {
            var accomodation = _dbContext.Accomodations
               .Include(a => a.Reviews)
               .Include(a => a.Photo)
               .FirstOrDefault(a => a.ID == id);

            if (accomodation == null)
                return NotFound();

            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            ViewBag.NumberOfGuests = numberOfGuests;

            var reviews = _dbContext.Reviews
                .Include(r => r.Reservation)
                .Where(r => r.Reservation.AccomodationID == id)
                .OrderByDescending(r => r.ID)
                .Take(3)
                .ToList();

            ViewBag.Reviews = reviews;

            return View(accomodation);
 
        }


    }

}
