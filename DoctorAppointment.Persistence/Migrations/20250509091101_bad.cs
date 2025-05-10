using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAppointment.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class bad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_AspNetUsers_PatientId1",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_PatientId1",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "Bills");

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 5, 12, 12, 11, 0, 758, DateTimeKind.Local).AddTicks(5487));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 5, 11, 12, 11, 0, 758, DateTimeKind.Local).AddTicks(5551));

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2025, 5, 10, 12, 11, 0, 758, DateTimeKind.Local).AddTicks(5553));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d737f4c3-feec-48c0-ba05-de53813e8744");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4a5dd451-8e64-44db-b5d1-7f54578d9bec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "1f956354-29bb-465f-8441-6258bfea6de0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "561e901b-0fd0-43cc-8b53-b3808110d12f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f3d86475-f6ce-4684-aa5d-2906e769ccfe", "AQAAAAIAAYagAAAAEMRTAPKDPwcd5MtFz6w2Ao9xArY5kaW6glC4CEYCGX+YyA8IREyUKx76UenirkbIwA==", "a73dac12-c911-4b62-8939-fa13a6253e15" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "08571eef-06f7-4dd6-ac64-20a479b3192d", "AQAAAAIAAYagAAAAELW3aEvCGPNm6zI6H2w4ZHL02tmBf/8p9KpwYUbTTHSanSqdpe84+sozm5A6H0Y6xg==", "4d2952d6-fb65-47f6-a8eb-b9c75c1fce5c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7e065fd-a4b8-4571-bc1d-ab10e2219efc", "AQAAAAIAAYagAAAAEPkwaj/QinuqAugyodzpWbcACgPXc6eO2ouVHDmPSBasgQW6s3k9okg2+JEMLvCL2Q==", "0c944d05-4c08-4c40-a2e2-c75bda66e16e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c1ec7ce-5835-4ffd-8952-084eeb0d74c8", "AQAAAAIAAYagAAAAEJnwtubl+JuYybaDAQ8sp9W7F4d92J+lmJG9G2XhHctget3fXgCjJae6uXH8SRgqfw==", "5a359418-1164-4baf-865f-d459e2922b89" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PatientId",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PatientId1",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "17f5ecda-4595-4a54-8012-0fa9925825ca");

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62719c43-e852-4b1a-94cb-ae15ad072c47", "AQAAAAIAAYagAAAAEJrCvEGsKVMvT0UiCJofjMeMfyQHcuOYm5C6BFUFunoVcrXScLQSbk6b+qPxhCk3yw==", "daf35bac-56a5-4596-91d9-1a203d75ea45" });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PatientId1",
                table: "Bills",
                column: "PatientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_AspNetUsers_PatientId1",
                table: "Bills",
                column: "PatientId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
