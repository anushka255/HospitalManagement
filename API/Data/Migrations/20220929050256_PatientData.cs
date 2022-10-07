using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class PatientData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "SSN",
                table: "Users",
                newName: "Ssn");

            migrationBuilder.AlterColumn<string>(
                name: "Ssn",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "Billing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BillId = table.Column<int>(type: "INTEGER", nullable: false),
                    DoctorCharge = table.Column<float>(type: "REAL", nullable: false),
                    MedicineCharge = table.Column<float>(type: "REAL", nullable: false),
                    RoomCharge = table.Column<float>(type: "REAL", nullable: false),
                    OperationCharge = table.Column<float>(type: "REAL", nullable: false),
                    NursingCharge = table.Column<float>(type: "REAL", nullable: false),
                    LabCharge = table.Column<float>(type: "REAL", nullable: false),
                    TotalCharge = table.Column<float>(type: "REAL", nullable: false),
                    PublicId = table.Column<string>(type: "TEXT", nullable: true),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Billing_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<float>(type: "REAL", nullable: false),
                    Dosage = table.Column<float>(type: "REAL", nullable: false),
                    DoctorsId = table.Column<int>(type: "INTEGER", nullable: false),
                    PharmacistId = table.Column<int>(type: "INTEGER", nullable: false),
                    Recommendation = table.Column<string>(type: "TEXT", nullable: true),
                    PublicId = table.Column<string>(type: "TEXT", nullable: true),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medication_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TestId = table.Column<int>(type: "INTEGER", nullable: false),
                    TestName = table.Column<string>(type: "TEXT", nullable: true),
                    TestDescription = table.Column<string>(type: "TEXT", nullable: true),
                    LabScientistId = table.Column<int>(type: "INTEGER", nullable: false),
                    DoctorId = table.Column<int>(type: "INTEGER", nullable: false),
                    PrescriptionTime = table.Column<string>(type: "TEXT", nullable: true),
                    TestTime = table.Column<string>(type: "TEXT", nullable: true),
                    TestResult = table.Column<string>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    Bill = table.Column<float>(type: "REAL", nullable: false),
                    PublicId = table.Column<string>(type: "TEXT", nullable: true),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Test_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Triage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TriageId = table.Column<int>(type: "INTEGER", nullable: false),
                    NurseId = table.Column<int>(type: "INTEGER", nullable: false),
                    BloodPressure = table.Column<string>(type: "TEXT", nullable: true),
                    HeartBeat = table.Column<string>(type: "TEXT", nullable: true),
                    SugarLevel = table.Column<string>(type: "TEXT", nullable: true),
                    Height = table.Column<string>(type: "TEXT", nullable: true),
                    Weight = table.Column<string>(type: "TEXT", nullable: true),
                    Time = table.Column<string>(type: "TEXT", nullable: true),
                    Bill = table.Column<float>(type: "REAL", nullable: false),
                    PublicId = table.Column<string>(type: "TEXT", nullable: true),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Triage_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Billing_AppUserId",
                table: "Billing",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Medication_AppUserId",
                table: "Medication",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Test_AppUserId",
                table: "Test",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Triage_AppUserId",
                table: "Triage",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Billing");

            migrationBuilder.DropTable(
                name: "Medication");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "Triage");

            migrationBuilder.RenameColumn(
                name: "Ssn",
                table: "Users",
                newName: "SSN");

            migrationBuilder.AlterColumn<int>(
                name: "SSN",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SystemId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
