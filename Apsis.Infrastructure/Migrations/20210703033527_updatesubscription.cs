using Microsoft.EntityFrameworkCore.Migrations;

namespace Apsis.Infrastructure.Migrations
{
    public partial class updatesubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Flats_FlatId",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<int>(
                name: "FlatId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Flats_FlatId",
                table: "Subscriptions",
                column: "FlatId",
                principalTable: "Flats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Flats_FlatId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<int>(
                name: "FlatId",
                table: "Subscriptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Flats_FlatId",
                table: "Subscriptions",
                column: "FlatId",
                principalTable: "Flats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
