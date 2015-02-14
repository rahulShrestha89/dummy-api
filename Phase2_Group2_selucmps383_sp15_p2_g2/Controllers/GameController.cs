using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Phase2_Group2_selucmps383_sp15_p2_g2.Models;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Controllers
{
    public class GameController : ApiController
    {
        static readonly IGameStoreRepository _repo = new GameStoreRepository();

        public IEnumerable<Game> GetAll()
        {
            return _repo.GetAll();
        }

        public Game GetGame(int gameId)
        {
            Game game = _repo.Get(gameId);
            if(game == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return game;
        }
        
    }
}
