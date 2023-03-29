using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.DataDB;

public partial class TestDbContext : DbContext
{
    public TestDbContext()
    {
    }

    public TestDbContext(DbContextOptions<TestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<JsonDatum> JsonData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("my_connection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JsonDatum>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Added)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("added");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.EndYear)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("end_year");
            entity.Property(e => e.Impact)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("impact");
            entity.Property(e => e.Insight)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("insight");
            entity.Property(e => e.Intensity).HasColumnName("intensity");
            entity.Property(e => e.Likelihood).HasColumnName("likelihood");
            entity.Property(e => e.Pestle)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("pestle");
            entity.Property(e => e.Publicshed)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("publicshed");
            entity.Property(e => e.Region)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("region");
            entity.Property(e => e.Relevance).HasColumnName("relevance");
            entity.Property(e => e.Source)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("source");
            entity.Property(e => e.StartYear)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("start_year");
            entity.Property(e => e.Title)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.Topic)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("topic");
            entity.Property(e => e.Url)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("url");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
