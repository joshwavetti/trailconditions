using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrailApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Region = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Difficulty = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    LengthKm = table.Column<double>(type: "double precision", nullable: false),
                    ElevationGainM = table.Column<int>(type: "integer", nullable: false),
                    ExternalUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConditionReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TrailId = table.Column<Guid>(type: "uuid", nullable: false),
                    OverallCondition = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    ReportedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReportedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConditionReports_Trails_TrailId",
                        column: x => x.TrailId,
                        principalTable: "Trails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConditionReportId = table.Column<Guid>(type: "uuid", nullable: false),
                    Tag = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportTags_ConditionReports_ConditionReportId",
                        column: x => x.ConditionReportId,
                        principalTable: "ConditionReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConditionReports_TrailId",
                table: "ConditionReports",
                column: "TrailId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTags_ConditionReportId",
                table: "ReportTags",
                column: "ConditionReportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportTags");

            migrationBuilder.DropTable(
                name: "ConditionReports");

            migrationBuilder.DropTable(
                name: "Trails");
        }
    }
}
