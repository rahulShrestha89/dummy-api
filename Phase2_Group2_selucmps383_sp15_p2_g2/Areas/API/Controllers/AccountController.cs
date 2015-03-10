using Phase2_Group2_selucmps383_sp15_p2_g2.Authentication;
using Phase2_Group2_selucmps383_sp15_p2_g2.DbContext;
using Phase2_Group2_selucmps383_sp15_p2_g2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Areas.API.Controllers
{
    [ValidateByApiKey]
    [RoutePrefix("Account")]
    public class AccountController : BaseApiController
    {
    
       
            public AccountController()
    {
       
    }

            public UserModel Create(User user)
            {
                return new UserModel()
                {
                   
                    UserId = user.UserId,
                    Email = user.EmailAddress,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role,
                  
                };
            }
 
 
        private bool UserExists(int id)
        {
            return _db.Users.Count(e => e.UserId == id) > 0;
        }
 
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
 
    }
    
}
