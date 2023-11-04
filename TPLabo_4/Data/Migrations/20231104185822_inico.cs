using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPLabo_4.Data.Migrations
{
    public partial class inico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "calidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoMadera = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Artesanal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_calidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    edad = table.Column<int>(type: "int", nullable: false),
                    experiencia = table.Column<int>(type: "int", nullable: false),
                    fotografia = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empleados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ferreterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<bool>(type: "bit", nullable: false),
                    fotografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    precio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ferreterias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "carpinterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<bool>(type: "bit", nullable: false),
                    fotografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    precio = table.Column<int>(type: "int", nullable: false),
                    IdCalidad = table.Column<int>(type: "int", nullable: false),
                    CalidadId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carpinterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_carpinterias_calidades_CalidadId",
                        column: x => x.CalidadId,
                        principalTable: "calidades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_carpinterias_CalidadId",
                table: "carpinterias",
                column: "CalidadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "carpinterias");

            migrationBuilder.DropTable(
                name: "empleados");

            migrationBuilder.DropTable(
                name: "ferreterias");

            migrationBuilder.DropTable(
                name: "calidades");
        }
    }
}
