using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.MVC.Demo.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyImageName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Imagename",
                table: "Employees",
                newName: "ImageName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Employees",
                newName: "Imagename");
        }
    }
}
