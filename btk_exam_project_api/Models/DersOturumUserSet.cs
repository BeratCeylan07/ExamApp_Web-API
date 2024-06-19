using System;
using System.Collections.Generic;

namespace btk_exam_project_api.Models;

public partial class DersOturumUserSet
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public int OturumId { get; set; }

    public int UserDersSetId { get; set; }

    public bool Status { get; set; }

    public string Bilgi { get; set; } = null!;

    public int IsCreatedUserId { get; set; }

    public DateTime IsCreatedDate { get; set; }

    public int IsModifiedUserId { get; set; }

    public DateTime IsModifiedDate { get; set; }

    public int IsActive { get; set; }

    public virtual DersOturumSet Oturum { get; set; } = null!;

    public virtual UserDersSet UserDersSet { get; set; } = null!;
}
