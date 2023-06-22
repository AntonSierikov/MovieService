using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddJobStates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorDalMovieDal");

            migrationBuilder.AlterColumn<decimal>(
                name: "average_mark",
                table: "movies",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "movie_extra_id",
                table: "movies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "vote_count",
                table: "movies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "casts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    character = table.Column<string>(type: "text", nullable: false),
                    extra_id = table.Column<string>(type: "text", nullable: false),
                    actor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    movie_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_casts", x => x.id);
                    table.ForeignKey(
                        name: "FK_casts_actors_actor_id",
                        column: x => x.actor_id,
                        principalTable: "actors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_casts_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "job_states",
                columns: table => new
                {
                    job_id = table.Column<string>(type: "text", nullable: false),
                    state = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_states", x => x.job_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_casts_actor_id",
                table: "casts",
                column: "actor_id");

            migrationBuilder.CreateIndex(
                name: "IX_casts_movie_id",
                table: "casts",
                column: "movie_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "casts");

            migrationBuilder.DropTable(
                name: "job_states");

            migrationBuilder.DropColumn(
                name: "movie_extra_id",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "vote_count",
                table: "movies");

            migrationBuilder.AlterColumn<int>(
                name: "average_mark",
                table: "movies",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.CreateTable(
                name: "ActorDalMovieDal",
                columns: table => new
                {
                    ActorsId = table.Column<Guid>(type: "uuid", nullable: false),
                    MoviesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorDalMovieDal", x => new { x.ActorsId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_ActorDalMovieDal_actors_ActorsId",
                        column: x => x.ActorsId,
                        principalTable: "actors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorDalMovieDal_movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorDalMovieDal_MoviesId",
                table: "ActorDalMovieDal",
                column: "MoviesId");
        }
    }
}
