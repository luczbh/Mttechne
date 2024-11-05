﻿// <auto-generated />
using System;
using ApiProject.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiProject.Repository.Migrations
{
    [DbContext(typeof(OperationDBContext))]
    [Migration("20241104173450_CreatingData")]
    partial class CreatingData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiProject.Entities.DB.Balance", b =>
                {
                    b.Property<int>("BalanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BalanceId"));

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("SellerId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalCredits")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalDebits")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("BalanceId")
                        .HasName("PK_Balance");

                    b.HasIndex("ClientId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SellerId");

                    b.ToTable("Balance");
                });

            modelBuilder.Entity("ApiProject.Entities.DB.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientId")
                        .HasName("PK_Client");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("ApiProject.Entities.DB.Operation", b =>
                {
                    b.Property<int>("OperationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OperationId"));

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OperationDate")
                        .HasColumnType("datetime");

                    b.Property<int>("OperationType")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OperationId")
                        .HasName("PK_Operation");

                    b.HasIndex("ClientId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SellerId");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("ApiProject.Entities.DB.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId")
                        .HasName("PK_Product");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ApiProject.Entities.DB.Seller", b =>
                {
                    b.Property<int>("SellerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SellerId"));

                    b.Property<string>("SellerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SellerId")
                        .HasName("PK_Seller");

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("ApiProject.Entities.DB.Balance", b =>
                {
                    b.HasOne("ApiProject.Entities.DB.Client", "Client")
                        .WithMany("Balances")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK_Client_Balance");

                    b.HasOne("ApiProject.Entities.DB.Product", "Product")
                        .WithMany("Balances")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_Product_Balance");

                    b.HasOne("ApiProject.Entities.DB.Seller", "Seller")
                        .WithMany("Balances")
                        .HasForeignKey("SellerId")
                        .HasConstraintName("FK_Seller_Balance");

                    b.Navigation("Client");

                    b.Navigation("Product");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("ApiProject.Entities.DB.Operation", b =>
                {
                    b.HasOne("ApiProject.Entities.DB.Client", "Client")
                        .WithMany("Operations")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK_Client_Operation");

                    b.HasOne("ApiProject.Entities.DB.Product", "Product")
                        .WithMany("Operations")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Product_Operation");

                    b.HasOne("ApiProject.Entities.DB.Seller", "Seller")
                        .WithMany("Operations")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Seller_Operation");

                    b.Navigation("Client");

                    b.Navigation("Product");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("ApiProject.Entities.DB.Client", b =>
                {
                    b.Navigation("Balances");

                    b.Navigation("Operations");
                });

            modelBuilder.Entity("ApiProject.Entities.DB.Product", b =>
                {
                    b.Navigation("Balances");

                    b.Navigation("Operations");
                });

            modelBuilder.Entity("ApiProject.Entities.DB.Seller", b =>
                {
                    b.Navigation("Balances");

                    b.Navigation("Operations");
                });
#pragma warning restore 612, 618
        }
    }
}
