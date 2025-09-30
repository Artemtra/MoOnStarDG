using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace MoOnStarDG.DB;

public partial class MoOnStarDgContext : DbContext
{
    public MoOnStarDgContext()
    {
    }

    public MoOnStarDgContext(DbContextOptions<MoOnStarDgContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<LevelOfTraining> LevelOfTrainings { get; set; }

    public virtual DbSet<Sportsman> Sportsmen { get; set; }

    public virtual DbSet<Training> Training { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("userid=student;password=student;database=MoOnStarDG;server=192.168.200.13", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.39-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Category");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Grade");

            entity.HasIndex(e => e.IdSportsman, "FK_Grade_Sportsman_ID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Comment).HasMaxLength(1000);
            entity.Property(e => e.IdSportsman)
                .HasColumnType("int(11)")
                .HasColumnName("ID_Sportsman");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<LevelOfTraining>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("LevelOfTraining");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Sportsman>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Sportsman");

            entity.HasIndex(e => e.IdCategory, "FK_Sportsman");

            entity.HasIndex(e => e.IdLevel, "FK_Sportsman_LevelOfTraining_ID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.DdataBirsDay)
                .HasMaxLength(255)
                .HasColumnName("DDataBirsDay");
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.IdCategory)
                .HasColumnType("int(11)")
                .HasColumnName("ID_Category");
            entity.Property(e => e.IdLevel)
                .HasColumnType("int(11)")
                .HasColumnName("ID_Level");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Sportsmen)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("FK_Sportsman");

            entity.HasOne(d => d.IdLevelNavigation).WithMany(p => p.Sportsmen)
                .HasForeignKey(d => d.IdLevel)
                .HasConstraintName("FK_Sportsman_LevelOfTraining_ID");
        });

        modelBuilder.Entity<Training>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.IdGrade, "FK_Training_Grade_ID");

            entity.HasIndex(e => e.TypeId, "FK_Training_Math_ID");

            entity.HasIndex(e => e.IdSportsmen, "FK_Training_Sportsman_ID");

            entity.HasIndex(e => e.TrainingTime, "FK_Training_TrainingTime_ID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.IdGrade)
                .HasColumnType("int(11)")
                .HasColumnName("ID_grade");
            entity.Property(e => e.IdSportsmen)
                .HasColumnType("int(11)")
                .HasColumnName("ID_sportsmen");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.TrainingDate).HasMaxLength(255);
            entity.Property(e => e.TypeId).HasColumnType("int(11)");

            entity.HasOne(d => d.IdGradeNavigation).WithMany(p => p.Training)
                .HasForeignKey(d => d.IdGrade)
                .HasConstraintName("FK_Training_Grade_ID");

            entity.HasOne(d => d.IdSportsmenNavigation).WithMany(p => p.Training)
                .HasForeignKey(d => d.IdSportsmen)
                .HasConstraintName("FK_Training_Sportsman_ID");

            entity.HasOne(d => d.Type).WithMany(p => p.Training)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Training_Math_ID");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Type");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
