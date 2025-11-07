using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendChallenge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "delivery_drivers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    cnpj = table.Column<string>(type: "text", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    drivers_license = table.Column<string>(type: "text", nullable: false),
                    type_of_drivers_license = table.Column<string>(type: "text", nullable: false),
                    url_of_the_drivers_license_image = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delivery_drivers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "motorcycles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id = table.Column<string>(type: "text", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    model = table.Column<string>(type: "text", nullable: false),
                    plate = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motorcycles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    motorcycle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    motorcycle_external_id = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "motorcycle_rentals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id = table.Column<string>(type: "text", nullable: false),
                    delivery_person_id = table.Column<Guid>(type: "uuid", nullable: false),
                    motorcycle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    estimated_end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rental_plan = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motorcycle_rentals", x => x.id);
                    table.ForeignKey(
                        name: "FK_motorcycle_rentals_delivery_drivers_delivery_person_id",
                        column: x => x.delivery_person_id,
                        principalTable: "delivery_drivers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_motorcycle_rentals_motorcycles_motorcycle_id",
                        column: x => x.motorcycle_id,
                        principalTable: "motorcycles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_delivery_drivers_cnpj",
                table: "delivery_drivers",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_delivery_drivers_drivers_license",
                table: "delivery_drivers",
                column: "drivers_license",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_delivery_drivers_external_id",
                table: "delivery_drivers",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_motorcycle_rentals_delivery_person_id",
                table: "motorcycle_rentals",
                column: "delivery_person_id");

            migrationBuilder.CreateIndex(
                name: "IX_motorcycle_rentals_external_id",
                table: "motorcycle_rentals",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_motorcycle_rentals_motorcycle_id",
                table: "motorcycle_rentals",
                column: "motorcycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_motorcycles_external_id",
                table: "motorcycles",
                column: "external_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_motorcycles_plate",
                table: "motorcycles",
                column: "plate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "motorcycle_rentals");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "delivery_drivers");

            migrationBuilder.DropTable(
                name: "motorcycles");
        }
    }
}
