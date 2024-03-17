﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bouvet_fagkaffe_repository.Context;

#nullable disable

namespace bouvet_fagkaffe_repository.Migrations
{
    [DbContext(typeof(FagkaffeContext))]
    partial class FagkaffeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CandidateUser", b =>
                {
                    b.Property<Guid>("RegisteredOnCandidatesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RegisteredPresentersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RegisteredOnCandidatesId", "RegisteredPresentersId");

                    b.HasIndex("RegisteredPresentersId");

                    b.ToTable("CandidateUser");
                });

            modelBuilder.Entity("CandidateUser1", b =>
                {
                    b.Property<Guid>("VotedOnById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VotedOnId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("VotedOnById", "VotedOnId");

                    b.HasIndex("VotedOnId");

                    b.ToTable("CandidateUser1");
                });

            modelBuilder.Entity("LectureUser", b =>
                {
                    b.Property<Guid>("HeldById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PresentsLecturesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("HeldById", "PresentsLecturesId");

                    b.HasIndex("PresentsLecturesId");

                    b.ToTable("LectureUser");
                });

            modelBuilder.Entity("bouvet_fagkaffe_repository.Models.Candidate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProposedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProposedById");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("bouvet_fagkaffe_repository.Models.Lecture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ApprovedByAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("ApprovedByPresenter")
                        .HasColumnType("bit");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("HeldAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("bouvet_fagkaffe_repository.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ForeignId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Groups")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CandidateUser", b =>
                {
                    b.HasOne("bouvet_fagkaffe_repository.Models.Candidate", null)
                        .WithMany()
                        .HasForeignKey("RegisteredOnCandidatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bouvet_fagkaffe_repository.Models.User", null)
                        .WithMany()
                        .HasForeignKey("RegisteredPresentersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CandidateUser1", b =>
                {
                    b.HasOne("bouvet_fagkaffe_repository.Models.User", null)
                        .WithMany()
                        .HasForeignKey("VotedOnById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bouvet_fagkaffe_repository.Models.Candidate", null)
                        .WithMany()
                        .HasForeignKey("VotedOnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LectureUser", b =>
                {
                    b.HasOne("bouvet_fagkaffe_repository.Models.User", null)
                        .WithMany()
                        .HasForeignKey("HeldById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("bouvet_fagkaffe_repository.Models.Lecture", null)
                        .WithMany()
                        .HasForeignKey("PresentsLecturesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("bouvet_fagkaffe_repository.Models.Candidate", b =>
                {
                    b.HasOne("bouvet_fagkaffe_repository.Models.User", "ProposedBy")
                        .WithMany()
                        .HasForeignKey("ProposedById");

                    b.Navigation("ProposedBy");
                });

            modelBuilder.Entity("bouvet_fagkaffe_repository.Models.Lecture", b =>
                {
                    b.OwnsMany("bouvet_fagkaffe_repository.Models.MeetingLink", "MeetingLinks", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<Guid?>("LectureId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Link")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Id");

                            b1.HasIndex("LectureId");

                            b1.ToTable("MeetingLink");

                            b1.WithOwner()
                                .HasForeignKey("LectureId");
                        });

                    b.Navigation("MeetingLinks");
                });
#pragma warning restore 612, 618
        }
    }
}
