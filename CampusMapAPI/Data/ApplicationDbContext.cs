using CampusMapAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusMapAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Building> Buildings { get; set; }

        public virtual DbSet<Hotspot> Hotspots { get; set; }

        public virtual DbSet<Media> Media { get; set; }

        public virtual DbSet<Scene> Scenes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Building>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("building");

                entity.Property(e => e.Id).HasColumnName("Building_ID");
                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<Hotspot>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("hotspot");

                entity.HasIndex(e => e.MediaId, "Media_ID_idx");

                entity.HasIndex(e => e.SceneId, "Scene_ID_idx");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("Hotspot_ID");
                entity.Property(e => e.IconType)
                    .HasMaxLength(20)
                    .HasColumnName("Icon_Type");
                entity.Property(e => e.MediaId).HasColumnName("Media_ID");
                entity.Property(e => e.Name).HasMaxLength(20);
                entity.Property(e => e.Pitch).HasPrecision(10);
                entity.Property(e => e.SceneId).HasColumnName("Scene_ID");
                entity.Property(e => e.TargetSceneId).HasColumnName("Target_Scene_ID");
                entity.Property(e => e.Text).HasColumnType("text");
                entity.Property(e => e.Type).HasMaxLength(20);
                entity.Property(e => e.Yaw).HasPrecision(10);
                entity.Property(e => e.TargetPitch)
                    .HasPrecision(10)
                    .HasColumnName("TargetPitch");
                entity.Property(e => e.TargetYaw)
                    .HasPrecision(10)
                    .HasColumnName("TargetYaw");

                entity.HasOne(d => d.Media).WithMany(p => p.Hotspots)
                    .HasForeignKey(d => d.MediaId)
                    .HasConstraintName("Media_ID");

                entity.HasOne(d => d.Scene).WithMany(p => p.Hotspots)
                    .HasForeignKey(d => d.SceneId)
                    .HasConstraintName("Scene_ID");
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("media");

                entity.Property(e => e.Id).HasColumnName("Media_ID");
                entity.Property(e => e.MediaType)
                    .HasMaxLength(50)
                    .HasColumnName("Media_Type");
                entity.Property(e => e.Title).HasMaxLength(20);
                entity.Property(e => e.Url)
                    .HasColumnType("text")
                    .HasColumnName("URL");
            });

            modelBuilder.Entity<Scene>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("scene");

                entity.HasIndex(e => e.BuildingId, "Building_ID_idx");

                entity.HasIndex(e => e.PreviewMediaId, "Preview_Media_ID_idx");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("Scene_ID");
                entity.Property(e => e.BuildingId).HasColumnName("Building_ID");
                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.Name).HasMaxLength(45);
                entity.Property(e => e.PreviewMediaId).HasColumnName("Preview_Media_ID");
                entity.Property(e => e.Scenecol)
                    .HasMaxLength(45)
                    .HasColumnName("scenecol");
                entity.Property(e => e.StartHfov)
                    .HasPrecision(10)
                    .HasColumnName("Start_HFov");

                entity.HasOne(d => d.Building).WithMany(p => p.Scenes)
                    .HasForeignKey(d => d.BuildingId)
                    .HasConstraintName("Building_ID");

                entity.HasOne(d => d.PreviewMedia).WithMany(p => p.Scenes)
                    .HasForeignKey(d => d.PreviewMediaId)
                    .HasConstraintName("Preview_Media_ID");
            });
        }
    }
}