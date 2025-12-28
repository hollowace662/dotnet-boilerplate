using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace garantisa.Migrations
{
    /// <inheritdoc />
    public partial class UserConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersRoles_Users_UserId",
                table: "UsersRoles"
            );

            migrationBuilder.DropPrimaryKey(name: "PK_Users", table: "Users");

            migrationBuilder.RenameTable(name: "Users", newName: "User");

            migrationBuilder.AddPrimaryKey(name: "PK_User", table: "User", column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true
            );

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRoles_User_UserId",
                table: "UsersRoles",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_UsersRoles_User_UserId", table: "UsersRoles");

            migrationBuilder.DropPrimaryKey(name: "PK_User", table: "User");

            migrationBuilder.DropIndex(name: "IX_User_Email", table: "User");

            migrationBuilder.DropIndex(name: "IX_User_Username", table: "User");

            migrationBuilder.RenameTable(name: "User", newName: "Users");

            migrationBuilder.AddPrimaryKey(name: "PK_Users", table: "Users", column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRoles_Users_UserId",
                table: "UsersRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
