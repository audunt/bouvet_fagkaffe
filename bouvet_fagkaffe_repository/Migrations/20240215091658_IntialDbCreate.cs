using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bouvet_fagkaffe_repository.Migrations
{
    /// <inheritdoc />
    public partial class IntialDbCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeldAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeetingLink",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LectureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingLink", x => new { x.LectureId, x.Id });
                    table.ForeignKey(
                        name: "FK_MeetingLink_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProposedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Votes = table.Column<int>(type: "int", nullable: false),
                    Format = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ForeignId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Groups = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LectureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_ProposedById",
                table: "Candidates",
                column: "ProposedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CandidateId",
                table: "Users",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LectureId",
                table: "Users",
                column: "LectureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Users_ProposedById",
                table: "Candidates",
                column: "ProposedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Users_ProposedById",
                table: "Candidates");

            migrationBuilder.DropTable(
                name: "MeetingLink");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Lectures");
        }
    }
}
