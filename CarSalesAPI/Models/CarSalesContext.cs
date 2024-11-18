using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarSalesAPI.Models;

public partial class CarSalesContext : DbContext
{
    public CarSalesContext()
    {
    }

    public CarSalesContext(DbContextOptions<CarSalesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Vin).HasName("PK__cars__C5DF234DF0955F0A");

            entity.ToTable("cars");

            entity.Property(e => e.Vin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VIN");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("brand");
            entity.Property(e => e.BuildYear).HasColumnName("build_year");
            entity.Property(e => e.Color)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Con)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("con");
            entity.Property(e => e.DoorCount).HasColumnName("door_count");
            entity.Property(e => e.FuelType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("fuel_type");
            entity.Property(e => e.Modell)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("modell");
            entity.Property(e => e.Power).HasColumnName("power");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Weight).HasColumnName("weight");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => e.Url).HasName("PK__pages__DD778416D4071580");

            entity.ToTable("pages");

            entity.Property(e => e.Url)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("url");
            entity.Property(e => e.Page1)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("page");
            entity.Property(e => e.Permission)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("permission");
            entity.Property(e => e.Sortingorder).HasColumnName("sortingorder");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__users__F3DBC573AF80E29A");

            entity.ToTable("users");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.HashedPsw)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("hashed_psw");
            entity.Property(e => e.JoinDate).HasColumnName("join_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Permission)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValue("_1_")
                .HasColumnName("permission");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("unknown")
                .HasColumnName("phone_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
