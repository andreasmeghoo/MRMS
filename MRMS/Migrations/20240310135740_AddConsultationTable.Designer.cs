﻿// <auto-generated />
using System;
using MRMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MRMS.Migrations
{
    [DbContext(typeof(MRMSContext))]
    [Migration("20240310135740_MigrationTwo")]
    partial class AddConsultationTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MRMS.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("PreferredDoctorId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecialAccomodations")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("AppointmentId");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("MRMS.Models.Consultation", b =>
                {
                    b.Property<int>("ConsultationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConsultationId"));

                    b.Property<int>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<string>("CarePlan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diagnosis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("ExamDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NurseId")
                        .HasColumnType("int");

                    b.Property<string>("PatientHistory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConsultationId");

                    b.ToTable("Consultation");
                });
#pragma warning restore 612, 618
        }
    }
}
