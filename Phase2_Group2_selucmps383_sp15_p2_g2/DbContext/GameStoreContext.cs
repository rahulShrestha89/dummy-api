using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using Phase2_Group2_selucmps383_sp15_p2_g2.Models;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.DbContext
{
    public class GameStoreContext: System.Data.Entity.DbContext
    {
        public GameStoreContext()
            : base("name=DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelbuilder) 
        {
            modelbuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }




    }




}