using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace btk_exam_project_api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sube",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    UNVAN = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    TEL = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    EPOSTA = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    SUBE_NUMBER = table.Column<int>(type: "int", nullable: false),
                    IS_CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    IS_MODIFIED_DATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sube__3214EC279D75311D", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "deneme_sinav",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    SUBEID = table.Column<int>(type: "int", nullable: false),
                    DortBirRule = table.Column<bool>(type: "bit", nullable: false),
                    DENEME_ADI = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    SINAV_KATEGORI = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    YAYIN_ADI = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    YAYIN_LOGO = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    UCRET = table.Column<double>(type: "float", nullable: false),
                    sinavYeri = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    KITAPCIK_TOPLAM = table.Column<int>(type: "int", nullable: true),
                    KITAPCIK_ADET_MALIYET = table.Column<double>(type: "float", nullable: true),
                    IS_CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    IS_MODIFIED_DATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    IS_CREATED_USERID = table.Column<int>(type: "int", nullable: false),
                    IS_MODIFIED_USERID = table.Column<int>(type: "int", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__deneme_s__3214EC271477057A", x => x.ID);
                    table.ForeignKey(
                        name: "FK_deneme_sinav_sube",
                        column: x => x.SUBEID,
                        principalTable: "sube",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    subeID = table.Column<int>(type: "int", nullable: false),
                    Ders_Ad = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    bilgi = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    isCreatedUserID = table.Column<int>(type: "int", nullable: false),
                    isCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    isModifiedUserID = table.Column<int>(type: "int", nullable: false),
                    isModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ders__3214EC27B52AE52C", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ders_sube",
                        column: x => x.subeID,
                        principalTable: "sube",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "kullanicilar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    SUBE_ID = table.Column<int>(type: "int", nullable: false),
                    AD = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    SOYAD = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    KULLANICI_ADI = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    SIFRE = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    TEL = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: true),
                    EPOSTA = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: true),
                    ROLE = table.Column<int>(type: "int", nullable: false),
                    IS_CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    IS_MODIFIED_DATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IS_CREATED_USERID = table.Column<int>(type: "int", nullable: false),
                    IS_MODIFIED_USERID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__kullanic__3214EC2710BFF59C", x => x.ID);
                    table.ForeignKey(
                        name: "FK_kullanicilar_sube",
                        column: x => x.SUBE_ID,
                        principalTable: "sube",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "deneme_sinavi_oturum",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    DENEME_SINAV_ID = table.Column<int>(type: "int", nullable: false),
                    OTURUM_NO = table.Column<int>(type: "int", nullable: false),
                    TARIH = table.Column<DateTime>(type: "datetime", nullable: false),
                    BILGI = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    KONTENJAN = table.Column<int>(type: "int", nullable: false),
                    IS_CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    IS_MODIFIED_DATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    IS_CREATED_USERID = table.Column<int>(type: "int", nullable: false),
                    IS_MODIFIED_USERID = table.Column<int>(type: "int", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__deneme_s__3214EC273D333995", x => x.ID);
                    table.ForeignKey(
                        name: "FK_deneme_sinavi_oturum_deneme_sinav",
                        column: x => x.DENEME_SINAV_ID,
                        principalTable: "deneme_sinav",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "action_logs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    baslik = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    aciklama = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    IS_CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    udi = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    action_uid = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__action_l__3214EC2710CEDEFD", x => x.ID);
                    table.ForeignKey(
                        name: "FK_action_logs_kullanicilar",
                        column: x => x.userID,
                        principalTable: "kullanicilar",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ders_oturum_set",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    DERSID = table.Column<int>(type: "int", nullable: false),
                    TEACHERID = table.Column<int>(type: "int", nullable: false),
                    tarih = table.Column<DateTime>(type: "datetime", nullable: false),
                    BASLANGIC = table.Column<DateTime>(type: "datetime", nullable: false),
                    BITIS = table.Column<DateTime>(type: "datetime", nullable: false),
                    isCreatedUserID = table.Column<int>(type: "int", nullable: false),
                    isCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    isModifiedUserID = table.Column<int>(type: "int", nullable: false),
                    isModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ders_otu__3214EC27E82E9DE0", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ders_oturum_set_ders",
                        column: x => x.DERSID,
                        principalTable: "ders",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ders_oturum_set_kullanicilar",
                        column: x => x.TEACHERID,
                        principalTable: "kullanicilar",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "kullanici_mesaj_log",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    mesaj = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    IS_CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    IS_MODIFIED_DATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    IS_CREATED_USERID = table.Column<int>(type: "int", nullable: false),
                    IS_MODIFIED_USERID = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    mesaj_tarih = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__kullanic__3214EC27443B29B5", x => x.ID);
                    table.ForeignKey(
                        name: "FK_kullanici_mesaj_log_kullanicilar",
                        column: x => x.userID,
                        principalTable: "kullanicilar",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "student_other_info",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    baslik = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    link = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    username = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    isCreatedUserID = table.Column<int>(type: "int", nullable: false),
                    isModifiedUserID = table.Column<int>(type: "int", nullable: false),
                    isCreateeDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    isModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__student___3214EC27CEF8019A", x => x.ID);
                    table.ForeignKey(
                        name: "FK_student_other_info_kullanicilar",
                        column: x => x.userID,
                        principalTable: "kullanicilar",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "teacher_hafta_gun_set",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    TEACHERID = table.Column<int>(type: "int", nullable: false),
                    GUN = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__teacher___3214EC27336E5A27", x => x.ID);
                    table.ForeignKey(
                        name: "FK_teacher_hafta_gun_set_kullanicilar",
                        column: x => x.TEACHERID,
                        principalTable: "kullanicilar",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "user_ders_set",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    DERSID = table.Column<int>(type: "int", nullable: false),
                    USERID = table.Column<int>(type: "int", nullable: false),
                    isCreatedUserID = table.Column<int>(type: "int", nullable: false),
                    isCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    isModifiedUserID = table.Column<int>(type: "int", nullable: false),
                    isModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_der__3214EC278BBE2E21", x => x.ID);
                    table.ForeignKey(
                        name: "FK_user_ders_set_ders",
                        column: x => x.DERSID,
                        principalTable: "ders",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_user_ders_set_kullanicilar",
                        column: x => x.USERID,
                        principalTable: "kullanicilar",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "user_oturum_set",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: false),
                    Dogru = table.Column<double>(type: "float", nullable: true),
                    Yanlis = table.Column<double>(type: "float", nullable: true),
                    Net = table.Column<double>(type: "float", nullable: true),
                    OTURUM_ID = table.Column<int>(type: "int", nullable: false),
                    USER_ID = table.Column<int>(type: "int", nullable: false),
                    STATUS = table.Column<int>(type: "int", nullable: false),
                    IS_CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    IS_MODIFIED_DATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    IS_CREATED_USERID = table.Column<int>(type: "int", nullable: false),
                    IS_MODIFIED_USERID = table.Column<int>(type: "int", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_otu__3214EC27D9DC9FDD", x => x.ID);
                    table.ForeignKey(
                        name: "FK_user_oturum_set_deneme_sinavi_oturum",
                        column: x => x.OTURUM_ID,
                        principalTable: "deneme_sinavi_oturum",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_user_oturum_set_kullanicilar",
                        column: x => x.USER_ID,
                        principalTable: "kullanicilar",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ders_oturum_user_set",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    OTURUM_ID = table.Column<int>(type: "int", nullable: false),
                    STUDENT_ID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Bilgi = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    isCreatedUserID = table.Column<int>(type: "int", nullable: false),
                    isCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    isModifiedUserID = table.Column<int>(type: "int", nullable: false),
                    isModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ders_otu__3214EC27A1D3A1F8", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ders_oturum_user_set_ders_oturum_set",
                        column: x => x.OTURUM_ID,
                        principalTable: "ders_oturum_set",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ders_oturum_user_set_kullanicilar",
                        column: x => x.STUDENT_ID,
                        principalTable: "kullanicilar",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_action_logs_userID",
                table: "action_logs",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_deneme_sinav_SUBEID",
                table: "deneme_sinav",
                column: "SUBEID");

            migrationBuilder.CreateIndex(
                name: "IX_deneme_sinavi_oturum_DENEME_SINAV_ID",
                table: "deneme_sinavi_oturum",
                column: "DENEME_SINAV_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ders_subeID",
                table: "ders",
                column: "subeID");

            migrationBuilder.CreateIndex(
                name: "IX_ders_oturum_set_DERSID",
                table: "ders_oturum_set",
                column: "DERSID");

            migrationBuilder.CreateIndex(
                name: "IX_ders_oturum_set_TEACHERID",
                table: "ders_oturum_set",
                column: "TEACHERID");

            migrationBuilder.CreateIndex(
                name: "IX_ders_oturum_user_set_OTURUM_ID",
                table: "ders_oturum_user_set",
                column: "OTURUM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ders_oturum_user_set_STUDENT_ID",
                table: "ders_oturum_user_set",
                column: "STUDENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_kullanici_mesaj_log_userID",
                table: "kullanici_mesaj_log",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_kullanicilar_SUBE_ID",
                table: "kullanicilar",
                column: "SUBE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_student_other_info_userID",
                table: "student_other_info",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_teacher_hafta_gun_set_TEACHERID",
                table: "teacher_hafta_gun_set",
                column: "TEACHERID");

            migrationBuilder.CreateIndex(
                name: "IX_user_ders_set_DERSID",
                table: "user_ders_set",
                column: "DERSID");

            migrationBuilder.CreateIndex(
                name: "IX_user_ders_set_USERID",
                table: "user_ders_set",
                column: "USERID");

            migrationBuilder.CreateIndex(
                name: "IX_user_oturum_set_OTURUM_ID",
                table: "user_oturum_set",
                column: "OTURUM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_user_oturum_set_USER_ID",
                table: "user_oturum_set",
                column: "USER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "action_logs");

            migrationBuilder.DropTable(
                name: "ders_oturum_user_set");

            migrationBuilder.DropTable(
                name: "kullanici_mesaj_log");

            migrationBuilder.DropTable(
                name: "student_other_info");

            migrationBuilder.DropTable(
                name: "teacher_hafta_gun_set");

            migrationBuilder.DropTable(
                name: "user_ders_set");

            migrationBuilder.DropTable(
                name: "user_oturum_set");

            migrationBuilder.DropTable(
                name: "ders_oturum_set");

            migrationBuilder.DropTable(
                name: "deneme_sinavi_oturum");

            migrationBuilder.DropTable(
                name: "ders");

            migrationBuilder.DropTable(
                name: "kullanicilar");

            migrationBuilder.DropTable(
                name: "deneme_sinav");

            migrationBuilder.DropTable(
                name: "sube");
        }
    }
}
