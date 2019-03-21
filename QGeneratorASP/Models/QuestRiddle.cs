using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QGeneratorASP.Models
{
    public class QuestRiddle
    {
        public int Id_Quest_Fk { get; set; }
        public int Id_Riddle_Fk { get; set; }

        public virtual Quest Quest { get; set; }
        public virtual Riddle Riddle { get; set; }
    }
}
