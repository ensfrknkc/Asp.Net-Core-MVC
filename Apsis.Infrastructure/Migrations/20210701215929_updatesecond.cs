using Microsoft.EntityFrameworkCore.Migrations;

namespace Apsis.Infrastructure.Migrations
{
    public partial class updatesecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flats_Blocks_BlockId",
                table: "Flats");

            migrationBuilder.AlterColumn<int>(
                name: "BlockId",
                table: "Flats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Flats_Blocks_BlockId",
                table: "Flats",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flats_Blocks_BlockId",
                table: "Flats");

            migrationBuilder.AlterColumn<int>(
                name: "BlockId",
                table: "Flats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Flats_Blocks_BlockId",
                table: "Flats",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
