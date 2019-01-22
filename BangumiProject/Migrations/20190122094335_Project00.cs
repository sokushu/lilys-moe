using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BangumiProject.Migrations
{
    public partial class Project00 : Migration
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
                name: "VideoInfos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VideoName = table.Column<string>(nullable: true),
                    VInfo = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoInfos", x => x.ID);
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
                name: "Blogs",
                columns: table => new
                {
                    BlogID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnimeID = table.Column<int>(nullable: false),
                    AnimeName = table.Column<string>(nullable: true),
                    AnimePic = table.Column<string>(nullable: true),
                    AnimeInfo = table.Column<string>(nullable: true),
                    UpLoadUserId = table.Column<string>(nullable: true),
                    BlogStr = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.BlogID);
                    table.ForeignKey(
                        name: "Blog_User_PK",
                        column: x => x.UpLoadUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "BlogTags",
                columns: table => new
                {
                    BolgTagID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BlogTagString = table.Column<string>(nullable: true),
                    BlogsBlogID = table.Column<int>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTags", x => x.BolgTagID);
                    table.ForeignKey(
                        name: "BlogTags_Blog_PK",
                        column: x => x.BlogsBlogID,
                        principalTable: "Blogs",
                        principalColumn: "BlogID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsersId = table.Column<string>(nullable: true),
                    BlogsBlogID = table.Column<int>(nullable: true),
                    CommStr = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommID);
                    table.ForeignKey(
                        name: "Comment_Blogs_PK",
                        column: x => x.BlogsBlogID,
                        principalTable: "Blogs",
                        principalColumn: "BlogID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Comment_User_PK",
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "855ac6ed-72b5-4d56-9786-0f32e03da3e7", "4631c981-4c57-4b7d-958c-e1a42f78ce98", "Admin,", "ADMIN," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fdf41a43-688a-4294-881f-bf59fa6b1738", "dd9dcdf1-1ff1-45be-9924-d092308cabd6", "Girl,", "GIRL," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cb3420c5-2d47-4b24-ac02-e403d93d9ea3", "5b690d0d-6e1a-43b3-b7ee-d58243d84412", "Yuri5,", "YURI5," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0292817c-324f-46ff-bdd7-a62988a9a914", "b9ec249e-7d87-40f3-8a2a-f4dc612bf7bf", "Yuri4,", "YURI4," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7e9ba309-320d-4d6f-a978-a62a52bcd80a", "3f3c087a-e084-4d05-b37e-3f85962fe07e", "Yuri3,", "YURI3," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "329dc6b2-19f2-4026-b22a-e716a4dff7f0", "ccc813c2-4803-41c6-bbd9-5ecc6e63806f", "Yuri2,", "YURI2," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "86211fa7-2c8f-4b3d-b4d8-ca89a5fa29c2", "f99bdf4b-e392-4ada-930c-0025b7e91b27", "Yuri1,", "YURI1," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1157339b-2e85-4e30-9cb8-6a0dee8ecebd", "613de846-f82f-4f83-82f0-be40e895d690", "Boy,", "BOY," });

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
                name: "IX_Blogs_UpLoadUserId",
                table: "Blogs",
                column: "UpLoadUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTags_BlogsBlogID",
                table: "BlogTags",
                column: "BlogsBlogID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BlogsBlogID",
                table: "Comments",
                column: "BlogsBlogID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UsersId",
                table: "Comments",
                column: "UsersId");

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
                name: "IX_UserAnimeInfos_SubAnimeAnimeID",
                table: "UserAnimeInfos",
                column: "SubAnimeAnimeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimeInfos_UsersId",
                table: "UserAnimeInfos",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "BlogTags");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Memos");

            migrationBuilder.DropTable(
                name: "VideoInfos");

            migrationBuilder.DropTable(
                name: "AnimeSouces");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "UserAnimeInfos");

            migrationBuilder.DropTable(
                name: "Anime");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
