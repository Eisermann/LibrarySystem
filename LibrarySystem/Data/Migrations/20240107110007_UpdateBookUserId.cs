using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLoan_AspNetUsers_UserId1",
                table: "BookLoan");

            migrationBuilder.DropIndex(
                name: "IX_BookLoan_UserId1",
                table: "BookLoan");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "BookLoan");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BookLoan",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_BookLoan_UserId",
                table: "BookLoan",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLoan_AspNetUsers_UserId",
                table: "BookLoan",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLoan_AspNetUsers_UserId",
                table: "BookLoan");

            migrationBuilder.DropIndex(
                name: "IX_BookLoan_UserId",
                table: "BookLoan");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BookLoan",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "BookLoan",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookLoan_UserId1",
                table: "BookLoan",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLoan_AspNetUsers_UserId1",
                table: "BookLoan",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
