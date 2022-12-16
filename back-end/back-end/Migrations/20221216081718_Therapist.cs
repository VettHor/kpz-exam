using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Therapist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Therapists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceptionRoom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanEdit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Therapists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TherapistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendars_Therapists_TherapistId",
                        column: x => x.TherapistId,
                        principalTable: "Therapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CalendarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Records_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Therapists",
                columns: new[] { "Id", "CanEdit", "Name", "ReceptionRoom", "Surname" },
                values: new object[,]
                {
                    { new Guid("044d4cbb-b7b0-4279-aee1-e5fae3d48747"), false, "Ostap", "Orthopedics", "Hovda" },
                    { new Guid("1dfdd8cf-568d-4460-aa76-d244228b7af6"), false, "Taras", "Dentistry", "Topolya" },
                    { new Guid("7d1c2950-2205-4a51-bfee-86eb0ae73ad2"), false, "Oksana", "Urology", "Dohubets" },
                    { new Guid("ad6902ac-42f2-46bf-a8fd-0694996a7e3b"), false, "Vitalii", "Traumatology", "Horbovyi" },
                    { new Guid("fa2efa4a-bbd4-4b8c-a0cf-0fc249db5e5c"), false, "John", "Traumatology", "Hams" }
                });

            migrationBuilder.InsertData(
                table: "Calendars",
                columns: new[] { "Id", "TherapistId" },
                values: new object[,]
                {
                    { new Guid("1b69586e-fa28-4260-bef6-28188d25f6df"), new Guid("fa2efa4a-bbd4-4b8c-a0cf-0fc249db5e5c") },
                    { new Guid("555b4338-4725-4341-8fa2-57d0be5c4eb5"), new Guid("1dfdd8cf-568d-4460-aa76-d244228b7af6") },
                    { new Guid("89488d05-18cc-4315-9c4f-155a98c2fc7c"), new Guid("7d1c2950-2205-4a51-bfee-86eb0ae73ad2") },
                    { new Guid("d129c0e0-d31f-4faa-b002-46ec391489cb"), new Guid("ad6902ac-42f2-46bf-a8fd-0694996a7e3b") },
                    { new Guid("f8f6aae0-034f-48fb-924f-37b4cea6f4d9"), new Guid("044d4cbb-b7b0-4279-aee1-e5fae3d48747") }
                });

            migrationBuilder.InsertData(
                table: "Records",
                columns: new[] { "Id", "CalendarId", "Frequency", "Text", "VisitTime" },
                values: new object[,]
                {
                    { new Guid("07e611ba-4ae1-4935-8870-9777ed2d41ca"), new Guid("555b4338-4725-4341-8fa2-57d0be5c4eb5"), 2, "Tooth pain", new DateTime(2022, 12, 13, 10, 17, 18, 315, DateTimeKind.Local).AddTicks(1947) },
                    { new Guid("19917699-7403-4c7f-b141-3430d0fab7c4"), new Guid("f8f6aae0-034f-48fb-924f-37b4cea6f4d9"), 1, "Pain in foot", new DateTime(2022, 12, 15, 10, 17, 18, 315, DateTimeKind.Local).AddTicks(1950) },
                    { new Guid("47dc833f-f177-40c8-93df-f9efaf96c27c"), new Guid("1b69586e-fa28-4260-bef6-28188d25f6df"), 2, "Broken hand", new DateTime(2022, 12, 15, 10, 17, 18, 315, DateTimeKind.Local).AddTicks(1959) },
                    { new Guid("b08ee792-92ae-4a77-ac8a-80d618a90389"), new Guid("89488d05-18cc-4315-9c4f-155a98c2fc7c"), 5, "Groin pain", new DateTime(2022, 12, 15, 10, 17, 18, 315, DateTimeKind.Local).AddTicks(1956) },
                    { new Guid("d0d0a445-15be-46e3-887f-39c736af60b8"), new Guid("d129c0e0-d31f-4faa-b002-46ec391489cb"), 4, "Broken leg", new DateTime(2022, 12, 11, 10, 17, 18, 315, DateTimeKind.Local).AddTicks(1906) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_TherapistId",
                table: "Calendars",
                column: "TherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_CalendarId",
                table: "Records",
                column: "CalendarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropTable(
                name: "Therapists");
        }
    }
}
