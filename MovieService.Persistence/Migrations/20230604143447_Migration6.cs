using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Migration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_casts_id",
                table: "casts");

            migrationBuilder.DropIndex(
                name: "IX_actors_id",
                table: "actors");

            migrationBuilder.CreateIndex(
                name: "IX_casts_extra_id",
                table: "casts",
                column: "extra_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_actors_extra_id",
                table: "actors",
                column: "extra_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_casts_extra_id",
                table: "casts");

            migrationBuilder.DropIndex(
                name: "IX_actors_extra_id",
                table: "actors");

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
        }
    }
}
