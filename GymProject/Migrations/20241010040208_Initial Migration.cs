using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Ejercicios",
                columns: table => new
                {
                    IdEjercicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ejercicios", x => x.IdEjercicio);
                });

            migrationBuilder.CreateTable(
                name: "Rutinas",
                columns: table => new
                {
                    IdRutina = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rutinas", x => x.IdRutina);
                });

            migrationBuilder.CreateTable(
                name: "CategoriasEjercicios",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    IdEjercicio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasEjercicios", x => new { x.IdCategoria, x.IdEjercicio });
                    table.ForeignKey(
                        name: "FK_CategoriasEjercicios_Categorias_CategoriasIdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriasEjercicios_Ejercicios_EjerciciosIdEjercicio",
                        column: x => x.IdEjercicio,
                        principalTable: "Ejercicios",
                        principalColumn: "IdEjercicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoriasRutinas",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    IdRutina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasRutinas", x => new { x.IdCategoria, x.IdRutina });
                    table.ForeignKey(
                        name: "FK_CategoriasRutinas_Categorias_CategoriasIdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriasRutinas_Rutinas_RutinasIdRutina",
                        column: x => x.IdRutina,
                        principalTable: "Rutinas",
                        principalColumn: "IdRutina",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasEjercicios_EjerciciosIdEjercicio",
                table: "CategoriasEjercicios",
                column: "IdEjercicio");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasRutinas_RutinasIdRutina",
                table: "CategoriasRutinas",
                column: "IdRutina");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriasEjercicios");

            migrationBuilder.DropTable(
                name: "CategoriasRutinas");

            migrationBuilder.DropTable(
                name: "Ejercicios");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Rutinas");
        }
    }
}
