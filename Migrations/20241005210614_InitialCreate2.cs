using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PakemonReviewWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonCategories_Categories_CategoryId1",
                table: "PokemonCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonCategories_Pokemons_CategoryId",
                table: "PokemonCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonOwners_Owners_OwnerId1",
                table: "PokemonOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonOwners_Pokemons_OwnerId",
                table: "PokemonOwners");

            migrationBuilder.DropIndex(
                name: "IX_PokemonOwners_OwnerId1",
                table: "PokemonOwners");

            migrationBuilder.DropIndex(
                name: "IX_PokemonCategories_CategoryId1",
                table: "PokemonCategories");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "PokemonOwners");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "PokemonCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonCategories_Categories_CategoryId",
                table: "PokemonCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonCategories_Pokemons_PokemonId",
                table: "PokemonCategories",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonOwners_Owners_OwnerId",
                table: "PokemonOwners",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonOwners_Pokemons_PokemonId",
                table: "PokemonOwners",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonCategories_Categories_CategoryId",
                table: "PokemonCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonCategories_Pokemons_PokemonId",
                table: "PokemonCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonOwners_Owners_OwnerId",
                table: "PokemonOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonOwners_Pokemons_PokemonId",
                table: "PokemonOwners");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId1",
                table: "PokemonOwners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "PokemonCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonOwners_OwnerId1",
                table: "PokemonOwners",
                column: "OwnerId1");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonCategories_CategoryId1",
                table: "PokemonCategories",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonCategories_Categories_CategoryId1",
                table: "PokemonCategories",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonCategories_Pokemons_CategoryId",
                table: "PokemonCategories",
                column: "CategoryId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonOwners_Owners_OwnerId1",
                table: "PokemonOwners",
                column: "OwnerId1",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonOwners_Pokemons_OwnerId",
                table: "PokemonOwners",
                column: "OwnerId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
