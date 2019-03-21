using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QGeneratorASP.Models
{
    public class Type_of_question
    {
        public Type_of_question()
        {
            Riddle = new HashSet<Riddle>();
        }

        [Key]
        public int Id_type { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }


        public virtual ICollection<Riddle> Riddle { get; set; }
    }
}
