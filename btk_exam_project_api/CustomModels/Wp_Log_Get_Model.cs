using System;
using btk_exam_project_api.Models;

namespace btk_exam_project_api.CustomModels
{
	public class Wp_Log_Get_Model
	{
        public int Id { get; set; }

        public string Uid { get; set; } = null!;

        public int SubeId { get; set; }

        public string Mesaj { get; set; } = null!;

        public DateTime IsCreatedDate { get; set; }

        public DateTime IsModifiedDate { get; set; }

        public int IsCreatedUserid { get; set; }

        public int IsModifiedUserid { get; set; }

        public bool IsActive { get; set; }

        public DateTime MesajTarih { get; set; }

        public virtual Kullanicilar Student { get; set; } = null!;
        public virtual Kullanicilar isCreatedUser { get; set; }
        public virtual Kullanicilar isModifiedUser { get; set; }

    }
}

