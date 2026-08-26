using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rad.Model
{
    public class Accomodation
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Unesite barem 3 znaka")]
        public string? Name { get; set; } = "";
        [Required]
        public int? Capacity { get; set; }
        [Required]
        public int? Size { get; set; }

        public decimal PricePerNight { get; set; }

        public string? ImageUrl { get; set; }

        public string? PoolImg { get; set; }

        public virtual ICollection<Review>? Reviews { get; set; }

        public virtual ICollection<Reservation>? Reservations { get; set; }

        public string? Description { get; set; } = "";

        public int? PoolDistance { get; set; }

        public ICollection<Photo>? Photo { get; set; }
    }
}
