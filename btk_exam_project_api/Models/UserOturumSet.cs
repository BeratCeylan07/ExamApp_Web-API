using System;
using System.Collections.Generic;

namespace btk_exam_project_api.Models;

public partial class UserOturumSet
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public double? Dogru { get; set; }

    public double? Yanlis { get; set; }

    public double? Net { get; set; }

    public int OturumId { get; set; }

    public int UserId { get; set; }

    public int Status { get; set; }

    public DateTime IsCreatedDate { get; set; }

    public DateTime IsModifiedDate { get; set; }

    public int IsCreatedUserid { get; set; }

    public int IsModifiedUserid { get; set; }

    public bool IsActive { get; set; }

    public virtual DenemeSinaviOturum Oturum { get; set; } = null!;

    public virtual Kullanicilar User { get; set; } = null!;
}
