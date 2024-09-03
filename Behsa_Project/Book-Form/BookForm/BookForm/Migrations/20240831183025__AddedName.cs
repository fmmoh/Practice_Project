using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookForm.Migrations
{
    /// <inheritdoc />
    public partial class _AddedName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookWriter_BookDb_BooksId",
                table: "BookWriter");

            migrationBuilder.DropForeignKey(
                name: "FK_BookWriter_WriterDb_WritersId",
                table: "BookWriter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WriterDb",
                table: "WriterDb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookDb",
                table: "BookDb");

            migrationBuilder.RenameTable(
                name: "WriterDb",
                newName: "Writer");

            migrationBuilder.RenameTable(
                name: "BookDb",
                newName: "Book");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Writer",
                table: "Writer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book",
                table: "Book",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookWriter_Book_BooksId",
                table: "BookWriter",
                column: "BooksId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookWriter_Writer_WritersId",
                table: "BookWriter",
                column: "WritersId",
                principalTable: "Writer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookWriter_Book_BooksId",
                table: "BookWriter");

            migrationBuilder.DropForeignKey(
                name: "FK_BookWriter_Writer_WritersId",
                table: "BookWriter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Writer",
                table: "Writer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book",
                table: "Book");

            migrationBuilder.RenameTable(
                name: "Writer",
                newName: "WriterDb");

            migrationBuilder.RenameTable(
                name: "Book",
                newName: "BookDb");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WriterDb",
                table: "WriterDb",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookDb",
                table: "BookDb",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookWriter_BookDb_BooksId",
                table: "BookWriter",
                column: "BooksId",
                principalTable: "BookDb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookWriter_WriterDb_WritersId",
                table: "BookWriter",
                column: "WritersId",
                principalTable: "WriterDb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
