using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace contatos.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioTabelaCorrecaoCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "petfilEnum",
                table: "Usuarios",
                newName: "Perfil");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Usuarios",
                newName: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Perfil",
                table: "Usuarios",
                newName: "petfilEnum");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Usuarios",
                newName: "Name");
        }
    }
}
