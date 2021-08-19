using DAL.DO.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.EF
{
    public partial class SolutionDbContext : DbContext
    {
        public SolutionDbContext() { }

        public SolutionDbContext(DbContextOptions<SolutionDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Gastos> Gastos { get; set; }
        public virtual DbSet<Ingresos> Ingresos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Categorias>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Gastos>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseIdentityColumn();

                entity.Property(e => e.Descripcion).HasMaxLength(150);

                entity.Property(e => e.Fecha).HasColumnType("datetime").ValueGeneratedOnAdd();

                entity.Property(e => e.Idcategoria).HasColumnName("IDCategoria");

                entity.Property(e => e.Idcliente)
                    .IsRequired()
                    .HasColumnName("IDCliente")
                    .HasMaxLength(450);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Gastos)
                    .HasForeignKey(d => d.Idcategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gastos_Categorias");

                /*entity.HasOne(d => d.Idcliente)
                    .WithMany(p => p.Gastos)
                    .HasForeignKey(d => d.Idcliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gastos_AspNetUsers");*/
            });

            modelBuilder.Entity<Ingresos>(entity =>
            {
                entity.Property(e => e.Id)
                    .UseIdentityColumn();

                entity.Property(e => e.Descripcion).HasMaxLength(200);

                entity.Property(e => e.Fecha).HasColumnType("datetime").ValueGeneratedOnAdd();

                entity.Property(e => e.Idcategoria).HasColumnName("IDCategoria");

                entity.Property(e => e.Idusuario)
                    .IsRequired()
                    .HasColumnName("IDUsuario")
                    .HasMaxLength(450);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Monto)
                .IsRequired()
                .HasColumnName("Monto");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Ingresos)
                    .HasForeignKey(d => d.Idcategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingresos_Categorias");

                //entity.HasOne(d => d.IdusuarioNavigation)
                //    .WithMany(p => p.Ingresos)
                //    .HasForeignKey(d => d.Idusuario)
                //   .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Ingresos_AspNetUsers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
