﻿// <auto-generated />
using System;
using AviaTM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AviaTM.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("AviaTM.Models.Cargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<int>("Depth")
                        .HasColumnType("integer");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("AviaTM.Models.Country", b =>
                {
                    b.Property<string>("NameCountry")
                        .HasColumnType("text");

                    b.Property<string>("Index")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Latitude")
                        .HasColumnType("integer");

                    b.Property<int>("Longitude")
                        .HasColumnType("integer");

                    b.HasKey("NameCountry");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("AviaTM.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PassportData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("AviaTM.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("CargoId")
                        .HasColumnType("integer");

                    b.Property<double>("CastDelivery")
                        .HasColumnType("double precision");

                    b.Property<string>("CountryIdFrom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CountryIdTo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateDelivery")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateDeparture")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PlaneId")
                        .HasColumnType("integer");

                    b.Property<int>("ProviderId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("OrderId");

                    b.HasIndex("CargoId");

                    b.HasIndex("CountryIdFrom");

                    b.HasIndex("CountryIdTo");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PlaneId");

                    b.HasIndex("ProviderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AviaTM.Models.Plane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("CapacityWeight")
                        .HasColumnType("integer");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<int>("IdProvider")
                        .HasColumnType("integer");

                    b.Property<string>("ModelPlane")
                        .HasColumnType("text");

                    b.Property<string>("NamePlane")
                        .HasColumnType("text");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.Property<int>("depth")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IdProvider");

                    b.ToTable("Planes");
                });

            modelBuilder.Entity("AviaTM.Models.Provider", b =>
                {
                    b.Property<int>("ProviderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("CountresProvider")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<string>("LicenceNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NameCompany")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ProviderId");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("AviaTM.Models.RequestDelivery", b =>
                {
                    b.Property<int>("IdRequest")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("CargoId")
                        .HasColumnType("integer");

                    b.Property<double>("CastDelivery")
                        .HasColumnType("double precision");

                    b.Property<string>("CountryIdFrom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CountryIdTo")
                        .HasColumnType("text");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateDelivery")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateDeparture")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ProviderId")
                        .HasColumnType("integer");

                    b.Property<bool>("StatusRequest")
                        .HasColumnType("boolean");

                    b.HasKey("IdRequest");

                    b.HasIndex("CargoId");

                    b.HasIndex("CountryIdFrom");

                    b.HasIndex("CountryIdTo");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProviderId");

                    b.ToTable("RequestDeliveries");
                });

            modelBuilder.Entity("AviaTM.Models.RequestUser", b =>
                {
                    b.Property<string>("Login")
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Login");

                    b.ToTable("RequestUser");
                });

            modelBuilder.Entity("AviaTM.Models.User", b =>
                {
                    b.Property<string>("Login")
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<bool>("LockoutEnable")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Login");

                    b.ToTable("User");
                });

            modelBuilder.Entity("AviaTM.Models.Cargo", b =>
                {
                    b.HasOne("AviaTM.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AviaTM.Models.Order", b =>
                {
                    b.HasOne("AviaTM.Models.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.Models.Country", "Country2")
                        .WithMany()
                        .HasForeignKey("CountryIdFrom")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryIdTo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.Models.Plane", "Plane")
                        .WithMany()
                        .HasForeignKey("PlaneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.Models.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Country");

                    b.Navigation("Country2");

                    b.Navigation("Customer");

                    b.Navigation("Plane");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("AviaTM.Models.Plane", b =>
                {
                    b.HasOne("AviaTM.Models.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("IdProvider")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("AviaTM.Models.RequestDelivery", b =>
                {
                    b.HasOne("AviaTM.Models.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.Models.Country", "Country2")
                        .WithMany()
                        .HasForeignKey("CountryIdFrom")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryIdTo");

                    b.HasOne("AviaTM.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.Models.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Country");

                    b.Navigation("Country2");

                    b.Navigation("Customer");

                    b.Navigation("Provider");
                });
#pragma warning restore 612, 618
        }
    }
}
