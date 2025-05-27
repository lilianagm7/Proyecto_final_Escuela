using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pruebas.Models
{
    public partial class EscuelaBContext : DbContext
    {
        public EscuelaBContext(DbContextOptions<EscuelaBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alumno> Alumnos { get; set; } = null!;
        public virtual DbSet<AlumnoSeMatriculaAsignatura> AlumnoSeMatriculaAsignaturas { get; set; } = null!;
        public virtual DbSet<Asignatura> Asignaturas { get; set; } = null!;
        public virtual DbSet<CursoEscolar> CursoEscolars { get; set; } = null!;
        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Grado> Grados { get; set; } = null!;
        public virtual DbSet<Profesor> Profesors { get; set; } = null!;

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=DESKTOP-STSSI82;database=EscuelaB;integrated security=true;encrypt=false;");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.ToTable("alumno");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido1");

                entity.Property(e => e.Apellido2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido2");

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ciudad");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nacimiento");

                entity.Property(e => e.Nif)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("nif");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sexo")
                    .IsFixedLength();

                entity.Property(e => e.Telefono)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<AlumnoSeMatriculaAsignatura>(entity =>
            {
                entity.HasKey(e => new { e.IdAlumno, e.IdAsignatura, e.IdCursoEscolar })
                    .HasName("PK__alumno_s__0F4142E9B0FD4EF0");

                entity.ToTable("alumno_se_matricula_asignatura");

                entity.Property(e => e.IdAlumno).HasColumnName("id_alumno");

                entity.Property(e => e.IdAsignatura).HasColumnName("id_asignatura");

                entity.Property(e => e.IdCursoEscolar).HasColumnName("id_curso_escolar");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.AlumnoSeMatriculaAsignaturas)
                    .HasForeignKey(d => d.IdAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__alumno_se__id_al__4AB81AF0");

                entity.HasOne(d => d.IdAsignaturaNavigation)
                    .WithMany(p => p.AlumnoSeMatriculaAsignaturas)
                    .HasForeignKey(d => d.IdAsignatura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__alumno_se__id_as__4BAC3F29");

                entity.HasOne(d => d.IdCursoEscolarNavigation)
                    .WithMany(p => p.AlumnoSeMatriculaAsignaturas)
                    .HasForeignKey(d => d.IdCursoEscolar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__alumno_se__id_cu__4CA06362");
            });

            modelBuilder.Entity<Asignatura>(entity =>
            {
                entity.ToTable("asignatura");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Creditos).HasColumnName("creditos");

                entity.Property(e => e.Cuatrimestre).HasColumnName("cuatrimestre");

                entity.Property(e => e.Curso).HasColumnName("curso");

                entity.Property(e => e.IdGrado).HasColumnName("id_grado");

                entity.Property(e => e.IdProfesor).HasColumnName("id_profesor");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipo");

                entity.HasOne(d => d.IdGradoNavigation)
                    .WithMany(p => p.Asignaturas)
                    .HasForeignKey(d => d.IdGrado)
                    .HasConstraintName("FK__asignatur__id_gr__47DBAE45");

                entity.HasOne(d => d.IdProfesorNavigation)
                    .WithMany(p => p.Asignaturas)
                    .HasForeignKey(d => d.IdProfesor)
                    .HasConstraintName("FK__asignatur__id_pr__46E78A0C");
            });

            modelBuilder.Entity<CursoEscolar>(entity =>
            {
                entity.ToTable("curso_escolar");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnyoFin).HasColumnName("anyo_fin");

                entity.Property(e => e.AnyoInicio).HasColumnName("anyo_inicio");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("departamento");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Grado>(entity =>
            {
                entity.ToTable("grado");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.ToTable("profesor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido1");

                entity.Property(e => e.Apellido2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido2");

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ciudad");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nacimiento");

                entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");

                entity.Property(e => e.Nif)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("nif");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sexo")
                    .IsFixedLength();

                entity.Property(e => e.Telefono)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Profesors)
                    .HasForeignKey(d => d.IdDepartamento)
                    .HasConstraintName("FK__profesor__id_dep__3A81B327");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
