using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarteBlanche.Models
{
    public partial class CarteBlancheContext : DbContext
    {
        public CarteBlancheContext()
        {
        }

        public CarteBlancheContext(DbContextOptions<CarteBlancheContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=CarteBlanche;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.Tid)
                    .HasName("PK__Task__C451DB3157ADDFB2");

                entity.ToTable("Task");

                entity.Property(e => e.Label)
                    .IsUnicode(false)
                    .HasColumnName("label");

                entity.Property(e => e.Priority)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("priority");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK_Task_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Cpwd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cpwd");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Pwd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pwd");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
