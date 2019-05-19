namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Quest")]
    public partial class Quest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quest()
        {
            Riddle = new HashSet<Riddle>();
        }

        [Key]
        public int Id_quest { get; set; }

        public bool Status { get; set; }

        public int Number_of_questions { get; set; }

        [Column(TypeName = "text")]
        public string Thematics { get; set; }

        public int Id_Level_FK { get; set; }

        public int Id_Autor_FK { get; set; }

        public DateTime Date { get; set; }

        public virtual Level_of_complexity Level_of_complexity { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Riddle> Riddle { get; set; }
    }
}
