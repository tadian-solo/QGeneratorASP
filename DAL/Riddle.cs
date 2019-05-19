namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Riddle")]
    public partial class Riddle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Riddle()
        {
            Quest = new HashSet<Quest>();
        }

        [Key]
        public int Id_riddle { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Text { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Description { get; set; }

        public bool Status { get; set; }

        public int Id_Autor_FK { get; set; }

        public int Id_Level_FK { get; set; }

        public int Id_Answer_FK { get; set; }

        public int Id_Type_FK { get; set; }

        public int? Id_Image_FK { get; set; }

        public virtual Answer Answer { get; set; }

        public virtual Image Image { get; set; }

        public virtual Level_of_complexity Level_of_complexity { get; set; }

        public virtual Type_of_question Type_of_question { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quest> Quest { get; set; }
    }
}
