using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MovieService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string[]>(
                name: "genres",
                table: "movies",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    genre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "movie_genres",
                columns: table => new
                {
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false),
                    genre_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_genres", x => new { x.movie_id, x.genre_id });
                    table.ForeignKey(
                        name: "FK_movie_genres_genres_genre_id",
                        column: x => x.genre_id,
                        principalTable: "genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_genres_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_movies_movie_extra_id",
                table: "movies",
                column: "movie_extra_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_casts_id",
                table: "casts",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_actors_id",
                table: "actors",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_genres_genre",
                table: "genres",
                column: "genre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_movie_genres_genre_id",
                table: "movie_genres",
                column: "genre_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movie_genres");

            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropIndex(
                name: "IX_movies_movie_extra_id",
                table: "movies");

            migrationBuilder.DropIndex(
                name: "IX_casts_id",
                table: "casts");

            migrationBuilder.DropIndex(
                name: "IX_actors_id",
                table: "actors");

            migrationBuilder.DropColumn(
                name: "genres",
                table: "movies");
        }
    }
}
