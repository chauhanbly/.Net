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
    public class LoginDetailController : ApiController
    {
        private dbModel db = new dbModel();

        // GET: api/LoginDetail
        public IQueryable<LoginDetail> GetLoginDetails()
        {
            return db.LoginDetails;
        }

        // GET: api/LoginDetail/5
        [ResponseType(typeof(LoginDetail))]
        public IHttpActionResult GetLoginDetail(int id)
        {
            LoginDetail loginDetail = db.LoginDetails.Find(id);
            if (loginDetail == null)
            {
                return NotFound();
            }

            return Ok(loginDetail);
        }

        // PUT: api/LoginDetail/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLoginDetail(int id, LoginDetail loginDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loginDetail.loginid)
            {
                return BadRequest();
            }

            db.Entry(loginDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginDetailExists(id))
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

        // POST: api/LoginDetail
        [ResponseType(typeof(LoginDetail))]
        public IHttpActionResult PostLoginDetail(LoginDetail loginDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LoginDetails.Add(loginDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = loginDetail.loginid }, loginDetail);
        }

        // DELETE: api/LoginDetail/5
        [ResponseType(typeof(LoginDetail))]
        public IHttpActionResult DeleteLoginDetail(int id)
        {
            LoginDetail loginDetail = db.LoginDetails.Find(id);
            if (loginDetail == null)
            {
                return NotFound();
            }

            db.LoginDetails.Remove(loginDetail);
            db.SaveChanges();

            return Ok(loginDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoginDetailExists(int id)
        {
            return db.LoginDetails.Count(e => e.loginid == id) > 0;
        }
    }
}