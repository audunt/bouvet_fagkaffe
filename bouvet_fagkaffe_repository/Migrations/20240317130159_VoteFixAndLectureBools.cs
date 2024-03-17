using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bouvet_fagkaffe_repository.Migrations
{
    /// <inheritdoc />
    public partial class VoteFixAndLectureBools : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Votes",
                table: "Candidates");

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedByAdmin",
                table: "Lectures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ApprovedByPresenter",
                table: "Lectures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CandidateUser1",
                columns: table => new
                {
                    VotedOnById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VotedOnId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateUser1", x => new { x.VotedOnById, x.VotedOnId });
                    table.ForeignKey(
                        name: "FK_CandidateUser1_Candidates_VotedOnId",
                        column: x => x.VotedOnId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateUser1_Users_VotedOnById",
                        column: x => x.VotedOnById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateUser1_VotedOnId",
                table: "CandidateUser1",
                column: "VotedOnId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateUser1");

            migrationBuilder.DropColumn(
                name: "ApprovedByAdmin",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "ApprovedByPresenter",
                table: "Lectures");

            migrationBuilder.AddColumn<int>(
                name: "Votes",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
