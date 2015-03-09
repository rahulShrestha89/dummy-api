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
    public class UserController : BaseApiController
    {
        IGameStoreRepository _repo;
        ModelFactory _modelFactory;
        private GameStoreContext db = new GameStoreContext();

        public UserController()
        {

        }
        public UserController(IGameStoreRepository repository)     
        {
            _repo = repository;
            _modelFactory = new ModelFactory();   
        }

        
        /// GET: api/users
        /// <summary>
        /// returns all the users
        /// </summary>
        /// <returns></returns>
        [RoleAuthentication("StoreAdmin")]
        [System.Web.Http.ActionName("GetAllUsers")]
        public IQueryable<UserBaseModel> GetAllUsers()
        {
            return _repo.GetAllUsers().Select(u=>_modelFactory.Create(u));
        }


        /// GET api/User/getuser/5
        /// <summary>
        /// returns the specified user based on ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [System.Web.Http.ActionName("GetUser")]
        [ResponseType(typeof(UserBaseModel))]
        public IHttpActionResult GetUser(int userId)
        {
            if (!IsStoreAdmin())
            {
                return Unauthorized();
            }

            UserBaseModel user = _modelFactory.Create(_repo.GetUserById(userId));

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// POST: /User/PostUser
        /// <summary>
        /// Creates a user from a form. Only Accessible by Store Admin.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [RoleAuthentication("StoreAdmin")]
        [ValidateAntiForgeryToken]
        [ResponseType(typeof(User))]
        [System.Web.Http.ActionName("PostUser")]
        public IHttpActionResult PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.ApiKey = GetApiKey();
            string hashedPassword = Crypto.HashPassword(user.Password);
            user.Password = hashedPassword;
            _repo.AddUser(user);
            _repo.SaveAll();

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }


        // PUT: api/User/PutUser
        [RoleAuthentication("StoreAdmin")]
        [ResponseType(typeof(User))]
        [System.Web.Http.ActionName("PutUser")]
        public IHttpActionResult PutUser(int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            var checkUserInDb = _repo.GetUserById(id);   // check if the user exists
            if (checkUserInDb == null)
            {
                return NotFound();
            }

            //// remove from role
            /*
            if (user.Role != null)
            {
                var roleToBeRemovedFrom = Enum.GetName(typeof(Role), user.Role);
                if(roleToBeRemovedFrom==null || !roleToBeRemovedFrom.Contains(Enum.GetName(typeof(Role), checkUserInDb.Role)))
                {
                    return BadRequest();
                }
                else
                {
                   checkUserInDb.Role.
                }
            }*/

            //// add role
            if(user.Role != null)
            {
                //var roleToBeAdded = Enum.GetName(typeof(Role), user.Role);
                checkUserInDb.Role = user.Role;
            }

            if (user.FirstName != null)
            {
                checkUserInDb.FirstName = user.FirstName;
            }
            if (user.LastName != null)
            {
                checkUserInDb.LastName = user.LastName; 
            }
            if (user.EmailAddress != null)
            {
                checkUserInDb.EmailAddress = user.EmailAddress;
            }

            _repo.UpdateUser(id, checkUserInDb);

            try
            {
                if(_repo.SaveAll())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

           // return StatusCode(HttpStatusCode.NoContent);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }

        [System.Web.Http.ActionName("DeleteUser")]
        // DELETE: api/User/DeleteUser
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int userId)
        {
            User user = db.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public static string GetApiKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[16];
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }

        
    }
}
