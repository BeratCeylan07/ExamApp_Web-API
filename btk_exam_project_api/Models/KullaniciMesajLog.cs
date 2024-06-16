using System;
using System.Collections.Generic;

namespace btk_exam_project_api.Models;

public partial class KullaniciMesajLog
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public int SubeId { get; set; }

    public int UserId { get; set; }

    public string Mesaj { get; set; } = null!;

    public DateTime IsCreatedDate { get; set; }

    public DateTime IsModifiedDate { get; set; }

    public int IsCreatedUserid { get; set; }

    public int IsModifiedUserid { get; set; }

    public bool IsActive { get; set; }

    public DateTime MesajTarih { get; set; }

    public virtual Sube Sube { get; set; } = null!;

    public virtual Kullanicilar User { get; set; } = null!;
}
