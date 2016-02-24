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
using Pawze.API.Domain;
using Pawze.API.Infrastructure;
using Pawze.API.Models;
using AutoMapper;

namespace Pawze.API.Controllers
{
    [Authorize]
    public class ShipmentsController : ApiController
    {
        private PawzeDataContext db = new PawzeDataContext();

        // GET: api/Shipments
        public IEnumerable<ShipmentsModel> GetShipments()
        {
            return Mapper.Map<IEnumerable<ShipmentsModel>>(
                db.Shipments.Where(s => s.Subscription.PawzeUser.UserName == User.Identity.Name)               
            );
        }

        // GET: api/Shipments/5
        [ResponseType(typeof(ShipmentsModel))]
        public IHttpActionResult GetShipment(int id)
        {
            // Shipment dbShipment = db.Shipments.Find(id);
            Shipment dbShipment = db.Shipments.FirstOrDefault(s => s.Subscription.PawzeUser.UserName == User.Identity.Name && s.ShipmentId == id);

            if (dbShipment == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ShipmentsModel>(dbShipment));
        }

        // PUT: api/Shipments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShipment(int id, ShipmentsModel shipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Shipment dbShipment = db.Shipments.FirstOrDefault(s => s.Subscription.PawzeUser.UserName == User.Identity.Name && s.ShipmentId == id);

            if (id != shipment.ShipmentId)
            {
                return BadRequest();
            }

            dbShipment.Update(shipment);

            db.Entry(dbShipment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipmentExists(id))
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

        // POST: api/Shipments
        [ResponseType(typeof(ShipmentsModel))]
        public IHttpActionResult PostShipment(ShipmentsModel shipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbShipment = new Shipment();

            dbShipment.Update(shipment);

            db.Shipments.Add(dbShipment);
            db.SaveChanges();

            shipment.ShipmentId = dbShipment.ShipmentId;

            return CreatedAtRoute("DefaultApi", new { id = shipment.ShipmentId }, shipment);
        }

        // DELETE: api/Shipments/5
        [ResponseType(typeof(ShipmentsModel))]
        public IHttpActionResult DeleteShipment(int id)
        {
            // Shipment shipment = db.Shipments.Find(id);

            Shipment dbShipment = db.Shipments.FirstOrDefault(s => s.Subscription.PawzeUser.UserName == User.Identity.Name && s.ShipmentId == id);
            if (dbShipment == null)
            {
                return NotFound();
            }

            db.Shipments.Remove(dbShipment);
            db.SaveChanges();

            return Ok(Mapper.Map<ShipmentsModel>(dbShipment));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShipmentExists(int id)
        {
            return db.Shipments.Count(e => e.ShipmentId == id) > 0;
        }
    }
}