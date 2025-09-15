using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AjusteParametrosProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "unite",
                table: "Productos",
                newName: "Unite");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Productos",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "category",
                table: "Productos",
                newName: "Category");

            migrationBuilder.AlterColumn<int>(
                name: "Unite",
                table: "Productos",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "Productos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unite",
                table: "Productos",
                newName: "unite");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Productos",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Productos",
                newName: "category");

            migrationBuilder.AlterColumn<float>(
                name: "unite",
                table: "Productos",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "category",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
