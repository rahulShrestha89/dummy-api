using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Phase2_Group2_selucmps383_sp15_p2_g2.Enums;
using Phase2_Group2_selucmps383_sp15_p2_g2.Models;
using Phase2_Group2_selucmps383_sp15_p2_g2.DbContext;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Controllers
{
    
    // make base controller abstract so it cannot be instantiated
    // all our controllers will derive from the base controller to
    // inherit common functionality
    public abstract class BaseApiController : ApiController
    {
        public User storeUser;
        public GameStoreContext _db;
        
        public IGameStoreRepository _repo ;
        public ModelFactory _modelFactory;
        

        public BaseApiController(IGameStoreRepository repo)
        {
            _repo = repo;
            string h1 = Request.Headers.ElementAt(0).ToString();
            string h2 = Request.Headers.ElementAt(1).ToString();

            storeUser = _db.Users.Where(s => s.EmailAddress == h1 && s.ApiKey == h2).FirstOrDefault();
        }

        protected IGameStoreRepository TheRepository
        {
            get { return _repo; }
        }


        // key point: we need to defer the creation of the model factory until
        // it is needed. So here we create a deferred property.
        // This is basically a singleton pattern, which will exist for the lifetime of the controller
        protected ModelFactory TheModelFactory
        {

            // remember we're using the factory pattern to copy database entities into our models that contain discoverable values for each resources
            get
            {
                // the first get will cause the factory to be created
                if (_modelFactory == null)
                {
                    // it should be late enough for the request to not be null
                    // the model factory will be created as a result of the user request
                    // so we can create the model factory and pass it the request so we may
                    // use it and the Url helper to generate the requested resource's URI hyperlinks for including
                    // in the models that we pass back to the user
                    _modelFactory = new ModelFactory(Request, TheRepository);
                }
                return _modelFactory;
            }
        }

        protected bool IsStoreAdmin()
        {
            return Enum.GetName(typeof(Role), storeUser.Role)==("StoreAdmin");
        }

        protected bool IsEmployee()
        {
            if (IsStoreAdmin())
            {
                return true;
            }

            return Enum.GetName(typeof (Role), storeUser.Role) == ("StoreEmployee");
        }

        protected bool IsCustomer()
        {
            return Enum.GetName(typeof (Role), storeUser.Role) == ("StoreCustomer");
        }

        protected bool CanAccessUser(int userId)
        {
            if (IsStoreAdmin())
            {
                return true;
            }
            return false;
        }
    }
}
