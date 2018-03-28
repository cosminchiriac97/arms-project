using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Data.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Comfort = table.Column<string>(nullable: true),
                    Floor = table.Column<int>(nullable: false),
                    FurnishedAndFit = table.Column<string>(nullable: true),
                    LivingSurface = table.Column<int>(nullable: false),
                    Neighborhood = table.Column<string>(nullable: true),
                    NumberOfBathrooms = table.Column<int>(nullable: false),
                    NumberOfParkingSpaces = table.Column<int>(nullable: false),
                    NumberOfRooms = table.Column<int>(nullable: false),
                    Partitioning = table.Column<string>(nullable: true),
                    PossibilityOfParking = table.Column<bool>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    PropertyStatus = table.Column<string>(nullable: true),
                    PropertyType = table.Column<string>(nullable: true),
                    TotalNumberOfFloors = table.Column<int>(nullable: false),
                    YearBuilt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Houses");
        }
    }
}
