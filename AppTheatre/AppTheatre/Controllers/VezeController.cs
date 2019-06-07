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
    public class VezeController : ApiController
    {
        IVeza _repository { get; set; }

        public VezeController(IVeza repository)
        {
            _repository = repository;
        }

        public IHttpActionResult GetById(int id)
        {
            var veza = _repository.GetById(id);
            if (veza == null)
            {
                return NotFound();
            }

            return Ok(veza);
        }
        public IEnumerable<Veza> GetAll()
        {
            return _repository.GetAll();
        }
        public IHttpActionResult Post(Veza veza)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            _repository.Add(veza);

            return CreatedAtRoute("DefaultApi", new { id = veza.Id }, veza);
        }
        
        public IHttpActionResult Put(int id, Veza veza)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != veza.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(veza);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(veza);
        }
        public IHttpActionResult Delete(int id)
        {
            var veza = _repository.GetById(id);
            if (veza == null)
            {
                return NotFound();
            }

            _repository.Delete(veza);

            return StatusCode(HttpStatusCode.NoContent);
        }
        public IEnumerable<Veza> GetByIme(string ime)
        {
            return _repository.GetByIme(ime);
        }
        public IEnumerable<Veza> GetByPrezime(string prezime)
        {
            return _repository.GetByIme(prezime);
        }
        
    }
}
