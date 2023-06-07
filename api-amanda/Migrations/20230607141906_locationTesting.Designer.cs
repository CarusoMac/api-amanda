﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using api_amanda;

#nullable disable

namespace api_amanda.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230607141906_locationTesting")]
    partial class locationTesting
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("api_amanda.DTOs.CsvBtsDTO", b =>
                {
                    b.Property<string>("cellid")
                        .HasColumnType("text");

                    b.Property<Point>("Location")
                        .HasColumnType("geometry");

                    b.Property<double>("btsLat")
                        .HasColumnType("double precision");

                    b.Property<double>("btsLon")
                        .HasColumnType("double precision");

                    b.HasKey("cellid");

                    b.ToTable("BtsCoordinates");
                });

            modelBuilder.Entity("api_amanda.DTOs.CsvFileDTO", b =>
                {
                    b.Property<string>("csvFileId")
                        .HasColumnType("text");

                    b.Property<string>("csvFileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("fileTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("firstTimeStamp")
                        .HasColumnType("bigint");

                    b.Property<long>("lastTimeStamp")
                        .HasColumnType("bigint");

                    b.Property<string>("uploadDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("csvFileId");

                    b.ToTable("CsvFiles");
                });

            modelBuilder.Entity("api_amanda.DTOs.CsvRecordDTO", b =>
                {
                    b.Property<string>("recordId")
                        .HasColumnType("text");

                    b.Property<Point>("RecLocation")
                        .IsRequired()
                        .HasColumnType("geometry");

                    b.Property<string>("act")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("bid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("cellid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("csvFileId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("direction")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("lac")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("lat")
                        .HasColumnType("double precision");

                    b.Property<double>("lon")
                        .HasColumnType("double precision");

                    b.Property<string>("mcc")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("measured_at")
                        .HasColumnType("bigint");

                    b.Property<string>("mnc")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("nid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("pci")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("psc")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("rating")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("sid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("signal")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("speed")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ta")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("tac")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("recordId");

                    b.ToTable("Records");
                });
#pragma warning restore 612, 618
        }
    }
}