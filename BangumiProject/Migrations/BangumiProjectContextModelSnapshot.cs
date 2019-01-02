﻿// <auto-generated />
using System;
using BangumiProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BangumiProject.Migrations
{
    [DbContext(typeof(BangumiProjectContext))]
    partial class BangumiProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity("BangumiProject.Models.Anime", b =>
                {
                    b.Property<int>("AnimeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnimeInfo");

                    b.Property<string>("AnimeName")
                        .IsRequired();

                    b.Property<int>("AnimeNum");

                    b.Property<string>("AnimePic");

                    b.Property<DateTime>("AnimePlayTime");

                    b.Property<string>("AnimeType")
                        .IsRequired();

                    b.Property<bool>("IsEnd");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.HasKey("AnimeID");

                    b.HasIndex("AnimeID", "AnimeName", "AnimeType");

                    b.ToTable("Anime");
                });

            modelBuilder.Entity("BangumiProject.Models.AnimeComm", b =>
                {
                    b.Property<int>("CommID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnimeID");

                    b.Property<string>("CommStr")
                        .IsRequired();

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("UsersId")
                        .IsRequired();

                    b.HasKey("CommID");

                    b.HasIndex("AnimeID");

                    b.HasIndex("UsersId");

                    b.ToTable("AnimeComms");
                });

            modelBuilder.Entity("BangumiProject.Models.AnimeSouce", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnimeID");

                    b.Property<string>("Info");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Pic");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("URL")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("AnimeID");

                    b.HasIndex("ID", "Name");

                    b.ToTable("AnimeSouces");
                });

            modelBuilder.Entity("BangumiProject.Models.AnimeSouceComm", b =>
                {
                    b.Property<int>("CommID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnimeSouceID");

                    b.Property<string>("CommStr")
                        .IsRequired();

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("UsersId");

                    b.HasKey("CommID");

                    b.HasIndex("AnimeSouceID");

                    b.HasIndex("UsersId");

                    b.ToTable("AnimeSouceComms");
                });

            modelBuilder.Entity("BangumiProject.Models.AnimeTag", b =>
                {
                    b.Property<int>("TagID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnimeID");

                    b.Property<string>("TagName");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.HasKey("TagID");

                    b.HasIndex("AnimeID");

                    b.HasIndex("TagID", "TagName");

                    b.ToTable("AnimeTag");
                });

            modelBuilder.Entity("BangumiProject.Models.Blog", b =>
                {
                    b.Property<int>("BlogID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnimeID");

                    b.Property<string>("AnimeInfo");

                    b.Property<string>("AnimeName");

                    b.Property<string>("AnimePic");

                    b.Property<string>("BlogStr")
                        .IsRequired();

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("UpLoadUserId");

                    b.HasKey("BlogID");

                    b.HasIndex("UpLoadUserId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("BangumiProject.Models.BlogTags", b =>
                {
                    b.Property<int>("BolgTagID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BlogTagString");

                    b.Property<int?>("BlogsBlogID");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.HasKey("BolgTagID");

                    b.HasIndex("BlogsBlogID");

                    b.ToTable("BlogTags");
                });

            modelBuilder.Entity("BangumiProject.Models.Comment", b =>
                {
                    b.Property<int>("CommID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BlogsBlogID");

                    b.Property<string>("CommStr");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("UsersId");

                    b.HasKey("CommID");

                    b.HasIndex("BlogsBlogID");

                    b.HasIndex("UsersId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BangumiProject.Models.Images", b =>
                {
                    b.Property<string>("ImageID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType");

                    b.Property<string>("ImageName")
                        .IsRequired();

                    b.Property<string>("ImagePath")
                        .IsRequired();

                    b.Property<int?>("PhotosID");

                    b.Property<string>("ReadUsers");

                    b.Property<string>("StaticPath");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("UpLoadUsersId");

                    b.HasKey("ImageID");

                    b.HasIndex("PhotosID");

                    b.HasIndex("UpLoadUsersId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("BangumiProject.Models.Memo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MemoStr");

                    b.Property<int>("NowAnimeNum");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<int?>("UserAnimeInfoID");

                    b.HasKey("ID");

                    b.HasIndex("UserAnimeInfoID");

                    b.ToTable("Memos");
                });

            modelBuilder.Entity("BangumiProject.Models.Photos", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Info");

                    b.Property<string>("Name");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.HasKey("ID");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("BangumiProject.Models.UserAnimeInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NowAnimeNum");

                    b.Property<int?>("SubAnimeAnimeID");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("UsersId");

                    b.HasKey("ID");

                    b.HasIndex("SubAnimeAnimeID");

                    b.HasIndex("UsersId");

                    b.ToTable("UserAnimeInfos");
                });

            modelBuilder.Entity("BangumiProject.Models.UserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.Property<int>("KenGenChi");

                    b.Property<DateTime>("Time");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("BangumiProject.Models.Users", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("URL");

                    b.Property<string>("UserBackPic");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("UserPic");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "36987946-a120-4e1d-9404-5e5fb201d20e",
                            ConcurrencyStamp = "c77e1c0f-a7be-486b-9848-e901c82d6c96",
                            Name = "Admin,",
                            NormalizedName = "ADMIN,"
                        },
                        new
                        {
                            Id = "6e068d89-5a94-424a-bef6-3a6493191daf",
                            ConcurrencyStamp = "4c7729d4-9414-4921-97ea-8776e178343c",
                            Name = "Girl,",
                            NormalizedName = "GIRL,"
                        },
                        new
                        {
                            Id = "fcfbbb4b-bb77-479e-b4b8-32e7ee598653",
                            ConcurrencyStamp = "1be843ba-347c-42a5-bd1f-951c851f5ab9",
                            Name = "Yuri5,",
                            NormalizedName = "YURI5,"
                        },
                        new
                        {
                            Id = "8d2f0252-f11d-49a9-9044-7bf1f5799654",
                            ConcurrencyStamp = "2403b8dd-f653-4b70-b45c-eb2af7b25d15",
                            Name = "Yuri4,",
                            NormalizedName = "YURI4,"
                        },
                        new
                        {
                            Id = "bae2461f-bfa1-4d26-b41d-5a0bdf732a29",
                            ConcurrencyStamp = "21406832-f12e-46bb-8755-3290e4f04112",
                            Name = "Yuri3,",
                            NormalizedName = "YURI3,"
                        },
                        new
                        {
                            Id = "f0362f00-883c-45ad-8ec0-c703a11bfe44",
                            ConcurrencyStamp = "dff2eec3-5205-437e-9ba8-67a169e2aef7",
                            Name = "Yuri2,",
                            NormalizedName = "YURI2,"
                        },
                        new
                        {
                            Id = "b2f11ac6-6fcf-4b09-93c2-0ee4e0d9d784",
                            ConcurrencyStamp = "a032f1e8-dbc8-447a-81dc-a86c2cbf1d69",
                            Name = "Yuri1,",
                            NormalizedName = "YURI1,"
                        },
                        new
                        {
                            Id = "b34f4a98-355b-4e03-bde7-b139e8a3703b",
                            ConcurrencyStamp = "a1160c79-20b9-4dbc-9208-5fb6c89a9b33",
                            Name = "Boy,",
                            NormalizedName = "BOY,"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BangumiProject.Models.AnimeComm", b =>
                {
                    b.HasOne("BangumiProject.Models.Anime", "Anime")
                        .WithMany("AnimeComms")
                        .HasForeignKey("AnimeID")
                        .HasConstraintName("AnimeComm_Anime_PK");

                    b.HasOne("BangumiProject.Models.Users", "Users")
                        .WithMany("AnimeComms")
                        .HasForeignKey("UsersId")
                        .HasConstraintName("AnimeComm_User_PK")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BangumiProject.Models.AnimeSouce", b =>
                {
                    b.HasOne("BangumiProject.Models.Anime", "Anime")
                        .WithMany("Souce")
                        .HasForeignKey("AnimeID")
                        .HasConstraintName("AnimeSouce_Anime_PK");
                });

            modelBuilder.Entity("BangumiProject.Models.AnimeSouceComm", b =>
                {
                    b.HasOne("BangumiProject.Models.AnimeSouce", "AnimeSouce")
                        .WithMany("AnimeSouceComms")
                        .HasForeignKey("AnimeSouceID")
                        .HasConstraintName("AnimeSouceComm_AnimeSouce_PK");

                    b.HasOne("BangumiProject.Models.Users", "Users")
                        .WithMany("AnimeSouceComms")
                        .HasForeignKey("UsersId")
                        .HasConstraintName("AnimeSouceComm_User_PK");
                });

            modelBuilder.Entity("BangumiProject.Models.AnimeTag", b =>
                {
                    b.HasOne("BangumiProject.Models.Anime", "Anime")
                        .WithMany("Tags")
                        .HasForeignKey("AnimeID")
                        .HasConstraintName("AnimeTag_Anime_PK");
                });

            modelBuilder.Entity("BangumiProject.Models.Blog", b =>
                {
                    b.HasOne("BangumiProject.Models.Users", "UpLoadUser")
                        .WithMany("Blogs")
                        .HasForeignKey("UpLoadUserId")
                        .HasConstraintName("Blog_User_PK");
                });

            modelBuilder.Entity("BangumiProject.Models.BlogTags", b =>
                {
                    b.HasOne("BangumiProject.Models.Blog", "Blogs")
                        .WithMany("TagIDs")
                        .HasForeignKey("BlogsBlogID")
                        .HasConstraintName("BlogTags_Blog_PK");
                });

            modelBuilder.Entity("BangumiProject.Models.Comment", b =>
                {
                    b.HasOne("BangumiProject.Models.Blog", "Blogs")
                        .WithMany("Comments")
                        .HasForeignKey("BlogsBlogID")
                        .HasConstraintName("Comment_Blogs_PK");

                    b.HasOne("BangumiProject.Models.Users", "Users")
                        .WithMany("Comments")
                        .HasForeignKey("UsersId")
                        .HasConstraintName("Comment_User_PK");
                });

            modelBuilder.Entity("BangumiProject.Models.Images", b =>
                {
                    b.HasOne("BangumiProject.Models.Photos", "Photos")
                        .WithMany("Images")
                        .HasForeignKey("PhotosID")
                        .HasConstraintName("Image_Photo_PK");

                    b.HasOne("BangumiProject.Models.Users", "UpLoadUsers")
                        .WithMany("Images")
                        .HasForeignKey("UpLoadUsersId")
                        .HasConstraintName("Images_User_PK");
                });

            modelBuilder.Entity("BangumiProject.Models.Memo", b =>
                {
                    b.HasOne("BangumiProject.Models.UserAnimeInfo", "UserAnimeInfo")
                        .WithMany("Memos")
                        .HasForeignKey("UserAnimeInfoID")
                        .HasConstraintName("Memo_UserAnimeInfo_PK");
                });

            modelBuilder.Entity("BangumiProject.Models.UserAnimeInfo", b =>
                {
                    b.HasOne("BangumiProject.Models.Anime", "SubAnime")
                        .WithMany("UserAnimeInfos")
                        .HasForeignKey("SubAnimeAnimeID")
                        .HasConstraintName("UserAnimeInfo_Anime_PK");

                    b.HasOne("BangumiProject.Models.Users", "Users")
                        .WithMany("UserAnimeInfos")
                        .HasForeignKey("UsersId")
                        .HasConstraintName("UserAnimeInfo_User_PK");
                });

            modelBuilder.Entity("BangumiProject.Models.UserRole", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BangumiProject.Models.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BangumiProject.Models.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BangumiProject.Models.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BangumiProject.Models.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
