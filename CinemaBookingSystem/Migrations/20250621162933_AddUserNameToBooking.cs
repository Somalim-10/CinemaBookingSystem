using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddUserNameToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Bookings",
                newName: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Bookings",
                newName: "UserId");
        }
    }
}
