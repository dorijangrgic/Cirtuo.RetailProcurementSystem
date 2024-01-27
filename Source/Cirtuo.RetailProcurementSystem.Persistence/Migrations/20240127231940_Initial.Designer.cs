﻿// <auto-generated />
using System;
using Cirtuo.RetailProcurementSystem.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cirtuo.RetailProcurementSystem.Persistence.Migrations
{
    [DbContext(typeof(RetailProcurementDbContext))]
    [Migration("20240127231940_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("phone");

                    b.HasKey("Id")
                        .HasName("pk_contact");

                    b.ToTable("contact", (string)null);
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("address");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("city");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("state");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("zip_code");

                    b.HasKey("Id")
                        .HasName("pk_location");

                    b.ToTable("location", (string)null);
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("ContactId")
                        .HasColumnType("integer")
                        .HasColumnName("contact_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_manager");

                    b.HasIndex("ContactId")
                        .HasDatabaseName("ix_manager_contact_id");

                    b.ToTable("manager", (string)null);
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("delivery_date");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("order_date");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("payment_date");

                    b.Property<int>("RetailerId")
                        .HasColumnType("integer")
                        .HasColumnName("retailer_id");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("total_price");

                    b.HasKey("Id")
                        .HasName("pk_order");

                    b.HasIndex("RetailerId")
                        .HasDatabaseName("ix_order_retailer_id");

                    b.ToTable("order", (string)null);
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<decimal>("ItemPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("item_price");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer")
                        .HasColumnName("order_id");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<int>("SupplierStoreItemId")
                        .HasColumnType("integer")
                        .HasColumnName("supplier_store_item_id");

                    b.HasKey("Id")
                        .HasName("pk_order_item");

                    b.HasIndex("OrderId")
                        .HasDatabaseName("ix_order_item_order_id");

                    b.HasIndex("SupplierStoreItemId")
                        .HasDatabaseName("ix_order_item_supplier_store_item_id");

                    b.ToTable("order_item", (string)null);
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.Retailer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("ContactId")
                        .HasColumnType("integer")
                        .HasColumnName("contact_id");

                    b.Property<int>("LocationId")
                        .HasColumnType("integer")
                        .HasColumnName("location_id");

                    b.Property<int>("ManagerId")
                        .HasColumnType("integer")
                        .HasColumnName("manager_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_retailer");

                    b.HasIndex("ContactId")
                        .HasDatabaseName("ix_retailer_contact_id");

                    b.HasIndex("LocationId")
                        .HasDatabaseName("ix_retailer_location_id");

                    b.HasIndex("ManagerId")
                        .HasDatabaseName("ix_retailer_manager_id");

                    b.ToTable("retailer", (string)null);
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.StoreItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("Category")
                        .HasColumnType("integer")
                        .HasColumnName("category");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("sku");

                    b.HasKey("Id")
                        .HasName("pk_store_item");

                    b.HasIndex("Sku")
                        .IsUnique()
                        .HasDatabaseName("ix_store_item_sku");

                    b.ToTable("store_item", (string)null);
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int>("ContactId")
                        .HasColumnType("integer")
                        .HasColumnName("contact_id");

                    b.Property<int>("LocationId")
                        .HasColumnType("integer")
                        .HasColumnName("location_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_supplier");

                    b.HasIndex("ContactId")
                        .HasDatabaseName("ix_supplier_contact_id");

                    b.HasIndex("LocationId")
                        .HasDatabaseName("ix_supplier_location_id");

                    b.ToTable("supplier", (string)null);
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.SupplierRetailer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<int>("RetailerId")
                        .HasColumnType("integer")
                        .HasColumnName("retailer_id");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.Property<int>("SupplierId")
                        .HasColumnType("integer")
                        .HasColumnName("supplier_id");

                    b.HasKey("Id")
                        .HasName("pk_supplier_retailer");

                    b.HasIndex("RetailerId")
                        .HasDatabaseName("ix_supplier_retailer_retailer_id");

                    b.HasIndex("SupplierId")
                        .HasDatabaseName("ix_supplier_retailer_supplier_id");

                    b.ToTable("supplier_retailer", (string)null);
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.SupplierStoreItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<decimal>("ItemPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("item_price");

                    b.Property<int>("Quarter")
                        .HasColumnType("integer")
                        .HasColumnName("quarter");

                    b.Property<int>("SoldItems")
                        .HasColumnType("integer")
                        .HasColumnName("sold_items");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.Property<int>("StoreItemId")
                        .HasColumnType("integer")
                        .HasColumnName("store_item_id");

                    b.Property<int>("SupplierId")
                        .HasColumnType("integer")
                        .HasColumnName("supplier_id");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.HasKey("Id")
                        .HasName("pk_supplier_store_item");

                    b.HasIndex("StoreItemId")
                        .HasDatabaseName("ix_supplier_store_item_store_item_id");

                    b.HasIndex("SupplierId")
                        .HasDatabaseName("ix_supplier_store_item_supplier_id");

                    b.ToTable("supplier_store_item", (string)null);
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.Manager", b =>
                {
                    b.HasOne("Cirtuo.RetailProcurementSystem.Domain.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_manager_contact_contact_id");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.Order", b =>
                {
                    b.HasOne("Cirtuo.RetailProcurementSystem.Domain.Retailer", "Retailer")
                        .WithMany("Orders")
                        .HasForeignKey("RetailerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_retailer_retailer_id");

                    b.Navigation("Retailer");
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.OrderItem", b =>
                {
                    b.HasOne("Cirtuo.RetailProcurementSystem.Domain.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_item_order_order_id");

                    b.HasOne("Cirtuo.RetailProcurementSystem.Domain.SupplierStoreItem", "SupplierStoreItem")
                        .WithMany("OrderItems")
                        .HasForeignKey("SupplierStoreItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_item_supplier_store_item_supplier_store_item_id");

                    b.Navigation("Order");

                    b.Navigation("SupplierStoreItem");
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.Retailer", b =>
                {
                    b.HasOne("Cirtuo.RetailProcurementSystem.Domain.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_retailer_contact_contact_id");

                    b.HasOne("Cirtuo.RetailProcurementSystem.Domain.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_retailer_location_location_id");

                    b.HasOne("Cirtuo.RetailProcurementSystem.Domain.Manager", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_retailer_manager_manager_id");

                    b.Navigation("Contact");

                    b.Navigation("Location");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.Supplier", b =>
                {
                    b.HasOne("Cirtuo.RetailProcurementSystem.Domain.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_supplier_contact_contact_id");

                    b.HasOne("Cirtuo.RetailProcurementSystem.Domain.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_supplier_location_location_id");

                    b.Navigation("Contact");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.SupplierRetailer", b =>
                {
                    b.HasOne("Cirtuo.RetailProcurementSystem.Domain.Retailer", "Retailer")
                        .WithMany("SupplierRetailers")
                        .HasForeignKey("RetailerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_supplier_retailer_retailer_retailer_id");

                    b.HasOne("Cirtuo.RetailProcurementSystem.Domain.Supplier", "Supplier")
                        .WithMany("SupplierRetailers")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_supplier_retailer_supplier_supplier_id");

                    b.Navigation("Retailer");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.SupplierStoreItem", b =>
                {
                    b.HasOne("Cirtuo.RetailProcurementSystem.Domain.StoreItem", "StoreItem")
                        .WithMany("SupplierStoreItems")
                        .HasForeignKey("StoreItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_supplier_store_item_store_item_store_item_id");

                    b.HasOne("Cirtuo.RetailProcurementSystem.Domain.Supplier", "Supplier")
                        .WithMany("SupplierStoreItems")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_supplier_store_item_supplier_supplier_id");

                    b.Navigation("StoreItem");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.Retailer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("SupplierRetailers");
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.StoreItem", b =>
                {
                    b.Navigation("SupplierStoreItems");
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.Supplier", b =>
                {
                    b.Navigation("SupplierRetailers");

                    b.Navigation("SupplierStoreItems");
                });

            modelBuilder.Entity("Cirtuo.RetailProcurementSystem.Domain.SupplierStoreItem", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
