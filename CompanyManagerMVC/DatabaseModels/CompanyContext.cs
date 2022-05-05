using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CompanyManagerMVC.DatabaseModels
{
    public partial class CompanyContext : DbContext
    {
        public CompanyContext()
        {
        }

        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<File> Files { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<TaskStatus> TaskStatuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost; Port=5432; User Id=postgres; Password=12345; Database=Company;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.HasIndex(e => e.Name, "deparment_name_index")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.ToTable("file");

                entity.HasIndex(e => e.Location, "location_index")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Location)
                    .HasMaxLength(750)
                    .HasColumnName("location");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creation_date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CreatorId)
                    .HasConstraintName("fk_post_user_1");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_post_department_1");

                entity.HasMany(d => d.Files)
                    .WithMany(p => p.Posts)
                    .UsingEntity<Dictionary<string, object>>(
                        "PostFile",
                        l => l.HasOne<File>().WithMany().HasForeignKey("FileId").HasConstraintName("fk_post_file_file_1"),
                        r => r.HasOne<Post>().WithMany().HasForeignKey("PostId").HasConstraintName("fk_post_file_post_1"),
                        j =>
                        {
                            j.HasKey("PostId", "FileId").HasName("post_file_pkey");

                            j.ToTable("post_file");

                            j.IndexerProperty<int>("PostId").ValueGeneratedOnAdd().HasColumnName("post_id").UseIdentityAlwaysColumn();

                            j.IndexerProperty<int>("FileId").HasColumnName("file_id");
                        });
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("refresh_token");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creation_date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.CreationIp)
                    .HasMaxLength(255)
                    .HasColumnName("creation_ip");

                entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.Property(e => e.RevokeIp)
                    .HasMaxLength(255)
                    .HasColumnName("revoke_ip");

                entity.Property(e => e.Token)
                    .HasMaxLength(750)
                    .HasColumnName("token");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("fk_refresh_token_user_1");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("task");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatorId).HasColumnName("creator_id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.EndDate).HasColumnName("end_date");

                entity.Property(e => e.ExecutorId).HasColumnName("executor_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.TaskCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .HasConstraintName("fk_task_user_1");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_task_department_1");

                entity.HasOne(d => d.Executor)
                    .WithMany(p => p.TaskExecutors)
                    .HasForeignKey(d => d.ExecutorId)
                    .HasConstraintName("fk_task_user_2");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("fk_task_task_status_1");

                entity.HasMany(d => d.Files)
                    .WithMany(p => p.Tasks)
                    .UsingEntity<Dictionary<string, object>>(
                        "TaskFile",
                        l => l.HasOne<File>().WithMany().HasForeignKey("FileId").HasConstraintName("fk_task_file_file_1"),
                        r => r.HasOne<Task>().WithMany().HasForeignKey("TaskId").HasConstraintName("fk_task_file_task_1"),
                        j =>
                        {
                            j.HasKey("TaskId", "FileId").HasName("task_file_pkey");

                            j.ToTable("task_file");

                            j.IndexerProperty<int>("TaskId").ValueGeneratedOnAdd().HasColumnName("task_id").UseIdentityAlwaysColumn();

                            j.IndexerProperty<int>("FileId").HasColumnName("file_id");
                        });
            });

            modelBuilder.Entity<TaskStatus>(entity =>
            {
                entity.ToTable("task_status");

                entity.HasIndex(e => e.Name, "status_name_index")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "email_index")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "phone_index")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnName("registration_date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_user_department_1");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("fk_user_user_role_1");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("user_role");

                entity.HasIndex(e => e.Name, "role_index")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
