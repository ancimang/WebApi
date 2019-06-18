using AppTheatre.Interfaces;
using AppTheatre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppTheatre.Controllers
{
    public class PredstaveController : ApiController
    {
        IPredstava _repository { get; set; }

        public PredstaveController(IPredstava repository)
        {
            _repository = repository;

        }
        public IHttpActionResult GetById(int id)
        {
            var predstava = _repository.GetById(id);
            if (predstava == null)
            {
                return NotFound();
            }
            return Ok(predstava);

        }
        public IEnumerable<Predstava> GetAll()
        {
            return _repository.GetAll();
        }

        public IHttpActionResult Delete (int id)
        {
            var predstava = _repository.GetById(id);
            if (predstava == null)
            {
                return BadRequest();
            }

            _repository.Delete(predstava);

            return StatusCode(HttpStatusCode.NoContent);
        }
        public IHttpActionResult Put(int id, Predstava predstava)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id != predstava.Id)
            {
                return BadRequest();
            }
            try
            {
                _repository.Update(predstava);
            }
            catch
            {
                return BadRequest();

            }

            return Ok(predstava);
        }
        public IHttpActionResult Post(Predstava predstava)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _repository.Add(predstava);

            return CreatedAtRoute("DefaultApi", new { id = predstava.Id }, predstava);
        }
        public IEnumerable<Predstava> PostPretraga(DateTime min, DateTime max)
        {
           return _repository.PostPretraga(min, max);
        }
        public IEnumerable<Predstava> GetNaziv(string naziv)
        {
            return _repository.GetNaziv(naziv);
        }
        public IEnumerable<Predstava> GetZanrCena(string zanr, decimal cena)
        {
            return _repository.GetZanrCena(zanr, cena);
        }
        public IEnumerable<Predstava> GetCenaVreme(decimal cena, DateTime vreme)
        {
            return _repository.GetCenaVreme(cena, vreme);
        }
    }

}

