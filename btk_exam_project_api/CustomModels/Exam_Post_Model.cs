using System;
namespace btk_exam_project_api.CustomModels
{
	public class Exam_Post_Model
	{
        public int? examID { get; set; }
        public bool IsActive { get; set; }
        public int Subeid { get; set; }
        public int userID { get; set; }
        public string? examUID { get; set; }
        public string DenemeAdi { get; set; }

        public string SinavKategori { get; set; }

        public string YayinAdi { get; set; }

        public string YayinLogo { get; set; }

        public double Ucret { get; set; }

        public int? KitapcikToplam { get; set; }

        public string sinavYeri { get; set; }
        public double? KitapcikAdetMaliyet { get; set; }
        public bool DortBirRule { get; set; }

    }
}

