namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GQ : DbContext
    {
        public GQ()
            : base("name=GQ")
        {
        }

        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Level_of_complexity> Level_of_complexity { get; set; }
        public virtual DbSet<Quest> Quest { get; set; }
        public virtual DbSet<Riddle> Riddle { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Type_of_question> Type_of_question { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>()
                .Property(e => e.Object)
                .IsUnicode(false);

            modelBuilder.Entity<Answer>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<Answer>()
                .HasMany(e => e.Riddle)
                .WithRequired(e => e.Answer)
                .HasForeignKey(e => e.Id_Answer_FK);

            modelBuilder.Entity<Image>()
                .HasMany(e => e.Riddle)
                .WithOptional(e => e.Image)
                .HasForeignKey(e => e.Id_Image_FK);

            modelBuilder.Entity<Level_of_complexity>()
                .Property(e => e.Name_level)
                .IsUnicode(false);

            modelBuilder.Entity<Level_of_complexity>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<Level_of_complexity>()
                .HasMany(e => e.Quest)
                .WithRequired(e => e.Level_of_complexity)
                .HasForeignKey(e => e.Id_Level_FK);

            modelBuilder.Entity<Level_of_complexity>()
                .HasMany(e => e.Riddle)
                .WithRequired(e => e.Level_of_complexity)
                .HasForeignKey(e => e.Id_Level_FK);

            modelBuilder.Entity<Quest>()
                .Property(e => e.Thematics)
                .IsUnicode(false);

            modelBuilder.Entity<Quest>()
                .HasMany(e => e.Riddle)
                .WithMany(e => e.Quest)
                .Map(m => m.ToTable("Quest_Riddle").MapLeftKey("Id_Quest_FK").MapRightKey("Id_Riddle_FK"));

            modelBuilder.Entity<Riddle>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<Riddle>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Type_of_question>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Type_of_question>()
                .HasMany(e => e.Riddle)
                .WithRequired(e => e.Type_of_question)
                .HasForeignKey(e => e.Id_Type_FK);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Quest)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Id_Autor_FK);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Riddle)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Id_Autor_FK);
        }
    }
}
