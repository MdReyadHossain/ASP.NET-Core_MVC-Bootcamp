using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudApp.Migrations
{
    /// <inheritdoc />
    public partial class typecolumnaddedtousertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "ID");
        }
    }
}
