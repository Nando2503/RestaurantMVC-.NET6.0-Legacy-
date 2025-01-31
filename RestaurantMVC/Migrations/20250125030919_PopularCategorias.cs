using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantMVC.Migrations
{
    public partial class PopularCategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome,Descricao)" +
                "VALUES('Normal', 'regular dish made of regular ingredients')");
            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome,Descricao)" +
                "VALUES('Whole', 'Dish made with whole ingredients')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
