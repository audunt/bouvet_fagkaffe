using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bouvet_fagkaffe_repository.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Candidates_CandidateId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Lectures_LectureId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CandidateId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LectureId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetingLink",
                table: "MeetingLink");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LectureId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "LectureId",
                table: "MeetingLink",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetingLink",
                table: "MeetingLink",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CandidateUser",
                columns: table => new
                {
                    RegisteredOnCandidatesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegisteredPresentersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateUser", x => new { x.RegisteredOnCandidatesId, x.RegisteredPresentersId });
                    table.ForeignKey(
                        name: "FK_CandidateUser_Candidates_RegisteredOnCandidatesId",
                        column: x => x.RegisteredOnCandidatesId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateUser_Users_RegisteredPresentersId",
                        column: x => x.RegisteredPresentersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LectureUser",
                columns: table => new
                {
                    HeldById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PresentsLecturesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureUser", x => new { x.HeldById, x.PresentsLecturesId });
                    table.ForeignKey(
                        name: "FK_LectureUser_Lectures_PresentsLecturesId",
                        column: x => x.PresentsLecturesId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LectureUser_Users_HeldById",
                        column: x => x.HeldById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingLink_LectureId",
                table: "MeetingLink",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateUser_RegisteredPresentersId",
                table: "CandidateUser",
                column: "RegisteredPresentersId");

            migrationBuilder.CreateIndex(
                name: "IX_LectureUser_PresentsLecturesId",
                table: "LectureUser",
                column: "PresentsLecturesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateUser");

            migrationBuilder.DropTable(
                name: "LectureUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetingLink",
                table: "MeetingLink");

            migrationBuilder.DropIndex(
                name: "IX_MeetingLink_LectureId",
                table: "MeetingLink");

            migrationBuilder.AddColumn<Guid>(
                name: "CandidateId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LectureId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LectureId",
                table: "MeetingLink",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetingLink",
                table: "MeetingLink",
                columns: new[] { "LectureId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CandidateId",
                table: "Users",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LectureId",
                table: "Users",
                column: "LectureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Candidates_CandidateId",
                table: "Users",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Lectures_LectureId",
                table: "Users",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id");
        }
    }
}
