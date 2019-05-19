namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Type_of_question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Type_of_question()
        {
            Riddle = new HashSet<Riddle>();
        }

        [Key]
        public int Id_type { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool Need_for_image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Riddle> Riddle { get; set; }
    }
}
