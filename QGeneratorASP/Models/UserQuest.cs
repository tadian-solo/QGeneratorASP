using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QGeneratorASP.Models
{
    public class UserQuest//промежуточная таблица для Избранного
    {
        public int Id_Quest_Fk { get; set; }
        public int Id_User_Fk { get; set; }

        public virtual Quest Quest { get; set; }
        public virtual User User { get; set; }
    }
}
