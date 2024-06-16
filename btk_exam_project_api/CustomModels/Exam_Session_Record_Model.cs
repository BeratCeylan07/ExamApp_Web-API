using System;
using btk_exam_project_api.Models;

namespace btk_exam_project_api.CustomModels
{
	public class Exam_Session_Record_Model
	{
        public int UserID { get; set; }
        public int oturumNo { get; set; }
        public string ExamSessionUID { get; set; }
        public DateTime SessionDate { get; set; }
        public int Kontenjan { get; set; }
        public int ToplamKesinKayit { get; set; }
        public int ToplamOnKayit { get; set; }
        public int ToplamKitapcikAlan { get; set; }
        public int ToplamKatilimSaglayan { get; set; }
        public int ToplamDevamsiz { get; set; }
        public bool IsActive { get; set; }
        public Kullanicilar IsCreatedUser { get; set; }
        public Kullanicilar IsModifiedUser { get; set; }
        public DateTime IsCreatedDate { get; set; }
        public DateTime IsModifiedDate { get; set; }
        public string SessionBilgi { get; set; }
    }
}

