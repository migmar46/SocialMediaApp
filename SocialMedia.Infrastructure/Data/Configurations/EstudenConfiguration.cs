using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.Data.Configurations
{
    public class EstudenConfiguration : IEntityTypeConfiguration<Estuden>
    {
            public void Configure(EntityTypeBuilder<Estuden> builder)
            {
                builder.ToTable("Estudiantes");

                builder.HasKey(e => e.Id);

                builder.Property(e => e.Id)
                    .HasColumnName("IdEstudiante");

            builder.Property(e => e.Nombre)
                    .HasColumnName("Nombre")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.Property(e => e.Apellido)
                    .HasColumnName("Apellido")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.Property(e => e.Materia)
                    .HasColumnName("Materia")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.Property(e => e.Nota1)
                    .HasColumnName("Nota1")
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

            builder.Property(e => e.Nota2)
                    .HasColumnName("Nota2")
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

            builder.Property(e => e.Nota3)
                    .HasColumnName("Nota3")
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

            //builder.HasOne(d => d.User)
            //        .WithMany(p => p.Estudens)
            //        .HasForeignKey(d => d.IdEstudiante)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Publicacion_Usuario");

        }
    }
}

