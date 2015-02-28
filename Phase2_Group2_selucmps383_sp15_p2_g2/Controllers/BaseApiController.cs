using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Phase2_Group2_selucmps383_sp15_p2_g2.Models;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Controllers
{
    public class BaseApiController : ApiController
    {
        private IGameStoreRepository _repo ;

        public BaseApiController(IGameStoreRepository repo)
        {
            _repo = repo;
        }

        protected IGameStoreRepository TheRepository
        {
            get { return _repo; }
        }

    }
}
