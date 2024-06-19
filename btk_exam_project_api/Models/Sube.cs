using System;
using System.Collections.Generic;

namespace btk_exam_project_api.Models;

public partial class Sube
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public string Unvan { get; set; } = null!;

    public string Tel { get; set; } = null!;

    public string Eposta { get; set; } = null!;

    public int SubeNumber { get; set; }

    public DateTime IsCreatedDate { get; set; }

    public DateTime IsModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<DenemeSinav> DenemeSinavs { get; set; } = new List<DenemeSinav>();

    public virtual ICollection<Der> Ders { get; set; } = new List<Der>();

    public virtual ICollection<KullaniciMesajLog> KullaniciMesajLogs { get; set; } = new List<KullaniciMesajLog>();

    public virtual ICollection<Kullanicilar> Kullanicilars { get; set; } = new List<Kullanicilar>();
}
