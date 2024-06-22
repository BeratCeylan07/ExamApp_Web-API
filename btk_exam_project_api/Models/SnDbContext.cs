using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace btk_exam_project_api.Models;

public partial class SnDbContext : DbContext
{
    public SnDbContext()
    {
    }

    public SnDbContext(DbContextOptions<SnDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActionLog> ActionLogs { get; set; }

    public virtual DbSet<DenemeSinav> DenemeSinavs { get; set; }

    public virtual DbSet<DenemeSinaviOturum> DenemeSinaviOturums { get; set; }

    public virtual DbSet<Der> Ders { get; set; }

    public virtual DbSet<DersOturumSet> DersOturumSets { get; set; }

    public virtual DbSet<DersOturumUserSet> DersOturumUserSets { get; set; }

    public virtual DbSet<KullaniciMesajLog> KullaniciMesajLogs { get; set; }

    public virtual DbSet<Kullanicilar> Kullanicilars { get; set; }

    public virtual DbSet<StudentOtherInfo> StudentOtherInfos { get; set; }

    public virtual DbSet<Sube> Subes { get; set; }

    public virtual DbSet<TeacherHaftaGunSet> TeacherHaftaGunSets { get; set; }

    public virtual DbSet<UserDersSet> UserDersSets { get; set; }

    public virtual DbSet<UserOturumSet> UserOturumSets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=sn_DB;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_turkish_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<ActionLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("action_logs");

            entity.HasIndex(e => e.SubeId, "subeID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Aciklama)
                .HasMaxLength(150)
                .HasColumnName("aciklama");
            entity.Property(e => e.ActionUid)
                .HasMaxLength(150)
                .HasColumnName("action_uid");
            entity.Property(e => e.Baslik)
                .HasMaxLength(150)
                .HasColumnName("baslik");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_CREATED_DATE");
            entity.Property(e => e.SubeId)
                .HasColumnType("int(11)")
                .HasColumnName("subeID");
            entity.Property(e => e.Udi)
                .HasMaxLength(150)
                .HasColumnName("udi");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userID");
        });

        modelBuilder.Entity<DenemeSinav>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("deneme_sinav");

            entity.HasIndex(e => e.IsCreatedUserid, "INDEX_CREATED_USERID");

            entity.HasIndex(e => e.IsModifiedUserid, "INDEX_MODIFIED_USERID");

            entity.HasIndex(e => e.Subeid, "INDEX_SUBE");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.DenemeAdi)
                .HasMaxLength(200)
                .HasColumnName("DENEME_ADI");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_CREATED_DATE");
            entity.Property(e => e.IsCreatedUserid)
                .HasColumnType("int(11)")
                .HasColumnName("IS_CREATED_USERID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_MODIFIED_DATE");
            entity.Property(e => e.IsModifiedUserid)
                .HasColumnType("int(11)")
                .HasColumnName("IS_MODIFIED_USERID");
            entity.Property(e => e.KitapcikAdetMaliyet).HasColumnName("KITAPCIK_ADET_MALIYET");
            entity.Property(e => e.KitapcikToplam)
                .HasColumnType("int(11)")
                .HasColumnName("KITAPCIK_TOPLAM");
            entity.Property(e => e.SinavKategori)
                .HasMaxLength(70)
                .HasColumnName("SINAV_KATEGORI");
            entity.Property(e => e.SinavYeri)
                .HasMaxLength(150)
                .HasColumnName("sinavYeri");
            entity.Property(e => e.Subeid)
                .HasColumnType("int(11)")
                .HasColumnName("SUBEID");
            entity.Property(e => e.Ucret).HasColumnName("UCRET");
            entity.Property(e => e.Uid)
                .HasMaxLength(70)
                .HasColumnName("UID");
            entity.Property(e => e.YayinAdi)
                .HasMaxLength(150)
                .HasColumnName("YAYIN_ADI");
            entity.Property(e => e.YayinLogo)
                .HasColumnType("text")
                .HasColumnName("YAYIN_LOGO");

            entity.HasOne(d => d.Sube).WithMany(p => p.DenemeSinavs)
                .HasForeignKey(d => d.Subeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deneme_sinav_ibfk_1");
        });

        modelBuilder.Entity<DenemeSinaviOturum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("deneme_sinavi_oturum");

            entity.HasIndex(e => e.IsCreatedUserid, "INDEX_CREATED_USERID");

            entity.HasIndex(e => e.DenemeSinavId, "INDEX_DENEME_SINAVI");

            entity.HasIndex(e => e.IsModifiedUserid, "INDEX_MODIFIED_USERID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Bilgi)
                .HasMaxLength(250)
                .HasColumnName("BILGI");
            entity.Property(e => e.DenemeSinavId)
                .HasColumnType("int(11)")
                .HasColumnName("DENEME_SINAV_ID");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_CREATED_DATE");
            entity.Property(e => e.IsCreatedUserid)
                .HasColumnType("int(11)")
                .HasColumnName("IS_CREATED_USERID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_MODIFIED_DATE");
            entity.Property(e => e.IsModifiedUserid)
                .HasColumnType("int(11)")
                .HasColumnName("IS_MODIFIED_USERID");
            entity.Property(e => e.Kontenjan)
                .HasColumnType("int(11)")
                .HasColumnName("KONTENJAN");
            entity.Property(e => e.OturumNo)
                .HasColumnType("int(11)")
                .HasColumnName("OTURUM_NO");
            entity.Property(e => e.Tarih)
                .HasColumnType("datetime")
                .HasColumnName("TARIH");
            entity.Property(e => e.Uid)
                .HasMaxLength(70)
                .HasColumnName("UID");

            entity.HasOne(d => d.DenemeSinav).WithMany(p => p.DenemeSinaviOturums)
                .HasForeignKey(d => d.DenemeSinavId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deneme_sinavi_oturum_ibfk_1");
        });

        modelBuilder.Entity<Der>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ders");

            entity.HasIndex(e => e.SubeId, "subeID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Bilgi)
                .HasMaxLength(150)
                .HasColumnName("bilgi");
            entity.Property(e => e.DersAd)
                .HasMaxLength(150)
                .HasColumnName("Ders_Ad");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("isCreatedDate");
            entity.Property(e => e.IsCreatedUserId)
                .HasColumnType("int(11)")
                .HasColumnName("isCreatedUserID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("isModifiedDate");
            entity.Property(e => e.IsModifiedUserId)
                .HasColumnType("int(11)")
                .HasColumnName("isModifiedUserID");
            entity.Property(e => e.SubeId)
                .HasColumnType("int(11)")
                .HasColumnName("subeID");
            entity.Property(e => e.Uid)
                .HasMaxLength(100)
                .HasColumnName("UID");

            entity.HasOne(d => d.Sube).WithMany(p => p.Ders)
                .HasForeignKey(d => d.SubeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dersler_ibfk_1");
        });

        modelBuilder.Entity<DersOturumSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ders_oturum_set");

            entity.HasIndex(e => e.Dersid, "DERSID");

            entity.HasIndex(e => e.Teacherid, "TEACHERID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Baslangic)
                .HasColumnType("datetime")
                .HasColumnName("BASLANGIC");
            entity.Property(e => e.Bitis)
                .HasColumnType("datetime")
                .HasColumnName("BITIS");
            entity.Property(e => e.Dersid)
                .HasColumnType("int(11)")
                .HasColumnName("DERSID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("isCreatedDate");
            entity.Property(e => e.IsCreatedUserId)
                .HasColumnType("int(11)")
                .HasColumnName("isCreatedUserID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("isModifiedDate");
            entity.Property(e => e.IsModifiedUserId)
                .HasColumnType("int(11)")
                .HasColumnName("isModifiedUserID");
            entity.Property(e => e.Tarih)
                .HasColumnType("datetime")
                .HasColumnName("tarih");
            entity.Property(e => e.Teacherid)
                .HasColumnType("int(11)")
                .HasColumnName("TEACHERID");
            entity.Property(e => e.Uid)
                .HasMaxLength(100)
                .HasColumnName("UID");

            entity.HasOne(d => d.Ders).WithMany(p => p.DersOturumSets)
                .HasForeignKey(d => d.Dersid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ders_oturum_set_ibfk_1");

            entity.HasOne(d => d.Teacher).WithMany(p => p.DersOturumSets)
                .HasForeignKey(d => d.Teacherid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ders_oturum_set_ibfk_2");
        });

        modelBuilder.Entity<DersOturumUserSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ders_oturum_user_set");

            entity.HasIndex(e => e.OturumId, "OTURUM_ID");

            entity.HasIndex(e => e.StudentId, "STUDENT_ID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Bilgi).HasColumnType("text");
            entity.Property(e => e.IsActive)
                .HasColumnType("int(11)")
                .HasColumnName("isActive");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("isCreatedDate");
            entity.Property(e => e.IsCreatedUserId)
                .HasColumnType("int(11)")
                .HasColumnName("isCreatedUserID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("isModifiedDate");
            entity.Property(e => e.IsModifiedUserId)
                .HasColumnType("int(11)")
                .HasColumnName("isModifiedUserID");
            entity.Property(e => e.OturumId)
                .HasColumnType("int(11)")
                .HasColumnName("OTURUM_ID");
            entity.Property(e => e.StudentId)
                .HasColumnType("int(11)")
                .HasColumnName("STUDENT_ID");
            entity.Property(e => e.Uid)
                .HasMaxLength(100)
                .HasColumnName("UID");

            entity.HasOne(d => d.Oturum).WithMany(p => p.DersOturumUserSets)
                .HasForeignKey(d => d.OturumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ders_oturum_user_set_ibfk_1");

            entity.HasOne(d => d.Student).WithMany(p => p.DersOturumUserSets)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ders_oturum_user_set_ibfk_2");
        });

        modelBuilder.Entity<KullaniciMesajLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("kullanici_mesaj_log");

            entity.HasIndex(e => e.SubeId, "sube_user_message_logs");

            entity.HasIndex(e => e.UserId, "userID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_CREATED_DATE");
            entity.Property(e => e.IsCreatedUserid)
                .HasColumnType("int(11)")
                .HasColumnName("IS_CREATED_USERID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_MODIFIED_DATE");
            entity.Property(e => e.IsModifiedUserid)
                .HasColumnType("int(11)")
                .HasColumnName("IS_MODIFIED_USERID");
            entity.Property(e => e.Mesaj)
                .HasColumnType("text")
                .HasColumnName("mesaj");
            entity.Property(e => e.MesajTarih)
                .HasColumnType("datetime")
                .HasColumnName("mesaj_tarih");
            entity.Property(e => e.SubeId)
                .HasColumnType("int(11)")
                .HasColumnName("subeID");
            entity.Property(e => e.Uid)
                .HasMaxLength(150)
                .HasColumnName("UID");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userID");

            entity.HasOne(d => d.Sube).WithMany(p => p.KullaniciMesajLogs)
                .HasForeignKey(d => d.SubeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kullanici_mesaj_log_ibfk_1");

            entity.HasOne(d => d.User).WithMany(p => p.KullaniciMesajLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kullanici_mesaj_log_ibfk_2");
        });

        modelBuilder.Entity<Kullanicilar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("kullanicilar");

            entity.HasIndex(e => e.IsCreatedUserid, "CREATED_USER_INDEX");

            entity.HasIndex(e => e.SubeId, "INDEX_SUBE");

            entity.HasIndex(e => e.IsModifiedUserid, "MODIFIED_USER_INDEX");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Ad)
                .HasMaxLength(70)
                .HasColumnName("AD");
            entity.Property(e => e.Eposta)
                .HasMaxLength(70)
                .HasColumnName("EPOSTA");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_CREATED_DATE");
            entity.Property(e => e.IsCreatedUserid)
                .HasColumnType("int(11)")
                .HasColumnName("IS_CREATED_USERID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_MODIFIED_DATE");
            entity.Property(e => e.IsModifiedUserid)
                .HasColumnType("int(11)")
                .HasColumnName("IS_MODIFIED_USERID");
            entity.Property(e => e.KullaniciAdi)
                .HasMaxLength(70)
                .HasColumnName("KULLANICI_ADI");
            entity.Property(e => e.Role)
                .HasColumnType("int(11)")
                .HasColumnName("ROLE");
            entity.Property(e => e.Sifre)
                .HasMaxLength(70)
                .HasColumnName("SIFRE");
            entity.Property(e => e.Soyad)
                .HasMaxLength(70)
                .HasColumnName("SOYAD");
            entity.Property(e => e.SubeId)
                .HasColumnType("int(11)")
                .HasColumnName("SUBE_ID");
            entity.Property(e => e.Tel)
                .HasMaxLength(70)
                .HasColumnName("TEL");
            entity.Property(e => e.Uid)
                .HasMaxLength(70)
                .HasColumnName("UID");

            entity.HasOne(d => d.Sube).WithMany(p => p.Kullanicilars)
                .HasForeignKey(d => d.SubeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kullanicilar_ibfk_1");
        });

        modelBuilder.Entity<StudentOtherInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("student_other_info");

            entity.HasIndex(e => e.UserId, "userID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Baslik)
                .HasMaxLength(70)
                .HasColumnName("baslik");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsCreatedUserId)
                .HasColumnType("int(11)")
                .HasColumnName("isCreatedUserID");
            entity.Property(e => e.IsCreateeDate)
                .HasColumnType("datetime")
                .HasColumnName("isCreateeDate");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("isModifiedDate");
            entity.Property(e => e.IsModifiedUserId)
                .HasColumnType("int(11)")
                .HasColumnName("isModifiedUserID");
            entity.Property(e => e.Link)
                .HasMaxLength(150)
                .HasColumnName("link");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Uid)
                .HasMaxLength(150)
                .HasColumnName("UID");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userID");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");

            entity.HasOne(d => d.User).WithMany(p => p.StudentOtherInfos)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_erisim_bilgiler_ibfk_1");
        });

        modelBuilder.Entity<Sube>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sube");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Eposta)
                .HasMaxLength(100)
                .HasColumnName("EPOSTA");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_CREATED_DATE");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_MODIFIED_DATE");
            entity.Property(e => e.SubeNumber)
                .HasColumnType("int(11)")
                .HasColumnName("SUBE_NUMBER");
            entity.Property(e => e.Tel)
                .HasMaxLength(70)
                .HasColumnName("TEL");
            entity.Property(e => e.Uid)
                .HasMaxLength(70)
                .HasColumnName("UID");
            entity.Property(e => e.Unvan)
                .HasMaxLength(200)
                .HasColumnName("UNVAN");
        });

        modelBuilder.Entity<TeacherHaftaGunSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("teacher_hafta_gun_set");

            entity.HasIndex(e => e.Teacherid, "TEACHERID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Gun)
                .HasMaxLength(50)
                .HasColumnName("GUN");
            entity.Property(e => e.Teacherid)
                .HasColumnType("int(11)")
                .HasColumnName("TEACHERID");
            entity.Property(e => e.Uid)
                .HasMaxLength(150)
                .HasColumnName("UID");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherHaftaGunSets)
                .HasForeignKey(d => d.Teacherid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("teacher_hafta_gun_set_ibfk_1");
        });

        modelBuilder.Entity<UserDersSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_ders_set");

            entity.HasIndex(e => e.Dersid, "DERSID");

            entity.HasIndex(e => e.Userid, "USERID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Dersid)
                .HasColumnType("int(11)")
                .HasColumnName("DERSID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("isCreatedDate");
            entity.Property(e => e.IsCreatedUserId)
                .HasColumnType("int(11)")
                .HasColumnName("isCreatedUserID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("isModifiedDate");
            entity.Property(e => e.IsModifiedUserId)
                .HasColumnType("int(11)")
                .HasColumnName("isModifiedUserID");
            entity.Property(e => e.Uid)
                .HasMaxLength(100)
                .HasColumnName("UID");
            entity.Property(e => e.Userid)
                .HasColumnType("int(11)")
                .HasColumnName("USERID");

            entity.HasOne(d => d.Ders).WithMany(p => p.UserDersSets)
                .HasForeignKey(d => d.Dersid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_ders_set_ibfk_1");

            entity.HasOne(d => d.User).WithMany(p => p.UserDersSets)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_ders_set_ibfk_2");
        });

        modelBuilder.Entity<UserOturumSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_oturum_set");

            entity.HasIndex(e => e.IsCreatedUserid, "INDEX_CREATED_USERID");

            entity.HasIndex(e => e.IsModifiedUserid, "INDEX_MODIFIED_USERID");

            entity.HasIndex(e => e.OturumId, "INDEX_OTURUMID");

            entity.HasIndex(e => e.UserId, "INDEX_USERID");

            entity.HasIndex(e => new { e.OturumId, e.UserId }, "OTURUM_ID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_CREATED_DATE");
            entity.Property(e => e.IsCreatedUserid)
                .HasColumnType("int(11)")
                .HasColumnName("IS_CREATED_USERID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_MODIFIED_DATE");
            entity.Property(e => e.IsModifiedUserid)
                .HasColumnType("int(11)")
                .HasColumnName("IS_MODIFIED_USERID");
            entity.Property(e => e.OturumId)
                .HasColumnType("int(11)")
                .HasColumnName("OTURUM_ID");
            entity.Property(e => e.Status)
                .HasColumnType("int(11)")
                .HasColumnName("STATUS");
            entity.Property(e => e.Uid)
                .HasMaxLength(70)
                .HasColumnName("UID");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Oturum).WithMany(p => p.UserOturumSets)
                .HasForeignKey(d => d.OturumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_oturum_set_ibfk_1");

            entity.HasOne(d => d.User).WithMany(p => p.UserOturumSets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_oturum_set_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
