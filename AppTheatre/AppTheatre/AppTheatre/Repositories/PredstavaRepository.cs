using AppTheatre.Interfaces;
using AppTheatre.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;

namespace AppTheatre.Repositories
{
    public class PredstavaRepository:IDisposable,IPredstava
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add(Predstava predstava)
        {
            db.Predstave.Add(predstava);
            db.SaveChanges();

        }

        public void Delete(Predstava predstava)
        {
            db.Predstave.Add(predstava);
            db.SaveChanges();
        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;

                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        public IEnumerable<Predstava> GetAll()
        {
            return db.Predstave.Include(x => x.VrstaPredstave).Include(x => x.Pozoriste); 

        }

        public Predstava GetById(int id)
        {
            return db.Predstave.FirstOrDefault(x => x.Id == id);

        }

        public IEnumerable<Predstava> GetCenaVreme(decimal cena, DateTime vreme)
        {
            return db.Predstave.Where(x => x.CenaKarte == cena && x.VremeOdrzavanja > vreme);
        }

        public IEnumerable<Predstava> GetNaziv(string naziv)
        {
            return db.Predstave.Where(x => x.Naziv.ToLower().Equals(naziv.ToLower()));
        }

        public IEnumerable<Predstava> GetZanrCena(string zanr, decimal cena)
        {
            return db.Predstave.Include(x => x.VrstaPredstave).Where(x => x.VrstaPredstave.Equals(zanr));
        }

        public IEnumerable<Predstava> PostPretraga(DateTime min, DateTime max)
        {
            return db.Predstave.Where(x => x.DatumOdrazavanja >= min && x.DatumOdrazavanja == max);
        }

        public void Update(Predstava predstava)
        {
            db.Entry(predstava).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}