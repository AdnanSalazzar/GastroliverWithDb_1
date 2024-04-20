using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GastroliverWithDb.Models;

public partial class MtechGastroLiverContext : DbContext
{
    public MtechGastroLiverContext()
    {
    }

    public MtechGastroLiverContext(DbContextOptions<MtechGastroLiverContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblPatient> TblPatients { get; set; }

    public virtual DbSet<TblRoom> TblRooms { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=LAPTOP-95IL2ES0\\SQLEXPRESS;Database=MtechGastroLiver;Trusted_Connection=True;Encrypt=false; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblPatient>(entity =>
        {
            entity.HasKey(e => e.PatientId);

            entity.ToTable("tbl_patient");

            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Nid).HasColumnName("nid");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phoneNo");
            entity.Property(e => e.RoomId).HasColumnName("room_id");

            entity.HasOne(d => d.Room).WithMany(p => p.TblPatients)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_patient_tbl_room");
        });

        modelBuilder.Entity<TblRoom>(entity =>
        {
            entity.HasKey(e => e.RoomId);

            entity.ToTable("tbl_room");

            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.RoomNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("roomNo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
