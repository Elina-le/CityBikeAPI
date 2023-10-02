using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;

namespace CityBikeAPI.Models;

public partial class CityBikeDbContext : DbContext
{
    public CityBikeDbContext()
    {
    }

    public CityBikeDbContext(DbContextOptions<CityBikeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Journey> Journeys { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            return;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Journey>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CoveredDistanceM).HasColumnName("Covered_distance_m");
            entity.Property(e => e.DepartureStationId).HasColumnName("Departure_station_id");
            entity.Property(e => e.DepartureStationName)
                .HasMaxLength(50)
                .HasColumnName("Departure_station_name");
            entity.Property(e => e.DurationSec).HasColumnName("Duration_sec");
            entity.Property(e => e.ReturnStationId).HasColumnName("Return_station_id");
            entity.Property(e => e.ReturnStationName)
                .HasMaxLength(50)
                .HasColumnName("Return_station_name");

            entity.HasOne(d => d.DepartureStation).WithMany(p => p.JourneyDepartureStations)
                .HasForeignKey(d => d.DepartureStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Journeys_Stations");

            entity.HasOne(d => d.ReturnStation).WithMany(p => p.JourneyReturnStations)
                .HasForeignKey(d => d.ReturnStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Journeys_Stations_ReturnStation");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Adress).HasMaxLength(50);
            entity.Property(e => e.Fid).HasColumnName("FID");
            entity.Property(e => e.Kaupunki).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Namn).HasMaxLength(50);
            entity.Property(e => e.Nimi).HasMaxLength(50);
            entity.Property(e => e.Operaattor).HasMaxLength(50);
            entity.Property(e => e.Osoite).HasMaxLength(50);
            entity.Property(e => e.Stad).HasMaxLength(50);
            entity.Property(e => e.X)
                .HasMaxLength(50)
                .HasColumnName("x");
            entity.Property(e => e.Y)
                .HasMaxLength(50)
                .HasColumnName("y");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
