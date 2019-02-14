using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmpolyeeController : ApiController
    {
        private dbModel db = new dbModel();

        // GET: api/Empolyee
        public IQueryable<Empolyee> GetEmpolyees()
        {
            return db.Empolyees;
        }

        // GET: api/Empolyee/5
        [ResponseType(typeof(Empolyee))]
        public IHttpActionResult GetEmpolyee(int id)
        {
            Empolyee empolyee = db.Empolyees.Find(id);
            if (empolyee == null)
            {
                return NotFound();
            }

            return Ok(empolyee);
        }

        // PUT: api/Empolyee/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmpolyee(int id, Empolyee empolyee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != empolyee.EmployeeId)
            {
                return BadRequest();
            }

            db.Entry(empolyee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpolyeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Empolyee
        [ResponseType(typeof(Empolyee))]
        public IHttpActionResult PostEmpolyee(Empolyee empolyee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Empolyees.Add(empolyee);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = empolyee.EmployeeId }, empolyee);
        }

        // DELETE: api/Empolyee/5
        [ResponseType(typeof(Empolyee))]
        public IHttpActionResult DeleteEmpolyee(int id)
        {
            Empolyee empolyee = db.Empolyees.Find(id);
            if (empolyee == null)
            {
                return NotFound();
            }

            db.Empolyees.Remove(empolyee);
            db.SaveChanges();

            return Ok(empolyee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmpolyeeExists(int id)
        {
            return db.Empolyees.Count(e => e.EmployeeId == id) > 0;
        }
    }
}