using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReactNetCoreDB.Models
{
    public partial class AdventureWorks2014Context : DbContext
    {

        public AdventureWorks2014Context()
        {}
        public AdventureWorks2014Context(DbContextOptions<AdventureWorks2014Context> options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "Production");

                entity.HasIndex(e => e.Name)
                    .HasName("AK_Product_Name")
                    .IsUnique();

                entity.HasIndex(e => e.ProductNumber)
                    .HasName("AK_Product_ProductNumber")
                    .IsUnique();

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Class).HasColumnType("nchar(2)");

                entity.Property(e => e.Color).HasMaxLength(15);

                entity.Property(e => e.ListPrice).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("Name");

                entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");

                entity.Property(e => e.ProductNumber)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.ProductSubcategoryId).HasColumnName("ProductSubcategoryID");

                entity.Property(e => e.Size).HasMaxLength(5);

                entity.Property(e => e.Style).HasColumnType("nchar(2)");

                entity.Property(e => e.Weight).HasColumnType("decimal");

                entity.HasOne(d => d.ProductModel)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductModelId);

                entity.HasOne(d => d.ProductSubcategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductSubcategoryId);

            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory", "Production");

                entity.HasIndex(e => e.Name)
                    .HasName("AK_ProductCategory_Name")
                    .IsUnique();

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("Name");
            });

            modelBuilder.Entity<ProductDescription>(entity =>
            {
                entity.ToTable("ProductDescription", "Production");

                entity.Property(e => e.ProductDescriptionId).HasColumnName("ProductDescriptionID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(400);

            });

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.ToTable("ProductModel", "Production");

                entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");

            });

            modelBuilder.Entity<ProductModelProductDescriptionCulture>(entity =>
            {
                entity.HasKey(e => new { e.ProductModelId, e.ProductDescriptionId, e.CultureId })
                    .HasName("PK_ProductModelProductDescriptionCulture_ProductModelID_ProductDescriptionID_CultureID");

                entity.ToTable("ProductModelProductDescriptionCulture", "Production");

                entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");

                entity.Property(e => e.ProductDescriptionId).HasColumnName("ProductDescriptionID");

                entity.Property(e => e.CultureId)
                    .HasColumnName("CultureID")
                    .HasColumnType("nchar(6)");

                entity.HasOne(d => d.ProductDescription)
                    .WithMany(p => p.ProductModelProductDescriptionCulture)
                    .HasForeignKey(d => d.ProductDescriptionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ProductModel)
                    .WithMany(p => p.ProductModelProductDescriptionCulture)
                    .HasForeignKey(d => d.ProductModelId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductPhoto>(entity =>
            {
                entity.ToTable("ProductPhoto", "Production");

                entity.Property(e => e.ProductPhotoId).HasColumnName("ProductPhotoID");
            });

            modelBuilder.Entity<ProductProductPhoto>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.ProductPhotoId })
                    .HasName("PK_ProductProductPhoto_ProductID_ProductPhotoID");

                entity.ToTable("ProductProductPhoto", "Production");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductPhotoId).HasColumnName("ProductPhotoID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductProductPhoto)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ProductPhoto)
                    .WithMany(p => p.ProductProductPhoto)
                    .HasForeignKey(d => d.ProductPhotoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductSubcategory>(entity =>
            {
                entity.ToTable("ProductSubcategory", "Production");

                entity.Property(e => e.ProductSubcategoryId).HasColumnName("ProductSubcategoryID");

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.ProductSubcategory)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TransactionHistory>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK_TransactionHistory_TransactionID");

                entity.ToTable("TransactionHistory", "Production");

                entity.HasIndex(e => e.ProductId)
                    .HasName("IX_TransactionHistory_ProductID");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.TransactionType)
                    .IsRequired()
                    .HasColumnType("nchar(1)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TransactionHistory)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductDescription> ProductDescription { get; set; }
        public virtual DbSet<ProductModel> ProductModel { get; set; }
        public virtual DbSet<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulture { get; set; }
        public virtual DbSet<ProductPhoto> ProductPhoto { get; set; }
        public virtual DbSet<ProductProductPhoto> ProductProductPhoto { get; set; }
        public virtual DbSet<ProductSubcategory> ProductSubcategory { get; set; }
        public virtual DbSet<TransactionHistory> TransactionHistory { get; set; }
    }
}