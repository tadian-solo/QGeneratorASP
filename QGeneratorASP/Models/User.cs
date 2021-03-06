﻿using Microsoft.AspNetCore.Identity;
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
            UserQuest = new HashSet<UserQuest>();
        }
       
        public bool AccessLevel { get; set; }//поле уровень доступа

        public virtual ICollection<Quest> Quest { get; set; }
        public virtual ICollection<UserQuest> UserQuest { get; set; }
        public virtual ICollection<Riddle> Riddle { get; set; }
    }
}
