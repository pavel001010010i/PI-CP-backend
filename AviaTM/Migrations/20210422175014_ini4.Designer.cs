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
    [Migration("20210422175014_ini4")]
    partial class ini4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("AviaTM.DB.Model.Models.InfoTransfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("IdCargo")
                        .HasColumnType("integer");

                    b.Property<int>("IdRoute")
                        .HasColumnType("integer");

                    b.Property<int>("IdTransport")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IdCargo");

                    b.HasIndex("IdRoute");

                    b.HasIndex("IdTransport");

                    b.ToTable("InfoTransfers");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.OrderData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("IdInfoTransfer")
                        .HasColumnType("integer");

                    b.Property<int>("IdOrder")
                        .HasColumnType("integer");

                    b.Property<int>("IdTypeUser")
                        .HasColumnType("integer");

                    b.Property<string>("IdUser")
                        .HasColumnType("text");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("IdInfoTransfer");

                    b.HasIndex("IdOrder");

                    b.HasIndex("IdTypeUser");

                    b.HasIndex("IdUser");

                    b.ToTable("OrderDats");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.OrderMain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("OrderMains");
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

                    b.Property<string>("CountyFrom")
                        .HasColumnType("text");

                    b.Property<string>("CountyTo")
                        .HasColumnType("text");

                    b.Property<string>("FullAddressFrom")
                        .HasColumnType("text");

                    b.Property<string>("FullAddressTo")
                        .HasColumnType("text");

                    b.Property<string>("IsoCountryFrom")
                        .HasColumnType("text");

                    b.Property<string>("IsoCountryTo")
                        .HasColumnType("text");

                    b.Property<string>("PostCodeFrom")
                        .HasColumnType("text");

                    b.Property<string>("PostCodeTo")
                        .HasColumnType("text");

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

                    b.Property<int>("CapacityWeight")
                        .HasColumnType("integer");

                    b.Property<int>("Depth")
                        .HasColumnType("integer");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<int>("IdTypeTransport")
                        .HasColumnType("integer");

                    b.Property<string>("IdUser")
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("IdTypeTransport");

                    b.HasIndex("IdUser");

                    b.ToTable("Transports");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.TypeCargo", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("TypeCargoes");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.TypeTransport", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("TypeTransports");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.TypeUser", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("TypeUsers");
                });

            modelBuilder.Entity("AviaTM.Db.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

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

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("AviaTM.Db.Models.Cargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("Depth")
                        .HasColumnType("integer");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<string>("IdUser")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.Property<bool>("isStatus")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("AviaTM.Db.Models.RegisterViewModel", b =>
                {
                    b.Property<string>("Login")
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordConfirm")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Login");

                    b.ToTable("Registers");
                });

            modelBuilder.Entity("AviaTM.Db.Models.RequestDelivery", b =>
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

                    b.ToTable("RequestDeliveries");
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

            modelBuilder.Entity("AviaTM.DB.Model.Models.InfoTransfer", b =>
                {
                    b.HasOne("AviaTM.Db.Models.Cargo", "Cargo")
                        .WithMany("InfoTransfers")
                        .HasForeignKey("IdCargo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.DB.Model.Models.RouteMap", "RouteMap")
                        .WithMany("InfoTransfers")
                        .HasForeignKey("IdRoute")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.DB.Model.Models.Transport", "Transport")
                        .WithMany("InfoTransfers")
                        .HasForeignKey("IdTransport")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("RouteMap");

                    b.Navigation("Transport");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.OrderData", b =>
                {
                    b.HasOne("AviaTM.DB.Model.Models.InfoTransfer", "InfoTransfer")
                        .WithMany("OrderDatas")
                        .HasForeignKey("IdInfoTransfer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.DB.Model.Models.OrderMain", "Order")
                        .WithMany("OrderDatas")
                        .HasForeignKey("IdOrder")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.DB.Model.Models.TypeUser", "TypeUser")
                        .WithMany()
                        .HasForeignKey("IdTypeUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.Db.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("IdUser");

                    b.Navigation("AppUser");

                    b.Navigation("InfoTransfer");

                    b.Navigation("Order");

                    b.Navigation("TypeUser");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.Transport", b =>
                {
                    b.HasOne("AviaTM.DB.Model.Models.TypeTransport", "TypeTransport")
                        .WithMany()
                        .HasForeignKey("IdTypeTransport")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AviaTM.Db.Models.AppUser", "AppUser")
                        .WithMany("Transports")
                        .HasForeignKey("IdUser");

                    b.Navigation("AppUser");

                    b.Navigation("TypeTransport");
                });

            modelBuilder.Entity("AviaTM.Db.Models.Cargo", b =>
                {
                    b.HasOne("AviaTM.Db.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("IdUser");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("AviaTM.Db.Models.RequestDelivery", b =>
                {
                    b.HasOne("AviaTM.Db.Models.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");
                });

            modelBuilder.Entity("CargoTypeCargo", b =>
                {
                    b.HasOne("AviaTM.Db.Models.Cargo", null)
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
                    b.HasOne("AviaTM.Db.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AviaTM.Db.Models.AppUser", null)
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

                    b.HasOne("AviaTM.Db.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AviaTM.Db.Models.AppUser", null)
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

            modelBuilder.Entity("AviaTM.DB.Model.Models.InfoTransfer", b =>
                {
                    b.Navigation("OrderDatas");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.OrderMain", b =>
                {
                    b.Navigation("OrderDatas");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.RouteMap", b =>
                {
                    b.Navigation("InfoTransfers");
                });

            modelBuilder.Entity("AviaTM.DB.Model.Models.Transport", b =>
                {
                    b.Navigation("InfoTransfers");
                });

            modelBuilder.Entity("AviaTM.Db.Models.AppUser", b =>
                {
                    b.Navigation("Transports");
                });

            modelBuilder.Entity("AviaTM.Db.Models.Cargo", b =>
                {
                    b.Navigation("InfoTransfers");
                });
#pragma warning restore 612, 618
        }
    }
}
