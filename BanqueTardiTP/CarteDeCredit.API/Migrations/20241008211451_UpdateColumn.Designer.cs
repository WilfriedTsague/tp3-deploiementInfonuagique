﻿// <auto-generated />
using CarteDeCredit.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarteDeCredit.API.Migrations
{
    [DbContext(typeof(CarteDeCreditDBContext))]
    [Migration("20241008211451_UpdateColumn")]
    partial class UpdateColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("CarteDeCredit.API.Models.CarteCredit", b =>
                {
                    b.Property<string>("Numero")
                        .HasColumnType("TEXT");

                    b.Property<int>("LimiteCredit")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NomDemandeur")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeCarte")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Numero");

                    b.ToTable("CarteCredits");
                });
#pragma warning restore 612, 618
        }
    }
}
