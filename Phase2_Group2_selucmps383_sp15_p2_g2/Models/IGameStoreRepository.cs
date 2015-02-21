using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    interface IGameStoreRepository
    {
        IQueryable<Game> GetAll();
        Game Get(int id);
        void Remove(int id);
        bool Update(Game game, int id);
        Game Add(Game game);
        bool GameExists(int id);
       
    }
}
