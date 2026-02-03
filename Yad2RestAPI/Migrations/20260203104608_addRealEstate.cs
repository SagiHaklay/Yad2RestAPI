using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yad2RestAPI.Migrations
{
    /// <inheritdoc />
    public partial class addRealEstate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RealEstateAds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    TotalFloors = table.Column<int>(type: "int", nullable: false),
                    OnColumns = table.Column<bool>(type: "bit", nullable: false),
                    PropertyStatus = table.Column<int>(type: "int", nullable: false),
                    AirDirectionCount = table.Column<int>(type: "int", nullable: false),
                    View = table.Column<int>(type: "int", nullable: false),
                    RoomCount = table.Column<float>(type: "real", nullable: false),
                    ShowerCount = table.Column<int>(type: "int", nullable: false),
                    ParkingCount = table.Column<int>(type: "int", nullable: false),
                    BalconyCount = table.Column<int>(type: "int", nullable: false),
                    PropertyFeatures = table.Column<int>(type: "int", nullable: false),
                    PropertyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentCount = table.Column<int>(type: "int", nullable: true),
                    HouseCommiteePayment = table.Column<float>(type: "real", nullable: true),
                    PropertyTax = table.Column<float>(type: "real", nullable: true),
                    BuiltArea = table.Column<double>(type: "float", nullable: true),
                    GardenArea = table.Column<double>(type: "float", nullable: true),
                    TotalArea = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsLongTerm = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrls = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrls = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateAds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealEstateAds_Profiles_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProfileModelRealEstateAdModel",
                columns: table => new
                {
                    FavoriteAdsId = table.Column<int>(type: "int", nullable: false),
                    ProfileModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileModelRealEstateAdModel", x => new { x.FavoriteAdsId, x.ProfileModelId });
                    table.ForeignKey(
                        name: "FK_ProfileModelRealEstateAdModel_Profiles_ProfileModelId",
                        column: x => x.ProfileModelId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileModelRealEstateAdModel_RealEstateAds_FavoriteAdsId",
                        column: x => x.FavoriteAdsId,
                        principalTable: "RealEstateAds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileModelRealEstateAdModel_ProfileModelId",
                table: "ProfileModelRealEstateAdModel",
                column: "ProfileModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateAds_PublisherId",
                table: "RealEstateAds",
                column: "PublisherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileModelRealEstateAdModel");

            migrationBuilder.DropTable(
                name: "RealEstateAds");
        }
    }
}
