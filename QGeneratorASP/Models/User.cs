using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QGeneratorASP.Models
{
    public class User: IdentityUser<int>
    {
        public User()
        {
            Quest = new HashSet<Quest>();
            Riddle = new HashSet<Riddle>();
        }
       // [Key]
       // public int Id_user { get; set; }
       // public string Name { get; set; }
       // public string Password { get; set; }
        public bool AccessLevel { get; set; }

        public virtual ICollection<Quest> Quest { get; set; }
        public virtual ICollection<Riddle> Riddle { get; set; }
    }
}
