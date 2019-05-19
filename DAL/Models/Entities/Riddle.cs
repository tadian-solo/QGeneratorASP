namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Riddle")]
    public partial class Riddle: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Riddle()
        {
            Quest = new HashSet<Quest>();
        }

        [Key]
        public int Id_riddle { get; set; }
        string text;
        [Column(TypeName = "text")]
        [Required]
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            }
        }
        string description;
        [Column(TypeName = "text")]
        [Required]
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Description"));
            }
        }
        bool status;
        public bool Status
        {
            get { return status; }
            set
            {
                status = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }
        int id_Autor_FK;
        public int Id_Autor_FK
        {
            get { return id_Autor_FK; }
            set
            {
                id_Autor_FK = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Id_Autor_FK"));
            }
        }
        int id_Level_FK;
        public int Id_Level_FK
        {
            get { return id_Level_FK; }
            set
            {
                id_Level_FK = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Id_Level_FK"));
            }
        }
        int id_Answer_FK;
        public int Id_Answer_FK
        {
            get { return id_Answer_FK; }
            set
            {
                id_Answer_FK = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Id_Answer_FK"));
            }
        }
        int id_Type_FK;
        public int Id_Type_FK
        {
            get { return id_Type_FK; }
            set
            {
                id_Type_FK = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Id_Type_FK"));
            }
        }
        public int? Id_Image_FK { get; set; }
        Answer answer;
        public virtual Answer Answer
        {
            get { return answer; }
            set
            {
                answer = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Answer"));
            }
        }
        public virtual Image Image { get; set; }
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
        Type_of_question type_of_question;
        public virtual Type_of_question Type_of_question
        {
            get { return type_of_question; }
            set
            {
                type_of_question = value;
                if (this.PropertyChanged != null) this.PropertyChanged(this, new PropertyChangedEventArgs("Type_of_question"));
            }
        }
        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quest> Quest { get; set; }
    }
}
