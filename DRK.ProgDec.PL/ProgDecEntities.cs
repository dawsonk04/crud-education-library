﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DRK.ProgDec.PL;

public partial class ProgDecEntities : DbContext
{
    public ProgDecEntities()
    {
    }

    public ProgDecEntities(DbContextOptions<ProgDecEntities> options)
        : base(options)
    {
    }

    public virtual DbSet<tblAdvisor> tblAdvisors { get; set; }

    public virtual DbSet<tblDeclaration> tblDeclarations { get; set; }

    public virtual DbSet<tblDegreeType> tblDegreeTypes { get; set; }

    public virtual DbSet<tblProgram> tblPrograms { get; set; }

    public virtual DbSet<tblStudent> tblStudents { get; set; }

    public virtual DbSet<tblStudentAdvisor> tblStudentAdvisors { get; set; }

    public virtual DbSet<tblUser> tblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DRK.ProgDec.DB;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tblAdvisor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblAdvis__3214EC075496670A");

            entity.ToTable("tblAdvisor");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblDeclaration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblDecla__3214EC076E2716FE");

            entity.ToTable("tblDeclaration");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ChangeDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblDegreeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblDegre__3214EC07F7E0D6B8");

            entity.ToTable("tblDegreeType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblProgram>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblProgr__3214EC0773F92A77");

            entity.ToTable("tblProgram");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ImagePath).IsUnicode(false);
        });

        modelBuilder.Entity<tblStudent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblStude__3214EC0742E63E77");

            entity.ToTable("tblStudent");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudentId)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<tblStudentAdvisor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblStude__3214EC07EC23AA04");

            entity.ToTable("tblStudentAdvisor");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<tblUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblUser__3214EC07126141FA");

            entity.ToTable("tblUser");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(28)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
