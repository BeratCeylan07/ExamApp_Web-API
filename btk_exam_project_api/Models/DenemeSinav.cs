using System;
using System.Collections.Generic;

namespace btk_exam_project_api.Models;

public partial class DenemeSinav
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public int Subeid { get; set; }

    public bool DortBirRule { get; set; }

    public string DenemeAdi { get; set; } = null!;

    public string SinavKategori { get; set; } = null!;

    public string YayinAdi { get; set; } = null!;

    public string YayinLogo { get; set; } = null!;

    public double Ucret { get; set; }

    public string SinavYeri { get; set; } = null!;

    public int? KitapcikToplam { get; set; }

    public double? KitapcikAdetMaliyet { get; set; }

    public DateTime IsCreatedDate { get; set; }

    public DateTime IsModifiedDate { get; set; }

    public int IsCreatedUserid { get; set; }

    public int IsModifiedUserid { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<DenemeSinaviOturum> DenemeSinaviOturums { get; set; } = new List<DenemeSinaviOturum>();

    public virtual Sube Sube { get; set; } = null!;
}
