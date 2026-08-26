using Rad.Model;
using System.ComponentModel.DataAnnotations;

namespace KanducarValent_Laura_0246111632.Models
{
    public class AccomodationFilterModel
    {
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Kapacitet je obavezan")]
        [Range(1, 20, ErrorMessage = "Broj osoba mora biti između 1 i 20.")]
        public int Capacity { get; set; }

        public List<Accomodation> Results { get; set; } = new();
        public bool NoResult { get; set; }

    }
}
