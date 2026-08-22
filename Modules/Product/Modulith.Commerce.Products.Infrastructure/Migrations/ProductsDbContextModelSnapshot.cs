
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modulith.Commerce.Products.Infrastructure.Data;

#nullable disable

namespace Modulith.Commerce.Products.Infrastructure.Migrations
{
    [DbContext(typeof(ProductsDbContext))]
    partial class ProductsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("products")
                .HasAnnotation("ProductVersion", "10.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.Brands.Brand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Brands", "products");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.Categories.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories", "products");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.CategoryProperties.CategoryProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AddToFilter")
                        .HasColumnType("bit");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("CategoryProperties", "products");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.CategoryPropertyValues.CategoryPropertyValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CategoryPropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ProductVariantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryPropertyId");

                    b.HasIndex("ProductVariantId");

                    b.ToTable("CategoryPropertyValues", "products");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.Models.Model", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("BrandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Models", "products");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.ProductCategories.ProductCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductId", "CategoryId")
                        .IsUnique();

                    b.ToTable("ProductCategories", "products");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.ProductVariantImages.ProductVariantImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProductVariantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductVariantId");

                    b.ToTable("ProductVariantImages", "products");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.ProductVariantProperties.ProductVariantProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CategoryPropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProductVariantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryPropertyId");

                    b.HasIndex("ProductVariantId", "CategoryPropertyId")
                        .IsUnique();

                    b.ToTable("ProductVariantProperties", "products");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.ProductVariants.ProductVariant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DiscountEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DiscountStartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("TaxRate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductVariants", "products");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("AvgRating")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3, 2)
                        .HasColumnType("decimal(3,2)")
                        .HasDefaultValue(0m);

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFeatured")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTimeOffset?>("PublishedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("ReviewCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("ShortDescription")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("TaxClass")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("standard");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Products", "products");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.StaffMembers.StaffMember", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("Email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("FullName");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Status");

                    b.Property<Guid?>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("StaffMembers", "products");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(MAX)");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Occured")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Processed")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages", "products");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.Brands.Brand", b =>
                {
                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "Name", b1 =>
                        {
                            b1.Property<Guid>("BrandId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("BrandId");

                            b1.ToTable("Brands", "products");

                            b1.WithOwner()
                                .HasForeignKey("BrandId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.Categories.Category", b =>
                {
                    b.HasOne("Modulith.Commerce.Products.Domain.Categories.Category", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "Name", b1 =>
                        {
                            b1.Property<Guid>("CategoryId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("CategoryId");

                            b1.ToTable("Categories", "products");

                            b1.WithOwner()
                                .HasForeignKey("CategoryId");
                        });

                    b.OwnsOne("Modulith.Commerce.Products.Domain.Categories.Icon", "Icon", b1 =>
                        {
                            b1.Property<Guid>("CategoryId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Icon");

                            b1.HasKey("CategoryId");

                            b1.ToTable("Categories", "products");

                            b1.WithOwner()
                                .HasForeignKey("CategoryId");
                        });

                    b.Navigation("Icon");

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.CategoryProperties.CategoryProperty", b =>
                {
                    b.HasOne("Modulith.Commerce.Products.Domain.Categories.Category", "Category")
                        .WithMany("Properties")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "Name", b1 =>
                        {
                            b1.Property<Guid>("CategoryPropertyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("CategoryPropertyId");

                            b1.ToTable("CategoryProperties", "products");

                            b1.WithOwner()
                                .HasForeignKey("CategoryPropertyId");
                        });

                    b.Navigation("Category");

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.CategoryPropertyValues.CategoryPropertyValue", b =>
                {
                    b.HasOne("Modulith.Commerce.Products.Domain.CategoryProperties.CategoryProperty", "CategoryProperty")
                        .WithMany("Values")
                        .HasForeignKey("CategoryPropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modulith.Commerce.Products.Domain.ProductVariants.ProductVariant", null)
                        .WithMany("CategoryPropertyValues")
                        .HasForeignKey("ProductVariantId");

                    b.Navigation("CategoryProperty");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.Models.Model", b =>
                {
                    b.HasOne("Modulith.Commerce.Products.Domain.Brands.Brand", "Brand")
                        .WithMany("Models")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "Name", b1 =>
                        {
                            b1.Property<Guid>("ModelId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("ModelId");

                            b1.ToTable("Models", "products");

                            b1.WithOwner()
                                .HasForeignKey("ModelId");
                        });

                    b.Navigation("Brand");

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.ProductCategories.ProductCategory", b =>
                {
                    b.HasOne("Modulith.Commerce.Products.Domain.Categories.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modulith.Commerce.Products.Domain.Products.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.ProductVariantImages.ProductVariantImage", b =>
                {
                    b.HasOne("Modulith.Commerce.Products.Domain.ProductVariants.ProductVariant", "ProductVariant")
                        .WithMany("Images")
                        .HasForeignKey("ProductVariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Modulith.Commerce.Products.Domain.ProductVariantImages.ImageUrl", "ImageUrl", b1 =>
                        {
                            b1.Property<Guid>("ProductVariantImageId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ImageUrl");

                            b1.HasKey("ProductVariantImageId");

                            b1.ToTable("ProductVariantImages", "products");

                            b1.WithOwner()
                                .HasForeignKey("ProductVariantImageId");
                        });

                    b.Navigation("ImageUrl")
                        .IsRequired();

                    b.Navigation("ProductVariant");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.ProductVariantProperties.ProductVariantProperty", b =>
                {
                    b.HasOne("Modulith.Commerce.Products.Domain.CategoryProperties.CategoryProperty", "CategoryProperty")
                        .WithMany()
                        .HasForeignKey("CategoryPropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Modulith.Commerce.Products.Domain.ProductVariants.ProductVariant", "ProductVariant")
                        .WithMany("Properties")
                        .HasForeignKey("ProductVariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryProperty");

                    b.Navigation("ProductVariant");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.ProductVariants.ProductVariant", b =>
                {
                    b.HasOne("Modulith.Commerce.Products.Domain.Products.Product", "Product")
                        .WithMany("Variants")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Modulith.Commerce.Products.Domain.ProductVariants.Barcode", "Barcode", b1 =>
                        {
                            b1.Property<Guid>("ProductVariantId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Barcode");

                            b1.HasKey("ProductVariantId");

                            b1.ToTable("ProductVariants", "products");

                            b1.WithOwner()
                                .HasForeignKey("ProductVariantId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Money", "Cost", b1 =>
                        {
                            b1.Property<Guid>("ProductVariantId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Cost_Amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("Cost_Currency");

                            b1.HasKey("ProductVariantId");

                            b1.ToTable("ProductVariants", "products");

                            b1.WithOwner()
                                .HasForeignKey("ProductVariantId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Money", "DiscountCount", b1 =>
                        {
                            b1.Property<Guid>("ProductVariantId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("DiscountCount_Amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("DiscountCount_Currency");

                            b1.HasKey("ProductVariantId");

                            b1.ToTable("ProductVariants", "products");

                            b1.WithOwner()
                                .HasForeignKey("ProductVariantId");
                        });

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("ProductVariantId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Price_Amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("Price_Currency");

                            b1.HasKey("ProductVariantId");

                            b1.ToTable("ProductVariants", "products");

                            b1.WithOwner()
                                .HasForeignKey("ProductVariantId");
                        });

                    b.OwnsOne("Modulith.Commerce.Products.Domain.ProductVariants.Sku", "Sku", b1 =>
                        {
                            b1.Property<Guid>("ProductVariantId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)")
                                .HasColumnName("Sku");

                            b1.HasKey("ProductVariantId");

                            b1.HasIndex("Value")
                                .IsUnique();

                            b1.ToTable("ProductVariants", "products");

                            b1.WithOwner()
                                .HasForeignKey("ProductVariantId");
                        });

                    b.OwnsOne("Modulith.Commerce.Products.Domain.ProductVariants.Stock", "Stock", b1 =>
                        {
                            b1.Property<Guid>("ProductVariantId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Quantity")
                                .HasColumnType("int")
                                .HasColumnName("Stock");

                            b1.HasKey("ProductVariantId");

                            b1.ToTable("ProductVariants", "products");

                            b1.WithOwner()
                                .HasForeignKey("ProductVariantId");
                        });

                    b.Navigation("Barcode")
                        .IsRequired();

                    b.Navigation("Cost");

                    b.Navigation("DiscountCount")
                        .IsRequired();

                    b.Navigation("Price")
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Sku")
                        .IsRequired();

                    b.Navigation("Stock")
                        .IsRequired();
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.Products.Product", b =>
                {
                    b.HasOne("Modulith.Commerce.Products.Domain.Models.Model", "Model")
                        .WithMany("Products")
                        .HasForeignKey("ModelId");

                    b.OwnsOne("Modulith.Commerce.Common.Domain.ValueObjects.Text", "Name", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products", "products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("Modulith.Commerce.Products.Domain.Products.SeoMetadata", "Seo", b1 =>
                        {
                            b1.Property<Guid>("ProductId");

                            b1.Property<string>("Description");

                            b1.PrimitiveCollection<string>("Keywords")
                                .IsRequired();

                            b1.Property<string>("OgImage");

                            b1.Property<string>("Title");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products", "products");

                            b1
                                .ToJson("Seo")
                                .HasColumnType("nvarchar(max)");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.OwnsOne("Modulith.Commerce.Products.Domain.Products.Slug", "Slug", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(280)
                                .HasColumnType("nvarchar(280)")
                                .HasColumnName("Slug");

                            b1.HasKey("ProductId");

                            b1.HasIndex("Value")
                                .IsUnique()
                                .HasFilter("[DeletedDate] IS NULL");

                            b1.ToTable("Products", "products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Model");

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Seo");

                    b.Navigation("Slug")
                        .IsRequired();
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.Brands.Brand", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.Categories.Category", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Properties");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.CategoryProperties.CategoryProperty", b =>
                {
                    b.Navigation("Values");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.Models.Model", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.ProductVariants.ProductVariant", b =>
                {
                    b.Navigation("CategoryPropertyValues");

                    b.Navigation("Images");

                    b.Navigation("Properties");
                });

            modelBuilder.Entity("Modulith.Commerce.Products.Domain.Products.Product", b =>
                {
                    b.Navigation("ProductCategories");

                    b.Navigation("Variants");
                });
#pragma warning restore 612, 618
        }
    }
}
