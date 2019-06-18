using AppTheatre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTheatre.Interfaces
{
    public interface IVeza
    {
        Veza GetById(int id);
        IEnumerable<Veza> GetAll();
        void Add(Veza veza);
        void Update(Veza veza);
        void Delete(Veza veza);

        IEnumerable<Veza> GetByIme(string veza);
        IEnumerable<Veza> GetByPrezime(string veza);
    }
}
