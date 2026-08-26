using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rad.DAL;
using Rad.Model;

namespace KanducarValent_Laura_0246111632.Controllers
{
    [Route("api/acc")]
    [ApiController]
    public class AccomodationAPIController : Controller
    {
        private GuestManagerDbContext _dbContext;
        public AccomodationAPIController(GuestManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Get()
        {
            var clients = _dbContext.Accomodations.Select(c => new AccomodationDTO
            {
                ID = c.ID,
                Name = c.Name,
                Capacity = (int)c.Capacity,
                Size = (int)c.Size

            })
            .ToList();

            return Ok(clients);
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var client = _dbContext.Accomodations
                .Where(c => c.ID == id)
                .Select(c => new AccomodationDTO
                {
                    ID = c.ID,
                    Name = c.Name

                })
            .FirstOrDefault();

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [Route("pretraga/{q}")]
        public IActionResult Get(string q)
        {
            var client = _dbContext.Accomodations
                .Where(c => c.Name.Contains(q))
                .Select(c => new AccomodationDTO
                {
                    ID = c.ID,
                    Name = c.Name
                })
            .ToList();

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AccomodationDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this._dbContext.Accomodations.Add(new Accomodation()
            {
                Name = model.Name,
                Capacity = model.Capacity,
                Size = model.Size
            });


            this._dbContext.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        [Consumes("application/json")]
        public IActionResult Put(int id, [FromBody] AccomodationDTO model)
        {
            var clientDBO = this._dbContext.Accomodations.First(c => c.ID == id);

            clientDBO.Name = model.Name;
            clientDBO.Capacity = model.Capacity;
            clientDBO.Size = model.Size;
            this._dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var model = this._dbContext.Accomodations.FirstOrDefault(c => c.ID == id);
            if (model == null)
            {
                return NotFound();
            }
            this._dbContext.Remove(model);
            this._dbContext.SaveChanges();
            var clients = _dbContext.Accomodations.Select(c => new AccomodationDTO
            {
                ID = c.ID,
                Name = c.Name,
                Capacity = (int)c.Capacity,
                Size = (int)c.Size

            })
           .ToList();
            return Ok(clients);
        }
    }
}
