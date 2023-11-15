﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web.Data;

#nullable disable

namespace Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231115022045_updateTakeAway")]
    partial class updateTakeAway
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Web.Models.Bebida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ClasificacionBebidaRefId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("ImagemBebida")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("TamanoRefId")
                        .HasColumnType("int");

                    b.Property<int?>("TipoBebidaRefId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClasificacionBebidaRefId");

                    b.HasIndex("TamanoRefId");

                    b.HasIndex("TipoBebidaRefId");

                    b.ToTable("Bebida");
                });

            modelBuilder.Entity("Web.Models.ClasificacionBebida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.ToTable("ClasificacionBebida");
                });

            modelBuilder.Entity("Web.Models.ClasificacionComida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.ToTable("ClasificacionComida");
                });

            modelBuilder.Entity("Web.Models.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DireccionCliente")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("MetodoPagoRefId")
                        .HasColumnType("int");

                    b.Property<string>("NombreCliente")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("PrecioPedido")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("PromocionRefId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MetodoPagoRefId");

                    b.HasIndex("PromocionRefId");

                    b.ToTable("Delivery");
                });

            modelBuilder.Entity("Web.Models.Disponibilidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.ToTable("Disponibilidad");
                });

            modelBuilder.Entity("Web.Models.ListaPrecio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Producto")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("ListaPrecios");
                });

            modelBuilder.Entity("Web.Models.Mesas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("MetodoPagoRefId")
                        .HasColumnType("int");

                    b.Property<int>("NroMesa")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecioPedido")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("PromocionRefId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MetodoPagoRefId");

                    b.HasIndex("PromocionRefId");

                    b.ToTable("Mesas");
                });

            modelBuilder.Entity("Web.Models.MetodoPago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("MontoVariacion")
                        .HasColumnType("int");

                    b.Property<string>("VariaciónPago")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("MetodoPago");
                });

            modelBuilder.Entity("Web.Models.Plato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ClasificacionComidaRefId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("DisponibilidadRefId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.Property<string>("ImagemComida")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PorcionRefId")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("TipoComidaRefId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClasificacionComidaRefId");

                    b.HasIndex("DisponibilidadRefId");

                    b.HasIndex("PorcionRefId");

                    b.HasIndex("TipoComidaRefId");

                    b.ToTable("Plato");
                });

            modelBuilder.Entity("Web.Models.Porcion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.ToTable("Porcion");
                });

            modelBuilder.Entity("Web.Models.ProductosPedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeliveryId")
                        .HasColumnType("int");

                    b.Property<int>("ListaPrecioRefId")
                        .HasColumnType("int");

                    b.Property<int?>("MesasId")
                        .HasColumnType("int");

                    b.Property<int?>("TakeAwayId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("ListaPrecioRefId");

                    b.HasIndex("MesasId");

                    b.HasIndex("TakeAwayId");

                    b.ToTable("ProductosPedido");
                });

            modelBuilder.Entity("Web.Models.Promociones", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("ListaPreciosRefId")
                        .HasColumnType("int");

                    b.Property<int?>("MetodoPagoRefId")
                        .HasColumnType("int");

                    b.Property<int?>("MontoVariacionRefId")
                        .HasColumnType("int");

                    b.Property<decimal?>("TarifaPrecio")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime?>("ValidoHasta")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.HasIndex("ListaPreciosRefId");

                    b.HasIndex("MetodoPagoRefId");

                    b.HasIndex("MontoVariacionRefId");

                    b.ToTable("Promociones");
                });

            modelBuilder.Entity("Web.Models.TakeAway", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("HorarioEntrega")
                        .HasColumnType("datetime2");

                    b.Property<int>("MetodoPagoRefId")
                        .HasColumnType("int");

                    b.Property<string>("NombreCliente")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("PrecioPedido")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("PromocionRefId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MetodoPagoRefId");

                    b.HasIndex("PromocionRefId");

                    b.ToTable("TakeAway");
                });

            modelBuilder.Entity("Web.Models.Tamano", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.ToTable("Tamano");
                });

            modelBuilder.Entity("Web.Models.TipoBebida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.ToTable("TipoBebida");
                });

            modelBuilder.Entity("Web.Models.TipoComida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("FechaRegistro")
                        .HasColumnType("smalldatetime");

                    b.HasKey("Id");

                    b.ToTable("TipoComida");
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
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

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Web.Models.Bebida", b =>
                {
                    b.HasOne("Web.Models.ClasificacionBebida", "ClasificacionBebida")
                        .WithMany()
                        .HasForeignKey("ClasificacionBebidaRefId");

                    b.HasOne("Web.Models.Tamano", "Tamano")
                        .WithMany()
                        .HasForeignKey("TamanoRefId");

                    b.HasOne("Web.Models.TipoBebida", "TipoBebida")
                        .WithMany()
                        .HasForeignKey("TipoBebidaRefId");

                    b.Navigation("ClasificacionBebida");

                    b.Navigation("Tamano");

                    b.Navigation("TipoBebida");
                });

            modelBuilder.Entity("Web.Models.Delivery", b =>
                {
                    b.HasOne("Web.Models.MetodoPago", "MetodoPago")
                        .WithMany()
                        .HasForeignKey("MetodoPagoRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web.Models.Promociones", "Promocion")
                        .WithMany()
                        .HasForeignKey("PromocionRefId");

                    b.Navigation("MetodoPago");

                    b.Navigation("Promocion");
                });

            modelBuilder.Entity("Web.Models.Mesas", b =>
                {
                    b.HasOne("Web.Models.MetodoPago", "MetodoPago")
                        .WithMany()
                        .HasForeignKey("MetodoPagoRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web.Models.Promociones", "Promocion")
                        .WithMany()
                        .HasForeignKey("PromocionRefId");

                    b.Navigation("MetodoPago");

                    b.Navigation("Promocion");
                });

            modelBuilder.Entity("Web.Models.Plato", b =>
                {
                    b.HasOne("Web.Models.ClasificacionComida", "ClasificacionComida")
                        .WithMany()
                        .HasForeignKey("ClasificacionComidaRefId");

                    b.HasOne("Web.Models.Disponibilidad", "Disponibilidad")
                        .WithMany()
                        .HasForeignKey("DisponibilidadRefId");

                    b.HasOne("Web.Models.Porcion", "Porcion")
                        .WithMany()
                        .HasForeignKey("PorcionRefId");

                    b.HasOne("Web.Models.TipoComida", "TipoComida")
                        .WithMany()
                        .HasForeignKey("TipoComidaRefId");

                    b.Navigation("ClasificacionComida");

                    b.Navigation("Disponibilidad");

                    b.Navigation("Porcion");

                    b.Navigation("TipoComida");
                });

            modelBuilder.Entity("Web.Models.ProductosPedido", b =>
                {
                    b.HasOne("Web.Models.Delivery", null)
                        .WithMany("Productos")
                        .HasForeignKey("DeliveryId");

                    b.HasOne("Web.Models.ListaPrecio", "ListaPrecio")
                        .WithMany()
                        .HasForeignKey("ListaPrecioRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web.Models.Mesas", null)
                        .WithMany("Productos")
                        .HasForeignKey("MesasId");

                    b.HasOne("Web.Models.TakeAway", null)
                        .WithMany("Productos")
                        .HasForeignKey("TakeAwayId");

                    b.Navigation("ListaPrecio");
                });

            modelBuilder.Entity("Web.Models.Promociones", b =>
                {
                    b.HasOne("Web.Models.ListaPrecio", "ListaPrecios")
                        .WithMany()
                        .HasForeignKey("ListaPreciosRefId");

                    b.HasOne("Web.Models.MetodoPago", "MetodoPago")
                        .WithMany()
                        .HasForeignKey("MetodoPagoRefId");

                    b.HasOne("Web.Models.MetodoPago", "MontoVariacion")
                        .WithMany()
                        .HasForeignKey("MontoVariacionRefId");

                    b.Navigation("ListaPrecios");

                    b.Navigation("MetodoPago");

                    b.Navigation("MontoVariacion");
                });

            modelBuilder.Entity("Web.Models.TakeAway", b =>
                {
                    b.HasOne("Web.Models.MetodoPago", "MetodoPago")
                        .WithMany()
                        .HasForeignKey("MetodoPagoRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web.Models.Promociones", "Promocion")
                        .WithMany()
                        .HasForeignKey("PromocionRefId");

                    b.Navigation("MetodoPago");

                    b.Navigation("Promocion");
                });

            modelBuilder.Entity("Web.Models.Delivery", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("Web.Models.Mesas", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("Web.Models.TakeAway", b =>
                {
                    b.Navigation("Productos");
                });
#pragma warning restore 612, 618
        }
    }
}
