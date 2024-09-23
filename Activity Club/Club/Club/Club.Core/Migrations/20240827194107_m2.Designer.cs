﻿// <auto-generated />
using System;
using Club.Core.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Club.Core.Migrations
{
    [DbContext(typeof(MyDataContext))]
    [Migration("20240827194107_m2")]
    partial class m2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AdminManageUser", b =>
                {
                    b.Property<string>("AdminEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AdminEmail", "UserEmail");

                    b.HasIndex("UserEmail");

                    b.ToTable("AdminManageUser");
                });

            modelBuilder.Entity("Club.Core.DataModels.Event", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("cost")
                        .HasColumnType("int");

                    b.Property<int?>("lookupOrder")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("lookupOrder");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Club.Core.DataModels.Lookup", b =>
                {
                    b.Property<int>("Order")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Order"));

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Order");

                    b.ToTable("Lookups");
                });

            modelBuilder.Entity("Club.Core.DataModels.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("GuideEvent", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("GuideEmail")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EventId", "GuideEmail");

                    b.HasIndex("GuideEmail");

                    b.ToTable("GuideEvent");
                });

            modelBuilder.Entity("MemberEvent", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("MemberId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EventId", "MemberId");

                    b.HasIndex("MemberId");

                    b.ToTable("MemberEvent");
                });

            modelBuilder.Entity("UserJoinEvent", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EventId", "UserEmail");

                    b.HasIndex("UserEmail");

                    b.ToTable("UserJoinEvent");
                });

            modelBuilder.Entity("UserManageLookup", b =>
                {
                    b.Property<int>("LookupOrder")
                        .HasColumnType("int");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LookupOrder", "UserEmail");

                    b.HasIndex("UserEmail");

                    b.ToTable("UserManageLookup");
                });

            modelBuilder.Entity("Club.Core.DataModels.Guide", b =>
                {
                    b.HasBaseType("Club.Core.DataModels.User");

                    b.Property<byte[]>("Photo")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Profession")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Users", t =>
                        {
                            t.Property("Photo")
                                .HasColumnName("Guide_Photo");

                            t.Property("Profession")
                                .HasColumnName("Guide_Profession");
                        });

                    b.HasDiscriminator().HasValue("Guide");
                });

            modelBuilder.Entity("Club.Core.DataModels.Member", b =>
                {
                    b.HasBaseType("Club.Core.DataModels.User");

                    b.Property<int>("EmergencyNumber")
                        .HasColumnType("int");

                    b.Property<int>("MbileNumber")
                        .HasColumnType("int");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Photo")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Profession")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Member");
                });

            modelBuilder.Entity("AdminManageUser", b =>
                {
                    b.HasOne("Club.Core.DataModels.User", null)
                        .WithMany()
                        .HasForeignKey("AdminEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Club.Core.DataModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Club.Core.DataModels.Event", b =>
                {
                    b.HasOne("Club.Core.DataModels.Lookup", "lookup")
                        .WithMany("Events")
                        .HasForeignKey("lookupOrder");

                    b.Navigation("lookup");
                });

            modelBuilder.Entity("GuideEvent", b =>
                {
                    b.HasOne("Club.Core.DataModels.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Club.Core.DataModels.Guide", null)
                        .WithMany()
                        .HasForeignKey("GuideEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MemberEvent", b =>
                {
                    b.HasOne("Club.Core.DataModels.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Club.Core.DataModels.Member", null)
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserJoinEvent", b =>
                {
                    b.HasOne("Club.Core.DataModels.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Club.Core.DataModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserManageLookup", b =>
                {
                    b.HasOne("Club.Core.DataModels.Lookup", null)
                        .WithMany()
                        .HasForeignKey("LookupOrder")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Club.Core.DataModels.User", null)
                        .WithMany()
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Club.Core.DataModels.Lookup", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
