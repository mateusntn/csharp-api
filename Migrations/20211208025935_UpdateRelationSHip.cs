using Microsoft.EntityFrameworkCore.Migrations;

namespace MyApi.Migrations
{
    public partial class UpdateRelationSHip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "Questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LessonId1",
                table: "Questions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "Alternatives",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId1",
                table: "Alternatives",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_LessonId1",
                table: "Questions",
                column: "LessonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Alternatives_QuestionId1",
                table: "Alternatives",
                column: "QuestionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Alternatives_Questions_QuestionId1",
                table: "Alternatives",
                column: "QuestionId1",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Lessons_LessonId1",
                table: "Questions",
                column: "LessonId1",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alternatives_Questions_QuestionId1",
                table: "Alternatives");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Lessons_LessonId1",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_LessonId1",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Alternatives_QuestionId1",
                table: "Alternatives");

            migrationBuilder.DropColumn(
                name: "LessonId1",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionId1",
                table: "Alternatives");

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "Questions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "Alternatives",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
