﻿// <auto-generated />
using ExtractRunningData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExtractRunningData.Migrations
{
    [DbContext(typeof(EventDataContext))]
    [Migration("20241007101006_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ExtractRunningData.Models.EventData", b =>
                {
                    b.Property<int>("EventDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("EventDataId"));

                    b.Property<double>("Distance")
                        .HasColumnType("double");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsRoad")
                        .HasColumnType("tinyint(1)");

                    b.Property<double>("OC")
                        .HasColumnType("double");

                    b.HasKey("EventDataId");

                    b.ToTable("EventData");
                });

            modelBuilder.Entity("ExtractRunningData.Models.EventFactors", b =>
                {
                    b.Property<int>("EventFactorsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("EventFactorsId"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("EventDataId")
                        .HasColumnType("int");

                    b.Property<double>("Factor")
                        .HasColumnType("double");

                    b.HasKey("EventFactorsId");

                    b.HasIndex("EventDataId");

                    b.ToTable("EventFactors");
                });

            modelBuilder.Entity("ExtractRunningData.Models.EventFactors", b =>
                {
                    b.HasOne("ExtractRunningData.Models.EventData", "EventData")
                        .WithMany("EventFactors")
                        .HasForeignKey("EventDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventData");
                });

            modelBuilder.Entity("ExtractRunningData.Models.EventData", b =>
                {
                    b.Navigation("EventFactors");
                });
#pragma warning restore 612, 618
        }
    }
}
