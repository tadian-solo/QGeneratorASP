using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QGeneratorASP.Models
{
    public class Quest
    {
        public Quest()
        {
            QuestRiddle = new HashSet<QuestRiddle>();
        }
        [Key]
        public int Id_quest { get; set; }
        public bool Status { get; set; }
        public int Number_of_questions { get; set; }
        [Column(TypeName = "text")]
        public string Thematics { get; set; }
        public int Id_level_Fk { get; set; }
        public int Id_autor_Fk { get; set; }
        public DateTime Date { get; set; }
        public virtual Level_of_complexity Level_of_complexity { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<QuestRiddle> QuestRiddle { get; set; }
    }
}
