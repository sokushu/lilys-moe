﻿// <auto-generated />
using System;
using BangumiProjectDBServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BangumiProject.Migrations
{
    [DbContext(typeof(CoreContext))]
    [Migration("20190320024026_Init00")]
    partial class Init00
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity("BangumiProjectDBServices.Models.Anime", b =>
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

            modelBuilder.Entity("BangumiProjectDBServices.Models.AnimeComm", b =>
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

            modelBuilder.Entity("BangumiProjectDBServices.Models.AnimeMemo", b =>
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

            modelBuilder.Entity("BangumiProjectDBServices.Models.AnimeMoreInfo", b =>
                {
                    b.Property<int>("INFOID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnimeID");

                    b.Property<int?>("EDMIID");

                    b.Property<int?>("OPMIID");

                    b.HasKey("INFOID");

                    b.HasIndex("AnimeID");

                    b.HasIndex("EDMIID");

                    b.HasIndex("OPMIID");

                    b.ToTable("AnimeMoreInfos");
                });

            modelBuilder.Entity("BangumiProjectDBServices.Models.AnimeNumInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnimeID");

                    b.Property<int>("AnimeNum");

                    b.Property<string>("AnimeNumbInfo");

                    b.Property<bool>("IsStop");

                    b.Property<bool>("IsStopLong");

                    b.Property<DateTime>("PlayTime");

                    b.Property<string>("PlayURL");

                    b.Property<int>("StopCause");

                    b.Property<string>("StopLongStartPlay");

                    b.Property<bool>("StopLongStartPlayDVD");

                    b.Property<int>("StopNum");

                    b.Property<DateTime>("StopTime");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("AnimeNums");
                });

            modelBuilder.Entity("BangumiProjectDBServices.Models.AnimeSouce", b =>
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

            modelBuilder.Entity("BangumiProjectDBServices.Models.AnimeSouceComm", b =>
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

            modelBuilder.Entity("BangumiProjectDBServices.Models.AnimeTag", b =>
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

            modelBuilder.Entity("BangumiProjectDBServices.Models.AnimeUserInfo", b =>
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

            modelBuilder.Entity("BangumiProjectDBServices.Models.FileImages", b =>
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

            modelBuilder.Entity("BangumiProjectDBServices.Models.FilePhoto", b =>
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

            modelBuilder.Entity("BangumiProjectDBServices.Models.Music", b =>
                {
                    b.Property<int>("MID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("Info");

                    b.Property<int?>("MusicAlbumMIID");

                    b.Property<string>("MusicName");

                    b.Property<int>("Time");

                    b.HasKey("MID");

                    b.HasIndex("MusicAlbumMIID");

                    b.ToTable("Musics");
                });

            modelBuilder.Entity("BangumiProjectDBServices.Models.MusicAlbum", b =>
                {
                    b.Property<int>("MIID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnimeMoreInfoINFOID");

                    b.Property<DateTime>("DateTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("datetime('now')");

                    b.Property<string>("Name");

                    b.Property<string>("Pic");

                    b.Property<DateTime>("Time");

                    b.HasKey("MIID");

                    b.HasIndex("AnimeMoreInfoINFOID");

                    b.ToTable("MusicAlbum");
                });

            modelBuilder.Entity("BangumiProjectDBServices.Models.User", b =>
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

            modelBuilder.Entity("BangumiProjectDBServices.Models.UserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.Property<int>("KenGenChi");

                    b.Property<DateTime>("Time");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
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
                            Id = "89daa059-0441-4b26-8f3a-fa29eebce314",
                            ConcurrencyStamp = "3deafab4-97ef-4e98-b7a8-e1723632ed4a",
                            Name = "Admin,",
                            NormalizedName = "ADMIN,"
                        },
                        new
                        {
                            Id = "d8af03b9-e890-4127-a1ae-c3dfdd5bf69c",
                            ConcurrencyStamp = "5c7e4b81-cde8-4397-99fe-3648b7cf47df",
                            Name = "Girl,",
                            NormalizedName = "GIRL,"
                        },
                        new
                        {
                            Id = "1c43abde-2550-4382-9c7f-1e1f8b2b9ebc",
                            ConcurrencyStamp = "8279fd21-35de-43ae-b72f-23535c181bfe",
                            Name = "Yuri5,",
                            NormalizedName = "YURI5,"
                        },
                        new
                        {
                            Id = "c67c6408-07b1-4669-8e7c-4157f43d530f",
                            ConcurrencyStamp = "bad1a415-14b1-460c-a53d-b2eeddb89d33",
                            Name = "Yuri4,",
                            NormalizedName = "YURI4,"
                        },
                        new
                        {
                            Id = "cc12be5d-a53a-4fa6-953e-6fe4abbc61a1",
                            ConcurrencyStamp = "dca38b85-fe7b-4f18-9725-7b1397c2b139",
                            Name = "Yuri3,",
                            NormalizedName = "YURI3,"
                        },
                        new
                        {
                            Id = "9046b131-4eec-4cab-afbf-f316a7d045e3",
                            ConcurrencyStamp = "5bf69c7c-1afc-48c5-9db2-3032a44efdb7",
                            Name = "Yuri2,",
                            NormalizedName = "YURI2,"
                        },
                        new
                        {
                            Id = "3080887f-75f1-477b-a480-7350fe1dc10c",
                            ConcurrencyStamp = "a6ba8bd9-28be-4622-bc52-b4e6f53d98f1",
                            Name = "Yuri1,",
                            NormalizedName = "YURI1,"
                        },
                        new
                        {
                            Id = "1224f41b-b2c9-479a-b8ae-469e816edeb8",
                            ConcurrencyStamp = "6a0feaff-3f3c-4c6d-a826-d13b6b74987c",
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

            modelBuilder.Entity("BangumiProjectDBServices.Models.AnimeComm", b =>
                {
                    b.HasOne("BangumiProjectDBServices.Models.Anime", "Anime")
                        .WithMany("AnimeComms")
                        .HasForeignKey("AnimeID")
                        .HasConstraintName("AnimeComm_Anime_PK");

                    b.HasOne("BangumiProjectDBServices.Models.User", "Users")
                        .WithMany("AnimeComms")
                        .HasForeignKey("UsersId")
                        .HasConstraintName("AnimeComm_User_PK")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BangumiProjectDBServices.Models.AnimeMemo", b =>
                {
                    b.HasOne("BangumiProjectDBServices.Models.AnimeUserInfo", "UserAnimeInfo")
                        .WithMany("Memos")
                        .HasForeignKey("UserAnimeInfoID")
                        .HasConstraintName("Memo_UserAnimeInfo_PK");
                });

            modelBuilder.Entity("BangumiProjectDBServices.Models.AnimeMoreInfo", b =>
                {
                    b.HasOne("BangumiProjectDBServices.Models.Anime", "Anime")
                        .WithMany()
                        .HasForeignKey("AnimeID");

                    b.HasOne("BangumiProjectDBServices.Models.MusicAlbum", "ED")
                        .WithMany()
                        .HasForeignKey("EDMIID");

                    b.HasOne("BangumiProjectDBServices.Models.MusicAlbum", "OP")
                        .WithMany()
                        .HasForeignKey("OPMIID");
                });

            modelBuilder.Entity("BangumiProjectDBServices.Models.AnimeSouce", b =>
                {
                    b.HasOne("BangumiProjectDBServices.Models.Anime", "Anime")
                        .WithMany("Souce")
                        .HasForeignKey("AnimeID")
                        .HasConstraintName("AnimeSouce_Anime_PK");
                });

            modelBuilder.Entity("BangumiProjectDBServices.Models.AnimeSouceComm", b =>
                {
                    b.HasOne("BangumiProjectDBServices.Models.AnimeSouce", "AnimeSouce")
                        .WithMany("AnimeSouceComms")
                        .HasForeignKey("AnimeSouceID")
                        .HasConstraintName("AnimeSouceComm_AnimeSouce_PK");

                    b.HasOne("BangumiProjectDBServices.Models.User", "Users")
                        .WithMany("AnimeSouceComms")
                        .HasForeignKey("UsersId")
                        .HasConstraintName("AnimeSouceComm_User_PK");
                });

            modelBuilder.Entity("BangumiProjectDBServices.Models.AnimeTag", b =>
                {
                    b.HasOne("BangumiProjectDBServices.Models.Anime", "Anime")
                        .WithMany("Tags")
                        .HasForeignKey("AnimeID")
                        .HasConstraintName("AnimeTag_Anime_PK");
                });

            modelBuilder.Entity("BangumiProjectDBServices.Models.AnimeUserInfo", b =>
                {
                    b.HasOne("BangumiProjectDBServices.Models.Anime", "SubAnime")
                        .WithMany("UserAnimeInfos")
                        .HasForeignKey("SubAnimeAnimeID")
                        .HasConstraintName("UserAnimeInfo_Anime_PK");

                    b.HasOne("BangumiProjectDBServices.Models.User", "Users")
                        .WithMany("UserAnimeInfos")
                        .HasForeignKey("UsersId")
                        .HasConstraintName("UserAnimeInfo_User_PK");
                });

            modelBuilder.Entity("BangumiProjectDBServices.Models.FileImages", b =>
                {
                    b.HasOne("BangumiProjectDBServices.Models.FilePhoto", "Photos")
                        .WithMany("Images")
                        .HasForeignKey("PhotosID")
                        .HasConstraintName("Image_Photo_PK");

                    b.HasOne("BangumiProjectDBServices.Models.User", "UpLoadUsers")
                        .WithMany("Images")
                        .HasForeignKey("UpLoadUsersId")
                        .HasConstraintName("Images_User_PK");
                });

            modelBuilder.Entity("BangumiProjectDBServices.Models.Music", b =>
                {
                    b.HasOne("BangumiProjectDBServices.Models.MusicAlbum")
                        .WithMany("Musics")
                        .HasForeignKey("MusicAlbumMIID");
                });

            modelBuilder.Entity("BangumiProjectDBServices.Models.MusicAlbum", b =>
                {
                    b.HasOne("BangumiProjectDBServices.Models.AnimeMoreInfo")
                        .WithMany("MusicAlbums")
                        .HasForeignKey("AnimeMoreInfoINFOID");
                });

            modelBuilder.Entity("BangumiProjectDBServices.Models.UserRole", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BangumiProjectDBServices.Models.User")
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
                    b.HasOne("BangumiProjectDBServices.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BangumiProjectDBServices.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BangumiProjectDBServices.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
