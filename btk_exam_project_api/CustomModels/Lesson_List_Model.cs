using btk_exam_project_api.Models;

namespace btk_exam_project_api.CustomModels
{
    public class Lesson_List_Model
    {
        public string Uid { get; set; } = null!;
        public string DersAd { get; set; } = null!;
        public string? Bilgi { get; set; }
        public virtual ICollection<DersOturumSet> DersOturumSets { get; set; } = new List<DersOturumSet>();
        public int toplamKayitliOgrenci { get; set; }
        public bool IsActive { get; set; }

    }
}
