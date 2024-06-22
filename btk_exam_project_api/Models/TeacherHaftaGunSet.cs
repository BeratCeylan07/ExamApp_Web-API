using System;
using System.Collections.Generic;

namespace btk_exam_project_api.Models;

public partial class TeacherHaftaGunSet
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public int Teacherid { get; set; }

    public string Gun { get; set; } = null!;

    public virtual Kullanicilar Teacher { get; set; } = null!;
}
