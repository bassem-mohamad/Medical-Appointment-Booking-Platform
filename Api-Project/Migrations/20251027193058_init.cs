using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api_Project.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Area = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    ConsultationFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctors_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BookedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CancelledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancellationReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    SlotDurationMinutes = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSchedules_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsBooked = table.Column<bool>(type: "bit", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSlots_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Area", "City", "CreatedAt", "IsDeleted", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Nasr City", "Cairo", new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), false, null },
                    { 2, "Smouha", "Alexandria", new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), false, null },
                    { 3, "Dokki", "Giza", new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), false, null }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "FullName", "Gender", "IsDeleted", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1995, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Omar Ali", 1, false, null },
                    { 2, new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sara Mohamed", 2, false, null }
                });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), "Heart and blood vessel specialist", false, "Cardiology", null },
                    { 2, new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), "Skin, hair, and nail specialist", false, "Dermatology", null },
                    { 3, new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), "Brain and nervous system specialist", false, "Neurology", null },
                    { 4, new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), "Child healthcare specialist", false, "Pediatrics", null }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Bio", "ConsultationFee", "CreatedAt", "IsDeleted", "LocationId", "Name", "ProfileImage", "SpecialtyId", "UpdatedAt", "YearsOfExperience" },
                values: new object[,]
                {
                    { 1, "Specialized in heart surgery and cardiovascular diseases.", 400m, new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), false, 1, "Dr. Ahmed Hassan", "images/doctors/ahmed.jpg", 1, null, 12 },
                    { 2, "Expert in skin care and laser treatments.", 300m, new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), false, 3, "Dr. Mona Said", "images/doctors/mona.jpg", 2, null, 8 },
                    { 3, "Dedicated to child wellness and vaccination.", 250m, new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), false, 2, "Dr. Karim Nabil", "images/doctors/karim.jpg", 4, null, 6 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDate", "AppointmentTime", "BookedAt", "CancellationReason", "CancelledAt", "CreatedAt", "DoctorId", "Fee", "IsDeleted", "Notes", "PatientId", "Status", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 10, 0, 0, 0), new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, 400m, false, "Follow-up visit", 1, 2, null });

            migrationBuilder.InsertData(
                table: "DoctorSchedules",
                columns: new[] { "Id", "CreatedAt", "DayOfWeek", "DoctorId", "EndTime", "IsAvailable", "IsDeleted", "SlotDurationMinutes", "StartTime", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), 0, 1, new TimeSpan(0, 15, 0, 0, 0), true, false, 30, new TimeSpan(0, 9, 0, 0, 0), null },
                    { 2, new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, new TimeSpan(0, 16, 0, 0, 0), true, false, 30, new TimeSpan(0, 10, 0, 0, 0), null }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "AppointmentId", "Comment", "CreatedAt", "DoctorId", "IsDeleted", "PatientId", "Rating", "UpdatedAt" },
                values: new object[] { 1, 1, "Excellent doctor, very helpful!", new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, false, 1, 5, null });

            migrationBuilder.InsertData(
                table: "TimeSlots",
                columns: new[] { "Id", "AppointmentId", "CreatedAt", "Date", "DoctorId", "IsBooked", "IsDeleted", "Time", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, false, new TimeSpan(0, 10, 0, 0, 0), null },
                    { 2, null, new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, false, false, new TimeSpan(0, 11, 0, 0, 0), null },
                    { 3, null, new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, false, false, new TimeSpan(0, 12, 0, 0, 0), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_LocationId",
                table: "Doctors",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_DoctorId",
                table: "DoctorSchedules",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_DoctorId",
                table: "Reviews",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PatientId",
                table: "Reviews",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_DoctorId",
                table: "TimeSlots",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "DoctorSchedules");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Specialties");
        }
    }
}
