using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment5.Migrations
{
    /// <inheritdoc />
    public partial class YourMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "Song",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.ShoppingCartId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Song_ShoppingCartId",
                table: "Song",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_ShoppingCart_ShoppingCartId",
                table: "Song",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "ShoppingCartId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_ShoppingCart_ShoppingCartId",
                table: "Song");

            migrationBuilder.DropTable(
                name: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_Song_ShoppingCartId",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Song");
        }
    }
}
