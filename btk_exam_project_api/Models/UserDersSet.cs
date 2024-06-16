using System;
using System.Collections.Generic;

namespace btk_exam_project_api.Models;

public partial class UserDersSet
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public int Dersid { get; set; }

    public int Userid { get; set; }

    public int IsCreatedUserId { get; set; }

    public DateTime IsCreatedDate { get; set; }

    public int IsModifiedUserId { get; set; }

    public DateTime IsModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public virtual Der Ders { get; set; } = null!;

    public virtual ICollection<DersOturumUserSet> DersOturumUserSets { get; set; } = new List<DersOturumUserSet>();

    public virtual Kullanicilar User { get; set; } = null!;
}
