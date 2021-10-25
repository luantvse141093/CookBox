using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CookingBox.Data.Entities
{
    public partial class CookBoxContext : DbContext
    {
        public CookBoxContext()
        {
        }

        public CookBoxContext(DbContextOptions<CookBoxContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<DishIngredient> DishIngredients { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuDetail> MenuDetails { get; set; }
        public virtual DbSet<MenuStore> MenuStores { get; set; }
        public virtual DbSet<Metarial> Metarials { get; set; }
        public virtual DbSet<Nutrient> Nutrients { get; set; }
        public virtual DbSet<NutrientDetail> NutrientDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Repice> Repices { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Step> Steps { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Taste> Tastes { get; set; }
        public virtual DbSet<TasteDetail> TasteDetails { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=CookBoxDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("Dish");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Status).HasDefaultValueSql("('True')");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Dishes)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_DC");
            });

            modelBuilder.Entity<DishIngredient>(entity =>
            {
                entity.ToTable("DishIngredient");

                entity.Property(e => e.DishId).HasColumnName("DishID");

                entity.Property(e => e.MetarialId).HasColumnName("MetarialID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.DishIngredients)
                    .HasForeignKey(d => d.DishId)
                    .HasConstraintName("FK_DD");

                entity.HasOne(d => d.Metarial)
                    .WithMany(p => p.DishIngredients)
                    .HasForeignKey(d => d.MetarialId)
                    .HasConstraintName("FK_DID");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.Status).HasDefaultValueSql("('True')");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK_MSR");
            });

            modelBuilder.Entity<MenuDetail>(entity =>
            {
                entity.ToTable("MenuDetail");

                entity.Property(e => e.DishId).HasColumnName("DishID");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.MenuDetails)
                    .HasForeignKey(d => d.DishId)
                    .HasConstraintName("FK_MD");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuDetails)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_MM");
            });

            modelBuilder.Entity<MenuStore>(entity =>
            {
                entity.ToTable("MenuStore");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuStores)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_MenuStore_Menu");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.MenuStores)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_MenuStore_Store");
            });

            modelBuilder.Entity<Metarial>(entity =>
            {
                entity.ToTable("Metarial");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Nutrient>(entity =>
            {
                entity.ToTable("Nutrient");

                entity.Property(e => e.Name).HasMaxLength(300);
            });

            modelBuilder.Entity<NutrientDetail>(entity =>
            {
                entity.ToTable("NutrientDetail");

                entity.Property(e => e.DishId).HasColumnName("DishID");

                entity.Property(e => e.NutrientId).HasColumnName("NutrientID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.NutrientDetails)
                    .HasForeignKey(d => d.DishId)
                    .HasConstraintName("FK_NutrientDetail_Dish");

                entity.HasOne(d => d.Nutrient)
                    .WithMany(p => p.NutrientDetails)
                    .HasForeignKey(d => d.NutrientId)
                    .HasConstraintName("FK_NutrientDetail_Nutrient");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Note).HasMaxLength(150);

                entity.Property(e => e.OrderStatus)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("PaymentID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_OP");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_OS");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_OU");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.DishId).HasColumnName("DishID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.DishId)
                    .HasConstraintName("FK_OD");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OO");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Repice>(entity =>
            {
                entity.ToTable("Repice");

                entity.Property(e => e.DishId).HasColumnName("DishID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.Repices)
                    .HasForeignKey(d => d.DishId)
                    .HasConstraintName("FK_RD");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Session");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Step>(entity =>
            {
                entity.ToTable("Step");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RepiceId).HasColumnName("RepiceID");

                entity.HasOne(d => d.Repice)
                    .WithMany(p => p.Steps)
                    .HasForeignKey(d => d.RepiceId)
                    .HasConstraintName("FK_SR");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Taste>(entity =>
            {
                entity.ToTable("Taste");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TasteDetail>(entity =>
            {
                entity.ToTable("TasteDetail");

                entity.Property(e => e.DishId).HasColumnName("DishID");

                entity.Property(e => e.TasteId).HasColumnName("TasteID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.TasteDetails)
                    .HasForeignKey(d => d.DishId)
                    .HasConstraintName("FK_TasteDetail_Dish");

                entity.HasOne(d => d.Taste)
                    .WithMany(p => p.TasteDetails)
                    .HasForeignKey(d => d.TasteId)
                    .HasConstraintName("FK_TasteDetail_Taste");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("RoleID");

                entity.Property(e => e.Status).HasDefaultValueSql("('True')");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UR");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
