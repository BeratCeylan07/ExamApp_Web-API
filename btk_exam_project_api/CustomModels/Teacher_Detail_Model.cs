using System;
using btk_exam_project_api.Models;

namespace btk_exam_project_api.CustomModels
{
	public class Teacher_Detail_Model
	{
        public int teacherID { get; set; }
        public string teacherUID { get; set; }
        public string setedLessonUID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
        public DateTime IsCreatedDate { get; set; }
        public DateTime IsModifiedDate { get; set; }
        public string isCreatedUser { get; set; }
        public string IsModifiedUser { get; set; }
    }
}

