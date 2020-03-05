using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ChetTime.Models
{
    public class User:IdentityUser
    {
      //  public int Id { get; set; }
      //  public string UserName { get; set; }
        public string Password { get; set; }
        // public bool IsOnline { get; set; }
       //public List<string> Message { get; set; }


    }
}
