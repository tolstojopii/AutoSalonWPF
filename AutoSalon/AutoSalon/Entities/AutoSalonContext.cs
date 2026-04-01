using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalon.Entities
{
    public class AutoSalonContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Productphoto> Productphotos { get; set; }
        public DbSet<Productsale> Productsales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Строка подключения — измените под свои параметры (пароль, порт)
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=AutoSalon;Username=postgres;Password=1111");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка Gender
            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasKey(e => e.Code);
                entity.Property(e => e.Code)
                      .HasMaxLength(1)
                      .IsFixedLength()
                      .ValueGeneratedNever(); // не генерировать значение
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            // Настройка Client
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Firstname).HasMaxLength(50);
                entity.Property(e => e.Lastname).HasMaxLength(50);
                entity.Property(e => e.Patronymic).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Photopath).HasMaxLength(1000);
                entity.Property(e => e.Registrationtime).HasColumnType("timestamp without time zone");

                entity.HasOne(d => d.GendercodeNavigation)
                      .WithMany(p => p.Clients)
                      .HasForeignKey(d => d.Gendercode)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // Настройка Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            // Настройка User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Login).HasMaxLength(50);
                entity.Property(e => e.Password).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                      .WithMany(p => p.Users)
                      .HasForeignKey(d => d.Roleid)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // Настройка Manufacturer
            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            // Настройка Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).HasMaxLength(100);
                entity.Property(e => e.Cost).HasColumnType("money");
                entity.Property(e => e.Description).HasMaxLength(10000);
                entity.Property(e => e.Mainimage).HasMaxLength(1000);
                entity.Property(e => e.Isactive).HasColumnType("bit(1)");

                entity.HasOne(d => d.Manufacturer)
                      .WithMany(p => p.Products)
                      .HasForeignKey(d => d.Manufacturerid);
            });

            // Настройка Productphoto
            modelBuilder.Entity<Productphoto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Photopath).HasMaxLength(1000);

                entity.HasOne(d => d.Product)
                      .WithMany(p => p.Productphotos)
                      .HasForeignKey(d => d.Productid)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // Настройка Productsale
            modelBuilder.Entity<Productsale>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Saledate).HasColumnType("timestamp without time zone");

                entity.HasOne(d => d.Product)
                      .WithMany(p => p.Productsales)
                      .HasForeignKey(d => d.Productid)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Client)
                      .WithMany(c => c.Productsales)
                      .HasForeignKey(d => d.Clientid)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}