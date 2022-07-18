using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Label : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Label",
                columns: table => new
                {
                    LabelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelName = table.Column<string>(nullable: true),
                    Userid = table.Column<long>(nullable: false),
                    Noteid = table.Column<long>(nullable: false),
                    notesNoteID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => x.LabelId);
                    table.ForeignKey(
                        name: "FK_Label_UsersTable_Userid",
                        column: x => x.Userid,
                        principalTable: "UsersTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Label_Notes_notesNoteID",
                        column: x => x.notesNoteID,
                        principalTable: "Notes",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Label_Userid",
                table: "Label",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_Label_notesNoteID",
                table: "Label",
                column: "notesNoteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Label");
        }
    }
}
