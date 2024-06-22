using btk_exam_project_api.Models;

namespace btk_exam_project_api.CustomModels
{
    public class Lesson_Detail_Model
    {
        public string Uid { get; set; } = null!;

        public int SubeId { get; set; }

        public string DersAd { get; set; } = null!;

        public string? Bilgi { get; set; }

        public int IsCreatedUserId { get; set; }

        public DateTime IsCreatedDate { get; set; }

        public int IsModifiedUserId { get; set; }

        public DateTime IsModifiedDate { get; set; }

        public bool IsActive { get; set; }
        public Kullanicilar isCreatedUser { get; set; }
        public Kullanicilar isModifiedUser { get; set; }
    }
}
