using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BangumiProject.Migrations
{
    public partial class Init00 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anime",
                columns: table => new
                {
                    AnimeID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnimeName = table.Column<string>(nullable: false),
                    AnimePic = table.Column<string>(nullable: true),
                    AnimeNum = table.Column<int>(nullable: false),
                    AnimeType = table.Column<string>(nullable: false),
                    AnimeInfo = table.Column<string>(nullable: true),
                    IsEnd = table.Column<bool>(nullable: false),
                    AnimePlayTime = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anime", x => x.AnimeID);
                });

            migrationBuilder.CreateTable(
                name: "AnimeNums",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnimeID = table.Column<int>(nullable: false),
                    AnimeNum = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    PlayURL = table.Column<string>(nullable: true),
                    AnimeNumbInfo = table.Column<string>(nullable: true),
                    PlayTime = table.Column<DateTime>(nullable: false),
                    IsStop = table.Column<bool>(nullable: false),
                    StopNum = table.Column<int>(nullable: false),
                    StopTime = table.Column<DateTime>(nullable: false),
                    IsStopLong = table.Column<bool>(nullable: false),
                    StopLongStartPlay = table.Column<string>(nullable: true),
                    StopLongStartPlayDVD = table.Column<bool>(nullable: false),
                    StopCause = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeNums", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')"),
                    URL = table.Column<string>(nullable: true),
                    UserPic = table.Column<string>(nullable: true),
                    UserBackPic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AnimeSouces",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Pic = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    AnimeID = table.Column<int>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeSouces", x => x.ID);
                    table.ForeignKey(
                        name: "AnimeSouce_Anime_PK",
                        column: x => x.AnimeID,
                        principalTable: "Anime",
                        principalColumn: "AnimeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnimeTag",
                columns: table => new
                {
                    TagID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TagName = table.Column<string>(nullable: true),
                    AnimeID = table.Column<int>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeTag", x => x.TagID);
                    table.ForeignKey(
                        name: "AnimeTag_Anime_PK",
                        column: x => x.AnimeID,
                        principalTable: "Anime",
                        principalColumn: "AnimeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeComms",
                columns: table => new
                {
                    CommID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommStr = table.Column<string>(nullable: false),
                    UsersId = table.Column<string>(nullable: false),
                    AnimeID = table.Column<int>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeComms", x => x.CommID);
                    table.ForeignKey(
                        name: "AnimeComm_Anime_PK",
                        column: x => x.AnimeID,
                        principalTable: "Anime",
                        principalColumn: "AnimeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "AnimeComm_User_PK",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false),
                    KenGenChi = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAnimeInfos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubAnimeAnimeID = table.Column<int>(nullable: true),
                    NowAnimeNum = table.Column<int>(nullable: false),
                    UsersId = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnimeInfos", x => x.ID);
                    table.ForeignKey(
                        name: "UserAnimeInfo_Anime_PK",
                        column: x => x.SubAnimeAnimeID,
                        principalTable: "Anime",
                        principalColumn: "AnimeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "UserAnimeInfo_User_PK",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageID = table.Column<string>(nullable: false),
                    ImageName = table.Column<string>(nullable: false),
                    ImagePath = table.Column<string>(nullable: false),
                    StaticPath = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    UpLoadUsersId = table.Column<string>(nullable: true),
                    PhotosID = table.Column<int>(nullable: true),
                    ReadUsers = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageID);
                    table.ForeignKey(
                        name: "Image_Photo_PK",
                        column: x => x.PhotosID,
                        principalTable: "Photos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Images_User_PK",
                        column: x => x.UpLoadUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnimeSouceComms",
                columns: table => new
                {
                    CommID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommStr = table.Column<string>(nullable: false),
                    AnimeSouceID = table.Column<int>(nullable: true),
                    UsersId = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeSouceComms", x => x.CommID);
                    table.ForeignKey(
                        name: "AnimeSouceComm_AnimeSouce_PK",
                        column: x => x.AnimeSouceID,
                        principalTable: "AnimeSouces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "AnimeSouceComm_User_PK",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Memos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NowAnimeNum = table.Column<int>(nullable: false),
                    MemoStr = table.Column<string>(nullable: true),
                    UserAnimeInfoID = table.Column<int>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memos", x => x.ID);
                    table.ForeignKey(
                        name: "Memo_UserAnimeInfo_PK",
                        column: x => x.UserAnimeInfoID,
                        principalTable: "UserAnimeInfos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnimeMoreInfos",
                columns: table => new
                {
                    INFOID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnimeID = table.Column<int>(nullable: true),
                    OPMIID = table.Column<int>(nullable: true),
                    EDMIID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeMoreInfos", x => x.INFOID);
                    table.ForeignKey(
                        name: "FK_AnimeMoreInfos_Anime_AnimeID",
                        column: x => x.AnimeID,
                        principalTable: "Anime",
                        principalColumn: "AnimeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MusicAlbum",
                columns: table => new
                {
                    MIID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Pic = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')"),
                    AnimeMoreInfoINFOID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicAlbum", x => x.MIID);
                    table.ForeignKey(
                        name: "FK_MusicAlbum_AnimeMoreInfos_AnimeMoreInfoINFOID",
                        column: x => x.AnimeMoreInfoINFOID,
                        principalTable: "AnimeMoreInfos",
                        principalColumn: "INFOID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Musics",
                columns: table => new
                {
                    MID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MusicName = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    Time = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')"),
                    MusicAlbumMIID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musics", x => x.MID);
                    table.ForeignKey(
                        name: "FK_Musics_MusicAlbum_MusicAlbumMIID",
                        column: x => x.MusicAlbumMIID,
                        principalTable: "MusicAlbum",
                        principalColumn: "MIID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "89daa059-0441-4b26-8f3a-fa29eebce314", "3deafab4-97ef-4e98-b7a8-e1723632ed4a", "Admin,", "ADMIN," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d8af03b9-e890-4127-a1ae-c3dfdd5bf69c", "5c7e4b81-cde8-4397-99fe-3648b7cf47df", "Girl,", "GIRL," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1c43abde-2550-4382-9c7f-1e1f8b2b9ebc", "8279fd21-35de-43ae-b72f-23535c181bfe", "Yuri5,", "YURI5," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c67c6408-07b1-4669-8e7c-4157f43d530f", "bad1a415-14b1-460c-a53d-b2eeddb89d33", "Yuri4,", "YURI4," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cc12be5d-a53a-4fa6-953e-6fe4abbc61a1", "dca38b85-fe7b-4f18-9725-7b1397c2b139", "Yuri3,", "YURI3," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9046b131-4eec-4cab-afbf-f316a7d045e3", "5bf69c7c-1afc-48c5-9db2-3032a44efdb7", "Yuri2,", "YURI2," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3080887f-75f1-477b-a480-7350fe1dc10c", "a6ba8bd9-28be-4622-bc52-b4e6f53d98f1", "Yuri1,", "YURI1," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1224f41b-b2c9-479a-b8ae-469e816edeb8", "6a0feaff-3f3c-4c6d-a826-d13b6b74987c", "Boy,", "BOY," });

            migrationBuilder.CreateIndex(
                name: "IX_Anime_AnimeID_AnimeName_AnimeType",
                table: "Anime",
                columns: new[] { "AnimeID", "AnimeName", "AnimeType" });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeComms_AnimeID",
                table: "AnimeComms",
                column: "AnimeID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeComms_UsersId",
                table: "AnimeComms",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeMoreInfos_AnimeID",
                table: "AnimeMoreInfos",
                column: "AnimeID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeMoreInfos_EDMIID",
                table: "AnimeMoreInfos",
                column: "EDMIID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeMoreInfos_OPMIID",
                table: "AnimeMoreInfos",
                column: "OPMIID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeSouceComms_AnimeSouceID",
                table: "AnimeSouceComms",
                column: "AnimeSouceID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeSouceComms_UsersId",
                table: "AnimeSouceComms",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeSouces_AnimeID",
                table: "AnimeSouces",
                column: "AnimeID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeSouces_ID_Name",
                table: "AnimeSouces",
                columns: new[] { "ID", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeTag_AnimeID",
                table: "AnimeTag",
                column: "AnimeID");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeTag_TagID_TagName",
                table: "AnimeTag",
                columns: new[] { "TagID", "TagName" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_PhotosID",
                table: "Images",
                column: "PhotosID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_UpLoadUsersId",
                table: "Images",
                column: "UpLoadUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Memos_UserAnimeInfoID",
                table: "Memos",
                column: "UserAnimeInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_MusicAlbum_AnimeMoreInfoINFOID",
                table: "MusicAlbum",
                column: "AnimeMoreInfoINFOID");

            migrationBuilder.CreateIndex(
                name: "IX_Musics_MusicAlbumMIID",
                table: "Musics",
                column: "MusicAlbumMIID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimeInfos_SubAnimeAnimeID",
                table: "UserAnimeInfos",
                column: "SubAnimeAnimeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimeInfos_UsersId",
                table: "UserAnimeInfos",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeMoreInfos_MusicAlbum_EDMIID",
                table: "AnimeMoreInfos",
                column: "EDMIID",
                principalTable: "MusicAlbum",
                principalColumn: "MIID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeMoreInfos_MusicAlbum_OPMIID",
                table: "AnimeMoreInfos",
                column: "OPMIID",
                principalTable: "MusicAlbum",
                principalColumn: "MIID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeMoreInfos_Anime_AnimeID",
                table: "AnimeMoreInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeMoreInfos_MusicAlbum_EDMIID",
                table: "AnimeMoreInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeMoreInfos_MusicAlbum_OPMIID",
                table: "AnimeMoreInfos");

            migrationBuilder.DropTable(
                name: "AnimeComms");

            migrationBuilder.DropTable(
                name: "AnimeNums");

            migrationBuilder.DropTable(
                name: "AnimeSouceComms");

            migrationBuilder.DropTable(
                name: "AnimeTag");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Memos");

            migrationBuilder.DropTable(
                name: "Musics");

            migrationBuilder.DropTable(
                name: "AnimeSouces");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "UserAnimeInfos");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Anime");

            migrationBuilder.DropTable(
                name: "MusicAlbum");

            migrationBuilder.DropTable(
                name: "AnimeMoreInfos");
        }
    }
}
