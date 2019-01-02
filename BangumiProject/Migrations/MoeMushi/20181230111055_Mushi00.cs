using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BangumiProject.Migrations.MoeMushi
{
    public partial class Mushi00 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimeInfoSeas",
                columns: table => new
                {
                    DBID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Aid = table.Column<int>(nullable: false),
                    Cid = table.Column<int>(nullable: false),
                    Cover = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false),
                    Long_title = table.Column<string>(nullable: true),
                    Stat_play = table.Column<int>(nullable: false),
                    Share_url = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Season_id = table.Column<int>(nullable: false),
                    AnimeNum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeInfoSeas", x => x.DBID);
                });

            migrationBuilder.CreateTable(
                name: "LogInfos",
                columns: table => new
                {
                    INFOID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Info = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogInfos", x => x.INFOID);
                });

            migrationBuilder.CreateTable(
                name: "TimeLines",
                columns: table => new
                {
                    DBID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Cover = table.Column<string>(nullable: true),
                    Square_cover = table.Column<string>(nullable: true),
                    Pub_time = table.Column<string>(nullable: true),
                    Ep_id = table.Column<int>(nullable: false),
                    Season_id = table.Column<int>(nullable: false),
                    Pub_index = table.Column<string>(nullable: true),
                    Favorites = table.Column<int>(nullable: false),
                    Is_published = table.Column<bool>(nullable: false),
                    Delay = table.Column<bool>(nullable: false),
                    Delay_index = table.Column<string>(nullable: true),
                    Delay_reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeLines", x => x.DBID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeInfoSeas");

            migrationBuilder.DropTable(
                name: "LogInfos");

            migrationBuilder.DropTable(
                name: "TimeLines");
        }
    }
}
