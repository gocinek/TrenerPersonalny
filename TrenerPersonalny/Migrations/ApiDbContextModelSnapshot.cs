﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrenerPersonalny.Data;

namespace TrenerPersonalny.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.14");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.ExcerciseType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ExcerciseType");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Excercises", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("ExcerciseTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("PublicId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ExcerciseTypeId");

                    b.ToTable("Excercises");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Orders.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BuyerId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Expired")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PaymentIntentId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Summary")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TrainerOrderedName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Orders.OrderPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BuyerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClientSecret")
                        .HasColumnType("TEXT");

                    b.Property<string>("PaymentIntentId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("OrderPayments");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Orders.OrderTrainer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderTrainer");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProfileImg")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("PublicId")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TrainerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TrainerId")
                        .IsUnique();

                    b.ToTable("Person");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.PlanDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExcerciseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ManyInWeek")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PlansId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Repeats")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ExcerciseId");

                    b.HasIndex("PlansId");

                    b.ToTable("PlanDetails");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Plans", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrainerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("TrainerId");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "0473130d-4778-4445-8413-e93ae33770c8",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            ConcurrencyStamp = "00f81984-a754-46aa-8e2e-3ff21f572035",
                            Name = "Trainer",
                            NormalizedName = "TRAINER"
                        },
                        new
                        {
                            Id = 3,
                            ConcurrencyStamp = "4e05e295-3766-448e-8d22-99a27a2b5582",
                            Name = "Client",
                            NormalizedName = "CLIENT"
                        });
                });

            modelBuilder.Entity("TrenerPersonalny.Models.SizeDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExcerciseTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SizeCm")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SizesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ExcerciseTypeId");

                    b.HasIndex("SizesId");

                    b.ToTable("SizeDetails");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Sizes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Trainers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(510)
                        .HasColumnType("TEXT");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Trainers");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Registered")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("TrenerPersonalny.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("TrenerPersonalny.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("TrenerPersonalny.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("TrenerPersonalny.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrenerPersonalny.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("TrenerPersonalny.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Excercises", b =>
                {
                    b.HasOne("TrenerPersonalny.Models.ExcerciseType", "ExcerciseType")
                        .WithMany()
                        .HasForeignKey("ExcerciseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExcerciseType");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Orders.OrderTrainer", b =>
                {
                    b.HasOne("TrenerPersonalny.Models.Orders.Order", null)
                        .WithMany("OrderTrainer")
                        .HasForeignKey("OrderId");

                    b.OwnsOne("TrenerPersonalny.Models.Orders.TrainerPersonOrdered", "TrainerOrdered", b1 =>
                        {
                            b1.Property<int>("OrderTrainerId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Name")
                                .HasColumnType("TEXT");

                            b1.Property<string>("PictureUrl")
                                .HasColumnType("TEXT");

                            b1.Property<int>("TrainerId")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OrderTrainerId");

                            b1.ToTable("OrderTrainer");

                            b1.WithOwner()
                                .HasForeignKey("OrderTrainerId");
                        });

                    b.Navigation("TrainerOrdered");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Person", b =>
                {
                    b.HasOne("TrenerPersonalny.Models.Trainers", "Trainers")
                        .WithOne("Person")
                        .HasForeignKey("TrenerPersonalny.Models.Person", "TrainerId");

                    b.Navigation("Trainers");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.PlanDetails", b =>
                {
                    b.HasOne("TrenerPersonalny.Models.Excercises", "Excercise")
                        .WithMany()
                        .HasForeignKey("ExcerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrenerPersonalny.Models.Plans", null)
                        .WithMany("PlanDetails")
                        .HasForeignKey("PlansId");

                    b.Navigation("Excercise");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Plans", b =>
                {
                    b.HasOne("TrenerPersonalny.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrenerPersonalny.Models.Trainers", "Trainers")
                        .WithMany()
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Trainers");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.SizeDetails", b =>
                {
                    b.HasOne("TrenerPersonalny.Models.ExcerciseType", "ExcerciseType")
                        .WithMany()
                        .HasForeignKey("ExcerciseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrenerPersonalny.Models.Sizes", "Sizes")
                        .WithMany("SizeDetails")
                        .HasForeignKey("SizesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExcerciseType");

                    b.Navigation("Sizes");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Sizes", b =>
                {
                    b.HasOne("TrenerPersonalny.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.User", b =>
                {
                    b.HasOne("TrenerPersonalny.Models.Person", "Person")
                        .WithOne("Client")
                        .HasForeignKey("TrenerPersonalny.Models.User", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Orders.Order", b =>
                {
                    b.Navigation("OrderTrainer");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Person", b =>
                {
                    b.Navigation("Client");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Plans", b =>
                {
                    b.Navigation("PlanDetails");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Sizes", b =>
                {
                    b.Navigation("SizeDetails");
                });

            modelBuilder.Entity("TrenerPersonalny.Models.Trainers", b =>
                {
                    b.Navigation("Person");
                });
#pragma warning restore 612, 618
        }
    }
}
