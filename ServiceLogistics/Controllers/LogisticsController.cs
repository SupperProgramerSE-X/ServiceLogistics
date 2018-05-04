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
using ServiceLogistics;

namespace ServiceLogistics.Controllers
{
    public class LogisticsController : ApiController
    {
        private LogisticsDBEntities db = new LogisticsDBEntities();

        // GET: api/Logistics
        public IQueryable<Logistic> GetLogistics()
        {
            return db.Logistics;
        }

        // GET: api/Logistics/5
        [ResponseType(typeof(Logistic))]
        public IHttpActionResult GetLogistic(int id)
        {
            Logistic logistic = db.Logistics.Find(id);
            if (logistic == null)
            {
                return NotFound();
            }

            return Ok(logistic);
        }

        // PUT: api/Logistics/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLogistic(int id, Logistic logistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != logistic.Logistic_id)
            {
                return BadRequest();
            }

            db.Entry(logistic).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogisticExists(id))
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

        // POST: api/Logistics
        [ResponseType(typeof(Logistic))]
        public IHttpActionResult PostLogistic(Logistic logistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Logistics.Add(logistic);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = logistic.Logistic_id }, logistic);
        }

        // DELETE: api/Logistics/5
        [ResponseType(typeof(Logistic))]
        public IHttpActionResult DeleteLogistic(int id)
        {
            Logistic logistic = db.Logistics.Find(id);
            if (logistic == null)
            {
                return NotFound();
            }

            db.Logistics.Remove(logistic);
            db.SaveChanges();

            return Ok(logistic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LogisticExists(int id)
        {
            return db.Logistics.Count(e => e.Logistic_id == id) > 0;
        }
    }
}