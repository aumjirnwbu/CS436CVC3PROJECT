using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS436CVC3PROJECT.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailSystems");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Inventorys",
                newName: "Sale");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Inventorys",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Inventorys",
                newName: "Description");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Inventorys",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Inventorys",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Inventorys");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Inventorys");

            migrationBuilder.RenameColumn(
                name: "Sale",
                table: "Inventorys",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Inventorys",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Inventorys",
                newName: "Email");

            migrationBuilder.CreateTable(
                name: "EmailSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSystems", x => x.Id);
                });
        }
    }
}
