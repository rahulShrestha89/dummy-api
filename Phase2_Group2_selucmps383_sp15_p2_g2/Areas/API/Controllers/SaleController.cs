using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Phase2_Group2_selucmps383_sp15_p2_g2.Models;
using Phase2_Group2_selucmps383_sp15_p2_g2.DbContext;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Http.Description;
using Phase2_Group2_selucmps383_sp15_p2_g2.Controllers;
using Phase2_Group2_selucmps383_sp15_p2_g2.Authentication;
using System.Web.Helpers;
using Phase2_Group2_selucmps383_sp15_p2_g2.Enums;
using System.Data.Entity.Infrastructure;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Areas.API.Controllers
{
    [ValidateByApiKey]
    public class SaleController : BaseApiController
    {
        IGameStoreRepository _repo;
        ModelFactory _modelFactory;
        private GameStoreContext db = new GameStoreContext();

        public SaleController()
        {

        }

        public SaleController(IGameStoreRepository repo)
        {
            _repo = repo;
            _modelFactory = new ModelFactory();
        }

        [RoleAuthentication("StoreAdmin")]
        [System.Web.Http.ActionName("GetAllSales")]
        public IQueryable<SaleModel>  GetAllSales()
        {
            return _repo.GetAllSales().Select(s => _modelFactory.Create(s));
        }

        [System.Web.Http.ActionName("GetSale")]
        [ResponseType(typeof(SaleModel))]
        public IHttpActionResult GetSale(int saleId)
        {
            //need additional checks to see if user is the employee associated with the sale.
            if(!IsStoreAdmin())
            {
                return Unauthorized();
            }
            SaleModel sale = _modelFactory.Create(_repo.GetSale(saleId));

            if(sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }

        [System.Web.Http.HttpPost]
        [RoleAuthentication("StoreAdmin , Employee")]
        [ValidateAntiForgeryToken]
        [ResponseType(typeof(Sale))]
        [System.Web.Http.ActionName("PostSale")]
        public IHttpActionResult PostSale([FromBody]Sale sale)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            sale.UserId = storeUser.UserId;
            sale.User = storeUser;

            _repo.AddSale(sale);
            _repo.SaveAll();

            return CreatedAtRoute("DefaultApi", new {id = sale.SaleId }, sale);
        }

        [RoleAuthentication("StoreAdmin")]
        [ResponseType(typeof(Sale))]
        [System.Web.Http.ActionName("PutSale")]
        public IHttpActionResult PutSale(int saleId, [FromBody] Sale sale)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(saleId != sale.SaleId)
            {
                return BadRequest();
            }

            var saleInDb = _repo.GetSale(saleId);

            if(sale.SaleDate != null)
            {
                saleInDb.SaleDate = sale.SaleDate;
            }

            if(sale.User != saleInDb.User)
            {
                 saleInDb.User = sale.User;
                 saleInDb.UserId = sale.User.UserId;
            }

            if(sale.TotalAmount != saleInDb.TotalAmount)
            {
                 saleInDb.TotalAmount = sale.TotalAmount;
            }

            _repo.UpdateSale(saleInDb);

            try
            {
                if(_repo.SaveAll())
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!_repo.SaleExists(saleId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }


        [System.Web.Http.ActionName("DeleteSale")]
        [RoleAuthentication("StoreAdmin")]
        [ResponseType(typeof(Sale))]
        public IHttpActionResult DeleteSale(int saleId)
        {
            Sale sale = _repo.GetSale(saleId);
            if(sale == null)
            {
                return NotFound();
            }

            _repo.RemoveSale(sale);
            _repo.SaveAll();

            return Ok(sale);
        }

    }
}
