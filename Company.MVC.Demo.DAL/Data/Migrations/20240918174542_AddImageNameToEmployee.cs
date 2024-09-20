using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.MVC.Demo.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImageNameToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagename",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagename",
                table: "Employees");
        }
    }
}
