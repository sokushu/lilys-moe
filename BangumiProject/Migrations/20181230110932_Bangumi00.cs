using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BangumiProject.Migrations
{
    public partial class Bangumi00 : Migration
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
                values: new object[] { "36987946-a120-4e1d-9404-5e5fb201d20e", "c77e1c0f-a7be-486b-9848-e901c82d6c96", "Admin,", "ADMIN," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6e068d89-5a94-424a-bef6-3a6493191daf", "4c7729d4-9414-4921-97ea-8776e178343c", "Girl,", "GIRL," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fcfbbb4b-bb77-479e-b4b8-32e7ee598653", "1be843ba-347c-42a5-bd1f-951c851f5ab9", "Yuri5,", "YURI5," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8d2f0252-f11d-49a9-9044-7bf1f5799654", "2403b8dd-f653-4b70-b45c-eb2af7b25d15", "Yuri4,", "YURI4," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bae2461f-bfa1-4d26-b41d-5a0bdf732a29", "21406832-f12e-46bb-8755-3290e4f04112", "Yuri3,", "YURI3," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f0362f00-883c-45ad-8ec0-c703a11bfe44", "dff2eec3-5205-437e-9ba8-67a169e2aef7", "Yuri2,", "YURI2," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b2f11ac6-6fcf-4b09-93c2-0ee4e0d9d784", "a032f1e8-dbc8-447a-81dc-a86c2cbf1d69", "Yuri1,", "YURI1," });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b34f4a98-355b-4e03-bde7-b139e8a3703b", "a1160c79-20b9-4dbc-9208-5fb6c89a9b33", "Boy,", "BOY," });

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
