namespace Phase2_Group2_selucmps383_sp15_p2_g2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Phase2_Group2_selucmps383_sp15_p2_g2.Models;
    using System.Collections.Generic;



    internal sealed class Configuration : DbMigrationsConfiguration<Phase2_Group2_selucmps383_sp15_p2_g2.DbContext.GameStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Phase2_Group2_selucmps383_sp15_p2_g2.DbContext.GameStoreContext context)
        {
            var tags = new List<Tag>()
            {
                new Tag
                {
                     Games = new List<Game>{},
                     TagId = 0,
                     TagName = "shsdfgfrhh"

                }
            };
            
        }
    }
}
