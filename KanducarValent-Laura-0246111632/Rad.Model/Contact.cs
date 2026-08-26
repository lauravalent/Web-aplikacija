using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rad.Model
{
    public class Contact
    {
        [Required(ErrorMessage = "Ime je obavezno.")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Email je obavezan.")]
        [EmailAddress(ErrorMessage = "Neispravan format email adrese.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Poruka je obavezna.")]
        [StringLength (2000, MinimumLength = 10, ErrorMessage = "Poruka mora imati minimalno 10 znakova.")]
        public string Message { get; set; } = "";
    }
}
