using System;
using System.Collections.Generic;

namespace btk_exam_project_api.Models;

public partial class ActionLog
{
    public int Id { get; set; }

    public int SubeId { get; set; }

    public string Baslik { get; set; } = null!;

    public string Aciklama { get; set; } = null!;

    public DateTime IsCreatedDate { get; set; }

    public int UserId { get; set; }

    public string Udi { get; set; } = null!;

    public string ActionUid { get; set; } = null!;
}
