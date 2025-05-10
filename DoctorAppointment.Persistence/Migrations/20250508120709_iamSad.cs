using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAppointment.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class iamSad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    GeneratedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    GeneratedById = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_AspNetUsers_GeneratedById",
                        column: x => x.GeneratedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_AspNetUsers_PatientId1",
                        column: x => x.PatientId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 5, 11, 15, 7, 9, 274, DateTimeKind.Local).AddTicks(1387));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 5, 10, 15, 7, 9, 274, DateTimeKind.Local).AddTicks(1476));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2025, 5, 9, 15, 7, 9, 274, DateTimeKind.Local).AddTicks(1478));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7558a74e-a75c-4141-8d90-a9cc8e1cf772");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5d6e385d-d76c-4325-9cbf-09e0d58a2ed9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "bba66ba8-7800-4f1f-8d75-081f580a07c2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { 4, "17f5ecda-4595-4a54-8012-0fa9925825ca", "Staff can manage appointments and generate bills after doctor approval.", "Staff", "STAFF" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4787cfa0-aa58-4273-bc34-0ae4fd94b010", "AQAAAAIAAYagAAAAEBZla2sRdTaxMkDalwERy9JbCcjQBsWB5dUpOSpAecOw0tEwWCtIGFK2/pS9oQZgxQ==", "53d43286-bb55-46a8-8bb7-826ec5524c13" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1229b5a0-9aa0-457f-bba4-aa2b7dfb2423", "AQAAAAIAAYagAAAAEAMSrHlJZhll6VK6fckbqZ7P0AZSI9SBgTvLu+D6LnQvyK4wk2M0AQd4K0G40aD0XA==", "76401c7f-a51a-45f4-a521-a18becc4657c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d95a86b-8014-4d05-8f85-92fb6aad81b4", "AQAAAAIAAYagAAAAEJt+vGTNlprE+K1/fkWDbAM9Wluhuq+984EWEaHObGTJDJHQ1DSFU6aPNYdLgl4s4g==", "3534ffba-e49f-44cc-b969-4c36758480e2" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 4, 0, "62719c43-e852-4b1a-94cb-ae15ad072c47", "john.doe@email.com", true, "John", "Doe", false, null, "JOHN.DOE@EMAIL.COM", "JOHN.DOE", "AQAAAAIAAYagAAAAEJrCvEGsKVMvT0UiCJofjMeMfyQHcuOYm5C6BFUFunoVcrXScLQSbk6b+qPxhCk3yw==", null, false, "daf35bac-56a5-4596-91d9-1a203d75ea45", false, "john.doe" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 4, 4 });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_AppointmentId",
                table: "Bills",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_GeneratedById",
                table: "Bills",
                column: "GeneratedById");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PatientId1",
                table: "Bills",
                column: "PatientId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 7, 15, 12, 13, 38, 454, DateTimeKind.Local).AddTicks(2747));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 7, 14, 12, 13, 38, 454, DateTimeKind.Local).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2024, 7, 13, 12, 13, 38, 454, DateTimeKind.Local).AddTicks(2817));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ffea4d94-ab5b-43cc-915a-81aa890b8578");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "7cad8e26-b07f-43ff-b7ee-d339a6d351c3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5ee0798a-33c2-4eb2-a196-ce04cd2d9263");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e30080f0-2c00-4804-b8f5-3fb1ab46c241", "AQAAAAIAAYagAAAAEGar3u0CC2N7erwuyQSoIHVjehpdzQ8p/sRZn/GNym+xob5IM7ypfrXotCoQD6C9yw==", "fe560e1b-be47-4b53-ac29-8afdf650e260" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "097e7e95-7e0b-4f0c-ad89-8cf9da3386fb", "AQAAAAIAAYagAAAAEHENfouxnBlrR3th4mxGZNU5A25Qx5kHjBHdpIKeXRrejvP90BBdjTzwf7oivSH4QA==", "7954084e-89df-4e39-bdfa-bd01e27d5080" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fddd37fd-90f6-43e7-9f42-258bba649def", "AQAAAAIAAYagAAAAENQRFzUExU3qXdSeilbioiXZ6c+ws79YLAWEdxdIEmiVrhFjmoberKuAhmV3TGDu+w==", "90706ccc-3576-49f8-8123-faed85639aa8" });
        }
    }
}
