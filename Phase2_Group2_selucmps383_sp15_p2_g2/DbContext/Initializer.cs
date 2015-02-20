using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.DbContext
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data.Entity;
    using Phase2_Group2_selucmps383_sp15_p2_g2.Models;

    namespace ContosoUniversity.DAL
    {
        public class Initializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<GameStoreContext>
        {
            protected override void Seed(GameStoreContext context)
            {
                var Genres = new List<Genre>
            {
                new Genre
                {
                    GenreName="Fantasy",
                    Games=new List<Game>()
                }
           };

                Genres.ForEach(s => context.Genres.Add(s));
                context.SaveChanges();

              
            }
        }
    }
}