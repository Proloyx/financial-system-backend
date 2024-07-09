using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinancialSystem.Models.DB.AppDBContext;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Ticker> Tickers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=pg-2b23edca-eloycruzmartin97-4d55.e.aivencloud.com;Port=28480;Database=FSObserverDB;Username=avnadmin;Password=AVNS_tvF7h_1a23ei4g63dG7");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Cik).HasName("companies_pkey");

            entity.ToTable("companies", "fso");

            entity.Property(e => e.Cik)
                .HasMaxLength(50)
                .HasColumnName("cik");
            entity.Property(e => e.Entityname)
                .HasMaxLength(100)
                .HasColumnName("entityname");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("logs", "fso");

            entity.Property(e => e.Exception).HasColumnName("exception");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.LogEvent)
                .HasColumnType("jsonb")
                .HasColumnName("log_event");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.MessageTemplate).HasColumnName("message_template");
            entity.Property(e => e.Timestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles", "fso");

            entity.HasIndex(e => e.Name, "roles_name_key").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Ticker>(entity =>
        {
            entity.HasKey(e => e.CikStr).HasName("tickers_pkey");

            entity.ToTable("tickers", "fso");

            entity.Property(e => e.CikStr)
                .HasMaxLength(50)
                .HasColumnName("cik_str");
            entity.Property(e => e.Ticker1)
                .HasMaxLength(50)
                .HasColumnName("ticker");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users", "fso");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("user_roles_role_id_fkey"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("user_roles_user_id_fkey"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("user_roles_pkey");
                        j.ToTable("user_roles", "fso");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
