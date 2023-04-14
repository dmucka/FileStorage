using Microsoft.EntityFrameworkCore.Migrations;

namespace FileStorageDAL.Migrations
{
    public partial class NewestVersionFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileVersions_VersionedFiles_VersionedFileId",
                table: "FileVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_VersionedFiles_Folders_FolderId",
                table: "VersionedFiles");

            migrationBuilder.DropIndex(
                name: "IX_VersionedFiles_NewestVersionId",
                table: "VersionedFiles");

            migrationBuilder.AlterColumn<int>(
                name: "NewestVersionId",
                table: "VersionedFiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FolderId",
                table: "VersionedFiles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VersionedFileId",
                table: "FileVersions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAEAACcQAAAAEMsRjIdIZPytpCnNfwWRPXgftI+mEvPoXfvLnAJ3gFwfoM2UJbhIfyM5EDjJ8H0SyQ==");

            migrationBuilder.CreateIndex(
                name: "IX_VersionedFiles_NewestVersionId",
                table: "VersionedFiles",
                column: "NewestVersionId",
                unique: true,
                filter: "[NewestVersionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FileVersions_VersionedFiles_VersionedFileId",
                table: "FileVersions",
                column: "VersionedFileId",
                principalTable: "VersionedFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VersionedFiles_Folders_FolderId",
                table: "VersionedFiles",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileVersions_VersionedFiles_VersionedFileId",
                table: "FileVersions");

            migrationBuilder.DropForeignKey(
                name: "FK_VersionedFiles_Folders_FolderId",
                table: "VersionedFiles");

            migrationBuilder.DropIndex(
                name: "IX_VersionedFiles_NewestVersionId",
                table: "VersionedFiles");

            migrationBuilder.AlterColumn<int>(
                name: "NewestVersionId",
                table: "VersionedFiles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FolderId",
                table: "VersionedFiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "VersionedFileId",
                table: "FileVersions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAEAACcQAAAAEM0t7smk9a/uGgbeTNKL89K3RONbGbJ+7SdsluPZ/8GKSatiXY3ua9IeHBHuc2A6gg==");

            migrationBuilder.CreateIndex(
                name: "IX_VersionedFiles_NewestVersionId",
                table: "VersionedFiles",
                column: "NewestVersionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileVersions_VersionedFiles_VersionedFileId",
                table: "FileVersions",
                column: "VersionedFileId",
                principalTable: "VersionedFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VersionedFiles_Folders_FolderId",
                table: "VersionedFiles",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
