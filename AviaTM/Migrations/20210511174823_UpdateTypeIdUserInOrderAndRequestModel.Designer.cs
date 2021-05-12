﻿// <auto-generated />
using System;
using AviaTM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AviaTM.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210511174823_UpdateTypeIdUserInOrderAndRequestModel")]
    partial class UpdateTypeIdUserInOrderAndRequestModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("AviaTM.DB.Model.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("AnotherContact")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NameOrganization")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UNP")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("isLockdown")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.Cargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("CostDelivery")
                        .HasColumnType("integer");

                    b.Property<int>("Depth")
                        .HasColumnType("integer");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<int>("IdRouteMap")
                        .HasColumnType("integer");

                    b.Property<int>("IdTypeCurrency")
                        .HasColumnType("integer");

                    b.Property<int>("IdTypePayment")
                        .HasColumnType("integer");

                    b.Property<string>("IdUser")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("OrderDataId")
                        .HasColumnType("integer");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.Property<bool>("isStatus")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("IdRouteMap");

                    b.HasIndex("IdTypeCurrency");

                    b.HasIndex("IdTypePayment");

                    b.HasIndex("IdUser");

                    b.HasIndex("OrderDataId");

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.OrderData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("IdUser")
                        .HasColumnType("text");

                    b.Property<int?>("OrderMainId")
                        .HasColumnType("integer");

                    b.Property<int?>("RequestMainId")
                        .HasColumnType("integer");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.HasIndex("OrderMainId");

                    b.HasIndex("RequestMainId");

                    b.ToTable("OrderData");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.OrderMain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("IdTransport")
                        .HasColumnType("integer");

                    b.Property<string>("IdUser")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("IdTransport");

                    b.HasIndex("IdUser");

                    b.ToTable("OrderMain");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.RequestMain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("IdTransport")
                        .HasColumnType("integer");

                    b.Property<string>("IdUser")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("IdTransport");

                    b.HasIndex("IdUser");

                    b.ToTable("RequestMain");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.RouteMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("CityFrom")
                        .HasColumnType("text");

                    b.Property<string>("CityTo")
                        .HasColumnType("text");

                    b.Property<string>("CountryCodeFrom")
                        .HasColumnType("text");

                    b.Property<string>("CountryCodeTo")
                        .HasColumnType("text");

                    b.Property<string>("CountyFrom")
                        .HasColumnType("text");

                    b.Property<string>("CountyTo")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FullAddressFrom")
                        .HasColumnType("text");

                    b.Property<string>("FullAddressTo")
                        .HasColumnType("text");

                    b.Property<string>("PostCodeFrom")
                        .HasColumnType("text");

                    b.Property<string>("PostCodeTo")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("StateFrom")
                        .HasColumnType("text");

                    b.Property<string>("StateTo")
                        .HasColumnType("text");

                    b.Property<string>("StreetFrom")
                        .HasColumnType("text");

                    b.Property<string>("StreetTo")
                        .HasColumnType("text");

                    b.Property<float>("latFrom")
                        .HasColumnType("real");

                    b.Property<float>("latTo")
                        .HasColumnType("real");

                    b.Property<float>("lngFrom")
                        .HasColumnType("real");

                    b.Property<float>("lngTo")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("RouteMaps");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.Transport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("Depth")
                        .HasColumnType("integer");

                    b.Property<double>("FuelConsumption")
                        .HasColumnType("double precision");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<int>("IdRouteMap")
                        .HasColumnType("integer");

                    b.Property<int>("IdTransLoadCapacity")
                        .HasColumnType("integer");

                    b.Property<int>("IdTypeTransport")
                        .HasColumnType("integer");

                    b.Property<string>("IdUser")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("MaxLoadCapacity")
                        .HasColumnType("integer");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("NumberAxes")
                        .HasColumnType("integer");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IdRouteMap");

                    b.HasIndex("IdTransLoadCapacity");

                    b.HasIndex("IdTypeTransport");

                    b.HasIndex("IdUser");

                    b.ToTable("Transports");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.TransportLoadCapacity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("MaxValue")
                        .HasColumnType("integer");

                    b.Property<int>("MinValue")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("TransportLoadCapacity");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.TypeCargo", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("TypeCargoes");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.TypeCurrency", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("TypeCurrency");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.TypePayment", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("TypePayment");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.TypeTransport", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("TypeTransports");
                });

            modelBuilder.Entity("CargoTypeCargo", b =>
                {
                    b.Property<int>("CargosId")
                        .HasColumnType("integer");

                    b.Property<int>("TypeCargoId")
                        .HasColumnType("integer");

                    b.HasKey("CargosId", "TypeCargoId");

                    b.HasIndex("TypeCargoId");

                    b.ToTable("CargoTypeCargo");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TypeCargoTypeTransport", b =>
                {
                    b.Property<int>("TypeCargosId")
                        .HasColumnType("integer");

                    b.Property<int>("TypeTransportsId")
                        .HasColumnType("integer");

                    b.HasKey("TypeCargosId", "TypeTransportsId");

                    b.HasIndex("TypeTransportsId");

                    b.ToTable("TypeCargoTypeTransport");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.Cargo", b =>
                {
                    b.HasOne("AviaTM.DB.Model.Models.RouteMap", "RouteMap")
                        .WithMany("Cargos")
                        .HasForeignKey("IdRouteMap")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.DB.Model.Models.TypeCurrency", "TypeCurrency")
                        .WithMany("Cargos")
                        .HasForeignKey("IdTypeCurrency")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.DB.Model.Models.TypePayment", "TypePayment")
                        .WithMany("Cargos")
                        .HasForeignKey("IdTypePayment")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.DB.Model.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("IdUser");

                    b.HasOne("AviaTM.DB.Model.Models.OrderData", null)
                        .WithMany("Cargoes")
                        .HasForeignKey("OrderDataId");

                    b.Navigation("AppUser");

                    b.Navigation("RouteMap");

                    b.Navigation("TypeCurrency");

                    b.Navigation("TypePayment");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.OrderData", b =>
                {
                    b.HasOne("AviaTM.DB.Model.Models.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("IdUser");

                    b.HasOne("AviaTM.DB.Model.Models.OrderMain", null)
                        .WithMany("OrderDats")
                        .HasForeignKey("OrderMainId");

                    b.HasOne("AviaTM.DB.Model.Models.RequestMain", null)
                        .WithMany("OrderDats")
                        .HasForeignKey("RequestMainId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.OrderMain", b =>
                {
                    b.HasOne("AviaTM.DB.Model.Models.Transport", "Transport")
                        .WithMany()
                        .HasForeignKey("IdTransport")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.DB.Model.Models.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("IdUser");

                    b.Navigation("Transport");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.RequestMain", b =>
                {
                    b.HasOne("AviaTM.DB.Model.Models.Transport", "Transport")
                        .WithMany()
                        .HasForeignKey("IdTransport")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.DB.Model.Models.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("IdUser");

                    b.Navigation("Transport");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.Transport", b =>
                {
                    b.HasOne("AviaTM.DB.Model.Models.RouteMap", "RouteMap")
                        .WithMany()
                        .HasForeignKey("IdRouteMap")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.DB.Model.Models.TransportLoadCapacity", "TransportLoadCapacity")
                        .WithMany("Transports")
                        .HasForeignKey("IdTransLoadCapacity")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.DB.Model.Models.TypeTransport", "TypeTransport")
                        .WithMany()
                        .HasForeignKey("IdTypeTransport")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.DB.Model.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("IdUser");

                    b.Navigation("AppUser");

                    b.Navigation("RouteMap");

                    b.Navigation("TransportLoadCapacity");

                    b.Navigation("TypeTransport");
                });

            modelBuilder.Entity("CargoTypeCargo", b =>
                {
                    b.HasOne("AviaTM.DB.Model.Models.Cargo", null)
                        .WithMany()
                        .HasForeignKey("CargosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.DB.Model.Models.TypeCargo", null)
                        .WithMany()
                        .HasForeignKey("TypeCargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AviaTM.DB.Model.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AviaTM.DB.Model.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.DB.Model.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AviaTM.DB.Model.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TypeCargoTypeTransport", b =>
                {
                    b.HasOne("AviaTM.DB.Model.Models.TypeCargo", null)
                        .WithMany()
                        .HasForeignKey("TypeCargosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.DB.Model.Models.TypeTransport", null)
                        .WithMany()
                        .HasForeignKey("TypeTransportsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.OrderData", b =>
                {
                    b.Navigation("Cargoes");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.OrderMain", b =>
                {
                    b.Navigation("OrderDats");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.RequestMain", b =>
                {
                    b.Navigation("OrderDats");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.RouteMap", b =>
                {
                    b.Navigation("Cargos");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.TransportLoadCapacity", b =>
                {
                    b.Navigation("Transports");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.TypeCurrency", b =>
                {
                    b.Navigation("Cargos");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.TypePayment", b =>
                {
                    b.Navigation("Cargos");
                });
#pragma warning restore 612, 618
        }
    }
}
