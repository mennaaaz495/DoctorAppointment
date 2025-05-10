using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAppointment.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _5ra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "StaffId" },
                values: new object[] { new DateTime(2025, 5, 13, 23, 47, 54, 769, DateTimeKind.Local).AddTicks(1615), null });

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "StaffId" },
                values: new object[] { new DateTime(2025, 5, 12, 23, 47, 54, 769, DateTimeKind.Local).AddTicks(1677), null });

            migrationBuilder.UpdateData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "StaffId" },
                values: new object[] { new DateTime(2025, 5, 11, 23, 47, 54, 769, DateTimeKind.Local).AddTicks(1679), null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e1576aea-e98e-4727-b974-9ada03edfae5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ee0d4a1a-76e7-4e44-98fc-8c1c77b6e3cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "a312c7be-1aca-45c7-90ce-ba1c961e3fd2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "53f05914-29d8-4a8c-bc68-066a11fb21d2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14738902-4314-48e2-b25c-cecab751d5d2", "AQAAAAIAAYagAAAAEAEdZw5XOUCtAusQNhVMixKdSroOuGP7Fl2I08WJospcYyFMdiYGj+6Y/obiSBWGHg==", "49e23fe4-6d6e-4c77-a4c1-9f2eda7a1bc1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "132ea7f0-c09b-4bc3-a2fe-a10e7994b99d", "AQAAAAIAAYagAAAAEOJ74aDf2S2g0V2LQ/VZX1MoCTZdOow+c+jF26IPZYdDP2nN1VlLoBnjZOas8QpbCQ==", "c26f03a8-4216-475a-8fe3-886ff95ec871" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90259318-b789-495c-8794-005a56395569", "AQAAAAIAAYagAAAAEGdF1OtPqG7XomdkLsIarpTzc0g1+uJLgGN43nPS7aQ/5tVhD+oZVaQNKiyLjYb5kA==", "ccc8c346-2d83-40b6-9f14-35b9a34e3cb7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6343546a-da76-44bf-b6ce-85dbe0734db7", "AQAAAAIAAYagAAAAEEKq0bND/loQO5R0eA9QOCkKn3Y7krUlPfVQq03W6SEBPMX6gDwH4Lv5yvM4i7TCGQ==", "4354faf6-bc10-42fa-94f3-6c8f531ee7be" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_StaffId",
                table: "Appointments",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_StaffId",
                table: "Appointments",
                column: "StaffId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_StaffId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_StaffId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Appointments");

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
    }
}
