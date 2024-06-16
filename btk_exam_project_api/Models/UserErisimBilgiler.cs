using System;
using System.Collections.Generic;

namespace btk_exam_project_api.Models;

public partial class UserErisimBilgiler
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public int UserId { get; set; }

    public string Baslik { get; set; } = null!;

    public string Link { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public int IsCreatedUserId { get; set; }

    public int IsModifiedUserId { get; set; }

    public DateTime IsCreateeDate { get; set; }

    public DateTime IsModifiedDate { get; set; }

    public virtual Kullanicilar User { get; set; } = null!;
}
