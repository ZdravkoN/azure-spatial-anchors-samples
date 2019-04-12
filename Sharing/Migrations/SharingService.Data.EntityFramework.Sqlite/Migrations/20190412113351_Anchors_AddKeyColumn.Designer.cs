﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharingService.Data.EntityFramework;

namespace SharingService.Data.EntityFramework.Sqlite.Migrations
{
    [DbContext(typeof(SharingServiceContext))]
    [Migration("20190412113351_Anchors_AddKeyColumn")]
    partial class Anchors_AddKeyColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("SharingService.Data.Model.Anchor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnName("key");

                    b.Property<double?>("Latitude")
                        .HasColumnName("latitude");

                    b.Property<double?>("Longitude")
                        .HasColumnName("longitude");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("anchors");
                });
#pragma warning restore 612, 618
        }
    }
}
