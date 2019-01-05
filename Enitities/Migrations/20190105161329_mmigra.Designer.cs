﻿// <auto-generated />
using System;
using Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Enitities.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190105161329_mmigra")]
    partial class mmigra
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Enitities.Model.Gift", b =>
                {
                    b.Property<long>("GiftNumber")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("BoyGift");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<bool>("GirlGift");

                    b.Property<string>("Title");

                    b.HasKey("GiftNumber");

                    b.ToTable("gifts");
                });
#pragma warning restore 612, 618
        }
    }
}