using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bids_db_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Users_Id",
                table: "Bids");

            migrationBuilder.DropTable(
                name: "BidsLots");

            migrationBuilder.DropTable(
                name: "BidsUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Bids",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "lotId",
                table: "Bids",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Bids",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bids_lotId",
                table: "Bids",
                column: "lotId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_userId",
                table: "Bids",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Lots_lotId",
                table: "Bids",
                column: "lotId",
                principalTable: "Lots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Users_userId",
                table: "Bids",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Lots_lotId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Users_userId",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_lotId",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_userId",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "lotId",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Bids");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Bids",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateTable(
                name: "BidsLots",
                columns: table => new
                {
                    BidsId = table.Column<int>(type: "integer", nullable: false),
                    LotId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidsLots", x => new { x.BidsId, x.LotId });
                    table.ForeignKey(
                        name: "FK_BidsLots_Bids_BidsId",
                        column: x => x.BidsId,
                        principalTable: "Bids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BidsLots_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BidsUsers",
                columns: table => new
                {
                    BidsId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidsUsers", x => new { x.BidsId, x.UserId });
                    table.ForeignKey(
                        name: "FK_BidsUsers_Bids_BidsId",
                        column: x => x.BidsId,
                        principalTable: "Bids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BidsUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BidsLots_LotId",
                table: "BidsLots",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_BidsUsers_UserId",
                table: "BidsUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Users_Id",
                table: "Bids",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
