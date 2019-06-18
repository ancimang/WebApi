using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppTheatre.Models
{
    public class Pozoriste
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Naziv { get; set; }

        [StringLength(30)]
        public string UpravnikImePrezime { get; set; }

        [Range(1850, 2019)]
        public int GodinaOsnivanja { get; set; }

        [Required]
        [StringLength(20)]
        public string Grad { get; set; }

        [Required]
        [StringLength(30)]
        public string Adresa { get; set; }

        //[RegularExpression(@"^\ ?([0-9]{3})\ ?[-. ]?([0-9]{4})[-. ]?([0-9]{3})$")]
        public string Telefon { get; set; }
        
        public string Email { get; set; }



      
    }
}