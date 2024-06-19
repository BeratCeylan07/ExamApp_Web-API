using System;
using System.Collections.Generic;

namespace btk_exam_project_api.Models;

public partial class Der
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public int SubeId { get; set; }

    public string DersAd { get; set; } = null!;

    public string? Bilgi { get; set; }

    public int IsCreatedUserId { get; set; }

    public DateTime IsCreatedDate { get; set; }

    public int IsModifiedUserId { get; set; }

    public DateTime IsModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<DersOturumSet> DersOturumSets { get; set; } = new List<DersOturumSet>();

    public virtual Sube Sube { get; set; } = null!;

    public virtual ICollection<UserDersSet> UserDersSets { get; set; } = new List<UserDersSet>();
}
