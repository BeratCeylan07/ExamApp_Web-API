using System;
using btk_exam_project_api.Models;

namespace btk_exam_project_api.CustomModels
{
	public class Exam_Record_Data_Model
	{
        public int UserID { get; set; }
        public string UID { get; set; }
        public bool IsActive { get; set; }
        public int SubeID { get; set; }
        public string DenemeAdi { get; set; }
        public string SinavKategori { get; set; }
        public string YayinAdi { get; set; }
        public string YayinLogo { get; set; }
        public string SinavYeri { get; set; }
        public double? Ucret { get; set; }
        public int? KitapcikToplam { get; set; }
        public bool DortBirRule { get; set; }
        public double? KitapcikAdetMaliyet { get; set; }
        public Kullanicilar IsCreatedUser { get; set; }
        public DateTime IsCreatedDate { get; set; }
        public Kullanicilar IsModifiedUser { get; set; }
        public DateTime IsModifiedDate { get; set; }
    }
}

