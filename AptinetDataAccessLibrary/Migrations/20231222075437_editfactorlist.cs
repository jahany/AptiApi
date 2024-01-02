using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AptinetDataAccessLibrary.Migrations
{
    /// <inheritdoc />
    public partial class editfactorlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factor_User_Userid",
                table: "Factor");

            migrationBuilder.DropIndex(
                name: "IX_Factor_Userid",
                table: "Factor");

            migrationBuilder.AddColumn<string>(
                name: "productFinalPrice",
                table: "FactorList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "productPrice",
                table: "FactorList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "productSaving",
                table: "FactorList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "productTax",
                table: "FactorList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "productTotalFinalPrice",
                table: "FactorList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "productTotalPrice",
                table: "FactorList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "totalPrice",
                table: "Factor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "totalCount",
                table: "Factor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Userid",
                table: "Factor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Factor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "Userid1",
                table: "Factor",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Factor_Userid1",
                table: "Factor",
                column: "Userid1");

            migrationBuilder.AddForeignKey(
                name: "FK_Factor_User_Userid1",
                table: "Factor",
                column: "Userid1",
                principalTable: "User",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factor_User_Userid1",
                table: "Factor");

            migrationBuilder.DropIndex(
                name: "IX_Factor_Userid1",
                table: "Factor");

            migrationBuilder.DropColumn(
                name: "productFinalPrice",
                table: "FactorList");

            migrationBuilder.DropColumn(
                name: "productPrice",
                table: "FactorList");

            migrationBuilder.DropColumn(
                name: "productSaving",
                table: "FactorList");

            migrationBuilder.DropColumn(
                name: "productTax",
                table: "FactorList");

            migrationBuilder.DropColumn(
                name: "productTotalFinalPrice",
                table: "FactorList");

            migrationBuilder.DropColumn(
                name: "productTotalPrice",
                table: "FactorList");

            migrationBuilder.DropColumn(
                name: "Userid1",
                table: "Factor");

            migrationBuilder.AlterColumn<float>(
                name: "totalPrice",
                table: "Factor",
                type: "real",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "totalCount",
                table: "Factor",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Userid",
                table: "Factor",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Factor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Factor_Userid",
                table: "Factor",
                column: "Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Factor_User_Userid",
                table: "Factor",
                column: "Userid",
                principalTable: "User",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
