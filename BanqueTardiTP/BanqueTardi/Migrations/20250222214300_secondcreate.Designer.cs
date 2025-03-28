﻿// <auto-generated />
using System;
using BanqueTardi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BanqueTardi.MVC.Migrations
{
    [DbContext(typeof(ClientContext))]
    [Migration("20250222214300_secondcreate")]
    partial class secondcreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BanqueTardi.Models.Banque", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("InstitutionID")
                        .HasMaxLength(3)
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransitID")
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Banques", (string)null);
                });

            modelBuilder.Entity("BanqueTardi.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("CodePostale")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateNaissance")
                        .HasColumnType("datetime2");

                    b.Property<int>("NbDecouverts")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("NomParent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelephoneParent")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("BanqueTardi.Models.Compte", b =>
                {
                    b.Property<int>("CompteId")
                        .HasColumnType("int");

                    b.Property<int>("TypeCompteID")
                        .HasColumnType("int");

                    b.Property<int>("BanqueId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<decimal>("Solde")
                        .HasColumnType("money");

                    b.HasKey("CompteId", "TypeCompteID");

                    b.HasIndex("BanqueId");

                    b.HasIndex("ClientId");

                    b.HasIndex("TypeCompteID");

                    b.ToTable("Comptes", (string)null);
                });

            modelBuilder.Entity("BanqueTardi.Models.Operation", b =>
                {
                    b.Property<int>("OperationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OperationId"));

                    b.Property<int>("CompteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOperation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Montant")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TypeCompteID")
                        .HasColumnType("int");

                    b.Property<string>("TypeOperation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OperationId");

                    b.HasIndex("CompteId", "TypeCompteID");

                    b.ToTable("Operations", (string)null);
                });

            modelBuilder.Entity("BanqueTardi.Models.TypeCompte", b =>
                {
                    b.Property<int>("Identifiant")
                        .HasColumnType("int");

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TauxInteret")
                        .HasColumnType("int");

                    b.Property<int>("TauxInteretDecouvert")
                        .HasColumnType("int");

                    b.HasKey("Identifiant");

                    b.ToTable("TypeComptes", (string)null);
                });

            modelBuilder.Entity("BanqueTardi.Models.Compte", b =>
                {
                    b.HasOne("BanqueTardi.Models.Banque", "Banque")
                        .WithMany()
                        .HasForeignKey("BanqueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BanqueTardi.Models.Client", "Client")
                        .WithMany("Comptes")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BanqueTardi.Models.TypeCompte", "TypeCompte")
                        .WithMany()
                        .HasForeignKey("TypeCompteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Banque");

                    b.Navigation("Client");

                    b.Navigation("TypeCompte");
                });

            modelBuilder.Entity("BanqueTardi.Models.Operation", b =>
                {
                    b.HasOne("BanqueTardi.Models.Compte", "Compte")
                        .WithMany("Operations")
                        .HasForeignKey("CompteId", "TypeCompteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compte");
                });

            modelBuilder.Entity("BanqueTardi.Models.Client", b =>
                {
                    b.Navigation("Comptes");
                });

            modelBuilder.Entity("BanqueTardi.Models.Compte", b =>
                {
                    b.Navigation("Operations");
                });
#pragma warning restore 612, 618
        }
    }
}
