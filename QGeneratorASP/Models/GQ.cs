using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QGeneratorASP.Models
{
    public class GQ : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public GQ(DbContextOptions<GQ> options)
            : base(options)
        { }
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Level_of_complexity> Level_of_complexity { get; set; }
        public virtual DbSet<Quest> Quest { get; set; }
        public virtual DbSet<Riddle> Riddle { get; set; }
        public virtual DbSet<QuestRiddle> QuestRiddle { get; set; }
        public virtual DbSet<Type_of_question> Type_of_question { get; set; }
        public virtual DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(e => e.Id_answer);
              
                entity.Property(e => e.Note).HasColumnType("text");

                entity.Property(e => e.Object)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            

            modelBuilder.Entity<Level_of_complexity>(entity =>
            {
                entity.HasKey(e => e.Id_level);

                entity.ToTable("Level_of_complexity");

                entity.Property(e => e.Name_level)
                    .IsRequired()
                    .HasColumnName("Name_level")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note).HasColumnType("text");
            });

            modelBuilder.Entity<Quest>(entity =>
            {
                entity.HasKey(e => e.Id_quest);

                entity.Property(e => e.Id_quest).HasColumnName("Id_quest");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Id_autor_Fk).HasColumnName("Id_Autor_FK");

                entity.Property(e => e.Id_level_Fk).HasColumnName("Id_Level_FK");

                entity.Property(e => e.Number_of_questions).HasColumnName("Number_of_questions");

                entity.Property(e => e.Thematics).HasColumnType("nvarchar(1000)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Quest)
                    .HasForeignKey(d => d.Id_autor_Fk)
                    .HasConstraintName("FK_Quest_User");

                entity.HasOne(d => d.Level_of_complexity)
                    .WithMany(p => p.Quest)
                    .HasForeignKey(d => d.Id_level_Fk)
                    .HasConstraintName("FK_Quest_Level_of_complexity");
            });

            modelBuilder.Entity<QuestRiddle>(entity =>
            {
                entity.HasKey(e => new { e.Id_Quest_Fk, e.Id_Riddle_Fk });

                entity.ToTable("Quest_Riddle");

                entity.Property(e => e.Id_Quest_Fk).HasColumnName("Id_Quest_FK");

                entity.Property(e => e.Id_Riddle_Fk).HasColumnName("Id_Riddle_FK");

                entity.HasOne(d => d.Quest)
                    .WithMany(p => p.QuestRiddle)
                    .HasForeignKey(d => d.Id_Quest_Fk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quest_Riddle_Quest");

                entity.HasOne(d => d.Riddle)
                    .WithMany(p => p.QuestRiddle)
                    .HasForeignKey(d => d.Id_Riddle_Fk)
                    .HasConstraintName("FK_Quest_Riddle_Riddle");
            });

            modelBuilder.Entity<Riddle>(entity =>
            {
                entity.HasKey(e => e.Id_riddle);

                entity.Property(e => e.Id_riddle).HasColumnName("Id_riddle");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Id_Answer_FK).HasColumnName("Id_Answer_FK");

                entity.Property(e => e.Id_Autor_FK).HasColumnName("Id_Autor_FK");

                entity.Property(e => e.Id_Level_FK).HasColumnName("Id_Level_FK");

                entity.Property(e => e.Id_Type_FK).HasColumnName("Id_Type_FK");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.Riddle)
                    .HasForeignKey(d => d.Id_Answer_FK)
                    .HasConstraintName("FK_Riddle_Answer");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Riddle)
                    .HasForeignKey(d => d.Id_Autor_FK)
                    .HasConstraintName("FK_Riddle_User");
                

                entity.HasOne(d => d.Level_of_complexity)
                    .WithMany(p => p.Riddle)
                    .HasForeignKey(d => d.Id_Level_FK)
                    .HasConstraintName("FK_Riddle_Level_of_complexity");

                entity.HasOne(d => d.Type_of_question)
                    .WithMany(p => p.Riddle)
                    .HasForeignKey(d => d.Id_Type_FK)
                    .HasConstraintName("FK_Riddle_Type_of_question");
            });

            modelBuilder.Entity<Type_of_question>(entity =>
            {
                entity.HasKey(e => e.Id_type);

                entity.ToTable("Type_of_question");

                entity.Property(e => e.Id_type).HasColumnName("Id_type");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                
            });

            modelBuilder.Entity<User>(entity =>
            {
               // entity.HasKey(e => e.Id_user);

              //  entity.Property(e => e.Id_user).HasColumnName("Id_user");

                entity.Property(e => e.AccessLevel).HasColumnName("Access_level");

                /*entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
                    */
               /* entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);*/
            });
        }

    }
}
