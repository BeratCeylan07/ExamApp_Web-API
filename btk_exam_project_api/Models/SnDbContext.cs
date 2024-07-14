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
        => optionsBuilder.UseSqlServer("server=sql.bsite.net\\MSSQL2016;database=beratceylan0007_SampleDB;User Id=beratceylan0007_SampleDB;Password=960607Brt.;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActionLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__action_l__3214EC2710CEDEFD");

            entity.ToTable("action_logs");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Aciklama)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("aciklama");
            entity.Property(e => e.ActionUid)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("action_uid");
            entity.Property(e => e.Baslik)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("baslik");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_CREATED_DATE");
            entity.Property(e => e.Udi)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("udi");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.ActionLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_action_logs_kullanicilar");
        });

        modelBuilder.Entity<DenemeSinav>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__deneme_s__3214EC271477057A");

            entity.ToTable("deneme_sinav");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DenemeAdi)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DENEME_ADI");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_CREATED_DATE");
            entity.Property(e => e.IsCreatedUserid).HasColumnName("IS_CREATED_USERID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_MODIFIED_DATE");
            entity.Property(e => e.IsModifiedUserid).HasColumnName("IS_MODIFIED_USERID");
            entity.Property(e => e.KitapcikAdetMaliyet).HasColumnName("KITAPCIK_ADET_MALIYET");
            entity.Property(e => e.KitapcikToplam).HasColumnName("KITAPCIK_TOPLAM");
            entity.Property(e => e.SinavKategori)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("SINAV_KATEGORI");
            entity.Property(e => e.SinavYeri)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("sinavYeri");
            entity.Property(e => e.Subeid).HasColumnName("SUBEID");
            entity.Property(e => e.Ucret).HasColumnName("UCRET");
            entity.Property(e => e.Uid)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("UID");
            entity.Property(e => e.YayinAdi)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("YAYIN_ADI");
            entity.Property(e => e.YayinLogo)
                .IsUnicode(false)
                .HasColumnName("YAYIN_LOGO");

            entity.HasOne(d => d.Sube).WithMany(p => p.DenemeSinavs)
                .HasForeignKey(d => d.Subeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_deneme_sinav_sube");
        });

        modelBuilder.Entity<DenemeSinaviOturum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__deneme_s__3214EC273D333995");

            entity.ToTable("deneme_sinavi_oturum");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bilgi)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("BILGI");
            entity.Property(e => e.DenemeSinavId).HasColumnName("DENEME_SINAV_ID");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_CREATED_DATE");
            entity.Property(e => e.IsCreatedUserid).HasColumnName("IS_CREATED_USERID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_MODIFIED_DATE");
            entity.Property(e => e.IsModifiedUserid).HasColumnName("IS_MODIFIED_USERID");
            entity.Property(e => e.Kontenjan).HasColumnName("KONTENJAN");
            entity.Property(e => e.OturumNo).HasColumnName("OTURUM_NO");
            entity.Property(e => e.Tarih)
                .HasColumnType("datetime")
                .HasColumnName("TARIH");
            entity.Property(e => e.Uid)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("UID");

            entity.HasOne(d => d.DenemeSinav).WithMany(p => p.DenemeSinaviOturums)
                .HasForeignKey(d => d.DenemeSinavId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_deneme_sinavi_oturum_deneme_sinav");
        });

        modelBuilder.Entity<Der>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ders__3214EC27B52AE52C");

            entity.ToTable("ders");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bilgi)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("bilgi");
            entity.Property(e => e.DersAd)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Ders_Ad");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("isCreatedDate");
            entity.Property(e => e.IsCreatedUserId).HasColumnName("isCreatedUserID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("isModifiedDate");
            entity.Property(e => e.IsModifiedUserId).HasColumnName("isModifiedUserID");
            entity.Property(e => e.SubeId).HasColumnName("subeID");
            entity.Property(e => e.Uid)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UID");

            entity.HasOne(d => d.Sube).WithMany(p => p.Ders)
                .HasForeignKey(d => d.SubeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ders_sube");
        });

        modelBuilder.Entity<DersOturumSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ders_otu__3214EC27E82E9DE0");

            entity.ToTable("ders_oturum_set");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Baslangic)
                .HasColumnType("datetime")
                .HasColumnName("BASLANGIC");
            entity.Property(e => e.Bitis)
                .HasColumnType("datetime")
                .HasColumnName("BITIS");
            entity.Property(e => e.Dersid).HasColumnName("DERSID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("isCreatedDate");
            entity.Property(e => e.IsCreatedUserId).HasColumnName("isCreatedUserID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("isModifiedDate");
            entity.Property(e => e.IsModifiedUserId).HasColumnName("isModifiedUserID");
            entity.Property(e => e.Tarih)
                .HasColumnType("datetime")
                .HasColumnName("tarih");
            entity.Property(e => e.Teacherid).HasColumnName("TEACHERID");
            entity.Property(e => e.Uid)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UID");

            entity.HasOne(d => d.Ders).WithMany(p => p.DersOturumSets)
                .HasForeignKey(d => d.Dersid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ders_oturum_set_ders");

            entity.HasOne(d => d.Teacher).WithMany(p => p.DersOturumSets)
                .HasForeignKey(d => d.Teacherid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ders_oturum_set_kullanicilar");
        });

        modelBuilder.Entity<DersOturumUserSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ders_otu__3214EC27A1D3A1F8");

            entity.ToTable("ders_oturum_user_set");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bilgi).IsUnicode(false);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("isCreatedDate");
            entity.Property(e => e.IsCreatedUserId).HasColumnName("isCreatedUserID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("isModifiedDate");
            entity.Property(e => e.IsModifiedUserId).HasColumnName("isModifiedUserID");
            entity.Property(e => e.OturumId).HasColumnName("OTURUM_ID");
            entity.Property(e => e.StudentId).HasColumnName("STUDENT_ID");
            entity.Property(e => e.Uid)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UID");

            entity.HasOne(d => d.Oturum).WithMany(p => p.DersOturumUserSets)
                .HasForeignKey(d => d.OturumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ders_oturum_user_set_ders_oturum_set");

            entity.HasOne(d => d.Student).WithMany(p => p.DersOturumUserSets)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ders_oturum_user_set_kullanicilar");
        });

        modelBuilder.Entity<KullaniciMesajLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__kullanic__3214EC27443B29B5");

            entity.ToTable("kullanici_mesaj_log");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_CREATED_DATE");
            entity.Property(e => e.IsCreatedUserid).HasColumnName("IS_CREATED_USERID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_MODIFIED_DATE");
            entity.Property(e => e.IsModifiedUserid).HasColumnName("IS_MODIFIED_USERID");
            entity.Property(e => e.Mesaj)
                .IsUnicode(false)
                .HasColumnName("mesaj");
            entity.Property(e => e.MesajTarih)
                .HasColumnType("datetime")
                .HasColumnName("mesaj_tarih");
            entity.Property(e => e.Uid)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("UID");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.User).WithMany(p => p.KullaniciMesajLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_kullanici_mesaj_log_kullanicilar");
        });

        modelBuilder.Entity<Kullanicilar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__kullanic__3214EC2710BFF59C");

            entity.ToTable("kullanicilar");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Ad)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("AD");
            entity.Property(e => e.Eposta)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("EPOSTA");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_CREATED_DATE");
            entity.Property(e => e.IsCreatedUserid).HasColumnName("IS_CREATED_USERID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_MODIFIED_DATE");
            entity.Property(e => e.IsModifiedUserid).HasColumnName("IS_MODIFIED_USERID");
            entity.Property(e => e.KullaniciAdi)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("KULLANICI_ADI");
            entity.Property(e => e.Role).HasColumnName("ROLE");
            entity.Property(e => e.Sifre)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("SIFRE");
            entity.Property(e => e.Soyad)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("SOYAD");
            entity.Property(e => e.SubeId).HasColumnName("SUBE_ID");
            entity.Property(e => e.Tel)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("TEL");
            entity.Property(e => e.Uid)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("UID");

            entity.HasOne(d => d.Sube).WithMany(p => p.Kullanicilars)
                .HasForeignKey(d => d.SubeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_kullanicilar_sube");
        });

        modelBuilder.Entity<StudentOtherInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__student___3214EC27CEF8019A");

            entity.ToTable("student_other_info");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Baslik)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("baslik");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsCreatedUserId).HasColumnName("isCreatedUserID");
            entity.Property(e => e.IsCreateeDate)
                .HasColumnType("datetime")
                .HasColumnName("isCreateeDate");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("isModifiedDate");
            entity.Property(e => e.IsModifiedUserId).HasColumnName("isModifiedUserID");
            entity.Property(e => e.Link)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("link");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Uid)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("UID");
            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.User).WithMany(p => p.StudentOtherInfos)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_student_other_info_kullanicilar");
        });

        modelBuilder.Entity<Sube>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sube__3214EC279D75311D");

            entity.ToTable("sube");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Eposta)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EPOSTA");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_CREATED_DATE");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_MODIFIED_DATE");
            entity.Property(e => e.SubeNumber).HasColumnName("SUBE_NUMBER");
            entity.Property(e => e.Tel)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("TEL");
            entity.Property(e => e.Uid)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("UID");
            entity.Property(e => e.Unvan)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("UNVAN");
        });

        modelBuilder.Entity<TeacherHaftaGunSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__teacher___3214EC27336E5A27");

            entity.ToTable("teacher_hafta_gun_set");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Gun)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GUN");
            entity.Property(e => e.Teacherid).HasColumnName("TEACHERID");
            entity.Property(e => e.Uid)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("UID");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherHaftaGunSets)
                .HasForeignKey(d => d.Teacherid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_teacher_hafta_gun_set_kullanicilar");
        });

        modelBuilder.Entity<UserDersSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_der__3214EC278BBE2E21");

            entity.ToTable("user_ders_set");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Dersid).HasColumnName("DERSID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("isCreatedDate");
            entity.Property(e => e.IsCreatedUserId).HasColumnName("isCreatedUserID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("isModifiedDate");
            entity.Property(e => e.IsModifiedUserId).HasColumnName("isModifiedUserID");
            entity.Property(e => e.Uid)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UID");
            entity.Property(e => e.Userid).HasColumnName("USERID");

            entity.HasOne(d => d.Ders).WithMany(p => p.UserDersSets)
                .HasForeignKey(d => d.Dersid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_ders_set_ders");

            entity.HasOne(d => d.User).WithMany(p => p.UserDersSets)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_ders_set_kullanicilar");
        });

        modelBuilder.Entity<UserOturumSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_otu__3214EC27D9DC9FDD");

            entity.ToTable("user_oturum_set");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsActive).HasColumnName("IS_ACTIVE");
            entity.Property(e => e.IsCreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_CREATED_DATE");
            entity.Property(e => e.IsCreatedUserid).HasColumnName("IS_CREATED_USERID");
            entity.Property(e => e.IsModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("IS_MODIFIED_DATE");
            entity.Property(e => e.IsModifiedUserid).HasColumnName("IS_MODIFIED_USERID");
            entity.Property(e => e.OturumId).HasColumnName("OTURUM_ID");
            entity.Property(e => e.Status).HasColumnName("STATUS");
            entity.Property(e => e.Uid)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("UID");
            entity.Property(e => e.UserId).HasColumnName("USER_ID");

            entity.HasOne(d => d.Oturum).WithMany(p => p.UserOturumSets)
                .HasForeignKey(d => d.OturumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_oturum_set_deneme_sinavi_oturum");

            entity.HasOne(d => d.User).WithMany(p => p.UserOturumSets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_oturum_set_kullanicilar");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
