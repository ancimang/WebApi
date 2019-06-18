using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppTheatre.Models
{
    public class Predstava
    {
        public int Id { get; set; }
        [Required]
        public string Naziv { get; set; }

        [Required]
        public string  ImeRezisera { get; set; }
        [Required]
        public string Opis { get; set; }

        public DateTimeOffset? DatumOdrazavanja { get; set; }
        public DateTimeOffset? VremeOdrzavanja { get; set; }

        public decimal CenaKarte { get; set; }

        public DateTime? Premijera { get; set; }

        //foreign key
        public int VrstaPredstaveId { get; set; }

        //navigation property
        public VrstaPredstave VrstaPredstave { get; set; }

        //foreign key
        public int PozoristeId { get; set; }

        //navigation property
        public Pozoriste Pozoriste { get; set; }




    }
}