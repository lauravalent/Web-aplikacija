using Rad.Model;
using System.ComponentModel.DataAnnotations;

namespace KanducarValent_Laura_0246111632.Models
{
    public class ReservationViewModel
    {
        public Accomodation Accomodation { get; set; }
        public int AccomodationId { get; set; }
        [Required(ErrorMessage = "Datum dolaska je obavezan")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Datum odlaska je obavezan")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Ime gosta je obavezan")]
        public string GuestName { get; set; }
        [Required(ErrorMessage = "Email gosta je obavezan")]
        public string GuestEmail { get; set; }
        [Required(ErrorMessage = "Telefon gosta je obavezan")]
        public string GuestPhone { get; set; }
        [Required(ErrorMessage = "Broj gostiju je obavezan")]
        public int NumberOfGuests { get; set; }

    }
}
