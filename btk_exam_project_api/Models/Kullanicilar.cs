using System;
using System.Collections.Generic;

namespace btk_exam_project_api.Models;

public partial class Kullanicilar
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public int SubeId { get; set; }

    public string Ad { get; set; } = null!;

    public string Soyad { get; set; } = null!;

    public string KullaniciAdi { get; set; } = null!;

    public string Sifre { get; set; } = null!;

    public string? Tel { get; set; }

    public string? Eposta { get; set; }

    public int Role { get; set; }

    public DateTime IsCreatedDate { get; set; }

    public DateTime IsModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public int IsCreatedUserid { get; set; }

    public int IsModifiedUserid { get; set; }

    public virtual ICollection<DersOturumSet> DersOturumSets { get; set; } = new List<DersOturumSet>();

    public virtual ICollection<DersOturumUserSet> DersOturumUserSets { get; set; } = new List<DersOturumUserSet>();

    public virtual ICollection<KullaniciMesajLog> KullaniciMesajLogs { get; set; } = new List<KullaniciMesajLog>();

    public virtual ICollection<StudentOtherInfo> StudentOtherInfos { get; set; } = new List<StudentOtherInfo>();

    public virtual Sube Sube { get; set; } = null!;

    public virtual ICollection<TeacherHaftaGunSet> TeacherHaftaGunSets { get; set; } = new List<TeacherHaftaGunSet>();

    public virtual ICollection<UserDersSet> UserDersSets { get; set; } = new List<UserDersSet>();

    public virtual ICollection<UserOturumSet> UserOturumSets { get; set; } = new List<UserOturumSet>();
}
