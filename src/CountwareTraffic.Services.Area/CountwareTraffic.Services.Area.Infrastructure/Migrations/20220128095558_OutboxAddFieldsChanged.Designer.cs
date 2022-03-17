﻿// <auto-generated />
using System;
using CountwareTraffic.Services.Areas.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

namespace CountwareTraffic.Services.Areas.Infrastructure.Migrations
{
    [DbContext(typeof(AreaDbContext))]
    [Migration("20220128095558_OutboxAddFieldsChanged")]
    partial class OutboxAddFieldsChanged
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CountwareTraffic.Services.Areas.Core.Area", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuditCreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AuditCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("AuditIsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("AuditModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AuditModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(2500)
                        .HasColumnType("nvarchar(2500)");

                    b.Property<Guid>("DistrictId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("_areaTypeId")
                        .HasColumnType("int")
                        .HasColumnName("AreaTypeId");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.HasIndex("_areaTypeId");

                    b.ToTable("Areas", "areas");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Areas.Core.AreaType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("AreaTypes", "areas");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Areas.Core.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuditCreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AuditCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("AuditIsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("AuditModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AuditModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities", "areas");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Areas.Core.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuditCreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AuditCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("AuditIsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("AuditModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AuditModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(2500)
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Companies", "areas");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Areas.Core.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuditCreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AuditCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("AuditIsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("AuditModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AuditModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Capital")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContinentCode")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("CurrencyCode")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Iso")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("Iso3")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<int>("IsoNumeric")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Countries", "areas");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Areas.Core.District", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuditCreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AuditCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("AuditIsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("AuditModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AuditModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Districts", "areas");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Areas.Core.SubArea", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AreaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuditCreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AuditCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("AuditIsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("AuditModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AuditModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(2500)
                        .HasColumnType("nvarchar(2500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("_subAreaStatus")
                        .HasColumnType("int")
                        .HasColumnName("SubAreaStatus");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("_subAreaStatus");

                    b.ToTable("SubAreas", "areas");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Areas.Core.SubAreaStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("SubAreaStatuses", "areas");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Areas.Infrastructure.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EventRecordId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsTryFromQueue")
                        .HasColumnType("bit");

                    b.Property<string>("LastException")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccurredOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages", "area.app");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Areas.Core.Area", b =>
                {
                    b.HasOne("CountwareTraffic.Services.Areas.Core.District", null)
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CountwareTraffic.Services.Areas.Core.AreaType", "AreaType")
                        .WithMany()
                        .HasForeignKey("_areaTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CountwareTraffic.Services.Areas.Core.AreaAddress", "Address", b1 =>
                        {
                            b1.Property<Guid>("AreaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Point>("Location")
                                .HasColumnType("geography");

                            b1.Property<string>("Street")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.HasKey("AreaId");

                            b1.ToTable("Areas");

                            b1.WithOwner()
                                .HasForeignKey("AreaId");
                        });

                    b.OwnsOne("CountwareTraffic.Services.Areas.Core.AreaContact", "Contact", b1 =>
                        {
                            b1.Property<Guid>("AreaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("EmailAddress")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("FaxNumber")
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)");

                            b1.Property<string>("GsmNumber")
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.Property<string>("PhoneNumber")
                                .HasMaxLength(16)
                                .HasColumnType("nvarchar(16)");

                            b1.HasKey("AreaId");

                            b1.ToTable("Areas");

                            b1.WithOwner()
                                .HasForeignKey("AreaId");
                        });

                    b.Navigation("Address");

                    b.Navigation("AreaType");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Areas.Core.City", b =>
                {
                    b.HasOne("CountwareTraffic.Services.Areas.Core.Country", null)
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CountwareTraffic.Services.Areas.Core.Company", b =>
                {
                    b.OwnsOne("CountwareTraffic.Services.Areas.Core.CompanyAddress", "Address", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Country")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<Point>("Location")
                                .HasColumnType("geography");

                            b1.Property<string>("State")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Street")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("ZipCode")
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Companies");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.OwnsOne("CountwareTraffic.Services.Areas.Core.CompanyContact", "Contact", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("EmailAddress")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("FaxNumber")
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)");

                            b1.Property<string>("GsmNumber")
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.Property<string>("PhoneNumber")
                                .HasMaxLength(16)
                                .HasColumnType("nvarchar(16)");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Companies");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.Navigation("Address");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Areas.Core.Country", b =>
                {
                    b.HasOne("CountwareTraffic.Services.Areas.Core.Company", null)
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CountwareTraffic.Services.Areas.Core.District", b =>
                {
                    b.HasOne("CountwareTraffic.Services.Areas.Core.City", null)
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CountwareTraffic.Services.Areas.Core.SubArea", b =>
                {
                    b.HasOne("CountwareTraffic.Services.Areas.Core.Area", null)
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CountwareTraffic.Services.Areas.Core.SubAreaStatus", "SubAreaStatus")
                        .WithMany()
                        .HasForeignKey("_subAreaStatus")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubAreaStatus");
                });
#pragma warning restore 612, 618
        }
    }
}