namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Level_of_complexity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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
        public string Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quest> Quest { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Riddle> Riddle { get; set; }
    }
}
