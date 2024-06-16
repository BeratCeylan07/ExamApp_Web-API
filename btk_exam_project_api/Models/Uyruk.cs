using System;
using System.Collections.Generic;

namespace btk_exam_project_api.Models;

public partial class Uyruk
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public int SubeId { get; set; }

    public string UyrukBaslik { get; set; } = null!;

    public bool IsActive { get; set; }

    public int IsCreatedUserId { get; set; }

    public int IsModifiedUserId { get; set; }

    public DateTime IsCreatedDate { get; set; }

    public DateTime IsModifiedDate { get; set; }

    public virtual Sube Sube { get; set; } = null!;
}
