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
    public class VezaRepository : IDisposable, IVeza
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add(Veza veza)
        {
            db.Veze.Add(veza);
            db.SaveChanges();

        }

        public void Delete(Veza veza)
        {
            db.Veze.Remove(veza);
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

        public IEnumerable<Veza> GetAll()
        {
            return db.Veze.Include(x => x.Glumac).Include(y => y.Predstava);
        }

        public Veza GetById(int id)
        {
            var veza = db.Veze.Include(x => x.Glumac).Include(y => y.Predstava).FirstOrDefault(p=> p.Id==id);

            return veza;
               
        }

        public IEnumerable<Veza> GetByIme(string veza)
        {
            var v = db.Veze.Include(x => x.Glumac).Include(y => y.Predstava).Where(c=>c.Glumac.Ime.ToLower().Equals(veza.ToLower()));

            return v;
        }

        public IEnumerable<Veza> GetByPrezime(string veza)
        {
            var v = db.Veze.Include(x => x.Glumac).Include(y => y.Predstava).Where(c => c.Glumac.Ime.ToLower().Equals(veza.ToLower()));

            return v;
        }

        public void Update(Veza veza)
        {
            db.Entry(veza).State = EntityState.Modified;
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