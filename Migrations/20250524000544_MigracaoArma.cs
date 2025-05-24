using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoArma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_ARMAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Dano = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ARMAS", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TB_ARMAS",
                columns: new[] { "Id", "Dano", "Nome" },
                values: new object[,]
                {
                    { 1, 30, "Espada Longa" },
                    { 2, 40, "Machado" },
                    { 3, 20, "Arco" },
                    { 4, 35, "Lança" },
                    { 5, 15, "Adaga" },
                    { 6, 10, "Cajado" },
                    { 7, 45, "Martelo de Guerra" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ARMAS");
        }
    }
}
