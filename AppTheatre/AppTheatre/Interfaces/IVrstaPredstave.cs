using AppTheatre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTheatre.Interfaces
{
    public interface IVrstaPredstave
    {
        VrstaPredstave GetById(int id);
        IEnumerable<VrstaPredstave> GetAll();
        void Add(VrstaPredstave vrstaPredstave);
        void Update(VrstaPredstave vrstaPredstave);
        void Delete(VrstaPredstave vrstaPredstave);

        IEnumerable<VrstaPredstave> GetByNaziv(string naziv);



    }
}
