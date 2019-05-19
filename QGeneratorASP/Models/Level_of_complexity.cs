using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QGeneratorASP.Models
{
    public class Level_of_complexity//справочник уровней
    {
        public Level_of_complexity()
        {
            Quest = new HashSet<Quest>();
            Riddle = new HashSet<Riddle>();
        }

        [Key]
        public int Id_level { get; set; }

        [Required]
        [StringLength(50)]
        public string Name_level { get; set; }

        [Column(TypeName = "text")]
        public string Note { get; set; }//комментарий

        public virtual ICollection<Quest> Quest { get; set; }

        public virtual ICollection<Riddle> Riddle { get; set; }
    }
}
