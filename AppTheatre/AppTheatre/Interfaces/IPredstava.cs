using AppTheatre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTheatre.Interfaces
{
    public interface IPredstava
    {
        Predstava GetById(int id);
        IEnumerable<Predstava> GetAll();
        void Add(Predstava predstava);
        void Update(Predstava predstava);
        void Delete(Predstava predstava);

        IEnumerable<Predstava> PostPretraga(DateTime min, DateTime max);
        IEnumerable<Predstava> GetNaziv(string naziv);
        IEnumerable<Predstava> GetZanrCena(string zanr, decimal cena);
        IEnumerable<Predstava> GetCenaVreme(decimal cena, DateTime vreme);
    }
}
