namespace btk_exam_project_api.CustomModels
{
    public class Lesson_New_Session_Model
    {
        public string lessonUID { get; set; }
        public DateTime tarih { get; set; }
        public string baslangic { get; set; }
        public string bitis { get; set; }
        public string teacherUID { get; set; }
        public int userID { get; set; }
    }
}
