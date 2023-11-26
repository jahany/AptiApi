using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AptinetDataAccessLibrary.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUser",
                columns: table => new
                {
                    barcode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUser", x => x.barcode);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    loyalityBarcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    offerPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    offerLimitedPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    offerMount = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Versions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    appVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dbVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imagesVersion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Weights",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    w1 = table.Column<int>(type: "int", nullable: true),
                    w2 = table.Column<int>(type: "int", nullable: true),
                    w3 = table.Column<int>(type: "int", nullable: true),
                    w4 = table.Column<int>(type: "int", nullable: true),
                    w5 = table.Column<int>(type: "int", nullable: true),
                    w6 = table.Column<int>(type: "int", nullable: true),
                    w7 = table.Column<int>(type: "int", nullable: true),
                    w8 = table.Column<int>(type: "int", nullable: true),
                    w9 = table.Column<int>(type: "int", nullable: true),
                    w10 = table.Column<int>(type: "int", nullable: true),
                    qrCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weights", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rate = table.Column<int>(type: "int", nullable: false),
                    commentCount = table.Column<int>(type: "int", nullable: false),
                    w1 = table.Column<int>(type: "int", nullable: true),
                    w2 = table.Column<int>(type: "int", nullable: true),
                    w3 = table.Column<int>(type: "int", nullable: true),
                    w4 = table.Column<int>(type: "int", nullable: true),
                    w5 = table.Column<int>(type: "int", nullable: true),
                    w6 = table.Column<int>(type: "int", nullable: true),
                    w7 = table.Column<int>(type: "int", nullable: true),
                    w8 = table.Column<int>(type: "int", nullable: true),
                    w9 = table.Column<int>(type: "int", nullable: true),
                    w10 = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<float>(type: "real", nullable: false),
                    finalprice = table.Column<float>(type: "real", nullable: false),
                    meanWeight = table.Column<int>(type: "int", nullable: true),
                    tolerance = table.Column<int>(type: "int", nullable: true),
                    insertedWeighted = table.Column<int>(type: "int", nullable: true),
                    barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isOffer = table.Column<bool>(type: "bit", nullable: false),
                    isPlu = table.Column<bool>(type: "bit", nullable: true),
                    tax = table.Column<bool>(type: "bit", nullable: false),
                    qrCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Storeid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                    table.ForeignKey(
                        name: "FK_Product_Store_Storeid",
                        column: x => x.Storeid,
                        principalTable: "Store",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Factor",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Userid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    totalPrice = table.Column<float>(type: "real", nullable: true),
                    totalCount = table.Column<int>(type: "int", nullable: true),
                    regdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Storeid = table.Column<int>(type: "int", nullable: false),
                    basketName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    offerCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factor", x => x.id);
                    table.ForeignKey(
                        name: "FK_Factor_User_Userid",
                        column: x => x.Userid,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suggestion",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Productid = table.Column<long>(type: "bigint", nullable: false),
                    Productidsuggested = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestion", x => x.id);
                    table.ForeignKey(
                        name: "FK_Suggestion_Product_Productid",
                        column: x => x.Productid,
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactorList",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Productid = table.Column<long>(type: "bigint", nullable: false),
                    count = table.Column<int>(type: "int", nullable: true),
                    weight = table.Column<float>(type: "real", nullable: true),
                    Factorid = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorList", x => x.id);
                    table.ForeignKey(
                        name: "FK_FactorList_Factor_Factorid",
                        column: x => x.Factorid,
                        principalTable: "Factor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactorList_Product_Productid",
                        column: x => x.Productid,
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Factor_Userid",
                table: "Factor",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_FactorList_Factorid",
                table: "FactorList",
                column: "Factorid");

            migrationBuilder.CreateIndex(
                name: "IX_FactorList_Productid",
                table: "FactorList",
                column: "Productid");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Storeid",
                table: "Product",
                column: "Storeid");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestion_Productid",
                table: "Suggestion",
                column: "Productid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUser");

            migrationBuilder.DropTable(
                name: "FactorList");

            migrationBuilder.DropTable(
                name: "Suggestion");

            migrationBuilder.DropTable(
                name: "Versions");

            migrationBuilder.DropTable(
                name: "Weights");

            migrationBuilder.DropTable(
                name: "Factor");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Store");
        }
    }
}
