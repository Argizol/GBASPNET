﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.Models;

#nullable disable

namespace Store.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20240109185001_SecondCreate")]
    partial class SecondCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProductStore", b =>
                {
                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.Property<int>("StoresId")
                        .HasColumnType("int");

                    b.HasKey("ProductsId", "StoresId");

                    b.HasIndex("StoresId");

                    b.ToTable("StorageProduct", (string)null);
                });

            modelBuilder.Entity("Store.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ProductName");

                    b.HasKey("Id")
                        .HasName("GroupID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ProductGroups", (string)null);
                });

            modelBuilder.Entity("Store.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Description");

                    b.Property<int>("GroupID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ProductName");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("Price");

                    b.HasKey("Id")
                        .HasName("ProductID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Store.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int")
                        .HasColumnName("ProductCount");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("StorageName");

                    b.HasKey("Id")
                        .HasName("StoreID");

                    b.ToTable("Storage", (string)null);
                });

            modelBuilder.Entity("ProductStore", b =>
                {
                    b.HasOne("Store.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Store.Models.Store", null)
                        .WithMany()
                        .HasForeignKey("StoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Store.Models.Product", b =>
                {
                    b.HasOne("Store.Models.Group", "Group")
                        .WithMany("Products")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("GroupToProduct");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Store.Models.Group", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
