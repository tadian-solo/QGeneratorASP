using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QGeneratorASP.Models
{
    public class Answer
    {
        public Answer()
        {
            Riddle = new HashSet<Riddle>();
        }

        [Key]
        public int Id_answer { get; set; }

        [Required]
        [StringLength(100)]
        public string Object { get; set; }

        [Column(TypeName = "text")]
        public string Note { get; set; }

        public virtual ICollection<Riddle> Riddle { get; set; }
    }
}
