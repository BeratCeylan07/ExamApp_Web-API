using btk_exam_project_api.Models;

namespace btk_exam_project_api.CustomModels
{
    public class lesson_user_set_list_model
    {
        public Kullanicilar student { get; set; }
        public DersOturumSet oturum { get; set; }
        public DateTime kayitDate {  get; set; }
    }
}
