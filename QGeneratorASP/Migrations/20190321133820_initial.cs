using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QGeneratorASP.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id_answer = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Object = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id_answer);
                });

            migrationBuilder.CreateTable(
                name: "Level_of_complexity",
                columns: table => new
                {
                    Id_level = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name_level = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level_of_complexity", x => x.Id_level);
                });

            migrationBuilder.CreateTable(
                name: "Type_of_question",
                columns: table => new
                {
                    Id_type = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type_of_question", x => x.Id_type);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id_user = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Password = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Access_level = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id_user);
                });

            migrationBuilder.CreateTable(
                name: "Quest",
                columns: table => new
                {
                    Id_quest = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(nullable: false),
                    Number_of_questions = table.Column<int>(nullable: false),
                    Thematics = table.Column<string>(type: "text", nullable: true),
                    Id_Level_FK = table.Column<int>(nullable: false),
                    Id_Autor_FK = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quest", x => x.Id_quest);
                    table.ForeignKey(
                        name: "FK_Quest_User",
                        column: x => x.Id_Autor_FK,
                        principalTable: "User",
                        principalColumn: "Id_user",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quest_Level_of_complexity",
                        column: x => x.Id_Level_FK,
                        principalTable: "Level_of_complexity",
                        principalColumn: "Id_level",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Riddle",
                columns: table => new
                {
                    Id_riddle = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Id_Autor_FK = table.Column<int>(nullable: false),
                    Id_Level_FK = table.Column<int>(nullable: false),
                    Id_Answer_FK = table.Column<int>(nullable: false),
                    Id_Type_FK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Riddle", x => x.Id_riddle);
                    table.ForeignKey(
                        name: "FK_Riddle_Answer",
                        column: x => x.Id_Answer_FK,
                        principalTable: "Answer",
                        principalColumn: "Id_answer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Riddle_User",
                        column: x => x.Id_Autor_FK,
                        principalTable: "User",
                        principalColumn: "Id_user",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Riddle_Level_of_complexity",
                        column: x => x.Id_Level_FK,
                        principalTable: "Level_of_complexity",
                        principalColumn: "Id_level",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Riddle_Type_of_question",
                        column: x => x.Id_Type_FK,
                        principalTable: "Type_of_question",
                        principalColumn: "Id_type",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quest_Riddle",
                columns: table => new
                {
                    Id_Quest_FK = table.Column<int>(nullable: false),
                    Id_Riddle_FK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quest_Riddle", x => new { x.Id_Quest_FK, x.Id_Riddle_FK });
                    table.ForeignKey(
                        name: "FK_Quest_Riddle_Quest",
                        column: x => x.Id_Quest_FK,
                        principalTable: "Quest",
                        principalColumn: "Id_quest",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quest_Riddle_Riddle",
                        column: x => x.Id_Riddle_FK,
                        principalTable: "Riddle",
                        principalColumn: "Id_riddle",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quest_Id_Autor_FK",
                table: "Quest",
                column: "Id_Autor_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Quest_Id_Level_FK",
                table: "Quest",
                column: "Id_Level_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Quest_Riddle_Id_Riddle_FK",
                table: "Quest_Riddle",
                column: "Id_Riddle_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Riddle_Id_Answer_FK",
                table: "Riddle",
                column: "Id_Answer_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Riddle_Id_Autor_FK",
                table: "Riddle",
                column: "Id_Autor_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Riddle_Id_Level_FK",
                table: "Riddle",
                column: "Id_Level_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Riddle_Id_Type_FK",
                table: "Riddle",
                column: "Id_Type_FK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quest_Riddle");

            migrationBuilder.DropTable(
                name: "Quest");

            migrationBuilder.DropTable(
                name: "Riddle");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Level_of_complexity");

            migrationBuilder.DropTable(
                name: "Type_of_question");
        }
    }
}
