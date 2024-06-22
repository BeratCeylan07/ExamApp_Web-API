using System;
using System.Collections.Generic;

namespace btk_exam_project_api.Models;

public partial class DenemeSinaviOturum
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public int DenemeSinavId { get; set; }

    public int OturumNo { get; set; }

    public DateTime Tarih { get; set; }

    public string? Bilgi { get; set; }

    public int Kontenjan { get; set; }

    public DateTime IsCreatedDate { get; set; }

    public DateTime IsModifiedDate { get; set; }

    public int IsCreatedUserid { get; set; }

    public int IsModifiedUserid { get; set; }

    public bool IsActive { get; set; }

    public virtual DenemeSinav DenemeSinav { get; set; } = null!;

    public virtual ICollection<UserOturumSet> UserOturumSets { get; set; } = new List<UserOturumSet>();
}
