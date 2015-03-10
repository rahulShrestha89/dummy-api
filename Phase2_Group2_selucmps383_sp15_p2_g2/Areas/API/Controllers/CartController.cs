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
using System.Threading.Tasks;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Areas.API.Controllers
{
    public class CartController : BaseApiController
    {
        IGameStoreRepository _repo;
        ModelFactory _modelFactory;

        public GameStoreContext db = new GameStoreContext(); 

        public CartController()
        {

        }

        
        public CartController(IGameStoreRepository repo)
        {
            _repo = repo;
            _modelFactory = new ModelFactory();
        }

        // GET api/Cart
        [System.Web.Http.ActionName("GetAllCarts")]
        public IQueryable<Cart> GetAllCarts()
        {
            return _repo.GetAllCarts();
        }

        // GET api/Cart/5
        [System.Web.Http.ActionName("GetCart")]
        [ResponseType(typeof(Cart))]
        public IHttpActionResult GetCart(int cartId)
        {
            if(!IsEmployee() || storeUser.CustomerCart.CartId != cartId)
            {
                return Unauthorized();
            }

            Cart cart = _repo.GetCart(cartId);
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // PUT api/Cart/5
        [System.Web.Http.ActionName("PutCart")]
        [RoleAuthentication("StoreCustomer")]
        [ResponseType(typeof(Cart))]
        public IHttpActionResult PutCart(int cartId, Cart cart)
        {
            if(storeUser.CustomerCart.CartId != cartId)
            {
                Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cartId != cart.CartId)
            {
                return BadRequest();
            }

            var cartInDb = _repo.GetCart(cartId);
            var gamesInCart = cart.Games;

            if(cart.CartDate != null)
            {
                cartInDb.CartDate = cart.CartDate;
            }
            if(cart.Quantity != null)
            {
                cartInDb.Quantity = cart.Quantity;
            }
            if(cart.UserCartId != cartInDb.UserCartId)
            {
                return BadRequest();
            }

            if(gamesInCart != null)
            {
                cartInDb.Games = cartInDb.Games.Intersect(gamesInCart).ToList();
            }

            _repo.UpdateCart(cartInDb);

            try
            {
                if (_repo.SaveAll())
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
                if (!_repo.CartExists(cartId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST api/Cart
        [ResponseType(typeof(Cart))]
        [RoleAuthentication("StoreCustomer")]
        [System.Web.Http.ActionName("PostCart")]
        public IHttpActionResult PostCart(Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _repo.AddCart(cart);
            _repo.SaveAll();

            return CreatedAtRoute("DefaultApi", new { id = cart.CartId }, cart);
           
        }

        // DELETE api/Cart/5
        [ResponseType(typeof(Cart))]
        [RoleAuthentication("StoreCustomer")]
        [System.Web.Http.ActionName("DeleteCart")]
        public async Task<IHttpActionResult> DeleteCart(int cartId)
        {

            Cart cart = _repo.GetCart(cartId);
            
            if (cart == null)
            {
                return NotFound();
            }

            _repo.RemoveCart(cart);
            _repo.SaveAll();

            return Ok(cart);
        }
    }
}