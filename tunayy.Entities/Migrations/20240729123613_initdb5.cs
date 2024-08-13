using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tunayy.Migrations
{
    public partial class initdb5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_ApplicationUserId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Settings",
                newName: "SiteAdi");

            migrationBuilder.RenameColumn(
                name: "ThumbnailUrl",
                table: "Settings",
                newName: "Resim");

            migrationBuilder.RenameColumn(
                name: "SiteName",
                table: "Settings",
                newName: "KisaAciklama");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Settings",
                newName: "Baslik");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Posts",
                newName: "Resim");

            migrationBuilder.RenameColumn(
                name: "ThumbnailUrl",
                table: "Posts",
                newName: "KisaAciklama");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Posts",
                newName: "Baslik");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Posts",
                newName: "Aciklama");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Posts",
                newName: "KullaniciId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_ApplicationUserId",
                table: "Posts",
                newName: "IX_Posts_KullaniciId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Pages",
                newName: "Resim");

            migrationBuilder.RenameColumn(
                name: "ThumbnailUrl",
                table: "Pages",
                newName: "KisaAciklama");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Pages",
                newName: "Baslik");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Pages",
                newName: "Aciklama");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "Soyadi");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "Adi");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_KullaniciId",
                table: "Posts",
                column: "KullaniciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_KullaniciId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "SiteAdi",
                table: "Settings",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Resim",
                table: "Settings",
                newName: "ThumbnailUrl");

            migrationBuilder.RenameColumn(
                name: "KisaAciklama",
                table: "Settings",
                newName: "SiteName");

            migrationBuilder.RenameColumn(
                name: "Baslik",
                table: "Settings",
                newName: "ShortDescription");

            migrationBuilder.RenameColumn(
                name: "Resim",
                table: "Posts",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "KullaniciId",
                table: "Posts",
                newName: "ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "KisaAciklama",
                table: "Posts",
                newName: "ThumbnailUrl");

            migrationBuilder.RenameColumn(
                name: "Baslik",
                table: "Posts",
                newName: "ShortDescription");

            migrationBuilder.RenameColumn(
                name: "Aciklama",
                table: "Posts",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_KullaniciId",
                table: "Posts",
                newName: "IX_Posts_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "Resim",
                table: "Pages",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "KisaAciklama",
                table: "Pages",
                newName: "ThumbnailUrl");

            migrationBuilder.RenameColumn(
                name: "Baslik",
                table: "Pages",
                newName: "ShortDescription");

            migrationBuilder.RenameColumn(
                name: "Aciklama",
                table: "Pages",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Soyadi",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Adi",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_ApplicationUserId",
                table: "Posts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
