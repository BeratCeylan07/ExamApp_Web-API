using System;
using btk_exam_project_api.Models;

namespace btk_exam_project_api.CustomModels
{
	public class Log_List_Model
	{
        public int Id { get; set; }

        public int SubeId { get; set; }

        public string Baslik { get; set; } = null!;

        public string Aciklama { get; set; } = null!;

        public DateTime IsCreatedDate { get; set; }

        public int UserId { get; set; }

        public string Udi { get; set; } = null!;

        public string ActionUid { get; set; } = null!;

        public Kullanicilar user { get; set; }
    }
}

