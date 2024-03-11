using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace uszoeb_Gyurko_Levente_backend.Models;

public partial class UszoebContext : DbContext
{
    public UszoebContext()
    {
    }

    public UszoebContext(DbContextOptions<UszoebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Orszagok> Orszagoks { get; set; }

    public virtual DbSet<Szamok> Szamoks { get; set; }

    public virtual DbSet<Versenyzok> Versenyzoks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=uszoeb;user=root;password=rootpwd", Microsoft.EntityFrameworkCore.ServerVersion.Parse("11.1.2-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_hungarian_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Orszagok>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("orszagok");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nev)
                .HasMaxLength(60)
                .HasColumnName("nev");
            entity.Property(e => e.Nobid)
                .HasMaxLength(3)
                .HasColumnName("nobid");
        });

        modelBuilder.Entity<Szamok>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("szamok");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nev)
                .HasMaxLength(40)
                .HasColumnName("nev");
            entity.Property(e => e.VersenyzoId)
                .HasColumnType("int(11)")
                .HasColumnName("versenyzoid");
        });

        modelBuilder.Entity<Versenyzok>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("versenyzok");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nem)
                .HasColumnType("text")
                .HasColumnName("nem");
            entity.Property(e => e.Nev)
                .HasMaxLength(60)
                .HasColumnName("nev");
            entity.Property(e => e.OrszagId)
                .HasColumnType("int(11)")
                .HasColumnName("orszagId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
