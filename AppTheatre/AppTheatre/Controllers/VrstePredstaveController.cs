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
    public class VrstePredstaveController : ApiController
    {

        IVrstaPredstave _repository { get; set; }

        public VrstePredstaveController(IVrstaPredstave repository)
        {
            _repository = repository;
        }
        public IEnumerable<VrstaPredstave> Get()
        {
            return _repository.GetAll();
        }
        public IHttpActionResult Get(int id)
        {
            var vrsta = _repository.GetById(id);
            if (vrsta == null)
            {
                return NotFound();
            }
            return Ok(vrsta);
        }
        public IHttpActionResult Post(VrstaPredstave vrsta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repository.Add(vrsta);

            return CreatedAtRoute("DefaultApi", new { id = vrsta.Id }, vrsta);
        }
        public IHttpActionResult Put(int id, VrstaPredstave vrsta)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vrsta.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(vrsta);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(vrsta);
        }
        public IHttpActionResult Delete(int id)
        {
            var vrsta = _repository.GetById(id);
            if (vrsta == null)
            {
                return NotFound();
            }

            _repository.Delete(vrsta);

            return StatusCode(HttpStatusCode.NoContent);

        }
        [Route("api/vrsta/naziv")]
        public IEnumerable<VrstaPredstave> GetNaziv(string naziv)
        {
            return _repository.GetByNaziv(naziv);
        }
    }
}
