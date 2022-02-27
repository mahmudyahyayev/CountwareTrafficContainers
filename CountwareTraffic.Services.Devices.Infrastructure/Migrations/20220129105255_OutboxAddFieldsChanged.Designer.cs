﻿// <auto-generated />
using System;
using CountwareTraffic.Services.Devices.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CountwareTraffic.Services.Devices.Infrastructure.Migrations
{
    [DbContext(typeof(DeviceDbContext))]
    [Migration("20220129105255_OutboxAddFieldsChanged")]
    partial class OutboxAddFieldsChanged
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CountwareTraffic.Services.Devices.Core.Device", b =>
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

                    b.Property<string>("Model")
                        .HasMaxLength(130)
                        .HasColumnType("nvarchar(130)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<Guid>("SubAreaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("_deviceCreationStatus")
                        .HasColumnType("int")
                        .HasColumnName("DeviceCreationStatus");

                    b.Property<int>("_deviceStatusId")
                        .HasColumnType("int")
                        .HasColumnName("DeviceStatusId");

                    b.Property<int>("_deviceTypeId")
                        .HasColumnType("int")
                        .HasColumnName("DeviceTypeId");

                    b.HasKey("Id");

                    b.HasIndex("SubAreaId");

                    b.HasIndex("_deviceCreationStatus");

                    b.HasIndex("_deviceStatusId");

                    b.HasIndex("_deviceTypeId");

                    b.ToTable("Devices", "devices");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Devices.Core.DeviceCreationStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("DeviceCreationStatuses", "devices");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Devices.Core.DeviceStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("DeviceStatuses", "devices");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Devices.Core.DeviceType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("DeviceTypes", "devices");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Devices.Core.SubArea", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("SubAreas", "devices");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Devices.Infrastructure.OutboxMessage", b =>
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

                    b.ToTable("OutboxMessages", "device.app");
                });

            modelBuilder.Entity("CountwareTraffic.Services.Devices.Core.Device", b =>
                {
                    b.HasOne("CountwareTraffic.Services.Devices.Core.SubArea", null)
                        .WithMany()
                        .HasForeignKey("SubAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CountwareTraffic.Services.Devices.Core.DeviceCreationStatus", "DeviceCreationStatus")
                        .WithMany()
                        .HasForeignKey("_deviceCreationStatus")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CountwareTraffic.Services.Devices.Core.DeviceStatus", "DeviceStatus")
                        .WithMany()
                        .HasForeignKey("_deviceStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CountwareTraffic.Services.Devices.Core.DeviceType", "DeviceType")
                        .WithMany()
                        .HasForeignKey("_deviceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CountwareTraffic.Services.Devices.Core.DeviceConnectionInfo", "ConnectionInfo", b1 =>
                        {
                            b1.Property<Guid>("DeviceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Identity")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("IpAddress")
                                .HasMaxLength(15)
                                .HasColumnType("nvarchar(15)");

                            b1.Property<string>("MacAddress")
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.Property<string>("Password")
                                .HasMaxLength(16)
                                .HasColumnType("nvarchar(16)");

                            b1.Property<int>("Port")
                                .HasMaxLength(5)
                                .HasColumnType("int");

                            b1.Property<string>("UniqueId")
                                .HasMaxLength(16)
                                .HasColumnType("nvarchar(16)");

                            b1.HasKey("DeviceId");

                            b1.ToTable("Devices");

                            b1.WithOwner()
                                .HasForeignKey("DeviceId");
                        });

                    b.Navigation("ConnectionInfo");

                    b.Navigation("DeviceCreationStatus");

                    b.Navigation("DeviceStatus");

                    b.Navigation("DeviceType");
                });
#pragma warning restore 612, 618
        }
    }
}
