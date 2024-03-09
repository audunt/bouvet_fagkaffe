using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bouvet_fagkaffe_repository.Migrations
{
    /// <inheritdoc />
    public partial class CanLecDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Candidates");
        }
    }
}
