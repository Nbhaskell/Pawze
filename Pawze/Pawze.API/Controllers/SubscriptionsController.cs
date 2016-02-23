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
using AutoMapper;
using Pawze.API.Models;

namespace Pawze.API.Controllers
{
    public class SubscriptionsController : ApiController
    {
        private PawzeDataContext db = new PawzeDataContext();

        // GET: api/Subscriptions
        public IEnumerable<SubscriptionsModel> GetSubscriptions()
        {
            return Mapper.Map<IEnumerable<SubscriptionsModel>>(
                db.Subscriptions.Where(s => s.PawzeUser.UserName == User.Identity.Name));
        }

        // GET: api/Subscriptions/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(SubscriptionsModel))]
        public IHttpActionResult GetSubscription(int id)
        {
            // Subscription subscription = db.Subscriptions.Find(id);
            Subscription dbSubscription = db.Subscriptions.FirstOrDefault(s => s.PawzeUser.UserName == User.Identity.Name && s.SubscriptionId == id);

            if (dbSubscription == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<SubscriptionsModel>(dbSubscription));
        }

        // PUT: api/Subscriptions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubscription(int id, SubscriptionsModel subscription)

        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Subscription dbSubscription = db.Subscriptions.FirstOrDefault(s => s.PawzeUser.UserName == User.Identity.Name && s.SubscriptionId == id);

            if (id != subscription.SubscriptionId)
            {
                return BadRequest();
            }

            if(dbSubscription == null)
            {
                return BadRequest();
            }

            dbSubscription.Update(subscription);

            db.Entry(dbSubscription).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriptionExists(id))
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

        // POST: api/Subscriptions
        [ResponseType(typeof(SubscriptionsModel))]
        public IHttpActionResult PostSubscription(SubscriptionsModel subscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbSubscription = new Subscription();

            dbSubscription.PawzeUser = db.PawzeUsers.FirstOrDefault(u => u.UserName == User.Identity.Name);

            dbSubscription.Update(subscription);

            db.Subscriptions.Add(dbSubscription);
            db.SaveChanges();

            subscription.SubscriptionId = dbSubscription.SubscriptionId;

            return CreatedAtRoute("DefaultApi", new { id = subscription.SubscriptionId }, subscription);
        }

        // DELETE: api/Subscriptions/5
        [ResponseType(typeof(SubscriptionsModel))]
        public IHttpActionResult DeleteSubscription(int id)
        {
            // Subscription subscription = db.Subscriptions.Find(id);

            Subscription dbSubscription = db.Subscriptions.FirstOrDefault(s => s.PawzeUser.UserName == User.Identity.Name && s.SubscriptionId == id);
        
            if (dbSubscription == null)
            {
                return NotFound();
            }

            db.Subscriptions.Remove(dbSubscription);
            db.SaveChanges();

            return Ok(Mapper.Map<SubscriptionsModel>(dbSubscription));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubscriptionExists(int id)
        {
            return db.Subscriptions.Count(e => e.SubscriptionId == id) > 0;
        }
    }
}