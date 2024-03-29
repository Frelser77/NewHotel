﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewHotel.Data;

#nullable disable

namespace NewHotel.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240308100350_TipoPensioneENUM")]
    partial class TipoPensioneENUM
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NewHotel.Models.Camera", b =>
                {
                    b.Property<int>("IdCamera")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCamera"));

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("NumeroCamera")
                        .HasColumnType("int");

                    b.Property<string>("TipoCamera")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdCamera");

                    b.ToTable("Camere");
                });

            modelBuilder.Entity("NewHotel.Models.Cliente", b =>
                {
                    b.Property<int>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCliente"));

                    b.Property<string>("CF")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Cellulare")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Citta")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Cognome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Provincia")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCliente");

                    b.ToTable("Clienti");
                });

            modelBuilder.Entity("NewHotel.Models.Pensione", b =>
                {
                    b.Property<int>("IdPensione")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPensione"));

                    b.Property<int>("CostoGiornaliero")
                        .HasColumnType("int");

                    b.Property<int>("TipoPensione")
                        .HasColumnType("int");

                    b.HasKey("IdPensione");

                    b.ToTable("Pensioni");
                });

            modelBuilder.Entity("NewHotel.Models.Prenotazione", b =>
                {
                    b.Property<int>("IdPrenotazione")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPrenotazione"));

                    b.Property<int>("Acconto")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCheckOut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataPrenotazione")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCamera")
                        .HasColumnType("int");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdPensione")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("NumeroOspiti")
                        .HasColumnType("int");

                    b.Property<decimal>("PrezzoTotale")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdPrenotazione");

                    b.HasIndex("IdCamera");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdPensione");

                    b.ToTable("Prenotazioni");
                });

            modelBuilder.Entity("NewHotel.Models.Servizio", b =>
                {
                    b.Property<int>("IdServizio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdServizio"));

                    b.Property<DateTime>("DataAggiunta")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdPrenotazione")
                        .HasColumnType("int");

                    b.Property<decimal>("PrezzoTot")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Quantita")
                        .HasColumnType("int");

                    b.Property<string>("TipoServizio")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdServizio");

                    b.HasIndex("IdPrenotazione");

                    b.ToTable("Servizi");
                });

            modelBuilder.Entity("NewHotel.Models.Utente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.HasKey("Id");

                    b.ToTable("Utente");
                });

            modelBuilder.Entity("NewHotel.Models.Prenotazione", b =>
                {
                    b.HasOne("NewHotel.Models.Camera", "Camera")
                        .WithMany("Prenotazioni")
                        .HasForeignKey("IdCamera")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewHotel.Models.Cliente", "Cliente")
                        .WithMany("Prenotazioni")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewHotel.Models.Pensione", "Pensione")
                        .WithMany("Prenotazioni")
                        .HasForeignKey("IdPensione")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Camera");

                    b.Navigation("Cliente");

                    b.Navigation("Pensione");
                });

            modelBuilder.Entity("NewHotel.Models.Servizio", b =>
                {
                    b.HasOne("NewHotel.Models.Prenotazione", "Prenotazione")
                        .WithMany("Servizi")
                        .HasForeignKey("IdPrenotazione")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prenotazione");
                });

            modelBuilder.Entity("NewHotel.Models.Camera", b =>
                {
                    b.Navigation("Prenotazioni");
                });

            modelBuilder.Entity("NewHotel.Models.Cliente", b =>
                {
                    b.Navigation("Prenotazioni");
                });

            modelBuilder.Entity("NewHotel.Models.Pensione", b =>
                {
                    b.Navigation("Prenotazioni");
                });

            modelBuilder.Entity("NewHotel.Models.Prenotazione", b =>
                {
                    b.Navigation("Servizi");
                });
#pragma warning restore 612, 618
        }
    }
}
