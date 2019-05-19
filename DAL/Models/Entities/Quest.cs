namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    
    [Table("Quest")]
    public partial class Quest: INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quest()
        {
            Riddle = new HashSet<Riddle>();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void UpdatedRiddls();
        public event UpdatedRiddls Updated;
        [Key]
        public int Id_quest { get; set; }
        private bool status;
        public bool Status
        {
            get { return status; }
            set
            {
                status = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }
        int number_of_questions;
        public int Number_of_questions
        {
            get { return number_of_questions; }
            set
            {
                number_of_questions = value;
                if (this.PropertyChanged != null) { Updated(); this.PropertyChanged(this, new PropertyChangedEventArgs("Number_of_questions")); }
            }
        }
        string thematics;
        [Column(TypeName = "text")]
        public string Thematics
        {
            get { return thematics; }
            set
            {
                thematics = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Thematics"));
            }
        }
        public int Id_Level_FK { get; set; }

        public int Id_Autor_FK { get; set; }

        public DateTime Date { get; set; }
        Level_of_complexity level_of_complexity;
        public virtual Level_of_complexity Level_of_complexity
        {
            get { return level_of_complexity; }
            set
            {
                level_of_complexity = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Level_of_complexity"));
            }
        }
        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        private ICollection<Riddle> riddle;
        public virtual ICollection<Riddle> Riddle
        {
            get { return riddle; }
            set
            {
                riddle = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Riddle"));
            }
        }
    }
}
