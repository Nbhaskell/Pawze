using AutoMapper;
using Pawze.Core.Domain;
using Pawze.Core.Infrastructure;
using Pawze.Core.Models;
using Pawze.Core.Repository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Pawze.API.Controllers
{
    [Authorize]
    public class SubscriptionsController : ApiController
    {
        private ISubscriptionRepository _subscriptionRepository;
        private IUnitOfWork _unitOfWork;

        public SubscriptionsController(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Subscriptions
        public IEnumerable<SubscriptionsModel> GetSubscriptions()
        {
            return Mapper.Map<IEnumerable<SubscriptionsModel>>(
                _subscriptionRepository.GetAll()
            );
        }

        // GET: api/Subscriptions/5
        [ResponseType(typeof(SubscriptionsModel))]
        public IHttpActionResult GetSubscription(int id)
        {
            Subscription dbSubscription = _subscriptionRepository.GetById(id);

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

            if (id != subscription.SubscriptionId)
            {
                return BadRequest();
            }

            Subscription dbSubscription = _subscriptionRepository.GetById(id);
            dbSubscription.Update(subscription);

            _subscriptionRepository.Update(dbSubscription);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
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
            _subscriptionRepository.Add(dbSubscription);

            _unitOfWork.Commit();

            subscription.SubscriptionId = dbSubscription.SubscriptionId;

            return CreatedAtRoute("DefaultApi", new { id = subscription.SubscriptionId }, subscription);
        }

        // DELETE: api/Subscriptions/5
        [ResponseType(typeof(SubscriptionsModel))]
        public IHttpActionResult DeleteSubscription(int id)
        {
            Subscription subscription = _subscriptionRepository.GetById(id);
            if (subscription == null)
            {
                return NotFound();
            }

            _subscriptionRepository.Delete(subscription);
            _unitOfWork.Commit();

            return Ok(Mapper.Map<SubscriptionsModel>(subscription));
        }

        private bool SubscriptionExists(int id)
        {
            return _subscriptionRepository.Count(e => e.SubscriptionId == id) > 0;
        }
    }
}